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
        _logger.LogInformation("Received SubmitOrder, OrderNumber = {OrderNumber}", context.Message.OrderNumber);

        _logger.LogInformation("Responding with OrderSubmissionAccepted and publishing OrderSubmitted, OrderNumber = {OrderNumber}",
            context.Message.OrderNumber);

        return Task.WhenAll(
            context.Publish(new OrderSubmitted { OrderNumber = context.Message.OrderNumber }),
            context.RespondAsync(new OrderSubmissionAccepted { OrderNumber = context.Message.OrderNumber }));
    }
}