using System;
using System.Collections.Generic;


namespace Axtension
{
    public class RESTful2
    {
        private const int HTTPREQUEST_PROXYSETTING_DEFAULT = 0;
        private const int HTTPREQUEST_PROXYSETTING_PRECONFIG = 0;
        private const int HTTPREQUEST_PROXYSETTING_DIRECT = 1;
        private const int HTTPREQUEST_PROXYSETTING_PROXY = 2;
        private string _url = "";
        private string _verb = "";
        private string _tail = "";
        private string _body = "";
        private string _contentType = "";
        private string _result = "";
        private string _path = "";
        private int[] _timeouts = { 10000, 10000, 10000, 10000 };
        private string _proxy = "";
        private bool _async;
        private Dictionary<string, string> _reqheads = new Dictionary<string, string>();
        private Dictionary<string, object> _status = new Dictionary<string, object>();
        private WinHttp.WinHttpRequest _req = new WinHttp.WinHttpRequest();

        public string URL = string.Empty;

        //private HttpWebRequest _req = null;
        private Dictionary<string, object> _params = new Dictionary<string, object>();

        public RESTful2()
        {
        }
        public RESTful2 Url(string url = "")
        {
            this._url = url;
            return this;
        }
        public RESTful2 Verb(string verb = "GET")
        {
            this._verb = verb;
            return this;
        }
        public RESTful2 Accept(string accept = "*/*")
        {
            this._reqheads["Accept"] = accept;
            //this._accept = accept;
            return this;
        }
        public RESTful2 RequestHeader(string key, string value)
        {
            this._reqheads[key] = value;
            return this;
        }
        public RESTful2 Path(string path = "")
        {
            this._path = path;
            return this;
        }
        public RESTful2 Tail(string tail = "")
        {
            this._tail = tail;
            return this;
        }
        public RESTful2 Body(string body = "")
        {
            this._body = body;
            return this;
        }
        public RESTful2 ContentType(string contentType = "")
        {
            this._contentType = contentType;
            return this;
        }
        public RESTful2 Timeouts(int a = 10000, int b = 10000, int c = 10000, int d = 10000)
        {
            this._timeouts[0] = a;
            this._timeouts[1] = b;
            this._timeouts[2] = c;
            this._timeouts[3] = d;
            return this;
        }
        public RESTful2 Proxy(string proxy)
        {
            this._proxy = proxy;
            return this;
        }
        public RESTful2 UserAgent(string useragent)
        {
            this._reqheads["User-Agent"] = useragent;
            //this._useragent = useragent;
            return this;
        }
        public RESTful2 Async(bool flag = false)
        {
            this._async = flag;
            return this;
        }
        public RESTful2 Send()
        {
            string url = this._url + (this._path != string.Empty ? this._path : string.Empty) + (this._tail != string.Empty ? "?" + this._tail : string.Empty);
            this._url = string.Empty;
            this._path = string.Empty;
            this._tail = string.Empty;
            this.URL = url;

            this._req.Open(this._verb, url, this._async);

            this._async = false;

            this._req.SetTimeouts(this._timeouts[0], this._timeouts[1], this._timeouts[2], this._timeouts[3]);

            //this._req.SetRequestHeader("User-Agent", this._useragent);

            this.Accept();
            if (!this._reqheads.ContainsKey("User-Agent"))
                this.UserAgent("Mozilla/4.0 (compatible; Mozilla/5.0 (compatible; MSIE 8.0; Windows NT 6.0; Trident/4.0; Acoo Browser 1.98.744; .NET CLR 3.5.30729); Windows NT 5.1; Trident/4.0)");
            foreach (KeyValuePair<string, string> kvp in _reqheads)
            {
                this._req.SetRequestHeader(kvp.Key, kvp.Value);
            }

            if ("" != this._proxy)
            {
                this._req.SetProxy(HTTPREQUEST_PROXYSETTING_PROXY, this._proxy, string.Empty);
            }
            else
            {
                this._req.SetProxy(HTTPREQUEST_PROXYSETTING_DIRECT);
            }

            this._proxy = string.Empty;

            try
            {
                if (this._verb == "POST" || this._verb == "PUT")
                {
                    this._req.Send(this._body);
                }
                else
                {
                    this._req.Send();
                }

                this._status["OK"] = true;
                this._status["DATA"] = this._req.ResponseText;
                this._status["HRESULT"] = 0;
                this._status["STATUS"] = this._req.Status;
                this._status["STATUSTEXT"] = this._req.StatusText;
                this._result = this._req.ResponseText;
            }
            catch (Exception hexc)
            {
                this._status["OK"] = false;
                this._status["DATA"] = hexc.Message;
                this._status["HRESULT"] = hexc.HResult;

            }
            this._verb = string.Empty;
            this._body = string.Empty;
            return this;
        }
        public string Response()
        {
            return this._req.ResponseText;
        }
        public dynamic ResponseStream()
        {
            return this._req.ResponseStream;
        }

        public dynamic ResponseBody()
        {
            return this._req.ResponseBody;
        }
        public bool IsOK()
        {
            return (bool)this._status["OK"];
        }
        public string Data()
        {
            return this._status["DATA"].ToString();
        }
        public int HResult()
        {
            return (int)this._status["HRESULT"];
        }
        public int Status()
        {
            this._status["STATUS"] = this._req.Status;
            return (int)this._status["STATUS"];
        }
        public string StatusText()
        {
            this._status["STATUSTEXT"] = this._req.StatusText;
            return this._status["STATUSTEXT"].ToString();
        }
        override public string ToString()
        {
            return this._result;
        }
    }

}

