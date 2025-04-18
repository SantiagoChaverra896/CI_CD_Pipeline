using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Modules.Browser;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;


namespace Core.Core
{
    public class DriverManager
    {
        private static IWebDriver driver;
                        
        public static IWebDriver GetDriver(string browser) 
        {
            bool isHeadless = true;
            ChromeOptions options = new ChromeOptions();

            if (isHeadless)
            {
                options.AddArgument("--headless=new"); // Enable headless mode
                options.AddArgument("--disable-gpu"); // Recommended for Windows
                options.AddArgument("--window-size=1920,1080"); // Set a default window size
            }

            switch (browser.ToLower())
            {
                case "chrome":
                    driver = new ChromeDriver(options);
                    break;
                case "firefox":
                    driver = new FirefoxDriver();
                    break;
                case "edge":
                    driver = new EdgeDriver();
                    break;
                default:
                    throw new ArgumentException("Unsupported browser: " + browser);
            }
            driver.Manage().Window.Maximize();

            return driver;        
        }

        public static void QuitDriver() 
        {
            driver?.Quit();
        }
    }
}
