using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Commander.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Commander.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        // RequestDelegate is a function that can process an HTTP request
        // Now if there is no exception we want the middleware to move on to the next piece of middleware
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // this means if there is no exception then the request moves on to its next stage
                await _next(context);
            }
            // Now if there is an exception then we want to catch it.
            catch(Exception ex)
            {
                // log the error to the console
                _logger.LogError(ex, ex.Message);
                //  write our own response into the context response so that we can send it to the client
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                // if we are in development return status code with exception message and stacktrace, else return only the status code
                var response = _env.IsDevelopment()
                    ? new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    : new ApiException((int)HttpStatusCode.InternalServerError);

                // modifiy json response to use camel case notation
                var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

                // convert response into json format
                var json = JsonSerializer.Serialize(response, options);

                //  writes the given text to the response body
                await context.Response.WriteAsync(json);
            }
        }
    }
}