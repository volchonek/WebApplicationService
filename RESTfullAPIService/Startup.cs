using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using RESTfullAPIService.Models;
using Swashbuckle.AspNetCore.Swagger;
using System.Globalization;
using RESTfullAPIService.Interfaces;
using RESTfullAPIService.Implementations;

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
            services.AddControllers();
            services.AddMvc();

            // Conect to db posgree
            services.AddDbContext<UserDbContext>(options => options.UseNpgsql("Host = localhost; Port = 5432; Database = webApp; Username = webApp; Password = webApp"), ServiceLifetime.Transient);
            services.AddEntityFrameworkNpgsql();

            // Generate swagger
            services.AddSwaggerGen(swg =>
            swg.SwaggerDoc("v1", new Info
            {
                Version = "v1",
                Title = "Web Application",
                Description = "Trial period web application"
            }));

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
            app.UseSwaggerUI(swg => { swg.SwaggerEndpoint("/swagger/v1/swagger.json", "Web application"); });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
