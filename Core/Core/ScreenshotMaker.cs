using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Core
{
    public class ScreenshotMaker
    {
        public static void TakeScreenshot(IWebDriver driver, string testName)
        {
            try
            {
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                string screenshotsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshots");

                // Ensure the directory exists
                if (!Directory.Exists(screenshotsDirectory))
                {
                    Directory.CreateDirectory(screenshotsDirectory);
                }

                // Remove invalid characters from test name
                string safeTestName = Regex.Replace(testName, @"[<>:""/\\|?*]", "_");

                string filePath = Path.Combine(screenshotsDirectory, $"{safeTestName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");
                screenshot.SaveAsFile(filePath);

                Console.WriteLine($"Screenshot saved at: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error taking screenshot: {ex.Message}");
            }
        }

    }
}
