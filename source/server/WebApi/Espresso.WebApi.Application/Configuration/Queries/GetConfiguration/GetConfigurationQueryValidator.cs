// GetConfigurationQueryValidator.cs
//
// © 2021 Espresso News. All rights reserved.

using FluentValidation;

namespace Espresso.WebApi.Application.Configuration.Queries.GetConfiguration
{
    public class GetConfigurationQueryValidator : AbstractValidator<GetConfigurationQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetConfigurationQueryValidator"/> class.
        /// </summary>
        public GetConfigurationQueryValidator()
        {
            RuleFor(request => request.MaxAgeOfNewNewsPortal).NotEmpty();
        }
    }
}
