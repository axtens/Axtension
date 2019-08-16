using Microsoft.ClearScript;
using Microsoft.ClearScript.Windows;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using Ionic.Zip;
using System.Data;
using System.Globalization;
using System.Linq;

namespace Axtension
{
 
    public class Runner
    {
        public static JScriptEngine JSE;

        static void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            //T.Error().Send(Process.GetCurrentProcess().ProcessName + ": unhandled exception caught: " + e.Message);
            //T.Error().Send(e.StackTrace);
            //T.Error().Send(string.Format(CultureInfo.InvariantCulture, "Runtime terminating: {0}", args.IsTerminating));
            Console.WriteLine(e.Message);
        }

        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlAppDomain)]
        public Runner ()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);

            JSE = new JScriptEngine(WindowsScriptEngineFlags.EnableDebugging | WindowsScriptEngineFlags.EnableJITDebugging);
            JSE.AddHostObject("CSHost", new ExtendedHostFunctions());

            JSE.AddHostType("CSMarkup", typeof(Markup));
            JSE.AddHostType("CSMarkupFormatters", typeof(MarkupFormatters));

            /// System
            JSE.AddHostType("CSString", typeof(String));
            JSE.AddHostType("CSEnvironment", typeof(Environment));
            JSE.AddHostType("CSConsole", typeof(Console));
            JSE.AddHostType("CSFile", typeof(File));
            JSE.AddHostType("CSFileInfo", typeof(FileInfo));
            JSE.AddHostType("CSDirectory", typeof(Directory));
            JSE.AddHostType("CSPath", typeof(Path));
            JSE.AddHostType("CSSearchOption", typeof(SearchOption));
            JSE.AddHostType("CSEncoding", typeof(Encoding));
            JSE.AddHostType("CSMemoryStream", typeof(MemoryStream));
            JSE.AddHostType("CSTimeSpan", typeof(TimeSpan));
            JSE.AddHostType("CSThread", typeof(Thread));
            JSE.AddHostType("CSProcess", typeof(Process));
            JSE.AddHostType("CSProcessStartInfo", typeof(ProcessStartInfo));
            JSE.AddHostType("CSSearchOption", typeof(SearchOption));
            JSE.AddHostType("CSUri", typeof(Uri));
            JSE.AddHostType("CSWebClient", typeof(WebClient));
            JSE.AddHostType("CSStreamReader", typeof(StreamReader));
            JSE.AddHostType("CSStream", typeof(Stream));
            JSE.AddHostType("CSBitmap", typeof(Bitmap));
            JSE.AddHostType("CSImageFormat", typeof(ImageFormat));
            JSE.AddHostType("CSDebugger", typeof(Debugger));

            /// Mail
            JSE.AddHostType("CSMailMessage", typeof(MailMessage));
            JSE.AddHostType("CSMailAddress", typeof(MailAddress));
            JSE.AddHostType("CSAttachment", typeof(Attachment));
            JSE.AddHostType("CSNetworkCredential", typeof(NetworkCredential));
            JSE.AddHostType("CSSmtpClient", typeof(SmtpClient));

            /// Firefox
            JSE.AddHostType("CSFirefoxBinary", typeof(FirefoxBinary));
            JSE.AddHostType("CSFirefoxDriver", typeof(FirefoxDriver));
            JSE.AddHostType("CSFirefoxProfileManager", typeof(FirefoxProfileManager));
            JSE.AddHostType("CSFirefoxProfile", typeof(FirefoxProfile));
            JSE.AddHostType("CSFirefoxDriverCommandExecutor", typeof(FirefoxDriverCommandExecutor));
            JSE.AddHostType("CSFirefoxOptions", typeof(FirefoxOptions));
            JSE.AddHostType("CSFirefoxDriverService", typeof(FirefoxDriverService));

            /// PhantomJS
            JSE.AddHostType("CSPhantomJSDriver", typeof(PhantomJSDriver));
            JSE.AddHostType("CSPhantomJSOptions", typeof(PhantomJSOptions));
            JSE.AddHostType("CSPhantomJSDriverService", typeof(PhantomJSDriverService));

            /// Selenium
            JSE.AddHostType("CSBy", typeof(By));
            JSE.AddHostType("CSJavascriptExecutor", typeof(IJavaScriptExecutor));
            JSE.AddHostType("CSActions", typeof(Actions));
            JSE.AddHostType("CSDriverService", typeof(OpenQA.Selenium.DriverService));
            JSE.AddHostType("CSRemoteWebDriver", typeof(RemoteWebDriver));
            JSE.AddHostType("CSDesiredCapabilities", typeof(DesiredCapabilities));
            JSE.AddHostType("CSPlatform", typeof(Platform));
            JSE.AddHostType("CSPlatformType", typeof(PlatformType));
            JSE.AddHostType("CSProxy", typeof(Proxy));
            JSE.AddHostType("CSProxyKind", typeof(ProxyKind));
            JSE.AddHostType("CSIWebDriver", typeof(IWebDriver));
            JSE.AddHostType("CSITakesScreenshot", typeof(ITakesScreenshot));
            JSE.AddHostType("CSScreenshot", typeof(Screenshot));
            JSE.AddHostType("CSSelectElement", typeof(SelectElement));

            /// HTMLAgilityPack
            JSE.AddHostType("CSHtmlDocument", typeof(HtmlAgilityPack.HtmlDocument));
            JSE.AddHostType("CSHtmlNode", typeof(HtmlAgilityPack.HtmlNode));
            JSE.AddHostType("CSHtmlNodeCollection", typeof(HtmlAgilityPack.HtmlNodeCollection));
            JSE.AddHostType("CSHtmlAttribute", typeof(HtmlAgilityPack.HtmlAttribute));
            //JSE.AddHostType(typeof(HapCssExtensionMethods));

            /// Axtension
            JSE.AddHostType("T", typeof(Axtension.ApplicationLogging));
            JSE.AddHostType("CSConfig", typeof(Axtension.Config));
            JSE.AddHostType("CSREST", typeof(Axtension.REST));
            JSE.AddHostType("CSRESTful", typeof(Axtension.RESTful));
            JSE.AddHostType("CSFluentREST", typeof(Axtension.RESTful2));
            JSE.AddHostType("CSProcesses", typeof(Axtension.Processes));
            JSE.AddHostType("CSMail", typeof(Axtension.Mail));
            JSE.AddHostType("CSDatabase", typeof(Axtension.SQL));
            JSE.AddHostType("CSTelstraSMS", typeof(Axtension.TelstraSMS));
            JSE.AddHostType("CSXML", typeof(Axtension.XML));
            JSE.AddHostType("CSSQL", typeof(Axtension.SQL));
            JSE.AddHostType("CSGoogleOAuth2", typeof(Axtension.GoogleOAuth2));
            JSE.AddHostType("CSDebugPoints", typeof(Axtension.DebugPoints));

            //
            JSE.AddHostType("CSZipFile", typeof(ZipFile));
        }

        public Tuple<bool,object> Run(string scriptText, Config cfg, Dictionary<string,object> settings)
        {
            bool ok = false;

            JSE.AddHostObject("CSCFG", cfg);
            JSE.AddHostObject("CSSettings", settings);
            object evalResponse;
            try
            {
                evalResponse = JSE.Evaluate(scriptText);
                ok = true;
            }
            catch (ScriptEngineException sex)
            {
                evalResponse = String.Format("{0}\r\n{1}\r\n{2}\r\n", sex.ErrorDetails, sex.Message, sex.StackTrace);
                ok = false;
            }
            return new Tuple<bool, object> ( ok, evalResponse );
        }
    }
}
