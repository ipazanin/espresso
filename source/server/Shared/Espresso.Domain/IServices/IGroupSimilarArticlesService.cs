// IGroupSimilarArticlesService.cs
//
// © 2021 Espresso News. All rights reserved.

using System;
using System.Collections.Generic;
using Espresso.Domain.Entities;

namespace Espresso.Domain.IServices
{
    public interface IGroupSimilarArticlesService
    {
        public IEnumerable<SimilarArticle> GroupSimilarArticles(
            IEnumerable<Article> articles,
            ISet<Guid> subordinateArticleIds,
            DateTime lastSimilarityGroupingTime
        );
    }
}
