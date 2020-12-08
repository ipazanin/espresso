using FluentValidation;

namespace Espresso.WebApi.Application.Configuration.Queries.GetConfiguration
{
    public class GetConfigurationQueryValidator : AbstractValidator<GetConfigurationQuery>
    {
        public GetConfigurationQueryValidator()
        {
            RuleFor(request => request.MaxAgeOfNewNewsPortal).NotEmpty();
        }
    }
}
