using DemoAPI.Interface;
using DemoAPI.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DemoAPI.Impl
{
    public class ExternalService : IExternalService
    {
        private readonly HttpClient _httpClient;

        public ExternalService(HttpClient httpClient,IOptions<Settings> settings)
        {
            _httpClient = httpClient;
            _httpClient.Timeout = TimeSpan.FromSeconds(settings.Value.httpClientValues.TimeOut);
            _httpClient.BaseAddress = new Uri(settings.Value.httpClientValues.BaseAddress); 
        }

        public async Task<string> GetExternalServiceData(string url)
        {
            return await _httpClient.GetStringAsync(url);
        }
    }
}
