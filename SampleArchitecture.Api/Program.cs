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
                // Ortamýmýzý belirleyen Evniroment deðiþkenimizi okuyoruz. Buna göre çalýþýlacak ortam aylarýný ilgili json'dan okuyoruz.
                string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                return new ConfigurationBuilder()
                    .SetBasePath(System.IO.Directory.GetCurrentDirectory()) // Bu uygulamanýn ayarlarýnýn yükleneceði klasör, bu uygulamanýn içerisi.
                    .AddJsonFile("appsettings.json", optional: false) // Eðer hiçbir þey bulamazsa bunu kullan demek "optional:false"
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
                    webBuilder.UseConfiguration(Configuration); // Uygulamamýz çalýþtýðýnda gelip yukarýdaki ayarlarýmýzý okumasý için ekledik.

                });
    }
}
