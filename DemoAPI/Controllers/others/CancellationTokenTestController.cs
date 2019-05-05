using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DemoAPI.Interface;
using DemoAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace DemoAPI.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    public class CancellationTokenTestController : ControllerBase
    {         
        [HttpGet("api/cancellation/{waittime}")]
        public async Task<string> Get(CancellationToken cancellationToken,int waittime)
        {
            await Task.Delay(waittime, cancellationToken);          

            for (var i = 0; i < waittime; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                Thread.Sleep(1000);
            }           

            return $"WebApi call was not cancelled - Congrats";
        }
    }
}
