using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace UItests.Extensions
{
    public static class FormExtensions
    {
        public static void SetField(this IWebDriver driver, By by, string value)
        {
            driver.WaitForDisplayed(by);
            driver.FindElement(by).SendKeys(value);
        }
    }
}
