using OpenQA.Selenium;
using UITesting.Extensions;
using UITesting.Pages;



namespace UITesting.Base
{
    public abstract class TestBase : IDisposable
    {
        protected readonly IWebDriver Driver;
        protected readonly string BaseUrl = "https://myapp2-ui.onrender.com";

        protected TestBase()
        {
            string browser = Environment.GetEnvironmentVariable("BROWSER") ?? "chrome";
            string headlessStr = Environment.GetEnvironmentVariable("HEADLESS") ?? "false";
            // Usually http://localhost:4444/wd/hub for local Selenium Grid
            string gridUrl = Environment.GetEnvironmentVariable("GRID_URL") ?? "";

            bool isHeadless = bool.Parse(headlessStr);

            Driver = DriverFactory.GetDriver(browser, isHeadless, gridUrl);
            Driver.Manage().Window.Maximize();
            Driver.Navigate().GoToUrl(BaseUrl);

            LoginAsValidUser();

            

        }
        protected LoginPage LoginAsValidUser()
        {
            var loginPage = new LoginPage(Driver);
            return loginPage.ClientLogIn().LoginUser("Rob", "Password33!%"); // 


        }





        public void Dispose()
        {
            // Every test will automatically close the browser when finished
            Driver.Quit();
            Driver.Dispose();
        }
    }
}
