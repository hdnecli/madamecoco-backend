using MediatR;
using MadameCoco.Shared;
using MadameCoco.Order.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace MadameCoco.Order.Features.Orders.Commands.ChangeOrderStatus;

/// <summary>
/// Handles the order status update.
/// </summary>
public class ChangeOrderStatusCommandHandler : IRequestHandler<ChangeOrderStatusCommand, Response<bool>>
{
    private readonly IOrderRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChangeOrderStatusCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">The order repository.</param>
    public ChangeOrderStatusCommandHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Updates the order status in the database.
    /// </summary>
    /// <param name="request">The change status command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A response indicating success.</returns>
    public async Task<Response<bool>> Handle(ChangeOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var order = await _repository.GetByIdAsync(request.OrderId, cancellationToken);
        if (order == null)
        {
            return Response<bool>.Fail("Order not found", 404);
        }

        order.Status = request.Status;
        // In a real event-sourcing system we would generate an event here.
        // For now just updating state as per diagram requirements.
        
        await _repository.SaveChangesAsync(cancellationToken);

        return Response<bool>.Success(true, 200);
    }
}
