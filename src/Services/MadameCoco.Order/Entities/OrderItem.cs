using System;
using MadameCoco.Shared;

namespace MadameCoco.Order.Entities;

/// <summary>
/// Represents a line item within an order.
/// </summary>
public class OrderItem
{
    /// <summary>
    /// Gets or sets the unique identifier of the order item.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the product identifier.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the product name at the time of purchase.
    /// </summary>
    public string ProductName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the product image URL.
    /// </summary>
    public string ImageUrl { get; set; } = default!;

    /// <summary>
    /// Gets or sets the product status.
    /// </summary>
    public string Status { get; set; } = default!;

    /// <summary>
    /// Gets or sets the unit price at the time of purchase.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the quantity ordered.
    /// </summary>
    public int Quantity { get; set; }
}
