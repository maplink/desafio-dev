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
        }
    }
}