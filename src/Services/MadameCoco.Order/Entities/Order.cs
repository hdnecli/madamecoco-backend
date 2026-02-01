using System;
using System.Collections.Generic;
using MadameCoco.Shared;

namespace MadameCoco.Order.Entities;

/// <summary>
/// Represents a customer order.
/// </summary>
public class Order : BaseEntity
{
    /// <summary>
    /// Gets or sets the customer identifier.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the shipping address.
    /// </summary>
    public Address Address { get; set; } = default!;

    /// <summary>
    /// Gets or sets the current status of the order.
    /// </summary>
    public string Status { get; set; } = "Created"; // Default status

    /// <summary>
    /// Gets or sets the list of items in the order.
    /// </summary>
    public List<OrderItem> Items { get; set; } = new();
}
