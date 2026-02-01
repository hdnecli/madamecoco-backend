using System;
using System.Threading.Tasks;
using MadameCoco.Order.Features.Orders.Commands.CreateOrder;
using MadameCoco.Order.Features.Orders.Queries.GetOrderById;
using MadameCoco.Order.Features.Orders.Commands.ChangeOrderStatus;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MadameCoco.Order.Controllers;

/// <summary>
/// Service endpoint for managing customer orders.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrdersController"/> class.
    /// </summary>
    /// <param name="mediator">The MediatR mediator.</param>
    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new order.
    /// </summary>
    /// <param name="command">The create order command.</param>
    /// <returns>The created order ID or validation errors.</returns>
    /// <response code="201">Returns the newly created order ID.</response>
    /// <response code="400">If the input is invalid.</response>
    [HttpPost]
    [ProducesResponseType(typeof(MadameCoco.Shared.Response<Guid>), 201)]
    [ProducesResponseType(typeof(MadameCoco.Shared.Response<string>), 400)]
    public async Task<IActionResult> Create(CreateOrderCommand command)
    {
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    /// Retrieves an order by its unique identifier.
    /// </summary>
    /// <param name="id">The order ID.</param>
    /// <returns>The order details.</returns>
    /// <response code="200">Returns the order data.</response>
    /// <response code="404">If the order is not found.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(MadameCoco.Shared.Response<OrderDto>), 200)]
    [ProducesResponseType(typeof(MadameCoco.Shared.Response<OrderDto>), 404)]
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

    /// <summary>
    /// Updates the status of an existing order.
    /// </summary>
    /// <param name="id">The order ID.</param>
    /// <param name="status">The new status.</param>
    /// <returns>Confirmation of success.</returns>
    /// <response code="200">If the status update was successful.</response>
    /// <response code="404">If the order is not found.</response>
    [HttpPatch("{id}/status")]
    [ProducesResponseType(typeof(MadameCoco.Shared.Response<bool>), 200)]
    [ProducesResponseType(typeof(MadameCoco.Shared.Response<bool>), 404)]
    public async Task<IActionResult> ChangeStatus(Guid id, [FromBody] string status)
    {
        var command = new ChangeOrderStatusCommand(id, status);
        var result = await _mediator.Send(command);
        if (!result.IsSuccess)
        {
            return StatusCode(result.StatusCode, result);
        }
        return Ok(result);
    }
}
