using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox; //Unnecessary using

namespace TestTheTest
{
    public class LoginAutomation
    {
        [SetUp] //Setup and Test should be separated into different methods.
        private void Login() //Method should be renamed to describe test scenario better. e.g. 'SuccesfulLogin'. Method should have [Test] attribute
        {
            string strDriverPath = "path of driver"; //Unused variable? Why it is needed at all?
            IWebDriver driver = new ChromeDriver(); //Should be moved to Setup() method
            driver.Manage().Window.maximise(); //Should be moved to Setup() method. 
                                               //IWebDriver does not contain a definition for 'maximise'. Use Maximise.
            driver.Navigate().GoToUrl("https://ga.sorted.com/newtrack"); //It would be nice to store url in separate global variable or config file so that is possible to run tests on different envs
            Thread.Sleep(5000); //Explicit wait is considered a bad practice. It can increase run time by waiting too long or cause flaky tests when system is slower. Consider adding waiter with condition (e.g. WaitingFor( () => _loginPage.EmailTextField.Displayed, 5000));)
            IWebElement user = driver.FindElement(By.Id("//form [@id='loginForm']/input[1]")); // "//form [@id='loginForm']/input [1]" - provided xpath is not id. should be driver.FindElement(By.Xpath("//form [@id='loginForm']/input[1]"))
            IWebElement password = driver.FindElement(By.XPath("//form[@id='loginForm']/input[2]")); //bad practice to use index for html element xpath, try to use @id or some more specific/unique attribute
            IWebElement login = driver.find_element_by_xpath ("submit")); // IWebDriver does not contain a definition for 'find_element_by_xpath'. It should be driver.FindElement(By.XPath("[your xpath]")); 
                                                                          // Looks like this can be driver.FindElement(By.Id("submit"))
            username.SendKeys(usernameValue) //; expected
                                              // Variable "usernameValue" needs to be defined before it is used.
            password.SendKeys(passwordValue); //Variable "passwordValue" needs to be defined before it is used.
            string usernameValue = "john_smith@sorted.com"; //Would be great to store credentials in some config file. So that it is easier to run tests on different envs that have different users.
            string passwordValue = "Pa55word!""; //Would be great to store credentials in some config file. So that it is easier to run tests on different envs that have different users. 
                                                 // '"' character should be escaped ("Pa55word!\"")

            // Action is missed. :Looks like login.Click() should be added;

            string actualUrl = "http://ga.sorted.com/newtrack/loginSuccess"; // This is expected value please rename variable to be "expectedUrl"
            string expectedURL = driver.Url; // This is actual value, please rename variable to be "actualUrl"
            Assert.Equals (actualUrl, expectedURL);
        }

        [TearDown] //SetUp and TearDown methods can be moved into BaseTest class
        public void Teardown()
        {
            driver.quit(); //IWebDriver does not contain a definition for 'quit'. Use Quit. Aslo, you need to define field in this class and store driver there to be able to Quit the driver that was created in Login method

        }
    }
}

// Only one use-case of this story is covered. Please add other scenarios as well.
//Please fix these comments and as the next step we can discuss how to architect your test better

ToDo:
1. Move SetUp into separate method.
    - Use Maximise method instead of maximise
2. Define  IWebDriver driver field in the class.
3. Create json config file and specify url and credentials here. Read configuration from this file.
4. Refactor Test method:
    - rename this test method to describe test case;
    - add [Test] attribute;
    - write correct Wait metod and use it instead of Thread.Sleep
    - specify correct web elements (in future these can be moved to separate class e.g. LoginPage)
    - rename varibles for actual and expected result
    - add action to press login button
5. Use Quit method instead of quit
6. Cover other scenarios.