namespace Axtension
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;

    public static class REST
    {
        /// http://rest.elkstein.org/2008/02/using-rest-in-c-sharp.html

        public static string HttpGet(string url)
        {
            HttpWebRequest req = WebRequest.Create(url)
                                 as HttpWebRequest;
            string result = null;
            try
            {
                using (HttpWebResponse resp = req.GetResponse()
                                              as HttpWebResponse)
                {
                    StreamReader reader =
                        new StreamReader(resp.GetResponseStream());
                    result = reader.ReadToEnd();
                }
            }
            catch (WebException hex)
            {
                result = hex.Message;
            }

            return result;
        }

        public static string HttpPost(string url, Dictionary<string, object> paramDict)
        {
            HttpWebRequest req = WebRequest.Create(new Uri(url))
                                 as HttpWebRequest;
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            
            // Build a string with all the params, properly encoded.
            // We assume that the arrays paramName and paramVal are
            // of equal length:
            List<string> paramList = new List<string>();
            foreach (KeyValuePair<string, object> entry in paramDict)
            {
                paramList.Add(entry.Key + "=" + HttpUtility.UrlEncode(entry.Value.ToString()));
            }

            string paramString = string.Join("&", paramList.ToArray<string>());
            
            // Encode the parameters as form data:
            byte[] formData =
                UTF8Encoding.UTF8.GetBytes(paramString);
            req.ContentLength = formData.Length;

            // Send the request:
            using (Stream post = req.GetRequestStream())
            {
                post.Write(formData, 0, formData.Length);
            }

            // Pick up the response:
            string result = null;
            using (HttpWebResponse resp = req.GetResponse()
                                          as HttpWebResponse)
            {
                StreamReader reader =
                    new StreamReader(resp.GetResponseStream());
                result = reader.ReadToEnd();
            }

            return result;
        }
    }
}
