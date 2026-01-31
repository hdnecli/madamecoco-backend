using System;
using MadameCoco.Shared;
using MediatR;

namespace MadameCoco.Order.Features.Orders.Queries.GetOrderById;

public record GetOrderByIdQuery(Guid Id) : IRequest<Response<OrderDto>>;

public class OrderDto
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public string Status { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public AddressDto ShippingAddress { get; set; } = default!;
    public List<OrderItemDto> Items { get; set; } = new();
}

public class AddressDto
{
    public string AddressLine { get; set; } = default!;
    public string City { get; set; } = default!;
    public string Country { get; set; } = default!;
    public int CityCode { get; set; }
}
