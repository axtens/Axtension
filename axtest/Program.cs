using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Axtension;
using System.Security.Cryptography;
namespace axtest
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> arg = CommandLineArguments.Arguments();
            foreach (KeyValuePair<string,string> kvp in arg)
            {
                Console.WriteLine("{0} = {1}", kvp.Key, kvp.Value);
            }
            /*
            BOLT b = new BOLT();
            b.UnixTicks(new DateTime());
            */

            //SQL s = new SQL();
            //object ans = s.Eval("SELECT 1");

            //FluentJSON fj = new FluentJSON("{\"port\":8090}");
            //fj.Deserialize();
            //Dictionary<string, object> dss = (Dictionary<string, object>)fj.ToObject();
            //Console.WriteLine(dss["port"]);


            //TelstraSMS sms = new TelstraSMS(@"C:\temp\telstraSMS.cfg")
            //    .Authenticate()
            //    .SendSMS("0417990380", "Testing TelstraSMS.");

            //Mail m = new Mail();
            //m.Send("bruce@strapper.net", "bruce@strapper.net", "test", "test");

            //ApplicationLogging T = new ApplicationLogging("AxTest")
            //    .Module("mania")
            //    .SetReportPathTo(@"C:\temp\axtest")
            //    .Gathering(true)
            //    .Combined(true,@"C:\Temp\axtest\combined")
            //    .AlsoToConsole()
            //    .WithAppFirst().ElapsedTime(true);
            //T.Module("test");
            //T.Module("InformTest").Inform("Hello").Module();
            //T.Module("WarnTest").Warn("Hello").Module();
            //T.Module("FailTest").Fail("Hello").Module();
            //T.Module("InformationalTest").Informational().Send("my", "dog", "has", 2, "fleas").Module();
            //T.Inform("Back to original module");
            //T.Module(); T.Module();

            string accessToken = "EAADO1Xh1qTABAHpEnC4tEKhBFzaQH9JUUZCBFOLaF6lLBWD2khtY9wZBTOKSKzOZCfoCMaDHdFwBS15hNLK9GV0cf8QcGw6ZBOTxIq4DOSMkX0QEBSmfKsSF0MwJ0ZBiuzDORfd50j1addfdY7i125yeVdvKepqsZD";
            string accountid = "140560187";

            FluentJSON fj = new FluentJSON(System.IO.File.ReadAllText("c:\\scratch\\audience.json"));
            fj.Deserialize();
            Dictionary<string, object> audienceData = (Dictionary<string, object>)fj.ToObject();
            //Console.WriteLine(dss["port"]);

            Tuple<bool, string> result1 = Part1(accountid, accessToken, audienceData["audienceName"].ToString(), audienceData["audienceDescription"].ToString());
            if (result1.Item1)
            {
                Tuple<bool, string> result2 = Part2(result1.Item2, accessToken, (string[])audienceData["audienceEmails"]);
                Console.WriteLine(result2.Item1);
                Console.WriteLine(result2.Item2);
            }


        }
        static Tuple<bool, string> Part1(string accountid, string accessToken, string audienceName, string audienceDescription)
        {
            var R2 = new RESTful2();
            R2.UserAgent("Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36");
            R2.Url("https://graph.facebook.com").Path("/v2.8/act_" + accountid + "/customaudiences");
            List<string> body = new List<string>();
            body.Add("name=" + audienceName);
            body.Add("subtype=CUSTOM");
            body.Add("description=" + audienceDescription);
            body.Add("access_token=" + accessToken);
            R2.Body(string.Join("&", body.ToArray<string>()));
            R2.Verb("POST");
            R2.Send();
            Tuple<bool, string> result = new Tuple<bool, string>(false, "");
            if (R2.IsOK())
            {
                result = new Tuple<bool, string>(true, R2.Response());
            }
            return result;
        }

        static string emailtosha(string[] arr)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                for (var i = 0; i < arr.Length; i++)
                {
                    arr[i] = '"' + sha256.ComputeHash(Encoding.ASCII.GetBytes(arr[i])).ToString() + '"';
                }
            }
            return string.Join(",", arr);
        }

        static Tuple<bool, string> Part2(string audienceid, string accessToken, string[] addresses)
        {
            /*
            curl \
            -F 'payload={
            "schema": "EMAIL_SHA256",
            "data": [
            "9b431636bd164765d63c573c346708846af4f68fe3701a77a3bdd7e7e5166254",
            "8cc62c145cd0c6dc444168eaeb1b61b351f9b1809a579cc9b4c9e9d7213a39ee",
            "4eaf70b1f7a797962b9d2a533f122c8039012b31e0a52b34a426729319cb792a"
            ]
            }' \
            -F 'access_token=<ACCESS_TOKEN>' \
            https://graph.facebook.com/v2.8/<CUSTOM_AUDIENCE_ID>/users
             */
            var R2 = new RESTful2();
            R2.UserAgent("Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36");
            R2.Url("https://graph.facebook.com").Path("/v2.8/" + audienceid + "/users");
            var body = "payload={" +
              "\"schema\": \"EMAIL_SHA256\"," +
              "\"data\": [" +
              emailtosha(addresses) +
              "]" +
              "};&access_token=" + accessToken;
            R2.Body(body);
            R2.Verb("POST");
            R2.Send();
            Tuple<bool,string> result = new Tuple<bool,string>(false,"");
            if (R2.IsOK())
            {
                result = new Tuple<bool,string>(true, R2.Response());
            }
            return result;
        }

    }
}
