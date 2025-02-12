using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers; 
using System;

namespace SeleniumTests.Pages
{
    internal class BasePage
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver; // Corregido el error de asignación
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10)); // Corregido el tipo
        }

        protected IWebElement WaitAndFindElement(By by)
        {
            return wait.Until(ExpectedConditions.ElementIsVisible(by)); // Corregido el uso de ExpectedConditions
        }

        protected void WaitAndClick(By by)
        {
            WaitAndFindElement(by).Click();
        }

        protected void WaitAndSendKeys(By by, string text)
        {
            WaitAndFindElement(by).SendKeys(text);
        }

        protected bool IsElementPresent(By by)
        {
            try
            {
                WaitAndFindElement(by);
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
    }
}
