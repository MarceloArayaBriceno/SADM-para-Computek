using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests.Pages
{
    internal class LoginPage : BasePage
    {
        private readonly By _usernameInput = By.Id("username");
        private readonly By _passwordInput = By.Id("password");
        private readonly By _loginButton = By.Id("loginBtn");
        private readonly By _errorMessage = By.ClassName("error-message");

        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        public LoginPage EnterUsername(string username)
        {
            WaitAndSendKeys(_usernameInput, username);
            return this;
        }

        public LoginPage EnterPassword(string password)
        {
            WaitAndSendKeys(_passwordInput, password);
            return this;
        }

        /*public DashboardPage ClickLogin()
        {
            WaitAndClick(_loginButton);
            return new DashboardPage(Driver);
        }*/

        public bool IsErrorMessageDisplayed()
        {
            return IsElementPresent(_errorMessage);
        }

        // Fluent Interface Method
        /*public DashboardPage LoginAs(string username, string password)
        {
            return EnterUsername(username)
                   .EnterPassword(password)
                   .ClickLogin();
        }*/



    }

}
