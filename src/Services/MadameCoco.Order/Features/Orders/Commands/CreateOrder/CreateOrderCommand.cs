using System;
using System.Collections.Generic;
using MadameCoco.Shared;
using MadameCoco.Shared.Contracts;
using MediatR;

namespace MadameCoco.Order.Features.Orders.Commands.CreateOrder;

public record CreateOrderCommand(
    Guid CustomerId,
    Address Address,
    List<OrderItemDto> Items
) : IRequest<MadameCoco.Shared.Response<Guid>>;
