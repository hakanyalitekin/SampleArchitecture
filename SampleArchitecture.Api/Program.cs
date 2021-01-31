using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleArchitecture.Api
{
    public class Program
    {
        private static IConfiguration Configuration
        {
            get
            {
                // Ortam�m�z� belirleyen Evniroment de�i�kenimizi okuyoruz. Buna g�re �al���lacak ortam aylar�n� ilgili json'dan okuyoruz.
                string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                return new ConfigurationBuilder()
                    .SetBasePath(System.IO.Directory.GetCurrentDirectory()) // Bu uygulaman�n ayarlar�n�n y�klenece�i klas�r, bu uygulaman�n i�erisi.
                    .AddJsonFile("appsettings.json", optional: false) // E�er hi�bir �ey bulamazsa bunu kullan demek "optional:false"
                    .AddJsonFile($"appsettings.{env}.json", optional: true)
                    .AddEnvironmentVariables()
                    .Build();
            }
        }

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseConfiguration(Configuration); // Uygulamam�z �al��t���nda gelip yukar�daki ayarlar�m�z� okumas� i�in ekledik.

                });
    }
}
