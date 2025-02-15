using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests.Utils
{
    internal class WebDriverFactory
    {
        private readonly TestConfiguration _config;

        public WebDriverFactory(TestConfiguration config)
        {
            _config = config;
        }

        public IWebDriver CreateDriver()
        {
            IWebDriver driver = _config.Browser.ToLower() switch
            {
                "chrome" => CreateChromeDriver(),
                "firefox" => CreateFirefoxDriver(),
                "edge" => CreateEdgeDriver(),
                _ => throw new ArgumentException($"Navegador no soportado: {_config.Browser}")
            };

            // Configuración común para todos los drivers
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(_config.TimeoutSeconds);

            return driver;
        }

        private ChromeDriver CreateChromeDriver()
        {
            var options = new ChromeOptions();

            if (_config.Headless)
                options.AddArgument("--headless");

            // Configuraciones adicionales de Chrome
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-notifications");
            options.AddArgument("--disable-popup-blocking");

            // Configurar directorio de descargas
            if (!string.IsNullOrEmpty(_config.DownloadPath))
            {
                options.AddUserProfilePreference("download.default_directory", _config.DownloadPath);
                options.AddUserProfilePreference("download.prompt_for_download", false);
            }

            return new ChromeDriver(options);
        }

        private FirefoxDriver CreateFirefoxDriver()
        {
            var options = new FirefoxOptions();
            if (_config.Headless)
                options.AddArgument("--headless");

            return new FirefoxDriver(options);
        }

        private EdgeDriver CreateEdgeDriver()
        {
            var options = new EdgeOptions();
            if (_config.Headless)
                options.AddArgument("--headless");

            return new EdgeDriver(options);
        }
    }

}
