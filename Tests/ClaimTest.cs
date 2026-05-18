using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UITesting.Base;
using UITesting.Models;
using UITesting.Pages;

namespace UITesting.Tests
{
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
                Description = "Had an accicdent",
            };
            _claim.ClickFileAClaim();

        }

    }
}
