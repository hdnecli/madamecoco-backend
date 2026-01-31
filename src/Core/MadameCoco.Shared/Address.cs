namespace MadameCoco.Shared;

public class Address
{
    public string AddressLine { get; set; } = default!;
    public string City { get; set; } = default!;
    public string Country { get; set; } = default!;
    public int CityCode { get; set; }
}
