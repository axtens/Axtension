using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.PhantomJS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace Axtension
{
    public static class GoogleOAuth2
    {
        public static string OAuth2ConnectExplorer(string client_id, string scope, string login_hint)
        {
            if (DebugPoints.DebugPointRequested("GoogleOAuth2.OAuth2ConnectExplorer"))
            {
                Debugger.Launch();
            }
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
        public static string OAuth2Connect(string client_id, string scope, string login_hint)
        {
            if (DebugPoints.DebugPointRequested("GoogleOAuth2.OAuth2Connect"))
            {
                Debugger.Launch();
            }
            string title = "";
            var url = string.Format(
                "https://accounts.google.com/o/oauth2/auth?{0}&{1}&{2}&{3}&{4}&{5}",
                "response_type=code",
                "client_id=" + client_id,
                "redirect_uri=" + "urn:ietf:wg:oauth:2.0:oob",
                "scope=" + WebUtility.HtmlEncode(scope),
                "state=acit",
                "login_hint=" + login_hint);

            var driver = new FirefoxDriver();
            driver.Navigate().GoToUrl(url);
            // could automate the whole thing
            TimeSpan ts = new TimeSpan(0, 1, 0); // one minute
            IWebElement element = (new WebDriverWait(driver, ts))
                .Until(ExpectedConditions.ElementToBeClickable(By.Id("code")));
            title = driver.FindElement(By.Id("code")).GetAttribute("value");
            driver.Quit();
            driver.Dispose();
            return title;
        }
        public static string GetTokens(string client_id, string client_secret, string code)
        {
            Dictionary<string, object> dataDict = new Dictionary<string, object>()
            {
                {"code", code},
                {"client_id", client_id},
                {"client_secret", client_secret},
                {"redirect_uri", "urn:ietf:wg:oauth:2.0:oob"},
                {"grant_type", "authorization_code"}
            };

            
            string res = REST.HttpPost("https://accounts.google.com/o/oauth2/token", dataDict);
            return res;
        }

        public static Dictionary<string, object> RefreshToken(string client_id, string client_secret, string refresh_token)
        {
            Dictionary<string, object> dataDict = new Dictionary<string, object>()
            {
                {"client_id", client_id },
                {"client_secret", client_secret },
                {"refresh_token", refresh_token },
                {"grant_type", "refresh_token" }
            };
            string res = REST.HttpPost("https://accounts.google.com/o/oauth2/token", dataDict);
            return (Dictionary<string, object>)DecodeJson(res);
        }

        public static string GetData(string url, string access_token)
        {
            return REST.HttpGet(url + "&access_token=" + access_token);
        }

        public static object DecodeJson(string json) //Dictionary<string, object>
        {
            JavaScriptSerializer ser = new JavaScriptSerializer()
            {
                MaxJsonLength = 8388698 /* 8MB */
            };
            if (json.Substring(0, 1) == "[")
            {
                return ser.Deserialize<List<object>>(json);
            }
            else
            {
                return ser.Deserialize<Dictionary<string, object>>(json);
            }
        }

        public static string GetAccessToken(string client_id, string client_secret, string refresh_token)
        {
            if (DebugPoints.DebugPointRequested("GoogleOAuth2.GetAccessToken"))
            {
                Debugger.Launch();
            }

            Dictionary<string, object> dso = GoogleOAuth2.RefreshToken(client_id, client_secret, refresh_token);
            return dso["access_token"].ToString();
        }

        public static string GetAccessTokenViaCfg(string cfgFile, string scopes = "", bool forceReAuth = false, bool forceRefresh = false)
        {
            if (DebugPoints.DebugPointRequested("GoogleOAuth2.GetAccessTokenViaCfg"))
            {
                Debugger.Launch();
            }

            Config cfg = new Config(cfgFile);
            string secrets = cfg.Retrieve(".secrets", "");
            if (string.Empty == secrets || !System.IO.File.Exists(secrets))
            {
                return string.Empty;
            }
            string jsonText = System.IO.File.ReadAllText(secrets);
            if (string.Empty == jsonText)
            {
                return string.Empty;
            }
            Dictionary<string, object> dso = new Dictionary<string, object>();
            dso = (Dictionary<string, object>)GoogleOAuth2.DecodeJson(jsonText);
            dso = (Dictionary<string, object>)dso["installed"]; // might be "web" too
            string client_id = dso["client_id"].ToString();
            string client_secret = dso["client_secret"].ToString();

            var accessToken = cfg.Retrieve(".access_token", "");
            var refreshToken = cfg.Retrieve(".refresh_token", "");
            var developerToken = cfg.Retrieve(".developer");

            long timeout;
            string tokens;
            if (string.Empty == refreshToken || forceReAuth == true)
            {
                if (string.Empty == accessToken || forceReAuth == true)
                {
                    string code = GoogleOAuth2.OAuth2Connect(client_id, scopes, cfg.Retrieve(".login_hint"));
                    tokens = GoogleOAuth2.GetTokens(client_id, client_secret, code);
                    dso = (Dictionary<string, object>)GoogleOAuth2.DecodeJson(tokens);
                    cfg.Define(".access_token", dso["access_token"].ToString());
                    cfg.Define(".refresh_token", dso["refresh_token"].ToString());
                    cfg.Define(".expires_in", dso["expires_in"].ToString());
                    cfg.Define(".token_type", dso["token_type"].ToString());
                    timeout = long.Parse(dso["expires_in"].ToString());
                    DateTime dt = DateTime.Now.AddSeconds((timeout.FromUnixTime()).ToUnixTime());
                    cfg.Define(".timeout", (int)dt.ToUnixTime());
                    cfg.Define(".token_type", dso["token_type"].ToString());
                    cfg.Save();
                    accessToken = dso["access_token"].ToString();
                }
            }
            else
            {
                //var arg = "force";
                timeout = (long)cfg.Retrieve(".timeout", 0);
                long nowMilli = DateTime.Now.ToUnixTime();
                if (timeout < nowMilli || forceRefresh == true)
                {
                    dso = GoogleOAuth2.RefreshToken(client_id, client_secret, refreshToken);
                    accessToken = dso["access_token"].ToString();
                    cfg.Define(".access_token", accessToken);
                    cfg.Define(".expires_in", dso["expires_in"].ToString());
                    cfg.Define(".token_type", dso["token_type"].ToString());
                    timeout = long.Parse(dso["expires_in"].ToString());
                    DateTime dt = DateTime.Now.AddSeconds((timeout.FromUnixTime()).ToUnixTime());
                    cfg.Define(".timeout", (int)dt.ToUnixTime());
                    cfg.Save();
                }
                else
                {
                    accessToken = cfg.Retrieve(".access_token");
                }
            }

            return accessToken;
        }
       
    }
}
