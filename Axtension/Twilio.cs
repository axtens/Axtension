using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using RestSharp;

namespace Axtension
{
    public class Twilio
    {
        private static string accountSID;
        private static string authToken;
        private static string fromNumber;

        public Twilio(string _acct, string _auth, string _from)
        {
            accountSID = _acct;
            authToken = _auth;
            fromNumber = _from;
            return;
        }

        public string Send(string _to, string _msg)
        {
            if (accountSID == string.Empty)
            {
                return "accountSID empty";
            }
            if (authToken == string.Empty)
            {
                return "authToken empty";
            }
            if (fromNumber == string.Empty)
            {
                return "fromNumber empty";
            }

            // Create an instance of the Twilio client.
            TwilioRestClient client = new TwilioRestClient(accountSID, authToken);

            // Send an SMS message.
            Message result = client.SendMessage(fromNumber, _to, _msg);

            if (result.RestException != null)
            {
                //an exception occurred making the REST call
                return result.RestException.Message;
            }
            else
            {
                return "";
            }
        }
    }
}
