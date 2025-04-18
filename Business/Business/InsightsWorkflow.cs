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

namespace Business.Business
{
    public class InsightsWorkflow
    {
        private readonly InsightsPage insightsPage;

        public InsightsWorkflow(IWebDriver driver) 
        { 
            insightsPage = new InsightsPage(driver);
            Logger.Info("Initializing Insights Page Instance...");
        }

        public string GetArticleTitle() 
        {
            try
            {
                insightsPage.ClickInsightsBttn();

                for (int i = 0; i < Data.Data.numberOfClicks; i++)
                {
                    insightsPage.ClickNextSlideBttn();
                }
                string articleName = insightsPage.GetArticleTitle();
                Logger.Info("Article Name Retireved Successfully...");
                return articleName;

            }
            catch (Exception ex)
            {
                Logger.Error("Error trying to retrive article name", ex);
                throw;
            }
            
        }

        public string GetActualArticleTitle() 
        {
            try
            {
                insightsPage.ClickReadMoreBttn();
                Logger.Info("Actual Title retrieved Successfully...");
                return insightsPage.GetActuaTitle();
            }
            catch (Exception ex) 
            {
                Logger.Error("Error while trying to retrieve actual title", ex);
                throw;
            }
            
        }




    }
}
