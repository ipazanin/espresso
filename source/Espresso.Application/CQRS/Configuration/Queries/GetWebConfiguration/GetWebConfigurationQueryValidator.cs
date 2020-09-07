using FluentValidation;

namespace Espresso.Application.CQRS.Configuration.Queries.GetWebConfiguration
{
    public class GetWebConfigurationQueryValidator : AbstractValidator<GetWebConfigurationQuery>
    {
        public GetWebConfigurationQueryValidator()
        {
            RuleFor(request => request.MaxAgeOfNewNewsPortal).NotEmpty();
        }
    }
}
