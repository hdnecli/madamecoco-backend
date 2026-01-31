using System;
using System.Collections.Generic;
using MadameCoco.Shared;

namespace MadameCoco.Order.Entities;

public class Order : BaseEntity
{
    public Guid CustomerId { get; set; }
    public Address ShippingAddress { get; set; } = default!;
    public string Status { get; set; } = "Created"; // Default status
    public List<OrderItem> Items { get; set; } = new();
}
