using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using UITesting.Base;
using UITesting.Extensions;
using UITesting.Models;
using UITesting.Pages;

namespace UITesting.Tests
{

    [Collection("Sequential Tests")]
    public class ClaimTest : TestBase
    {
        private readonly ClaimPage _claim;

        public ClaimTest()
        {
            _claim = new ClaimPage(Driver);
        }

        [Fact]
        public void FileAclaim()
        {
            // 1. Arrange
            var claimData = new ClaimModel
            {
                Description = "Had an accident 2",
            };

            // 2. Act: Navigate to the claim view page 
            ClaimPage claimFormPage = _claim.ClickFileAClaim();

            // 3. Complete the form submit using the captured page object instance
            claimFormPage.SubmitClaim(claimData);

            // 4. Assert: Look for the success element using the normalized text validator
            By successAlert = By.XPath("//div[contains(normalize-space(.), 'Claim submitted successfully!')]");
            Driver.WaitAndFind(successAlert, 10);
        }

    }
}
