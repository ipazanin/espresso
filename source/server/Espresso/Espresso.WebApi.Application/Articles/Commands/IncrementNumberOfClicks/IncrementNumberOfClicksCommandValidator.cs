using FluentValidation;

namespace Espresso.WebApi.Application.Articles.Commands.IncrementTrendingArticleScore
{
    public class IncrementNumberOfClicksCommandValidator : AbstractValidator<IncrementNumberOfClicksCommand>
    {
        public IncrementNumberOfClicksCommandValidator()
        {
            RuleFor(incrementArticleTrendingScoreCommand => incrementArticleTrendingScoreCommand.Id).NotEmpty();
        }
    }
}
