using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RESTfulAPIService.DbContext;
using RESTfulAPIService.Implementations;
using RESTfulAPIService.Interfaces;
using Serilog;

namespace RESTfulAPIService
{
    /// <summary>
    /// </summary>
    public class Startup
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;

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
        ///     // This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddControllers();

            // Connect to database
            services.AddDbContextPool<UserDbContext>(options =>
            {
                // options.UseNpgsql("Host = 192.168.1.49; Port = 5433; Database = WebAppService; Username = user; Password = password");
                options.UseNpgsql(
                    $"Host = {_configuration.GetValue("host", "192.168.1.49")};" +
                    $" Port = {_configuration.GetValue("port", "5433")}; " +
                    $"Database = {_configuration.GetValue("database", "WebAppService")}; " +
                    $"Username = {_configuration.GetValue("user", "user")}; " +
                    $"Password = {_configuration.GetValue("password", "password")}");
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
                    .CreateLogger());
            });

            services.AddTransient<IUserRepository, UserRepository>();

            // Generate swagger
            services.AddSwaggerGen(swg =>
                {
                    swg.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "WebApplicationService",
                        Description = "Testing web application"
                    });
                    
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                    swg.IncludeXmlComments(xmlPath);
                }
            );
        }

        /// <summary>
        ///     // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseSwagger(swg => { swg.RouteTemplate = "/swagger/{documentName}/swagger.json"; });
            app.UseSwaggerUI(swg => { swg.SwaggerEndpoint("/swagger/v1/swagger.json", "Web Application v1"); });

            // TODO: https and authorization
            // app.UseHttpsRedirection();
            // app.UseAuthorization();

            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}