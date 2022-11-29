namespace LegacyApp.Components;

using Contracts;
using NServiceBus;
using NServiceBus.Logging;


public class SubmitOrderHandler :
    IHandleMessages<SubmitOrder>
{
    static readonly ILog _log = LogManager.GetLogger<SubmitOrderHandler>();

    public Task Handle(SubmitOrder message, IMessageHandlerContext context)
    {
        _log.Info($"Received SubmitOrder, OrderNumber = {message.OrderNumber}");

        // do the hard work here, typically

        // fail every time
        // throw new Exception("Intentional Failure");

        // fail sometimes
        // if (Random.Shared.Next(0, 5) == 0)
        // {
        //     throw new Exception("Transient Failure");
        // }

        var requestKey = context.MessageHeaders["X-Request-Key"];

        _log.InfoFormat("Replying with OrderSubmissionAccepted and publishing OrderSubmitted, OrderNumber = {OrderNumber}, RequestKey = {RequestKey}",
            message.OrderNumber, requestKey);

        ReplyOptions replyOptions = new();
        replyOptions.SetHeader("X-Request-Key", requestKey);
        
        return Task.WhenAll(
            context.Publish(new OrderSubmitted { OrderNumber = message.OrderNumber }),
            context.Reply(new OrderSubmissionAccepted { OrderNumber = message.OrderNumber }, replyOptions));
    }
}