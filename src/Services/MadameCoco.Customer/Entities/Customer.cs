using MadameCoco.Shared;

namespace MadameCoco.Customer.Entities;

public class Customer : BaseEntity
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public Address Address { get; set; } = default!;
}
