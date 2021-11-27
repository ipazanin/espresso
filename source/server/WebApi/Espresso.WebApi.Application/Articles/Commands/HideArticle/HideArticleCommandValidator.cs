// HideArticleCommandValidator.cs
//
// © 2021 Espresso News. All rights reserved.

using FluentValidation;

namespace Espresso.WebApi.Application.Articles.Commands.HideArticle;

public class HideArticleCommandValidator : AbstractValidator<HideArticleCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HideArticleCommandValidator"/> class.
    /// </summary>
    public HideArticleCommandValidator()
    {
        RuleFor(request => request.ArticleId).NotEmpty();
    }
}
