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

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, MadameCoco.Shared.Response<Guid>>
{
    private readonly Repositories.IOrderRepository _repository;
    private readonly IPublishEndpoint _publishEndpoint;

    public CreateOrderCommandHandler(Repositories.IOrderRepository repository, IPublishEndpoint publishEndpoint)
    {
        _repository = repository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<MadameCoco.Shared.Response<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Entities.Order
        {
            CustomerId = request.CustomerId,
            ShippingAddress = request.ShippingAddress,
            Status = "Created",
            Items = request.Items.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                ProductName = "Product Name Placeholder", // Ideally fetched from Product Service or passed in DTO
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity
            }).ToList()
        };

        _repository.Add(order);
        await _repository.SaveChangesAsync(cancellationToken);

        // Publish Event
        await _publishEndpoint.Publish<IOrderCreatedEvent>(new
        {
            OrderId = order.Id,
            CustomerId = order.CustomerId,
            Items = request.Items,
            ShippingAddress = order.ShippingAddress
        }, cancellationToken);

        return MadameCoco.Shared.Response<Guid>.Success(order.Id, 201);
    }
}
