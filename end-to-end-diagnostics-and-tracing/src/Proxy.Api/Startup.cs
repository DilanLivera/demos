using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Proxy.Api
{
    public class Startup
    {
        private const string ServiceName = "Demo.DiagnosticsAndTracing.Proxy";
        private const string ServiceVersion = "1.0.0";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "Proxy.Api",
                        Version = "v1"
                    });
            });

            services.AddOpenTelemetryTracing(builder =>
            {
                builder
                    .AddConsoleExporter()
                    .AddSource(ServiceName)
                    .SetResourceBuilder(ResourceBuilder
                        .CreateDefault()
                        .AddService(serviceName: ServiceName, serviceVersion: ServiceVersion))
                    .AddHttpClientInstrumentation()
                    .AddAspNetCoreInstrumentation();
            });

            services.AddHttpClient();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options => options.SwaggerEndpoint(
                    "/swagger/v1/swagger.json",
                    "Proxy.Api v1"));
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
