using Microsoft.Extensions.Logging;

namespace Libs;
using MassTransit;

public abstract class MessageConsumerBase<T>(ILogger logger) : IConsumer<T> where T : class
{
    protected readonly ILogger _logger = logger;
    
    public async Task Consume(ConsumeContext<T> context)
    {
        _logger.LogInformation($"Message {typeof(T)} received");

        await ConsumeMessage(context.Message);
        
        _logger.LogInformation($"Message {typeof(T)} consumed");
    }

    protected abstract Task ConsumeMessage(T message);
}