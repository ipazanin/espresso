using FluentValidation;

namespace Espresso.WebApi.Application.Articles.Commands.ToggleFeaturedArticle
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
