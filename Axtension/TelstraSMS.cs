using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using RestSharp;


namespace Axtension
{
    public class TelstraSMS
    {
        private string consumerKey = string.Empty;
        private string consumerSecret = string.Empty;
        private string url = string.Empty;
        private string authquery = string.Empty;
        private string sendquery = string.Empty;

        private string access_token = string.Empty;
        private string expires_in = string.Empty;

        private string cfgFile = string.Empty;
        private Config config = null;

        private bool authenticated = false;

        private string messageId = string.Empty;
        
        public TelstraSMS()
        {
            config = new Config();
        }

        public TelstraSMS(string _cfgfile)
        {
            cfgFile = _cfgfile;
            config = new Config(cfgFile);
        }

        public TelstraSMS Authenticate()
        {
            consumerKey = config.Retrieve("consumer.key");
            consumerSecret = config.Retrieve("consumer.secret");
            url = config.Retrieve("authurl", "https://api.telstra.com/v1/oauth/token");

            var client = new RestClient(url);
            var request = new RestRequest("", Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("client_id", consumerKey);
            request.AddParameter("client_secret", consumerSecret);
            request.AddParameter("scope", "SMS");
            request.AddParameter("grant_type", "client_credentials");

            IRestResponse response = client.Execute(request);
            var content = response.Content;
            FluentJSON FJ = new FluentJSON().Load(content).Deserialize();
            object fj = FJ.ToObject();
            access_token = ((Dictionary<string,object>) fj)["access_token"].ToString();
            expires_in = ((Dictionary<string, object>)fj)["expires_in"].ToString();
            return this;
        }

        public TelstraSMS SendSMS(string _number, string _message)
        {
            url = config.Retrieve("sendurl", "https://api.telstra.com/v1/sms/messages");

            var client = new RestClient(url);
            var request = new RestRequest(string.Empty, Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + access_token);
            request.AddHeader("Accept", "application/json");
            Dictionary<string, string> jsonBody = new Dictionary<string, string>() { ["to"] = _number, ["body"] = _message };
            request.AddJsonBody(jsonBody);
            IRestResponse response = client.Execute(request);
            var content = response.Content;
            FluentJSON FJ = new FluentJSON().Load(content).Deserialize();
            object fj = FJ.ToObject();
            messageId = ((Dictionary<string, object>)fj)["messageId"].ToString();
            return this;
        }

        public string GetMessageId()
        {
            return messageId;
        }
    }
}
