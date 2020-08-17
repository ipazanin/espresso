using FluentValidation;

namespace Espresso.Application.CQRS.Articles.Commands.IncrementTrendingArticleScore
{
    public class IncrementNumberOfClicksCommandValidator : AbstractValidator<IncrementNumberOfClicksCommand>
    {
        public IncrementNumberOfClicksCommandValidator()
        {
            _ = RuleFor(incrementArticleTrendingScoreCommand => incrementArticleTrendingScoreCommand.Id).NotEmpty();
        }
    }
}
