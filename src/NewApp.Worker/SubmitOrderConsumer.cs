using LegacyApp.Contracts.Commands;
using LegacyApp.Contracts.Events;

namespace NewApp.Worker;

using LegacyApp.Contracts;
using MassTransit;

public class SubmitOrderConsumer :
    IConsumer<SubmitOrder>
{
    readonly ILogger<SubmitOrderConsumer> _logger;

    public SubmitOrderConsumer(ILogger<SubmitOrderConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<SubmitOrder> context)
    {
        var requestKey = context.Headers.Get<string>("X-Request-Key");

        _logger.LogInformation(
            "Responding with OrderSubmissionAccepted and publishing OrderSubmitted, OrderNumber = {OrderNumber}, RequestKey = {RequestKey}",
            context.Message.OrderNumber, requestKey);

        return Task.WhenAll(
            context.Publish(new OrderSubmitted { OrderNumber = context.Message.OrderNumber }),
            context.RespondAsync(new OrderSubmissionAccepted { OrderNumber = context.Message.OrderNumber }));
    }
}