using System;
using System.Collections.Generic;
using System.Linq;

namespace Espresso.Domain.Validators
{
    public abstract class Validator
    {
        protected void NotEmpty<T>(T value, string parameterName, string? errorMessage = null)
        {
            if (Equals(value, default(T)))
            {
                throw new ArgumentException(
                    message: errorMessage ?? $"{parameterName} must not be empty!",
                    paramName: parameterName
                );
            }
        }

        protected void NotEmpty<T>(IEnumerable<T> values, string parameterName, string? errorMessage = null)
        {
            if (values.Count() == default)
            {
                throw new ArgumentException(
                    message: errorMessage ?? $"{parameterName} must not be empty!",
                    paramName: parameterName
                );
            }
        }

        protected void NotEmpty(string value, string parameterName, string? errorMessage = null)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException(
                    message: errorMessage ?? $"{parameterName} must not be empty!",
                    paramName: parameterName
                );
            }
        }

        protected void MaxLength(string value, string parameterName, int maxValue, string? errorMessage = null)
        {
            if (value.Length > maxValue)
            {
                throw new ArgumentException(
                    message: errorMessage ?? $"{parameterName} must be lower than {maxValue}!",
                    paramName: parameterName
                );
            }
        }

        protected void MustBeUrl(string value, string parameterName, string? errorMessage = null)
        {
            if (
               !(Uri.TryCreate(value, UriKind.Absolute, out var uriResult) &&
               (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
            )
            {
                throw new ArgumentException(
                    message: errorMessage ?? $"{parameterName} must be valid url!",
                    paramName: nameof(value)
                );
            }
        }
    }
}
