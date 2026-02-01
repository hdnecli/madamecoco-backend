using System;
using System.Collections.Generic;

namespace MadameCoco.Shared.Contracts;

/// <summary>
/// Integration event raised when a new order is successfully created.
/// </summary>
public interface IOrderCreatedEvent
{
    /// <summary>
    /// Gets the unique identifier of the order.
    /// </summary>
    public Guid OrderId { get; }

    /// <summary>
    /// Gets the unique identifier of the customer who placed the order.
    /// </summary>
    public Guid CustomerId { get; }

    /// <summary>
    /// Gets the list of items included in the order.
    /// </summary>
    public List<OrderItemDto> Items { get; }

    /// <summary>
    /// Gets the shipping address for the order.
    /// </summary>
    public Address ShippingAddress { get; }
}

/// <summary>
/// Data Transfer Object representing a line item in an order.
/// </summary>
public class OrderItemDto 
{
    /// <summary>
    /// Gets or sets the product identifier.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the product name.
    /// </summary>
    public string ProductName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the product image URL.
    /// </summary>
    public string ImageUrl { get; set; } = default!;

    /// <summary>
    /// Gets or sets the status of the item/product at purchase time.
    /// </summary>
    public string Status { get; set; } = default!;

    /// <summary>
    /// Gets or sets the quantity ordered.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price at the time of purchase.
    /// </summary>
    public decimal UnitPrice { get; set; }
}
