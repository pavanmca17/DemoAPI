using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DemoAPI.Interface
{
    public interface IExternalService
    {
        Task<String> GetExternalServiceData(string url);
    }
}
