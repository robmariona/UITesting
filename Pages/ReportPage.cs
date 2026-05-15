using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using UITesting.Extensions;

namespace UITesting.Pages
{
    public class ReportPage : BasePage
    {
        public ReportPage(IWebDriver driver) : base(driver) { }

        // Locators (Better to store 'By' selectors than IWebElements for stability)
        private readonly By _revenueDisplay = By.Id("total-revenue");
        private readonly By _insuranceTable = By.ClassName("insurance-table");

        public string GetTotalRevenue()
        {
            return Driver.WaitAndFind(_revenueDisplay).Text;
        }

        public bool IsTableDisplayed()
        {
            // We use a 5-second override for quick checks
            return Driver.WaitAndFind(_insuranceTable, 5).Displayed;
        }

    }
}
