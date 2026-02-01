using System;
using System.Collections.Generic;
using MadameCoco.Shared;
using MadameCoco.Shared.Contracts;
using MediatR;

namespace MadameCoco.Order.Features.Orders.Commands.CreateOrder;

/// <summary>
/// Command to create a new order.
/// </summary>
/// <param name="CustomerId">The identifier of the customer placing the order.</param>
/// <param name="Address">The shipping address.</param>
/// <param name="Items">The list of items to order.</param>
public record CreateOrderCommand(
    Guid CustomerId,
    Address Address,
    List<OrderItemDto> Items
) : IRequest<MadameCoco.Shared.Response<Guid>>;
