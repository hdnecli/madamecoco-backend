using MadameCoco.Shared;

namespace MadameCoco.Customer.Entities;

/// <summary>
/// Represents a customer in the system.
/// </summary>
public class Customer : BaseEntity
{
    /// <summary>
    /// Gets or sets the customer's full name.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Gets or sets the customer's email address.
    /// </summary>
    public string Email { get; set; } = default!;

    /// <summary>
    /// Gets or sets the customer's physical address.
    /// </summary>
    public Address Address { get; set; } = default!;
}
