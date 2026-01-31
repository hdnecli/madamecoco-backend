using MadameCoco.Shared;

namespace MadameCoco.Customer.Dtos;

public class CreateCustomerDto
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public Address Address { get; set; } = default!;
}
