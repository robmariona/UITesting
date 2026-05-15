using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using UITesting.Base;

namespace UITesting.Tests
{
    public class BrowserTest : TestBase
    {

        [Fact]
        public void TestOnChrome()
        {
            // You can even put "firefox" in a config file later!
            using IWebDriver driver = DriverFactory.GetDriver("chrome");

            driver.Navigate().GoToUrl($"{BaseUrl}/dashboard");
            // ... your test steps
        }

    }
}
