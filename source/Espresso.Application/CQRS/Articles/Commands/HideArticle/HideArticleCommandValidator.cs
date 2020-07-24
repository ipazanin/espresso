using FluentValidation;

namespace Espresso.Application.CQRS.Articles.Commands.HideArticle
{
    public class HideArticleCommandValidator : AbstractValidator<HideArticleCommand>
    {
        public HideArticleCommandValidator()
        {
            RuleFor(request => request.ArticleId).NotEmpty();
        }
    }
}
