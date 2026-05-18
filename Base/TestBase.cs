using OpenQA.Selenium;
using UITesting.Extensions;
using UITesting.Pages;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;



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
            loginPage.ClientLogIn().LoginUser("Rob", "Password33!%");

            // THE ULTIMATE FIX: Explicitly pause until the login state clears
            var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(Driver, TimeSpan.FromSeconds(10));

            wait.Until(d =>
            {
                try
                {
                    // 1. Check if the Login link is gone, or if a Logout action appears
                    // Adjust the text 'Logout' to match exactly what your application displays when logged in
                    bool isLoggedOutButtonVisible = d.PageSource.Contains("Logout") || d.PageSource.Contains("Logout");

                    // 2. Alternatively, ensure the login input box itself has been destroyed/hidden
                    bool isLoginFormGone = d.FindElements(By.CssSelector("input[type='password']")).Count == 0;

                    return isLoggedOutButtonVisible || isLoginFormGone;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            });

            return loginPage;
        }





        public void Dispose()
        {
            // Every test will automatically close the browser when finished
            Driver.Quit();
            Driver.Dispose();
        }
    }
}
