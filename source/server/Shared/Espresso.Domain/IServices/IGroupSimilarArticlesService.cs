// IGroupSimilarArticlesService.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;

namespace Espresso.Domain.IServices
{
    /// <summary>
    /// Groups similar articles.
    /// </summary>
    public interface IGroupSimilarArticlesService
    {
        /// <summary>
        /// Groups similar articles.
        /// </summary>
        /// <param name="articles">Articles.</param>
        /// <param name="subordinateArticleIds">Already grouped articles to skip.</param>
        /// <param name="lastSimilarityGroupingTime">Last time of grouping to avoid unecessary grouping.</param>
        /// <returns>Similar articles.</returns>
        public IEnumerable<SimilarArticle> GroupSimilarArticles(
            IEnumerable<Article> articles,
            ISet<Guid> subordinateArticleIds,
            DateTime lastSimilarityGroupingTime);
    }
}
