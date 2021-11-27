// IncrementNumberOfClicksCommandValidator.cs
//
// © 2021 Espresso News. All rights reserved.

using FluentValidation;

namespace Espresso.WebApi.Application.Articles.Commands.IncrementTrendingArticleScore;

public class IncrementNumberOfClicksCommandValidator : AbstractValidator<IncrementNumberOfClicksCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IncrementNumberOfClicksCommandValidator"/> class.
    /// </summary>
    public IncrementNumberOfClicksCommandValidator()
    {
        RuleFor(incrementArticleTrendingScoreCommand => incrementArticleTrendingScoreCommand.Id).NotEmpty();
    }
}
