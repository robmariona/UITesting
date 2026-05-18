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
            var ClaimtData = new ClaimModel
            {
                Description = "Had an accicdent 2",
            };
            ClaimPage claimFormPage = _claim.ClickFileAClaim();

            _claim.SubmitClaim(ClaimtData);
            // Option B: Alternatively, look for a success alert banner if your app has one:
            By successAlert = By.XPath("//div[contains(text(), 'Claim submitted successfully!')]");
            Driver.WaitAndFind(successAlert, 10);
        

        }

    }
}
