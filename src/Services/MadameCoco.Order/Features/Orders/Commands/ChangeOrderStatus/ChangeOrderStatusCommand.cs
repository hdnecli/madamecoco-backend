using MediatR;
using MadameCoco.Shared;
using System;

namespace MadameCoco.Order.Features.Orders.Commands.ChangeOrderStatus;

public record ChangeOrderStatusCommand(Guid OrderId, string Status) : IRequest<Response<bool>>;
