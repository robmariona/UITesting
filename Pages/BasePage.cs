using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using UITesting.Extensions;

namespace UITesting.Pages
{
    public class BasePage
    {

        protected readonly IWebDriver Driver;

        protected BasePage(IWebDriver driver)
        {
            Driver = driver;


        }
        public string BaseUrl = "https://myapp2-ui.onrender.com";

        protected readonly By ClientLogInLocator = By.CssSelector("a[href*='login']");
       // protected readonly By ClientLogOutLocator = By.XPath($"//button[contains(text(), 'Logout')]");
        protected readonly By DashboardButtonlocator = By.CssSelector("a[href*='dashboard']");
        protected readonly By FileAclaimLocator = By.CssSelector("a[href*='file-claim']");


        public IWebElement Navbar => Driver.FindElement(By.CssSelector("div[class*='justify-between']"));
        public IWebElement DashboardButton => Driver.FindElement(DashboardButtonlocator);

        public IWebElement ClientLogInButton => Driver.FindElement(ClientLogInLocator);
        //public IWebElement ClientLogOutButton => Driver.FindElement(ClientLogOutLocator);

        public IWebElement FileAclaimButton => Driver.FindElement(FileAclaimLocator);

        public FileAClaimPage ClickFileAClaim()
        {
            // Pass the By locator directly, do not pass the initialized element
            Driver.WaitAndClick(FileAclaimLocator, 10);

            // Return a new initialized instance of the destination page
            return new FileAClaimPage(Driver);
        }




    }
}
