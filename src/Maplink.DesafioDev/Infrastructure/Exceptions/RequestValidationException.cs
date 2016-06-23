using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using Maplink.DesafioDev.Infrastructure.Extensions;

namespace Maplink.DesafioDev.Infrastructure.Exceptions
{
    public class RequestValidationException : Exception
    {
        private readonly IEnumerable<string> _errors;

        public RequestValidationException(string message)
            : base(message)
        {
        }

        public RequestValidationException(string[] errors)
            : base(errors.ToErrorMessage())
        {
            _errors = errors;
        }

        public RequestValidationException(IEnumerable<ValidationFailure> validationFailures)
            : this(validationFailures.Select(e => e.ErrorMessage).ToArray())
        {
        }

        public IEnumerable<string> GetErrors()
        {
            return _errors;
        }
    }
}