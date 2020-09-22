using System.Collections.Generic;
using System.Linq;

namespace Espresso.WebApi.Application.CQRS.Articles.Queries.GetCategoryArticles
{
    public class GetCategoryArticlesQueryResponse
    {
        public IEnumerable<GetCategoryArticlesArticle> Articles { get; } = new List<GetCategoryArticlesArticle>();

        public IEnumerable<GetCategoryArticlesNewsPortal> NewNewsPortals { get; set; }

        public int NewNewsPortalsPosition { get; }

        public GetCategoryArticlesQueryResponse(
            IEnumerable<GetCategoryArticlesArticle> articles,
            IEnumerable<GetCategoryArticlesNewsPortal> newNewsPortals,
            int newNewsPortalsPosition
        )
        {
            Articles = articles;
            NewNewsPortals = newNewsPortals;
            NewNewsPortalsPosition = newNewsPortalsPosition;
        }

        public override string ToString()
        {
            return $"{nameof(Articles)}:{Articles.Count()}, " +
            $"{nameof(NewNewsPortals)}:{NewNewsPortals.Count()}, " +
            $"{nameof(NewNewsPortalsPosition)}:{NewNewsPortalsPosition}";
        }
    }
}
