using System.Collections.Generic;
using System.Linq;
using Espresso.Application.DataTransferObjects;

namespace Espresso.Application.CQRS.RssFeeds.Commands.ParseRssFeeds
{
    public class ParseRssFeedsCommandResponse
    {
        public IEnumerable<ArticleDto> CreatedArticles { get; }

        public IEnumerable<ArticleDto> UpdatedArticles { get; }

        public ParseRssFeedsCommandResponse(IEnumerable<ArticleDto> createdArticles, IEnumerable<ArticleDto> updatedArticles)
        {
            CreatedArticles = createdArticles;
            UpdatedArticles = updatedArticles;
        }

        public override string ToString()
        {
            return $"{nameof(CreatedArticles)}:{CreatedArticles.Count()}, {nameof(UpdatedArticles)}:{UpdatedArticles.Count()}";
        }
    }
}
