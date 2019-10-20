using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RESTfullAPIService.Implementations;
using RESTfullAPIService.Interfaces;
using RESTfullAPIService.Models;
using Serilog;
using System.Globalization;

namespace RESTfullAPIService
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

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

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddControllers();

            // Conect to db posgrees
            services.AddDbContextPool<UserDbContext>(options =>
            {
                // options.UseLoggerFactory(DebugLoggerFactory);
                // options.UseNpgsql(Configuration.GetConnectionString("Postgress"));
                // options.UseNpgsql(Configuration.GetSection("ConnectionString")["Postgress"]);
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

            services.AddTransient<ICRUD, CRUD>();

            // Generate swagger
            services.AddSwaggerGen(swg =>
                swg.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "WebApplicationService",
                    Description = "Testing web application"
                }
            ));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
