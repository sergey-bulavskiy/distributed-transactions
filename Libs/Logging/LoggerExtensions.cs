using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace Logging;

public static class LoggerExtensions
{
    public static void ConfigureLogging(this WebApplicationBuilder builder, string? otelOptionsEndpoint)
    {
        LoggerConfiguration logBuilder = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("MassTransit", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.Hosting", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Console();


        if (!string.IsNullOrEmpty(otelOptionsEndpoint))
        {
            logBuilder.WriteTo.OpenTelemetry(options =>
            {
                options.Endpoint = builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"];
                AddHeaders(options.Headers, builder.Configuration["OTEL_EXPORTER_OTLP_HEADERS"]);
                AddResourceAttributes(options.ResourceAttributes, builder.Configuration["OTEL_RESOURCE_ATTRIBUTES"]);

                options.ResourceAttributes.Add("service.name", "apiservice");
                return;

                void AddResourceAttributes(IDictionary<string, object> attributes, string attributeConfig)
                {
                    if (!string.IsNullOrEmpty(attributeConfig))
                    {
                        string[] parts = attributeConfig.Split('=');
                        if (parts.Length == 2)
                        {
                            attributes[parts[0]] = parts[1];
                        }
                        else
                        {
                            throw new InvalidOperationException(
                                $"Invalid resource attribute format: {attributeConfig}");
                        }
                    }
                }

                void AddHeaders(IDictionary<string, string> headers, string headerConfig)
                {
                    if (!string.IsNullOrEmpty(headerConfig))
                    {
                        foreach (string header in headerConfig.Split(','))
                        {
                            string[] parts = header.Split('=');
                            if (parts.Length == 2)
                            {
                                headers[parts[0]] = parts[1];
                            }
                            else
                            {
                                throw new InvalidOperationException($"Invalid header format: {header}");
                            }
                        }
                    }
                }
            });
        }
        
        Log.Logger = logBuilder.CreateBootstrapLogger();
        builder.Logging.ClearProviders().AddSerilog();
        builder.Host.UseSerilog();
        Log.Logger.Information("Configured Serilog to send to: {Endpoint}", otelOptionsEndpoint);
    }
}