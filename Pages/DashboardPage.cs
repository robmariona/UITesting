using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using UITesting.Extensions;

namespace UITesting.Pages
{
    public class DashboardPage : BasePage
    {




        public DashboardPage(IWebDriver driver) : base(driver) {

            /*var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            // This ensures the redirect is DONE before we look for the Products button
            wait.Until(d => d.Url.Contains("/dashboard"));*/
        }

        private readonly By _reportTab = By.XPath("//button[normalize-space()='Report']");
        private readonly By _productTab = By.XPath("//button[normalize-space()='Products']");




        public void ClickDashboard()
        {
            Driver.WaitAndClick(DashboardButtonlocator,10);
            Driver.WaitUntilUrlIsStable("/dashboard");
        }

        // Actions using the new Utility
        public void ClickReportTab()
        {
            Driver.WaitAndClick(_reportTab);
        }

        public void ClickProductTab()
        {
            Driver.WaitAndClick(_productTab,10);
        }


    }
}
