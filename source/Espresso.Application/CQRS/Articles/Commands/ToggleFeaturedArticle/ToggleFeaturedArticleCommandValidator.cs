using FluentValidation;

namespace Espresso.Application.CQRS.Articles.Commands.ToggleFeaturedArticle
{
    public class ToggleFeaturedArticleCommandValidator
        : AbstractValidator<ToggleFeaturedArticleCommand>
    {
        public ToggleFeaturedArticleCommandValidator()
        {
            RuleFor(request => request.ArticleId)
                .NotEmpty();
        }
    }
}
