using DemoAPI.Impl;
using DemoAPI.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Versioning;
using DemoAPI.Helpers;
using EF;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using HttpClientDelegatingHandler;
using DemoAPI.Providers;
using Microsoft.AspNetCore.Mvc.Formatters;
using DemoAPI.Logging;

namespace DemoAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
          
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add<OperationCancelledExceptionFilter>();
                options.Filters.Add<LoggingActionFilter>();
                options.RespectBrowserAcceptHeader = true; // false by default
                options.OutputFormatters.Add(new XmlSerializerOutputFormatter());

            })
             .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
             .ConfigureApiBehaviorOptions(options =>    
             {
                 options.InvalidModelStateResponseFactory = context =>
                 {
                     var Issues = new CustomBadRequest(context);

                     return new BadRequestObjectResult(Issues);
                 };
             });  
            // api versioning
            services.AddApiVersioning(o => o.ApiVersionReader = new HeaderApiVersionReader("api-version"));  

            services.AddHttpClient(Configuration);

            services.AddPollyServices();

            services.ConfigureValues(Configuration);

            services.AddDBContext<ProductContext>(Configuration);

            services.AddTransient<TimingHandler>();
            services.AddTransient<ValidateHeaderHandler>();

            services.AddTransient<INoteRepository, NoteRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ICustomterRepository, CustomerRepository>();

            services.AddCors(policy =>
            {
                policy.AddPolicy("CorsPolicy", options => options.AllowAnyHeader()
                                                                 .AllowAnyMethod()
                                                                 .AllowAnyOrigin());
            });

            services.AddLogging(configure => configure.AddConsole());
            services.AddSingleton<LoggingActionFilter>();

        }

       // This method gets called by the runtime.Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDistributedCache distributedCache)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if(env.IsStaging())
            {

            }

            if(env.IsProduction())
            {

            }

            //string appStartTimeKey = "app-Last-Start-Time";
            //var serverStartTimeString = DateTime.Now.ToString();
            //byte[] val = Encoding.UTF8.GetBytes(serverStartTimeString);
            //distributedCache.SetAsync(appStartTimeKey, val);
            //app.ConfigureApplicationStartTimeHeaderMiddleWare();
            app.ConfigureCorealtionIDMiddleWare();
            //app.ConfigureRequestResponseLoggingMiddleware();
            //app.ConfigureCustomExceptionMiddleware();
         
            app.UseCors("CorsPolicy");
            app.UseMvc();
           


        }
    }
}
