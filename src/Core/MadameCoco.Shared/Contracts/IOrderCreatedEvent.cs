using System;
using System.Collections.Generic;

namespace MadameCoco.Shared.Contracts;

public interface IOrderCreatedEvent
{
    public Guid OrderId { get; }
    public Guid CustomerId { get; }
    public List<OrderItemDto> Items { get; }
    public Address ShippingAddress { get; }
}

public class OrderItemDto 
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
    public string Status { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
