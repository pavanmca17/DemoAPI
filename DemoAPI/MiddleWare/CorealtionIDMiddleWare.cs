using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.Logging;

namespace DemoAPI.MiddleWare
{
    public class CorealtionIDMiddleWare
    {
        private readonly RequestDelegate _next;
        private ILogger _logger;

        public CorealtionIDMiddleWare(RequestDelegate requestDelegate, ILoggerFactory loggerFactory)
        {
            _next = requestDelegate;
            _logger = loggerFactory.CreateLogger<CorealtionIDMiddleWare>();
        }

        //constructor and service injection
        public async Task Invoke(HttpContext httpContext)
        {
           _logger.LogInformation("Starting of MiddleWare");
           await LogConfigurationID(httpContext);          
           await _next(httpContext);
           _logger.LogInformation("Ended MiddleWare");
        }

        private Task LogConfigurationID(HttpContext context)
        {
             if (context.Request.Headers.TryGetValue("CorealtionID", out StringValues correlationId))
             {  context.TraceIdentifier = correlationId;
             }             
             
            // apply the correlation ID to the response header for client side tracking
            context.Response.OnStarting(() =>
            {
                _logger.LogInformation("Adding GUID");
                context.Response.Headers.Add("corelationheader", Guid.NewGuid().ToString());
                return Task.CompletedTask;
            });

            return _next(context);
       }

    }
}
