namespace LegacyApp.Components;

using Contracts;
using NServiceBus;
using NServiceBus.Logging;


public class OrderSubmittedHandler :
    IHandleMessages<OrderSubmitted>
{
    static readonly ILog _log = LogManager.GetLogger<OrderSubmittedHandler>();

    public Task Handle(OrderSubmitted message, IMessageHandlerContext context)
    {
        _log.Info($"Received OrderSubmitted, OrderNumber = {message.OrderNumber} - Charging credit card...");

        return Task.CompletedTask;
    }
}