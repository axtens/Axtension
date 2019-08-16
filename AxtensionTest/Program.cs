using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Axtension;
using System.Diagnostics;
using System.Diagnostics.Tracing;

//using System.Web.Extensions;
//using System.Web.Script.Serialization;

namespace AxtensionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //FluentJSON fj = new FluentJSON();
            //fj.Clear().Load("{\"dog\":\"woof\"}").Deserialize();
            //object x = fj.ToObject();
            //fj.Clear().Load("[{\"dog\":12},{\"cat\":13}]").Deserialize();
            //x = fj.ToObject();

            FluentJScript fj = new FluentJScript();
            object temp = fj.AddScript("var json = {\"dog\":12};").AddScript("[json.dog,json.dog*2];").Execute().ToObject();
            fj.Shutdown();
            Debug.Print(temp.ToString());
            //CommandLineArguments cla = new Axtension.CommandLineArguments();
            //GoogleAnalytics ga = new GoogleAnalytics();
            //string res = ga.GetData("https://www.googleapis.com/analytics/v3/data/ga?ids=ga:65586618&dimensions=ga:date&metrics=ga:visits&sort=ga:date&start-date=yesterday&end-date=yesterday&max-results=1", "ya29.1.AADtN_UE7OPw0b7Eg-QXMQ6UeqUIrJM7hgXVYV85onZw-5HgdCaHQn0MjQEWKl6rMYSAtw");
            //string res = "{\"dog\":1,\"cat\":2}";
            //Dictionary<string, object> d = ccTLD.decodeJSON(res);
            //Dictionary<string, string> argz = cla.Arguments();
            //Config cfg = new Axtension.Config();
            /*var foo = GoogleJsonWebToken.GetAccessToken(cfg.Retrieve("id"), 
                cfg.Retrieve("keyfilepath"),
                cfg.Retrieve("scope"));
            Console.WriteLine(foo["access_token"]);
            REST r = new REST();
            int reports = cfg.Retrieve("reports",0);
            var url = "";
            var response = "";
            for (var i = 1; i <= reports; i++)
            {
                url = cfg.Retrieve("report." + i.ToString());
                url = url + "&access_token=" + foo["access_token"];
                response = r.HttpGet(url);
                Console.WriteLine(url);
                Console.WriteLine(response);
            }
            */
            //ccTLD cc = new Axtension.ccTLD();
            //Dictionary<string,string> res = cc.parseHost("www.google.it");
            //SQL sql = new Axtension.SQL();
            //sql.Connect("SERVER=172.16.44.5;DATABASE=BOBATSQL;UID=BobatSqlUser;PWD=i7vG98i76gy%*76gv%^Ufy6f76D85i7t6v8;");
            //string ddd = sql.Eval("SELECT COUNT(qSnapID) FROM tblqSnap WHERE (ArrivalGMT >= N'2014-01-01') AND (ArrivalGMT <= N'2014-01-20') AND (CId = 'waf') and InsertedIntotblBobat <> 1", 0);
            //FTP ftp = new FTP();
            //ftp.setupFTP("ftp.nehos.net", "N100914", "qhqnk7y8r");
            //string[] res2 = ftp.listFiles("/invoices/", "*");
            //string[] dir = Regex.Split(res2[1],"\r\n",RegexOptions.Multiline);
            //string f = ftp.nthFile("/invoices/", "*", -1);
            //f = ftp.nthFile("/invoices/", "*", 1);
            //IE oIE = new IE();
            //oIE.Launch(true);
            //oIE.Navigate("http://localhost:8077/index.lh1");
            //Dictionary<string, object> d = Axtension.CountryCodeTopLevelDomain.DecodeJson(File.ReadAllText(@"C:\Users\BruceAxtens\Documents\Projects\Work\Experimental\BOPAA\stuff.json"));
        }
    }
}