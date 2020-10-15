using FluentValidation;

namespace Espresso.WebApi.Application.Articles.Commands.SetFeaturedArticle
{
    public class SetFeaturedArticleCommandValidator
        : AbstractValidator<SetFeaturedArticleCommand>
    {
        public SetFeaturedArticleCommandValidator()
        {
            RuleFor(request => request.ArticleId)
                .NotEmpty();
        }
    }
}
