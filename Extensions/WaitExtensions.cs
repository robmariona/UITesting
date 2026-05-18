using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace UITesting.Extensions
{
    public static class WebDriverExtensions
    {


        public static IWebElement WaitAndFind(this IWebDriver driver, By locator, int timeoutSeconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds))
            {
                // Principal Tip: Ignore "StaleElement" exceptions during the poll
                PollingInterval = TimeSpan.FromMilliseconds(500)
            };
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(NoSuchElementException));

            return wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        public static IWebElement WaitAndClick(this IWebDriver driver, By locator, int timeoutSeconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
            var element = wait.Until(ExpectedConditions.ElementToBeClickable(locator));
            element.Click();
            return element;
        }
        public static void WaitAndType(this IWebDriver driver, By locator, string text, bool clearFirst = true)
        {
            var element = driver.WaitAndFind(locator);
            if (clearFirst) element.Clear();
            element.SendKeys(text);
        }
        public static void WaitAndSelectByText(this IWebDriver driver, By locator, string text, int timeoutSeconds = 10)
        {
            var element = driver.WaitAndFind(locator, timeoutSeconds);
            var select = new SelectElement(element);
            select.SelectByText(text);
        }

        public static void WaitAndSelectByValue(this IWebDriver driver, By locator, string value, int timeoutSeconds = 10)
        {
            var element = driver.WaitAndFind(locator, timeoutSeconds);
            var select = new SelectElement(element);
            select.SelectByValue(value);
        }
        // Add this new method right below the others!
        public static void WaitAndSelectByPartialText(this IWebDriver driver, By locator, string partialText, int timeoutSeconds = 10)
        {
            var element = driver.WaitAndFind(locator, timeoutSeconds);
            var select = new SelectElement(element);

            // SENIOR QA TRICK: Wait up to 5 seconds for the options count to be greater than 1
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            try
            {
                wait.Until(d => select.Options.Count > 1);
            }
            catch (WebDriverTimeoutException)
            {
                // Log or proceed if it's meant to have only 1 option
            }

            // Scan for the target text (using lowercase match for safety)
            var targetOption = select.Options.FirstOrDefault(o =>
                (o.Text ?? "").ToLower().Contains(partialText.ToLower()));

            if (targetOption != null)
            {
                select.SelectByText(targetOption.Text);
            }
            else
            {
                // This will print out ALL available options in your error log so you can see exactly what's wrong!
                string availableOptions = string.Join(", ", select.Options.Select(o => $"'{o.Text}'"));
                throw new NoSuchElementException($"Could not find option containing: '{partialText}'. Available options are: [{availableOptions}]");
            }
        }
        public static void WaitUntilUrlIsStable(this IWebDriver driver, string expectedUrlPart, int timeoutSeconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
            // This blocks the next command until the browser reaches the correct URL
            wait.Until(ExpectedConditions.UrlContains(expectedUrlPart));



        }
    }
}