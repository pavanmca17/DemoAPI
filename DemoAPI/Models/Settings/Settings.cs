using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAPI.Models
{
    public class Settings
    {
        public string MongoDBConnectionString;
        public string SqlServerConnectionString;
        public string NotesDatabase;
        public string EmployeeDatabase;
        public string Env;        
        public HttpClientValues httpClientValues;

    }

    public class HttpClientValues
    {
        public int TimeOut;
        public string BaseAddress;
    }

    public class RedisCacheSettings
    {
        public string  RedisCacheName;
        public string Connectingstring;

    }
}
