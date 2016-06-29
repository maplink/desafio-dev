using FluentValidation;
using Maplink.DesafioDev.Domain.Entities;

namespace Maplink.DesafioDev.Domain.Validators
{
    public class RouteRequestValidator : AbstractValidator<RouteRequest>
    {
        public RouteRequestValidator()
        {
            RuleFor(request => request.Addresses)
                .NotEmpty()
                .WithMessage("addresses should be informed")
                .SetCollectionValidator(new AddressValidator());

            RuleFor(request => request.Type)
                .NotEmpty()
                .WithMessage("route type should be informed");

            RuleFor(request => request)
                .Must(request => request.IsValidType())
                .WithMessage("entry route type is not valid")
                .When(request => !string.IsNullOrWhiteSpace(request.Type));
        }
    }
}