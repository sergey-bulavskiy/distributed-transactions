using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Libs;

public static class SetupMassTransit
{
    public static void AddMassTransit(IServiceCollection services, IConfiguration configuration)
    {
        IConfigurationSection section = configuration.GetSection("RabbitMQ");
        services.Configure<RabbitMQSettings>(section);

        RabbitMQSettings? settings = section.Get<RabbitMQSettings>();

        if (settings == null)
            throw new Exception("Cannot start up mass transit, rabbit mq settings are missing.");
        
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context,cfg) =>
            {
                cfg.Host(settings.Host, "/", h => {
                    h.Username(settings.UserName);
                    h.Password(settings.Password);
                });

                cfg.ConfigureEndpoints(context);
            });
        });
        
    }
}