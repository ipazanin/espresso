using FluentValidation;

namespace Espresso.WebApi.Application.CQRS.Articles.Commands.ToggleFeaturedArticle
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
