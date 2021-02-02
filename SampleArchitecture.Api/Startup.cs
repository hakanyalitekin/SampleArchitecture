using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SampleArchitecture.Api.Extensions;
using SampleArchitecture.Api.Models;
using SampleArchitecture.Api.Service;
using SampleArchitecture.Api.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleArchitecture.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddHttpClient("garantiApi", config =>
            {
                config.BaseAddress = new Uri("https://garantipi.com/v1");
                config.DefaultRequestHeaders.Add("Authorization", "Bearer 1231313113");
            });


            services.AddControllers()
                .AddFluentValidation(i => i.RunDefaultMvcValidationAfterFluentValidationExecutes = false);// RunDefaultMvcValidationAfterFluentValidationExecutes zorunlu deðil öncelik sýrasýný belirliyor.Adýndandan da anlaþýyor maksadý.

            services.ConfigureMapping(); //MappingConfigureExtension içerisinde barýnýyor.

            services.AddHealthChecks();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SampleArchitecture.Api", Version = "v1" });
            });

            services.AddScoped<IContactService, ContactService>();
            services.AddTransient<IValidator<ContactDTO>, ContactValidator>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SampleArchitecture.Api v1"));
            }

            app.UseCustomHealthCheck(); //HealthCheckConfigureExtension içerisinde barýnýyor.

            app.UseResponseCaching(); //ResponseCaching için hazýr paket.

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
