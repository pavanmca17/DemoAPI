using System;
using System.Net.Http;
using System.Threading.Tasks;
using DemoAPI.Helpers;
using DemoAPI.Interface;
using DemoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DemoAPI.Controllers.httpClientDemo
{
    
    [ApiController]
    [ApiVersion("2.0")]   
    public class HttpClientProxy : Controller
    {
       
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IExternalService _externalService;
        private readonly IOptions<Settings> _settings;

        public HttpClientProxy(IHttpClientFactory httpClientFactory, IExternalService externalService, IOptions<Settings> settings)
        {
            _httpClientFactory = httpClientFactory;
            _externalService = externalService;
            _settings = settings;
        }

        [Route("/normalhttpclient/{url}")]
        public async Task<ActionResult<String>> GetDatafromExternalApi(string url)
        {
            var httpclient = _httpClientFactory.CreateClient();
            httpclient.BaseAddress = new Uri(_settings.Value.httpClientValues.BaseAddress);
            string content = await httpclient.GetStringAsync("api/" + url);
            return Ok(content);
           
        }

        [Route("/namedhttpclient/{url}")]
        public async Task<ActionResult<String>> GetDataFromWebWithNamedHttpClient(string url)
        {
            var httpclient = _httpClientFactory.CreateClient(NamedHttpClients.namedHttpClient);
            httpclient.BaseAddress = new Uri(_settings.Value.httpClientValues.BaseAddress);
            string content = await httpclient.GetStringAsync("api/" + url);
            return Ok(content);

        }

        [Route("/injectedhttpclient/{url}")]
        public async Task<ActionResult<String>> GetDataFromWebwithInjectedClient(string url)
        {
            string content = await _externalService.GetExternalServiceData("api/" + url);
            return Ok(content);

        }

       
    }
}
