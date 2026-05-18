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
            // 1. Reuse your existing wait logic to find the dropdown
            var element = driver.WaitAndFind(locator, timeoutSeconds);
            var select = new SelectElement(element);

            // 2. Scan the available options to find the first one containing your text fragment
            var targetOption = select.Options.FirstOrDefault(o => o.Text.Contains(partialText));

            if (targetOption != null)
            {
                // 3. Select it by its full text matching what we found
                select.SelectByText(targetOption.Text);
            }
            else
            {
                // Fail clearly if the option isn't there (great for QA debugging!)
                throw new NoSuchElementException($"Could not find any option containing text: '{partialText}' in the dropdown.");
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