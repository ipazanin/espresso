using FluentValidation;

namespace Espresso.WebApi.Application.NewsPortals.Commands.NewSourcesRequest
{
    public class NewSourcesRequestCommandValidator : AbstractValidator<NewsSourcesRequestCommand>
    {
        public NewSourcesRequestCommandValidator()
        {
            RuleFor(request => request.NewsPortalName).NotEmpty();
            RuleFor(request => request.Email).NotEmpty().EmailAddress();
        }
    }
}
