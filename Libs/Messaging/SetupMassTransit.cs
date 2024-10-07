using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Libs;

public static class SetupMassTransit
{
    public static void AddMassTransit(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            
            var entryAssembly = Assembly.GetEntryAssembly();

            x.AddConsumers(entryAssembly);
            x.UsingRabbitMq((context,cfg) =>
            {
                var host = configuration.GetConnectionString("RabbitMQConnection");
                cfg.Host(host);
                cfg.ConfigureEndpoints(context);
            });
        });
        
    }
}