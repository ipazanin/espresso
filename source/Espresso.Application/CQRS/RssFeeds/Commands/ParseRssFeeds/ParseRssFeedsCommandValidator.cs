using FluentValidation;

namespace Espresso.Application.CQRS.RssFeeds.Commands.ParseRssFeeds
{
    public class ParseRssFeedsCommandValidator : AbstractValidator<ParseRssFeedsCommand>
    {
        public ParseRssFeedsCommandValidator()
        {
        }
    }
}
