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
    public class FileAClaimTest : TestBase
    {
        private readonly FileAClaimPage _claim;

        public FileAClaimTest()
        {
            _claim = new FileAClaimPage(Driver);
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void FileAclaim()
        {
            // 1. Clean data model
            var claimData = new FileAClaimModel
            {
                Description = "Had an accident 333333",
            };

            // 2. Act: Navigate via the proper page instance context
            FileAClaimPage claimFormPage = _claim.ClickFileAClaim();

            // 3. Complete form submission
            claimFormPage.SubmitClaim(claimData);

            // 4. Robust wait using normalized text strategy
            By successAlert = By.XPath("//div[contains(normalize-space(.), 'Claim submitted successfully!')]");
            Driver.WaitAndFind(successAlert, 15); // Bumped to 15s to account for Render cold-starts
        }

    }
}
