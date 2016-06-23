using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;
using Maplink.DesafioDev.Domain.Validators;

namespace Maplink.DesafioDev.Domain.Entities
{
    public class RouteRequest : IEntityValidation
    {
        private static readonly IValidator<RouteRequest> Validator = new RouteRequestValidator();

        public IEnumerable<Address> Addresses = new List<Address>();

        public ValidationResult Validate()
        {
            return Validator.Validate(this);
        }
    }
}