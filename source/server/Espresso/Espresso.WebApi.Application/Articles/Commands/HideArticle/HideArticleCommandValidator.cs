using FluentValidation;

namespace Espresso.WebApi.Application.Articles.Commands.HideArticle
{
    public class HideArticleCommandValidator : AbstractValidator<HideArticleCommand>
    {
        public HideArticleCommandValidator()
        {
            RuleFor(request => request.ArticleId).NotEmpty();
        }
    }
}
