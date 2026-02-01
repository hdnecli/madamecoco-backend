using MediatR;
using MadameCoco.Shared;
using System;

namespace MadameCoco.Order.Features.Orders.Commands.ChangeOrderStatus;

/// <summary>
/// Command to update the status of an existing order.
/// </summary>
/// <param name="OrderId">The unique identifier of the order.</param>
/// <param name="Status">The new status to set.</param>
public record ChangeOrderStatusCommand(Guid OrderId, string Status) : IRequest<Response<bool>>;
