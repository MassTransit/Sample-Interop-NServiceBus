using LegacyApp.Contracts.Events;

namespace LegacyApp.Components;

using Contracts;
using NServiceBus;
using NServiceBus.Logging;

public class OrderSubmissionAcceptedHandler :
    IHandleMessages<OrderSubmissionAccepted>
{
    static readonly ILog _log = LogManager.GetLogger<OrderSubmissionAcceptedHandler>();

    public Task Handle(OrderSubmissionAccepted message, IMessageHandlerContext context)
    {
        _log.InfoFormat("Received OrderSubmissionAccepted, OrderNumber = {OrderNumber}", message.OrderNumber);

        return Task.CompletedTask;
    }
}