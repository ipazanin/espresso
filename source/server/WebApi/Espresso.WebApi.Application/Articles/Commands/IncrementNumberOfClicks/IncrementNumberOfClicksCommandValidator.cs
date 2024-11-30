// IncrementNumberOfClicksCommandValidator.cs
//
// © 2022 Espresso News. All rights reserved.

using FluentValidation;

namespace Espresso.WebApi.Application.Articles.Commands.IncrementNumberOfClicks;

public class IncrementNumberOfClicksCommandValidator : AbstractValidator<IncrementNumberOfClicksCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IncrementNumberOfClicksCommandValidator"/> class.
    /// </summary>
    public IncrementNumberOfClicksCommandValidator()
    {
        _ = RuleFor(incrementArticleTrendingScoreCommand => incrementArticleTrendingScoreCommand.Id).NotEmpty();
    }
}
