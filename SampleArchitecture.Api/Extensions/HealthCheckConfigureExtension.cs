using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace SampleArchitecture.Api.Extensions
{
    public static class HealthCheckConfigureExtension
    {
        //Startup.cs bizden IApplicationBuilder bekleyecektir ondan sebep IApplicationBuilder dönüyoruz.
        public static IApplicationBuilder UseCustomHealthCheck(this IApplicationBuilder app) 
        {
            //HealthCheck Özelleştirilmiş Kullanım (-https://link.medium.com/8oFBVeiJudb)
            app.UseHealthChecks("/api/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
            {
                ResponseWriter = async (context, report) =>
                {
                    await context.Response.WriteAsync("OK");
                }
            });
             
            //HealthCheck Base Kullanım 
            //app.UseHealthChecks("/hc");

            return app; //kendisini direk geriye döndürüyoruz.
        }
    }
}
