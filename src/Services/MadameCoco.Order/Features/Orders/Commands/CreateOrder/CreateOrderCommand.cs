using System;
using System.Collections.Generic;
using MadameCoco.Shared;
using MediatR;

namespace MadameCoco.Order.Features.Orders.Commands.CreateOrder;

public record CreateOrderCommand(
    Guid CustomerId,
    Address ShippingAddress,
    List<OrderItemDto> Items
) : IRequest<Response<Guid>>;
