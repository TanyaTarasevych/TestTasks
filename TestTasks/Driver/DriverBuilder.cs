using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace UItests.Driver
{
    public class DriverBuilder
    {
        public IWebDriver GetChromeInstance()
        {
            var options = new ChromeOptions();
            options.AddArgument("--incognito");

            var driver = new ChromeDriver(options);

            return driver;
        }
    }
}
