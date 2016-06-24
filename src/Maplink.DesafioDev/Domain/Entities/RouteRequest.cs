using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using Maplink.DesafioDev.Domain.Validators;

namespace Maplink.DesafioDev.Domain.Entities
{
    public class RouteRequest : IEntityValidation
    {
        private static readonly IValidator<RouteRequest> Validator = new RouteRequestValidator();

        private static readonly IEnumerable<string> AcceptedTypes = new[]
        {
            "shortest", "fastest"
        };

        public IEnumerable<Address> Addresses = new List<Address>();

        public string Type { get; set; } = "fastest";

        public virtual bool IsValidType()
        {
            return AcceptedTypes.Contains(Type?.ToLower());
        }

        public ValidationResult Validate()
        {
            return Validator.Validate(this);
        }
    }
}