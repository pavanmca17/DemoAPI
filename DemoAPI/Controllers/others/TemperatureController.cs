using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers
{
   
    [ApiController]
    public class TemperatureController : ControllerBase
    {
        static int _counter = 0;
        static readonly Random randomTemperature = new Random();

        [Route("api/temperature/{locationId}")]
        public ActionResult Get(int locationId)
        {
            _counter++;

            if (_counter % 4 == 0) // only one out of four requests will succeed
            {
                return Ok(randomTemperature.Next(0, 120));
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, "Something went wrong when getting the temperature.");
        }
    }
}