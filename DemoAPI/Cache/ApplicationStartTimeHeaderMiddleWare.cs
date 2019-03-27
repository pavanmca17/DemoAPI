using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoAPI.Cache
{
    public class ApplicationStartTimeHeaderMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly IDistributedCache _distrubedCache;

        public ApplicationStartTimeHeaderMiddleWare(RequestDelegate next,IDistributedCache distributedCache)
        {
            _next = next;
            _distrubedCache = distributedCache;
        }

        public async Task Invoke(HttpContext httpContext)
          {
            string appStartTimeKey = "app-Last-Start-Time";
            string appStartTimeValue = "Not found.";
            var cachedvalue = await _distrubedCache.GetAsync(appStartTimeKey);
            if (cachedvalue != null)
            {
                appStartTimeValue = Encoding.UTF8.GetString(cachedvalue);
            }
            else
            {
                appStartTimeValue = DateTime.Now.ToString();
                byte[] val = Encoding.UTF8.GetBytes(appStartTimeValue);
                _distrubedCache.Set(appStartTimeKey, val);
            }


            httpContext.Response.Headers.Append(appStartTimeKey, appStartTimeValue);

            await _next.Invoke(httpContext);
        }

    }
}
