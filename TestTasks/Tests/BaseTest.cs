using NUnit.Framework;
using OpenQA.Selenium;
using UItests.Driver;

namespace UItests.Tests
{
    public class BaseTest
    {
        protected IWebDriver Driver;

        [SetUp]
        public void SetUp()
        {
            //TODO: add DI
            Driver = new DriverBuilder().GetChromeInstance();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void Cleanup()
        {
            Driver.Quit();
        }
    }
}
