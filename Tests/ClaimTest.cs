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
        [Trait("Category", "Regression")]
        public void FileAclaim()
        {
            // 1. Clean data model
            var claimData = new ClaimModel
            {
                Description = "Had an accident 333333",
            };

            // 2. Act: Navigate via the proper page instance context
            ClaimPage claimFormPage = _claim.ClickFileAClaim();

            // 3. Complete form submission
            claimFormPage.SubmitClaim(claimData);

            // 4. Robust wait using normalized text strategy
            By successAlert = By.XPath("//div[contains(normalize-space(.), 'Claim submitted successfully!')]");
            Driver.WaitAndFind(successAlert, 15); // Bumped to 15s to account for Render cold-starts
        }

    }
}
