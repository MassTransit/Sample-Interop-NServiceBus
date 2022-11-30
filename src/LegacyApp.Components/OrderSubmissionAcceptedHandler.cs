namespace LegacyApp.Components;

using Contracts.Events;
using NServiceBus;
using NServiceBus.Logging;


public class OrderSubmissionAcceptedHandler :
    IHandleMessages<OrderSubmissionAccepted>
{
    static readonly ILog _log = LogManager.GetLogger<OrderSubmissionAcceptedHandler>();

    public Task Handle(OrderSubmissionAccepted message, IMessageHandlerContext context)
    {
        _log.InfoFormat("Received OrderSubmissionAccepted, OrderNumber = {OrderNumber}, RequestKey = {RequestKey}",
            message.OrderNumber,
            context.MessageHeaders.ContainsKey("X-Request-Key") ? context.MessageHeaders["X-Request-Key"] : "NA");

        return Task.CompletedTask;
    }
}