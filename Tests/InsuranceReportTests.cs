using UITesting.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI; // Needed for WebDriverWait
using SeleniumExtras.WaitHelpers;
using Xunit;
using UITesting.Base;
using UITesting.Extensions;

namespace UITesting.Tests
{
    [Collection("Sequential Tests")]

    public class InsuranceReportTests : TestBase
    {
        private readonly DashboardPage _dashboard;
        private readonly LoginPage _loginPage;
        private readonly ReportPage _ReportPage;


        public InsuranceReportTests()

        {
            // We just pass the Driver from the Base to our Page
            _loginPage = new LoginPage(Driver);
            _dashboard = new DashboardPage(Driver);
            _ReportPage = new ReportPage(Driver);
        }

        [Fact]
        public void UserCanViewInsuranceReport_ShouldDisplayData()
        {
            

            _dashboard.ClickDashboard();
            _dashboard.ClickReportTab();

            Assert.Contains("$", _ReportPage.GetTotalRevenue());

           // _loginPage.LogoutUser();
        }



    }
}
