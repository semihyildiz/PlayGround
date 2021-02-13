using BigDataTechnoloy.Business;
using BigDataTechnoloy.Business.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigDataTechnology.WebApi.Middleware
{
    public class RequestResponseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IChatHubDispatcher _chatHubDispatcher;
        public RequestResponseMiddleware(RequestDelegate next, IChatHubDispatcher chatHubDispatcher)
        {
            _next = next;
            _chatHubDispatcher = chatHubDispatcher;
        }
        public async Task Invoke(HttpContext context)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var request = await FormatRequest(context.Request);
            var originalBodyStream = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;
                await _next(context);

                var response = await FormatResponse(context.Response);
                await responseBody.CopyToAsync(originalBodyStream);

                if (response.Contains("\"id\":0"))
                {/*new query*/
                    stopwatch.Stop();
                    //Task.Factory.StartNew(() =>
                    //{
                        string Message = "New Request:" + context.Request.QueryString + ", Duration:" + stopwatch.ElapsedMilliseconds.ToString() + " miliseconds.";

                        _chatHubDispatcher.SendAllClients(DateTime.Now.ToString("dd:MM:yyyy HH:MM:ss:fff") + Message, response);
                    //});

                }
                Task.Factory.StartNew(() =>
                {
                    InMemory.Flush();
                });

            }

        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            var body = request.Body;

            request.EnableBuffering();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];

            await request.Body.ReadAsync(buffer, 0, buffer.Length);

            var bodyAsText = Encoding.UTF8.GetString(buffer);

            request.Body = body;

            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);

            string text = await new StreamReader(response.Body).ReadToEndAsync();

            response.Body.Seek(0, SeekOrigin.Begin);

            return text;
        }
    }
}