using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Axtension
{
    class SeleniumCarbide
    {
        public SeleniumCarbide()
        {

        }

        public void ExplicitWaitGoToUrl(IWebDriver driver, string url, int timeout, string type, string typeValue)
        {
            By by = null;
            switch (type.ToLower())
            {
                case "id":
                    by = By.Id(typeValue);
                    break;
                case "class":
                    by = By.ClassName(typeValue);
                    break;
                case "name":
                    by = By.Name(typeValue);
                    break;
                case "tagname":
                    by = By.TagName(typeValue);
                    break;
                case "css":
                    by = By.CssSelector(typeValue);
                    break;
            }
            driver.Url = url;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            IWebElement myDynamicElement = wait.Until<IWebElement>(d => d.FindElement(by));
            return;
        }

        public bool WaitForDocumentReady(IWebDriver driver, int timeout = 60)
        {
            bool result = false;
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            for (int i = 0; i < timeout; i++)
            {
                if (js.ExecuteScript("return document.readyState").ToString() == "complete")
                {
                    result = true;
                    break;
                }
                Thread.Sleep(1000);
            }
            return result;
        }
    }
}
