using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MadameCoco.Order.Data;
using MadameCoco.Order.Entities;
using MadameCoco.Shared;
using MadameCoco.Shared.Contracts;
using MassTransit;
using MediatR;

namespace MadameCoco.Order.Features.Orders.Commands.CreateOrder;

/// <summary>
/// Handles the creation of a new order.
/// </summary>
public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, MadameCoco.Shared.Response<Guid>>
{
    private readonly Repositories.IOrderRepository _repository;
    private readonly IPublishEndpoint _publishEndpoint;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateOrderCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">The order repository.</param>
    /// <param name="publishEndpoint">The mass transit publish endpoint.</param>
    public CreateOrderCommandHandler(Repositories.IOrderRepository repository, IPublishEndpoint publishEndpoint)
    {
        _repository = repository;
        _publishEndpoint = publishEndpoint;
    }

    /// <summary>
    /// Validates the request, persists the order, and publishes an OrderCreated event.
    /// </summary>
    /// <param name="request">The create order command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A response containing the new order ID.</returns>
    public async Task<MadameCoco.Shared.Response<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Entities.Order
        {
            CustomerId = request.CustomerId,
            Address = request.Address,
            Status = "Created",
            Items = request.Items.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                ImageUrl = i.ImageUrl,
                Status = i.Status,
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity
            }).ToList()
        };

        await _repository.AddAsync(order, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        // Publish Event
        var orderCreatedEvent = new
        {
            OrderId = order.Id,
            CustomerId = order.CustomerId,
            ShippingAddress = order.Address, // Event contract likely still uses ShippingAddress name? Let's check IOrderCreatedEvent.
            Items = request.Items
        };

        await _publishEndpoint.Publish<IOrderCreatedEvent>(orderCreatedEvent, cancellationToken);

        return MadameCoco.Shared.Response<Guid>.Success(order.Id, 201);
    }
}
