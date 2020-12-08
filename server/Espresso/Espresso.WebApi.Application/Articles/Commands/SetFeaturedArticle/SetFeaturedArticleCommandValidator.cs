using FluentValidation;

namespace Espresso.WebApi.Application.Articles.Commands.SetFeaturedArticle
{
    public class SetFeaturedArticleCommandValidator
        : AbstractValidator<SetFeaturedArticleCommand>
    {
        public SetFeaturedArticleCommandValidator()
        {
            RuleForEach(request => request.FeaturedArticleConfigurations)
                .Must(featuredConfiguration => featuredConfiguration.articleId != default).WithMessage("Article ID cannot be empty!")
                .Must(featuredConfiguration => featuredConfiguration.featuredPosition is null or >= 0).WithMessage("Featured Position cannot be lower than 0");
        }
    }
}
