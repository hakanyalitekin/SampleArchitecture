using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SampleArchitecture.Api.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        IWebHostEnvironment _webHostEnvironment;

        public ExceptionHandlerMiddleware(RequestDelegate next, IWebHostEnvironment webHostEnvironment)
        {
            _next = next;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);

            }
            catch (Exception e)
            {
                Log(httpContext, e);

                await Task.FromException(e);
            }
        }

        private void Log(HttpContext context, Exception exception)
        {
            var savePath = _webHostEnvironment.ContentRootPath;
            var now = DateTime.UtcNow;
            var fileName = $"{now.ToString("yyyy_MM_dd")}.txt";
            var filePath = Path.Combine(savePath, "logs", fileName);

            // ensure that directory exists
            new FileInfo(filePath).Directory.Create();

            using (var writer = File.CreateText(filePath))
            {
                writer.WriteLine($"{now.ToString("HH:mm:ss")} {context.Request.Path}");
                writer.WriteLine(exception.Message);
            }
        }
    }
}
