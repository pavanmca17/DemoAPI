using DemoAPI.Cache;
using DemoAPI.Helpers;
using DemoAPI.Impl;
using DemoAPI.Interface;
using DemoAPI.MiddleWare;
using DemoAPI.Models;
using EF;
using HttpClientDelegatingHandler;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.CircuitBreaker;
using System;
using System.Net.Http;


namespace DemoAPI
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });
        }

       public static void ConfigureValues(this IServiceCollection services, IConfiguration Configuration)
        {
            services.Configure<Settings>(options =>
            {
                options.MongoDBConnectionString = Configuration.GetSection("DBConnections:MongoConnectionString").Value;
                options.SqlServerConnectionString = Configuration.GetSection("DBConnections:SqlServerConnectionString").Value;
                options.NotesDatabase = Configuration.GetSection("DBConnections:NoteDataBase").Value;
                options.EmployeeDatabase = Configuration.GetSection("DBConnections:EmployeeDatabase").Value;
                options.Env = Configuration.GetSection("Enviroment:Value").Value;
                options.httpClientValues = new HttpClientValues();
                options.httpClientValues.TimeOut = Convert.ToInt32(Configuration.GetSection("HttpClient:Timeout").Value);
                options.httpClientValues.BaseAddress = Configuration.GetSection("HttpClient:BaseAddress").Value;
            });
        }

        public static void AddHttpClient(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddHttpClient();

            services.AddHttpClient(NamedHttpClients.namedHttpClient, client =>
            {
                client.Timeout = TimeSpan.FromSeconds(Convert.ToInt32(Configuration.GetSection("HttpClient:Timeout").Value));
            });

            services.AddHttpClient<IExternalService, ExternalService>(client =>
            {
                client.Timeout = TimeSpan.FromSeconds(Convert.ToInt32(Configuration.GetSection("HttpClient:Timeout").Value));

            });           
  
        }

        public static void AddPollyServices(this IServiceCollection services)
        {
            IAsyncPolicy<HttpResponseMessage> retryPolicy =
            Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
           .RetryAsync(3);              


            services.AddSingleton<IAsyncPolicy<HttpResponseMessage>>(retryPolicy);            

            services.AddHttpClient(NamedHttpClients.pollyHttpClient, client => {

                client.BaseAddress = new Uri("http://localhost:50282");
                client.DefaultRequestHeaders.Add("api-version", "1.0");
            })
            .AddHttpMessageHandler<TimingHandler>() // This handler is on the outside and executes first on the way out and last on the way in.
            .AddHttpMessageHandler<ValidateHeaderHandler>(); // This handler is on the inside, closest to the request.

        }

        public static void AddDBContext<T>(this IServiceCollection services, IConfiguration Configuration) where T : DbContext
        {
            services.AddDbContext<T>(options =>
            {
                options.UseSqlServer(Configuration.GetSection("DBConnections:SqlServerConnectionString").Value,
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 10,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
                });
            });
        }

        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionMiddleware>();
        }

        public static void ConfigureRequestResponseLoggingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<RequestResponseLoggingMiddleware>();
        }

        public static void ConfigureApplicationStartTimeHeaderMiddleWare(this IApplicationBuilder app)
        {
            app.UseMiddleware<ApplicationStartTimeHeaderMiddleWare>();
        }
        
        public static void ConfigureCorealtionIDMiddleWare(this IApplicationBuilder app)
        {
            app.UseMiddleware<CorealtionIDMiddleWare>();
        }        


    }
}
