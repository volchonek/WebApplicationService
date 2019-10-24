using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RESTfulAPIService.Implementations;
using RESTfulAPIService.Interfaces;
using Serilog;
using System.Globalization;
using System.IO;
using System.Reflection;
using RESTfulAPIService.DbContext;

namespace RESTfulAPIService
{
    /// <summary>
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            //Change Level to Debug or smth... 
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.With(new UtcTimeStampEnricher())
                .WriteTo.Console(
                    outputTemplate:
                    "{utctimestamp:yyyy-mm-dd hh:mm:ss.fff} [{level}] {callermembername} {message:lj}{newline}{exception}",
                    formatProvider: CultureInfo.InvariantCulture)
                .CreateLogger();
        }

        /// <summary>
        /// // This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddControllers();

            // Connect to database
            services.AddDbContextPool<UserDbContext>(options =>
            {
                options.UseNpgsql("Host = 192.168.1.49; Port = 5433; Database = WebAppService; Username = user; Password = password");   
            });
            
            services.AddEntityFrameworkNpgsql();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog(new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .Enrich.With(new UtcTimeStampEnricher())
                    .WriteTo.Console(
                        outputTemplate:
                        "{UtcTimestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message:lj}{NewLine}{Exception}")
                    .CreateLogger(), dispose: false);
            });

            services.AddTransient<IUserRepository, UserRepository>();

            // Generate swagger
            services.AddSwaggerGen(swg =>
                {
                    swg.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Version = "v1",
                        Title = "WebApplicationService",
                        Description = "Testing web application"
                    });
                    
                swg.DescribeAllEnumsAsStrings();
                
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                
                swg.IncludeXmlComments(xmlPath);
                }
            );
        }

        /// <summary>
        /// // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger(swg => { swg.RouteTemplate = "/swagger/{documentName}/swagger.json"; });
            app.UseSwaggerUI(swg => { swg.SwaggerEndpoint("/swagger/v1/swagger.json", "Web Application v1"); });

            // TODO:
            // app.UseHttpsRedirection();
            // app.UseAuthorization();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
