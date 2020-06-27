using FluentValidation;

namespace Espresso.Application.CQRS.Configuration.Query.GetConfiguration
{
    public class GetConfigurationQueryValidator : AbstractValidator<GetConfigurationQuery>
    {
        public GetConfigurationQueryValidator()
        {
        }
    }
}
