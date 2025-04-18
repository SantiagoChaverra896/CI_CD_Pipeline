using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Core;

namespace Business.ApplicationInterface
{
    public class AboutPage: BasePage
    {
        public AboutPage(IWebDriver driver) : base(driver) 
        {
            CookieHandler();
        }
        //Locators for TC3
        private readonly By aboutBttn = By.XPath("//li[@class='top-navigation__item epam'][4]");
        private readonly By epamAAGSec = By.XPath("//span[contains(text(),'EPAM at')]");
        private readonly By downloadBttn = By.XPath("//span[@class='button__content button__content--desktop'][normalize-space()='DOWNLOAD']");

        //Methods for TC3
        public void ClickAboutBttn()
        {
            Driver.FindElement(aboutBttn).Click();
        }

        public void ScrolltoSection()
        {
            actions.MoveToElement(Driver.FindElement(epamAAGSec))
                   .Perform();
        }

        public void ClickDownloadBttn()
        {
            Driver.FindElement(downloadBttn).Click();
        }

    }
}
