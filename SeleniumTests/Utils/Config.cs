using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests.Utils
{
    internal class Config
    {
        private readonly IConfiguration _configuration;
        public Config ()
        {
            _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json", true)
            .Build();
        }
        // Propiedades para acceder a la configuración
        public string BaseUrl => _configuration["TestSettings:BaseUrl"];
        public string Browser => _configuration["TestSettings:Browser"];
        public int TimeoutSeconds => int.Parse(_configuration["TestSettings:TimeoutSeconds"]);
        public string Username => _configuration["TestSettings:DefaultUser:Username"];
        public string Password => _configuration["TestSettings:DefaultUser:Password"];

        // Configuraciones adicionales que puedas necesitar
        public bool Headless => bool.Parse(_configuration["TestSettings:Headless"]);
        public string DownloadPath => _configuration["TestSettings:DownloadPath"];
        public string ScreenshotPath => _configuration["TestSettings:ScreenshotPath"];
    }
}
