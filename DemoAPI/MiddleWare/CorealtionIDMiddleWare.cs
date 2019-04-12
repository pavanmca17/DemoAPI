using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DemoAPI.MiddleWare
{
    public class CorealtionIDMiddleWare
    {
        private readonly RequestDelegate _next;

        public CorealtionIDMiddleWare(RequestDelegate requestDelegate)
        {
            _next = requestDelegate;
        }

        //constructor and service injection
        public async Task Invoke(HttpContext httpContext)
        {
           await LogConfigurationID(httpContext);
           await _next(httpContext);
        }

        private static Task LogConfigurationID(HttpContext context)
        {
             if (context.Request.Headers.TryGetValue("CorealtionID", out StringValues correlationId))
             {    context.TraceIdentifier = correlationId;
             }
             
            // apply the correlation ID to the response header for client side tracking
            context.Response.OnStarting(() =>
            {
                   context.Response.Headers.Add(_options.Header, new[] { context.TraceIdentifier });
                   return Task.CompletedTask;
            });

            return _next(context);
       }

    }
}
