using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using Maplink.DesafioDev.Infrastructure.Exceptions;

namespace Maplink.DesafioDev.Infrastructure.Extensions
{
    public static class ValidationResultExtensions
    {
        public static void ThrowOnFailure(this ValidationResult validationResult)
        {
            if (!validationResult.IsValid)
            {
                throw new RequestValidationException(validationResult.Errors.GetMessages().ToArray());
            }
        }

        private static IEnumerable<string> GetMessages(this IEnumerable<ValidationFailure> errors)
        {
            return errors
                .Select(s => s.ErrorMessage);
        }
    }
}