// SetFeaturedArticleCommandValidator.cs
//
// © 2022 Espresso News. All rights reserved.

using FluentValidation;

namespace Espresso.WebApi.Application.Articles.Commands.SetFeaturedArticle;

public class SetFeaturedArticleCommandValidator
    : AbstractValidator<SetFeaturedArticleCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SetFeaturedArticleCommandValidator"/> class.
    /// </summary>
    public SetFeaturedArticleCommandValidator()
    {
        RuleForEach(request => request.FeaturedArticleConfigurations)
            .Must(featuredConfiguration => featuredConfiguration.articleId != Guid.Empty).WithMessage("Article ID cannot be empty!")
            .Must(featuredConfiguration => featuredConfiguration.featuredPosition is null or >= 0).WithMessage("Featured Position cannot be lower than 0");
    }
}
