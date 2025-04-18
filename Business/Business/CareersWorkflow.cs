using Business.ApplicationInterface;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Core.Core;

namespace Business.Business
{
    public class CareersWorkflow
    {
        private readonly CareersPage careersPage;
        public CareersWorkflow(IWebDriver driver) 
        {
            careersPage =  new CareersPage(driver);
            Logger.Info("Initializing Careers Page Instance...");
        }

        public bool PerformCareerSearch(string language, string location) 
        {
            try
            {
                careersPage.ClickCareersBttn();
                careersPage.EnterLanguage(language);
                careersPage.SelectLocation(location);
                careersPage.CheckRemoteOption();
                careersPage.ClickFindBttn();
                careersPage.AccessLatestJobPosition();
                bool containsLanguage = careersPage.ReturnProgrammingLanguage(language);
                
                Logger.Info("Perform Career Search ..Executed Successfully");
                return containsLanguage;
            }
            catch (Exception ex)
            {
                Logger.Error("Perform Career Search Failed" +  ex.Message);
                throw;
            }
        }
    }
}
