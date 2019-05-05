using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAPI.Logging
{
    public class LoggingActionFilter : IAsyncActionFilter
    {
        private ILogger _logger;

        public LoggingActionFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<LoggingActionFilter>();
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation($"Action '{context.ActionDescriptor.DisplayName}' executing");
            var resultContext = await next();
            _logger.LogInformation($"Action '{context.ActionDescriptor.DisplayName}' executed");
        }

       
    }
}
