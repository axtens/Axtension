namespace Axtension
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Net;
    using System.Text;

    public static class GoogleAnalytics
    {
        public static string OAuthConnect(string client_id, string scope, string login_hint)
        {
            var url = string.Format(
                "https://accounts.google.com/o/oauth2/auth?{0}&{1}&{2}&{3}&{4}&{5}",
                "response_type=code",
                "client_id=" + client_id,
                "redirect_uri=" + "urn:ietf:wg:oauth:2.0:oob",
                "scope=" + WebUtility.HtmlEncode(scope),
                "state=acit",
                "login_hint=" + login_hint);

            var internetExplorer = new IE();
            internetExplorer.Launch(true);
            internetExplorer.Navigate(url);
            string title = internetExplorer.GetTitle().Replace("Success state=acit&code=", string.Empty);
            internetExplorer.Quit();
            return title;
        }

/*
 * function OAuthConnect(client_id, scope, login_hint) {
  var url = XMAP("https://accounts.google.com/o/oauth2/auth?[1]&[2]&[3]&[4]&[5]&[6]",
      "response_type=code",
      "client_id=" + client_id,
      "redirect_uri=" + "urn:ietf:wg:oauth:2.0:oob",
      "scope=" + escape(scope),
      "state=acit",
      "login_hint=" + login_hint);

  var oIE = new ActiveXObject("InternetExplorer.Application");
  oIE.Visible = true;
  oIE.Navigate(url);
  while (oIE.busy) {
    WScript.Sleep(10);
  };
  var status = oIE.Document.title;
  while (status.indexOf("state=acit") === -1) {
    WScript.Sleep(100);
    status = oIE.Document.title;
  }
  // WScript.Echo(status);
  oIE.Quit();
  var code = RightOf(status, "&code=");
  return code;
}
        */
public static string GetTokens(string client_id, string client_secret, string code) 
{
    Dictionary<string, object> dataDict = new Dictionary<string, object>();
    dataDict.Add("code", code);
    dataDict.Add("client_id", client_id);
    dataDict.Add("client_secret", client_secret);
    dataDict.Add("redirect_uri", "urn:ietf:wg:oauth:2.0:oob");
    dataDict.Add("grant_type", "authorization_code");
    string res = REST.HttpPost("https://accounts.google.com/o/oauth2/token", dataDict);
    return res;
}

public static Dictionary<string, object> RefreshToken(string client_id, string client_secret, string refresh_token)
{
    Dictionary<string, object> dataDict = new Dictionary<string, object>();
    dataDict.Add("client_id", client_id);
    dataDict.Add("client_secret", client_secret);
    dataDict.Add("refresh_token", refresh_token);
    dataDict.Add("grant_type", "refresh_token");
    string res = REST.HttpPost("https://accounts.google.com/o/oauth2/token", dataDict);
    return (Dictionary<string, object>)CountryCodeTopLevelDomain.DecodeJson(res);
}
        
public static string GetData(string url, string access_token)
{
    return REST.HttpGet(url + "&access_token=" + access_token);
}

public static string[] GetData2CSV(string url, string access_token)
{
    string[] result = new string[2];
    StringBuilder csv = new StringBuilder();
    Dictionary<string, object> dictJSON = new Dictionary<string, object>();
    string json = REST.HttpGet(url + "&access_token=" + access_token);
    do 
    {
        dictJSON = (Dictionary<string, object>)CountryCodeTopLevelDomain.DecodeJson(json);
        if (dictJSON.ContainsKey("error"))
        {
            result[0] = "FAIL";
            result[1] = ((Dictionary<string, object>)dictJSON["error"])["message"].ToString();
            return result;
        }

        ArrayList headers = (ArrayList)dictJSON["columnHeaders"];
        for (var i = 0; i < headers.Count; i++)
        {
            var x = (Dictionary<string, object>)headers[i];
            csv.Append(x["name"].ToString());
            if (i < headers.Count - 1)
            {
                csv.Append(",");
            }
        }

        csv.AppendLine();

        ArrayList rowData = (ArrayList)dictJSON["rows"];
        int rowLines = rowData.Count;
        for (var i = 0; i < rowLines; i++)
        {
            string line = string.Empty;
            ArrayList row = (ArrayList)rowData[i];
            for (var j = 0; j < headers.Count; j++)
            {
                var s = string.Empty;
                var x = (Dictionary<string, object>)headers[j];
                var dt = x["dataType"].ToString();
                if ("STRING" == dt)
                {
                    s = "\"" + row[j].ToString() + "\"";
                }
                else
                {
                    s = row[j].ToString();
                }

                line += s;
                if (j < headers.Count - 1)
                {
                    line += ",";
                }
            }

            csv.AppendLine(line);   
        }

        if (dictJSON.ContainsKey("nextLink"))
        {
            json = REST.HttpGet(dictJSON["nextLink"].ToString() + "&access_token=" + access_token);
        }
        else
        {
            break;
        }
    } 
    while (true); 
    result[0] = "OK";
    result[1] = csv.ToString();
    return result;
}

public static string[] GetData2TSV(string url, string access_token)
{
    string[] result = new string[2];
    StringBuilder tsv = new StringBuilder();
    Dictionary<string, object> dictJSON = new Dictionary<string, object>();
    
    string json = REST.HttpGet(url + "&access_token=" + access_token);

    do
    {
        dictJSON = (Dictionary<string, object>)CountryCodeTopLevelDomain.DecodeJson(json);
        if (dictJSON.ContainsKey("error"))
        {
            result[0] = "FAIL";
            result[1] = ((Dictionary<string, object>)dictJSON["error"])["message"].ToString();
            return result;
        }

        ArrayList headers = (ArrayList)dictJSON["columnHeaders"];
        for (var i = 0; i < headers.Count; i++)
        {
            var x = (Dictionary<string, object>)headers[i];
            tsv.Append(x["name"].ToString());
            if (i < headers.Count - 1)
            {
                tsv.Append("\t");
            }
        }

        tsv.AppendLine();

        ArrayList rowData = (ArrayList)dictJSON["rows"];
        int rowLines = rowData.Count;
        for (var i = 0; i < rowLines; i++)
        {
            string line = string.Empty;
            ArrayList row = (ArrayList)rowData[i];
            for (var j = 0; j < headers.Count; j++)
            {
                var x = (Dictionary<string, object>)headers[j];
                var s = row[j].ToString();
                line += s;
                if (j < headers.Count - 1)
                {
                    line += "\t";
                }
            }

            tsv.AppendLine(line);
        }

        if (dictJSON.ContainsKey("nextLink"))
        {
            json = REST.HttpGet(dictJSON["nextLink"].ToString() + "&access_token=" + access_token);
        }
        else
        {
            break;
        }
    } 
    while (true); 
    result[0] = "OK";
    result[1] = tsv.ToString();
    return result;
}

/*
function GetData2(url, access_token) {
var res = HTTPGET(url + "&access_token=" + access_token);
var data =[];
var result;
eval("result = " + res);
if (result.error) {
return [false, result.error.message];
} else {
var headers = result.columnHeaders;
do {
data = data.concat(result.rows);
} while (result.totalResults > result.itemsPerPage);
return [true, headers, data];
}
} */
    public static string AdHocReport2(string authorization, string developerToken, string clientCustomerId, string returnMoneyInMicrosTF, string body)
    {
        return string.Empty;
    }

        /*
        function AdHocReport2(authorization, developerToken, clientCustomerId, returnMoneyInMicrosTF, body) {
          var headerDict = new ActiveXObject("Scripting.Dictionary");
          headerDict.Add("Authorization", "Bearer " + authorization);
          headerDict.Add("developerToken", developerToken);
          headerDict.Add("clientCustomerId", clientCustomerId);
          headerDict.Add("returnMoneyInMicros", returnMoneyInMicrosTF);
          headerDict.Add("Content-Length", body.length);
          var res = HTTPPOST2("https://adwords.google.com/api/adwords/reportdownload/v201309", headerDict, body);
          return res;
        }
        */
    }
}
