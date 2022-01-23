using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace WeatherForecast.Api
{
    public class Startup
    {
        private const string ServiceName = "Demo.DiagnosticsAndTracing.WeatherForecast";
        private const string ServiceVersion = "1.0.0";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddOpenTelemetryTracing(builder =>
            {
                builder
                    .AddConsoleExporter()
                    .AddSource(ServiceName)
                    .SetResourceBuilder(ResourceBuilder
                        .CreateDefault()
                        .AddService(serviceName: ServiceName, serviceVersion: ServiceVersion))
                    .AddAspNetCoreInstrumentation();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
