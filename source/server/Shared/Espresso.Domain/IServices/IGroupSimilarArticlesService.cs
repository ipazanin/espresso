// IGroupSimilarArticlesService.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using System;
using System.Collections.Generic;

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
