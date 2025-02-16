using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumTests.Pages;
using SeleniumTests.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests.Tests
{
    [TestFixture]
    internal class BaseTest
    {
        protected IWebDriver Driver;
        protected LoginPage LoginPage;


        [OneTimeSetUp]
        public void BeforeAll()
        {
            // Configuración global antes de todas las pruebas
        }

        [SetUp]
        public void Setup()
        {
            Driver = WebDriverFactory.CreateDriver();
            LoginPage = new LoginPage(Driver);
            Driver.Navigate().GoToUrl("https://example.com");
        }

        [TearDown]
        public void TearDown()
        {
            Driver?.Quit();
        }
    }
}
