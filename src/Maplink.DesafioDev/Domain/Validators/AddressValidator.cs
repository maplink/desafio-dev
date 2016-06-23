using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Maplink.DesafioDev.Domain.Entities;

namespace Maplink.DesafioDev.Domain.Validators
{
    public class AddressValidator : AbstractValidator<Address>
    {
        private static readonly IEnumerable<string> AcceptedUfs = new[]
        {
            "AC", "AL", "AP", "AM", "BA",
            "CE", "DF", "ES", "GO", "MA",
            "MT", "MS", "MG", "PA", "PB",
            "PR", "PE", "PI", "RJ", "RN",
            "RS", "RO", "RR", "SC", "SP",
            "SE", "TO"
        };

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
                RuleFor(request => request.State)
                    .Must(state => AcceptedUfs.Contains(state.ToUpper()))
                    .WithMessage("entry state information is not valid");
            });
        }
    }
}