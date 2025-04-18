using Business.ApplicationInterface;
using Core.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V132.DOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Business
{
    public class SearchWorkflow
    {
        private readonly SearchPage searchPage;

        public SearchWorkflow(IWebDriver driver)
        {
            searchPage = new SearchPage(driver);
            Logger.Info("Initializing Search Page Instance...");
        }

        public List<string> PerformSearch(string searchedValue) 
        {
            try
            {
                searchPage.ClickMagnifierIcon();
                searchPage.EnterSearchedValue(searchedValue);
                searchPage.ClickSearchBttn();
                searchPage.ClickOnTheLatestItem();

                var linksTexts = searchPage.ExtractAllLinksTexts();
                Logger.Info("Links text extraction performed...");
                return linksTexts;
            }
            catch (Exception ex) 
            {
                Logger.Error("Links text extraction failed", ex);
                throw;
            }
            
            
        }

        public bool ValidateAllItems(List<string> linksTexts, string searchString) 
        {
            try
            {
                bool allContaiingItems = linksTexts.TrueForAll(text => text.Contains(searchString, StringComparison.OrdinalIgnoreCase));
                Logger.Info("Links text Validation Performed...");
                return allContaiingItems;
            }
            catch (Exception ex) 
            {
                Logger.Error("Links text Validation Failed...", ex);
                throw;
            }
            
        }

        public int CountTextsWithSearchedValue(List<string> texts, string searchString)
        {
            int containing = texts.Count(text => text.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            return containing;
        }
        public int CountTextsWithoutSearchedValue(List<string> texts, string searchString)
        {
            int notContaining = texts.Count(text => !text.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            return notContaining;
        }


    }
}
