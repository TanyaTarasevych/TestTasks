using OpenQA.Selenium;
using UItests.Utils;

namespace UItests.Extensions
{
    public static class WaitExtensions
    {
        internal static void WaitForDisplayed(this IWebDriver driver, By by, TimeSpan? timeout = null)
        {
            driver.WaitForCondition(
                by,
                element => element.Displayed,
                $"Element '{by}' is not displayed after timeout",
                timeout);
        }

        private static void WaitForCondition(
            this IWebDriver driver,
            By by,
            Func<IWebElement, bool> condition,
            string message,
            TimeSpan? timeout = null)
        {
            var waitCondition = Wait.For(
                () => condition(driver.FindElement(by)),
                timeout);

            if (!waitCondition)
            {
                throw new NotFoundException(message);
            }
        }
    }
}
