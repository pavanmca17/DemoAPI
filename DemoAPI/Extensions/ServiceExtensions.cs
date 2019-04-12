using DemoAPI.Cache;
using DemoAPI.Helpers;
using DemoAPI.Impl;
using DemoAPI.Interface;
using DemoAPI.MiddleWare;
using DemoAPI.Models;
using EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;


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
        
        public static void ConfigureApplicationStartTimeHeaderMiddleWare(this IApplicationBuilder app)
        {
            app.UseMiddleware<CorealtionIDMiddleWare>();
        }  
        


    }
}
