namespace MadameCoco.Shared;

/// <summary>
/// Represents a physical address value object.
/// </summary>
public class Address
{
    /// <summary>
    /// Gets or sets the street address line.
    /// </summary>
    public string AddressLine { get; set; } = default!;

    /// <summary>
    /// Gets or sets the city name.
    /// </summary>
    public string City { get; set; } = default!;

    /// <summary>
    /// Gets or sets the country name.
    /// </summary>
    public string Country { get; set; } = default!;

    /// <summary>
    /// Gets or sets the numeric city code.
    /// </summary>
    public int CityCode { get; set; }
}
