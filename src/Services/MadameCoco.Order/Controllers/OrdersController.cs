using System;
using System.Threading.Tasks;
using MadameCoco.Order.Features.Orders.Commands.CreateOrder;
using MadameCoco.Order.Features.Orders.Queries.GetOrderById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MadameCoco.Order.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderCommand command)
    {
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetOrderByIdQuery(id);
        var result = await _mediator.Send(query);
        if (!result.IsSuccess)
        {
            return StatusCode(result.StatusCode, result);
        }
        return Ok(result);
    }
}
