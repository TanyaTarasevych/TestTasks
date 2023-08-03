using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using UItests.Configuration;
using UItests.Pages;
using UItests.Utils;

namespace UItests.Tests
{
    public class LoginTest: BaseTest
    {
        private readonly LoginPage _loginPage;

        public LoginTest()
        {
            _loginPage = new LoginPage(Driver);
        }

        [Test]
        public void Login_WithValidCredentials_Successful()
        {
            Driver.Navigate().GoToUrl(_loginPage.Url);
            _loginPage.WaitPage();

            _loginPage.SetUserName(AppSettings.Current.User.UserName);
            _loginPage.SetPassword(AppSettings.Current.User.Password);
            _loginPage.ClickLoginButton();

            var actualUrl = Driver.Url;

            actualUrl.Should().Be(AppSettings.Current.BaseUrl);
        }

        [Test]
        [TestCase("/page1")]
        [TestCase("/page2")]
        [TestCase("/page3")]
        public void OpenPage_WithoutLogin_LoginPageOpened(string page)
        {
            var url = AppSettings.Current.BaseUrl + page;
            Driver.Navigate().GoToUrl(url);
            Wait.For(() => Driver.Url != url);

            var actualUrl = Driver.Url;
            var expectedUrl = _loginPage.Url;

            actualUrl.Should().Be(expectedUrl);
        }

        [Test]
        [TestCase("tetetet", "kckckc")]
        [TestCase("", "kckckc")]
        [TestCase("lllll", "")]
        public void Login_WithInvalidCredentials_NotSuccessful(string userName, string password)
        {
            Driver.Navigate().GoToUrl(_loginPage.Url);
            _loginPage.WaitPage();

            _loginPage.SetUserName(userName);
            _loginPage.SetPassword(password);
            _loginPage.ClickLoginButton();

            var actualUrl = Driver.Url;
            using (new AssertionScope())
            {
                actualUrl.Should().Be(_loginPage.Url);
                _loginPage.ErrorMessageIsDisplayed.Should().BeTrue();
                _loginPage.ForgotPasswordIsDisplayedAndEnabled().Should().BeTrue();
            }
        }
    }
}
