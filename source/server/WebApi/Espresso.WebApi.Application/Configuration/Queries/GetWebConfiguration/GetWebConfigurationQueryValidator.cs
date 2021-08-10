// GetWebConfigurationQueryValidator.cs
//
// © 2021 Espresso News. All rights reserved.

using FluentValidation;

namespace Espresso.WebApi.Application.Configuration.Queries.GetWebConfiguration
{
    public class GetWebConfigurationQueryValidator : AbstractValidator<GetWebConfigurationQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetWebConfigurationQueryValidator"/> class.
        /// </summary>
        public GetWebConfigurationQueryValidator()
        {
            RuleFor(request => request.MaxAgeOfNewNewsPortal).NotEmpty();
        }
    }
}
