namespace LegacyApp.Api.Controllers;

using Contracts;
using Microsoft.AspNetCore.Mvc;
using NServiceBus;


[ApiController]
[Route("[controller]")]
public class OrderController :
    ControllerBase
{
    readonly ILogger<OrderController> _logger;

    public OrderController(ILogger<OrderController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "SubmitOrder")]
    public async Task<IActionResult> PostOrder(OrderModel order, [FromServices] IMessageSession session)
    {
        var requestKey = Guid.NewGuid().ToString();

        SendOptions sendOptions = new();
        sendOptions.SetHeader("X-Request-Key", requestKey);

        _logger.LogInformation("Sending SubmitOrder command, RequestKey is {RequestKey}, OrderNumber is {OrderNumber}", requestKey, order.OrderNumber);

        await session.Send(new SubmitOrder
        {
            OrderNumber = order.OrderNumber,
            Amount = order.Amount
        }, sendOptions);

        return Accepted();
    }
}