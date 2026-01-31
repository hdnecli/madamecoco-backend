using FluentValidation;
using MadameCoco.Shared;

namespace MadameCoco.Order.Validators;

public class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(x => x.AddressLine).NotEmpty();
        RuleFor(x => x.City).NotEmpty();
        RuleFor(x => x.Country).NotEmpty();
        RuleFor(x => x.CityCode).GreaterThan(0);
    }
}
