using FluentValidation;
using Maplink.DesafioDev.Domain.Entities;

namespace Maplink.DesafioDev.Domain.Validators
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(request => request.Street)
                .NotEmpty()
                .WithMessage("street information should be informed");

            RuleFor(request => request.Number)
                .NotEmpty()
                .WithMessage("number information should be informed");

            RuleFor(request => request.City)
                .NotEmpty()
                .WithMessage("city information should be informed");

            RuleFor(request => request.State)
                .NotEmpty()
                .WithMessage("state information should be informed");

            When(request => !string.IsNullOrEmpty(request.State), () =>
            {
                RuleFor(request => request)
                    .Must(request => request.IsValidState())
                    .WithMessage("entry state information is not valid");
            });
        }
    }
}