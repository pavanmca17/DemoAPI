using System;
using System.Net.Http;
using System.Threading.Tasks;
using DemoAPI.Helpers;
using DemoAPI.Interface;
using Microsoft.AspNetCore.Mvc;


namespace DemoAPI.Controllers.httpClientDemo
{
    
    [ApiController]
    [ApiVersion("2.0")]   
    public class HttpClientProxy : Controller
    {
       
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IExternalService _externalService;       

        public HttpClientProxy(IHttpClientFactory httpClientFactory, IExternalService externalService)
        {
            _httpClientFactory = httpClientFactory;
            _externalService = externalService;            
        }

        [Route("/normalhttpclient/{url}")]
        public async Task<ActionResult<String>> GetDatafromExternalApi(string url)
        {
            var httpclient = _httpClientFactory.CreateClient();
            string content = await httpclient.GetStringAsync(url);
            return Ok(content);
           
        }

        [Route("/namedhttpclient/{url}")]
        public async Task<ActionResult<String>> GetDataFromWebWithNamedHttpClient(string url)
        {
            var httpclient = _httpClientFactory.CreateClient(NamedHttpClients.namedHttpClient);
            httpclient.BaseAddress = new Uri(url);
            string content = await httpclient.GetStringAsync("/");
            return Ok(content);

        }

        [Route("/injectedhttpclient/{url}")]
        public async Task<ActionResult<String>> GetDataFromWebwithInjectedClient(string url)
        {
            string content = await _externalService.GetExternalServiceData();
            return Ok(content);

        }

       
    }
}
