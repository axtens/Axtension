namespace Axtension
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;

    public class RESTful
    {
        private string _url = "";
        private string _verb = "";
        private string _tail = "";
        private string _body = "";
        private string _contentType = "";
        private string _result = "";
        private string _path = "";
        private string _rawBody = "";
        private bool _hasRawBody = false;

        private HttpWebRequest _req = null;
        private Dictionary<string, object> _params = new Dictionary<string, object>();

        public RESTful()
        {
            
        }
        public RESTful(string url = "")
        {
            this._url = url;
        }
        public RESTful Url(string url = "")
        {
            this._url = url;
            return this;
        }
        public RESTful Verb(string verb = "GET")
        {
            this._verb = verb;
            return this;
        }
        public RESTful Path(string path = "")
        {
            this._path = path;
            return this;
        }
        public RESTful Tail(string tail = "")
        {
            this._tail = tail;
            return this;
        }
        public RESTful Body(string body = "")
        {
            this._body = body;
            return this;
        }
        public RESTful ContentType(string contentType = "")
        {
            this._contentType = contentType;
            return this;
        }
        public RESTful RawBody(string body = "")
        {
            this._rawBody = body;
            this._hasRawBody = true;
            return this;
        }
        public RESTful Send()
        {
            string url = this._url + 
                (this._path != "" ? this._path : "") + 
                (this._tail != "" ? "?" + this._tail : "");
            this._req = WebRequest.Create(new Uri(url)) as HttpWebRequest;
            this._req.Method = this._verb;
            if (this._contentType != string.Empty)
            {
                this._req.ContentType = this._contentType;
            }
            // Build a string with all the params, properly encoded.
            // We assume that the arrays paramName and paramVal are
            // of equal length:
            List<string> paramList = new List<string>();
            foreach (KeyValuePair<string, object> entry in this._params)
            {
                paramList.Add(entry.Key + "=" + HttpUtility.UrlEncode(entry.Value.ToString()));
            }

            string paramString = string.Join("&", paramList.ToArray<string>());

            // Encode the parameters as form data:
            byte[] formData = null;
            if (this._hasRawBody == false)
            {
                formData =
                    UTF8Encoding.UTF8.GetBytes(paramString);
            }
            else
            {
                formData =
                    UTF8Encoding.UTF8.GetBytes(this._rawBody);
            }
            this._req.ContentLength = formData.Length;

            // Send the request:
            if (this._verb == "POST" || this._verb == "PUT")
            {
                using (Stream post = this._req.GetRequestStream())
                {
                    post.Write(formData, 0, formData.Length);
                }
            }

            // Pick up the response:
            using (HttpWebResponse resp = this._req.GetResponse()
                                          as HttpWebResponse)
            {
                StreamReader reader =
                    new StreamReader(resp.GetResponseStream());
                this._result = reader.ReadToEnd();
            }
            return this;
        }
        override public string ToString()
        {
            return this._result;
        }
    }

}
