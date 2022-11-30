using LegacyApp.Contracts.Commands;

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
        _logger.LogInformation("Sending SubmitOrder command, OrderNumber is {OrderNumber}", order.OrderNumber);

        await session.Send(new SubmitOrder
        {
            OrderNumber = order.OrderNumber,
            Amount = order.Amount
        });

        return Accepted();
    }
}