using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using UITesting.Base;
using UITesting.Extensions;

namespace UITesting.Pages
{
    public class LoginPage : BasePage
    {

        public LoginPage(IWebDriver driver) : base(driver) {}



        private readonly By _Username = By.CssSelector("input[placeholder*='Username']");
        private readonly By _Password = By.CssSelector("input[placeholder*='Password']");
        private readonly By _Loginbutton = By.CssSelector("button[type*='submit']");

        public LoginPage ClientLogIn()
        {
            Driver.WaitAndClick(ClientLogInLocator);
            return new LoginPage(Driver);
        }

        public LoginPage LoginUser(string username, string password)
        {
            // Use your fixed extension method
            Driver.WaitAndType(_Username, username);
            Driver.WaitAndType(_Password, password);
            Driver.WaitAndClick(_Loginbutton);

            // Ensure the token exists before moving on
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return localStorage.getItem('token');") != null);


            wait.Until(ExpectedConditions.UrlContains($"{BaseUrl}"));

            return new LoginPage(Driver);
        
        }
        
        public void LogoutUser()
        {
            Driver.WaitAndClick(ClientLogOutLocator);
        }
        


    }
}
