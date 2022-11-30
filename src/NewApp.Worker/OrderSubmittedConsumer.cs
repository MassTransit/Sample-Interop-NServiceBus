using LegacyApp.Contracts.Events;

namespace NewApp.Worker;

using LegacyApp.Contracts;
using MassTransit;


public class OrderSubmittedConsumer :
    IConsumer<OrderSubmitted>
{
    readonly ILogger<OrderSubmittedConsumer> _logger;

    public OrderSubmittedConsumer(ILogger<OrderSubmittedConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<OrderSubmitted> context)
    {
        _logger.LogInformation("Received OrderSubmitted, OrderNumber = {OrderNumber}", context.Message.OrderNumber);

        return Task.CompletedTask;
    }
}