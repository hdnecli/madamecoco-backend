using System;
using MadameCoco.Shared;

namespace MadameCoco.Order.Entities;

public class OrderItem : BaseEntity
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = default!;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
}
