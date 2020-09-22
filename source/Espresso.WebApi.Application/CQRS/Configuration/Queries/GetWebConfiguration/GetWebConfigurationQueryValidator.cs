using FluentValidation;

namespace Espresso.WebApi.Application.CQRS.Configuration.Queries.GetWebConfiguration
{
    public class GetWebConfigurationQueryValidator : AbstractValidator<GetWebConfigurationQuery>
    {
        public GetWebConfigurationQueryValidator()
        {
            RuleFor(request => request.MaxAgeOfNewNewsPortal).NotEmpty();
        }
    }
}
