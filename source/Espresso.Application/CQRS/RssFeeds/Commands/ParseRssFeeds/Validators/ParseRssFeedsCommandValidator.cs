using FluentValidation;

namespace Espresso.Application.CQRS.RssFeeds.Commands.ParseRssFeeds.Validators
{
    public class ParseRssFeedsCommandValidator : AbstractValidator<ParseRssFeedsCommand>
    {
        public ParseRssFeedsCommandValidator()
        {
        }
    }
}
