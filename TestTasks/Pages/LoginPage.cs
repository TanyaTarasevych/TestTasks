using OpenQA.Selenium;
using UItests.Extensions;

namespace UItests.Pages
{
    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public override string Url => base.Url + "/login";
        public override By PageLocator => By.XPath("//*[@class='header' and contains(text(), 'Welcome')]");

        private By UserNameTextFiled => By.Id("userName");
        private By PasswordTextFiled => By.Id("password");
        private By LoginButton => By.Id("submit");
        private By ErrorMessage => By.XPath("//*[contains(@class, 'error') and contains(text(), 'Username or password is invalid. Please try again')]");
        private By ForgotPasswordLink => By.Id("forgotPassword");

        public void SetUserName(string value) => Driver.SetField(UserNameTextFiled, value);

        public void SetPassword(string value) => Driver.SetField(PasswordTextFiled, value);

        public void ClickLoginButton() => Driver.FindElement(LoginButton).Click();

        public bool ErrorMessageIsDisplayed => Driver.FindElement(ErrorMessage).Displayed;

        public bool ForgotPasswordIsDisplayedAndEnabled()
        {
            var element = Driver.FindElement(ForgotPasswordLink);
            return element.Displayed && element.Enabled;
        }
    }
}
