using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;

public static class DriverFactory
{
    public static IWebDriver GetDriver(string browser, bool headless = false, string gridUrl = "")
    {
        DriverOptions options = null!;

        switch (browser.ToLower())
        {
            case "firefox":
                var firefoxOptions = new FirefoxOptions();
                if (headless)
                {
                    firefoxOptions.AddArgument("-headless"); // Note: Firefox uses -headless
                }
                firefoxOptions.AddArgument("--width=1920");
                firefoxOptions.AddArgument("--height=1080");
                options = firefoxOptions;
                break;

            case "edge":
                var edgeOptions = new EdgeOptions();
                if (headless)
                {
                    edgeOptions.AddArgument("--headless=new");
                }
                // SENIOR QA TRICK: Apply window size uniformly
                edgeOptions.AddArgument("--window-size=1920,1080");
                edgeOptions.AddArgument("--start-maximized");
                edgeOptions.AddArgument("--ignore-certificate-errors");
                edgeOptions.AddArgument("--allow-insecure-localhost");
                options = edgeOptions;
                break;

            case "chrome":
            default:
                var chromeOptions = new ChromeOptions();
                if (headless)
                {
                    chromeOptions.AddArgument("--headless=new");
                    chromeOptions.AddArgument("--disable-gpu"); // Recommended for headless stability in Linux CI
                }
                // Move these OUTSIDE the headless check so local and CI are identical layout-wise
                chromeOptions.AddArgument("--window-size=1920,1080");
                chromeOptions.AddArgument("--start-maximized");
                chromeOptions.AddArgument("--incognito");
                chromeOptions.AddArgument("--ignore-certificate-errors");
                chromeOptions.AddArgument("--allow-running-insecure-content");
                options = chromeOptions;
                break;
        }

        IWebDriver driver;

        if (!string.IsNullOrEmpty(gridUrl))
        {
            driver = new RemoteWebDriver(new Uri(gridUrl), options.ToCapabilities());
        }
        else
        {
            driver = options switch
            {
                EdgeOptions e => new EdgeDriver(e),
                FirefoxOptions f => new FirefoxDriver(f),
                _ => new ChromeDriver((ChromeOptions)options)
            };
        }

        // THE ULTIMATE DEFENSE: Explicitly command the active window context to maximize
        // This forces headless Linux browsers to expand completely to the given arguments.
        try
        {
            driver.Manage().Window.Size = new System.Drawing.Size(1920, 1080);
        }
        catch (NotImplementedException)
        {
            // Some specific cloud grid configurations don't support direct manipulation APIs
        }

        return driver;
    }
}