using OpenQA.Selenium;
using UItests.Configuration;
using UItests.Extensions;

namespace UItests.Pages
{
    public abstract class BasePage
    {
        protected readonly IWebDriver Driver;

        protected BasePage(IWebDriver driver)
        {
            Driver = driver;
        }

        public virtual string Url => AppSettings.Current.BaseUrl;

        public abstract By PageLocator { get; }

        public void WaitPage() => Driver.WaitForDisplayed(PageLocator);
    }
}
