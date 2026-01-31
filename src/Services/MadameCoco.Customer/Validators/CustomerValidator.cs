using FluentValidation;
using MadameCoco.Customer.Dtos;

namespace MadameCoco.Customer.Validators;

public class CustomerValidator : AbstractValidator<CreateCustomerDto>
{
    public CustomerValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required.");
        RuleFor(x => x.Address).NotNull().WithMessage("Address is required.");
        RuleFor(x => x.Address.City).NotEmpty().WithMessage("City is required.");
        RuleFor(x => x.Address.Country).NotEmpty().WithMessage("Country is required.");
        RuleFor(x => x.Address.CityCode).GreaterThan(0).WithMessage("Valid City Code is required.");
    }
}
