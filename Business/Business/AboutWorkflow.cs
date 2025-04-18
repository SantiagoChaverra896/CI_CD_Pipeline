using Business.ApplicationInterface;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Data;
using log4net;
using Core.Core;
using System.Diagnostics;

namespace Business.Business
{
    public class AboutWorkflow
    {
        private readonly AboutPage aboutPage;

        public AboutWorkflow(IWebDriver driver) 
        {
            aboutPage = new AboutPage(driver);
            Logger.Info("Initializing About Page Instance...");
        }

        public bool DownloadFile() 
        {
            try
            {
                aboutPage.ClickAboutBttn();
                aboutPage.ScrolltoSection();
                aboutPage.ClickDownloadBttn();
                bool FileDownloaded = WaitForFileDownload(Data.Data.downloadPath, Data.Data.fileName, Data.Data.Timeout);
                Logger.Info("Perform File Download..Executed Successfully");
                return FileDownloaded;
            }
            catch (Exception ex)
            {
                Logger.Error("Perform File Download Failed" + ex.Message);
                throw;
            }           
            
        }

        public bool WaitForFileDownload(string directory, string fileName, int timeoutInSeconds)
        {
            string filePath = Path.Combine(directory, fileName);
            int elapsed = 0;

            while (elapsed < timeoutInSeconds)
            {
                if (File.Exists(filePath))
                    return true;  //File is found, exit early

                Task.Delay(1000).Wait();  //Non-blocking wait
                elapsed++;
            }
            return false;  //File was not downloaded in time
        }

        public void CloseOpenedPdf()
        {
            string[] pdfViewers = { "AcroRd32", "Acrobat" }; // Common PDF viewers

            foreach (var processName in pdfViewers)
            {
                var processes = Process.GetProcessesByName(processName);
                foreach (var process in processes)
                {
                    try
                    {
                        process.Kill();
                        process.WaitForExit(); // Ensure it fully closes
                        Logger.Info($"Closed {processName}");
                    }
                    catch (Exception ex)
                    {
                        Logger.Error($"Could not close {processName}: {ex.Message}");
                    }
                }
            }
        }

        public void DeleteDownloadedFile(string directory, string fileName)
        {
            string filePath = Path.Combine(directory, fileName);

            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                    Logger.Info($"✅ File {fileName} deleted successfully.");
                }
                catch (Exception ex)
                {
                    Logger.Error($"❌ Could not delete {fileName}: {ex.Message}");
                }
            }
            else
            {
                Logger.Info($"⚠️ File {fileName} does not exist.");
            }
        }
    }
}
