using FluentValidation;

namespace Espresso.WebApi.Application.CQRS.Articles.Commands.HideArticle
{
    public class HideArticleCommandValidator : AbstractValidator<HideArticleCommand>
    {
        public HideArticleCommandValidator()
        {
            _ = RuleFor(request => request.ArticleId).NotEmpty();
        }
    }
}
