using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArchitecture.Api.Middlewares
{

    //Kaynak --> https://www.it-swarm.asia/tr/c%23/asp.net-core-response.body-nasil-okunur/830666697/
    //Kaynak --> https://exceptionnotfound.net/using-middleware-to-log-requests-and-responses-in-asp-net-core/
    //Kaynak --> https://www.youtube.com/watch?v=K0DWvR_p6Ek&list=PLRp4oRsit1bwwNs65YnxQGeuLxGMF76Tc&index=7

    public class RequestResponseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseMiddleware> _logger;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        public RequestResponseMiddleware(RequestDelegate Next, ILogger<RequestResponseMiddleware> Logger)
        {
            _next = Next;
            _logger = Logger;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task Invoke(HttpContext httpContext)
        {
            // Request
            String requestText = await GetRequestBody(httpContext);


            // Response Part1

            var originalBodyStream = httpContext.Response.Body;

            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            httpContext.Response.Body = responseBody;

            await _next.Invoke(httpContext); // Response Bu satırda oluşuyor


            // Response Part2

            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            String responseText = await new StreamReader(httpContext.Response.Body, Encoding.UTF8).ReadToEndAsync();
            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);

            await responseBody.CopyToAsync(originalBodyStream);

            _logger.LogInformation($"İstek: {requestText}");
            _logger.LogInformation($"Cevap: {responseText}");
        }



        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;

            stream.Seek(0, SeekOrigin.Begin);

            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);

            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;

            do
            {
                readChunkLength = reader.ReadBlock(readChunk,
                                                   0,
                                                   readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);

            } while (readChunkLength > 0);

            return textWriter.ToString();
        }

        private async Task<String> GetRequestBody(HttpContext context)
        {
            context.Request.EnableBuffering();

            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);

            String reqBody = ReadStreamInChunks(requestStream);

            context.Request.Body.Position = 0;

            return reqBody;
        }


    }
}
