using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Interactions;

namespace Core.Core
{
    public abstract class BasePage
    {
        protected IWebDriver Driver;
        protected WebDriverWait wait;
        protected Actions actions;

        // Constructor
        public BasePage(IWebDriver driver)
        {
            this.Driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            this.actions = new Actions(driver);
        }

        // Common Elements
        protected By loaderSpinner = By.CssSelector(".preloader");
        protected By acceptCookiesBttn = By.Id("onetrust-accept-btn-handler");
        protected By cookiesBaner = By.XPath("//div[@id='onetrust-banner-sdk']");


        // Common Methods - Wait Helpers
        protected IWebElement WaitForElement(By locator)
        {
            return wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        protected void WaitForElementToDisapear(By locator)
        {
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
        }

        protected IWebElement WaitForElementToBeClickable(By locator) 
        {
            return wait.Until(ExpectedConditions.ElementToBeClickable(locator));
        }

        protected void CookieHandler() 
        {
            try
            {
                WaitForElementToBeClickable(acceptCookiesBttn);
                // Scroll the element into view
                ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", Driver.FindElement(acceptCookiesBttn));
                // Click using JavaScript to avoid further interaction issues
                ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", Driver.FindElement(acceptCookiesBttn));
                WaitForElementToDisapear(cookiesBaner);
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Cookies banner not found, proceeding...");
            }
        }





    }
}
