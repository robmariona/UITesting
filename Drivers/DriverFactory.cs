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
                    firefoxOptions.AddArgument("--headless=new");

                }
                firefoxOptions.AddArgument("--start-maximized");
                options = firefoxOptions;
                break;

            case "edge":
                var edgeoptions = new EdgeOptions();
                if (headless)
                {
                    edgeoptions.AddArgument("--headless=new");
                    edgeoptions.AddArgument("--window-size=1920,1080");
                }
                edgeoptions.AddArgument("--ignore-certificate-errors");
                edgeoptions.AddArgument("--allow-insecure-localhost");

                options = edgeoptions;
                break;

            case "chrome":
            default:
                var chromeOptions = new ChromeOptions();
                if (headless)
                {
                    chromeOptions.AddArgument("--headless=new");
                    chromeOptions.AddArgument("--window-size=1920,1080");
                }
                chromeOptions.AddArgument("--incognito");
                chromeOptions.AddArgument("--ignore-certificate-errors");
                chromeOptions.AddArgument("--allow-running-insecure-content");
                options = chromeOptions;
                break;
        }


        if (!string.IsNullOrEmpty(gridUrl))
        {
            return new RemoteWebDriver(new Uri(gridUrl), options.ToCapabilities());
        }

        // 3. Return the specific Local Driver
        return options switch
        {
            EdgeOptions e => new EdgeDriver(e),
            FirefoxOptions f => new FirefoxDriver(f),
            _ => new ChromeDriver((ChromeOptions)options)
        };
    }

}