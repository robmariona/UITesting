using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UITesting.Base;
using OpenQA.Selenium;
using UITesting.Extensions;
using UITesting.Models;

namespace UITesting.Pages
{
    public  class ClaimPage : BasePage
    {
        public ClaimPage(IWebDriver driver) : base(driver)
        {

        }

        private readonly By PolicySelect = By.XPath($"//select[contains(text(),'Select a policy')]");
        private readonly By ClaimDescription = By.CssSelector("textarea[placeholder*='Please describe the incident in detail...']");
        private readonly By btnSubmitClaim = By.XPath($"//button[contains(text(), 'Submit Claim')]");

        public void FileAClaim_form(ClaimModel claim)
        {
            
            Driver.WaitAndSelectByPartialText(PolicySelect, "Standard");
            Driver.WaitAndType(ClaimDescription, claim.Description);
        }

        public void SubmitClaim(ClaimModel claim)
        {
            FileAClaim_form(claim);
            Driver.WaitAndClick(btnSubmitClaim);

        }

    }
}
