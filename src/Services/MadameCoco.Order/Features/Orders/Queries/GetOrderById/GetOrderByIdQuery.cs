using System;
using MadameCoco.Shared;
using MadameCoco.Shared.Contracts;
using MediatR;

namespace MadameCoco.Order.Features.Orders.Queries.GetOrderById;

/// <summary>
/// Query to retrieve an order by its unique identifier.
/// </summary>
/// <param name="Id">The order ID.</param>
public record GetOrderByIdQuery(Guid Id) : IRequest<MadameCoco.Shared.Response<OrderDto>>;

/// <summary>
/// Data Transfer Object representing an Order.
/// </summary>
public class OrderDto
{
    /// <summary>
    /// Gets or sets the order identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the customer identifier.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the order status.
    /// </summary>
    public string Status { get; set; } = default!;

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the shipping address.
    /// </summary>
    public AddressDto Address { get; set; } = default!;

    /// <summary>
    /// Gets or sets the list of order items.
    /// </summary>
    public List<OrderItemDto> Items { get; set; } = new();
}

/// <summary>
/// Data Transfer Object representing an address.
/// </summary>
public class AddressDto
{
    /// <summary>
    /// Gets or sets the address line.
    /// </summary>
    public string AddressLine { get; set; } = default!;

    /// <summary>
    /// Gets or sets the city.
    /// </summary>
    public string City { get; set; } = default!;

    /// <summary>
    /// Gets or sets the country.
    /// </summary>
    public string Country { get; set; } = default!;

    /// <summary>
    /// Gets or sets the city code.
    /// </summary>
    public int CityCode { get; set; }
}
