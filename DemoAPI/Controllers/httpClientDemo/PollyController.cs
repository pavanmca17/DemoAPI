﻿using System.Net.Http;
using System.Threading.Tasks;
using DemoAPI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Polly;


namespace DemoAPI.Controllers.httpClientDemo
{
    [ApiController]
    [ApiVersion("2.0")]
    public class PollyController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAsyncPolicy<HttpResponseMessage> _retryPolicy;
        public PollyController(IHttpClientFactory httpClientFactory, IAsyncPolicy<HttpResponseMessage> retryPolicy)
        {
            _httpClientFactory = httpClientFactory;
            _retryPolicy = retryPolicy;          
        }

        [HttpGet()]
        [Route("/polly/retry/{locationId}")]
        public async Task<ActionResult> GetTemperature(int locationId)
        {
            var httpclient = _httpClientFactory.CreateClient(NamedHttpClients.pollyHttpClient);            
            
            HttpResponseMessage httpResponseMessage = await _retryPolicy.ExecuteAsync(() =>
            httpclient.GetAsync($"api/temperature/{locationId}"));          

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string temperature = await httpResponseMessage.Content.ReadAsStringAsync();
                return Ok(temperature);
            }

            return StatusCode((int)httpResponseMessage.StatusCode, "The temperature service returned an error.");
        }
        
    }
}