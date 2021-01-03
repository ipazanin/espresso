using System.Collections.Generic;
using Espresso.Domain.Entities;

namespace Espresso.Persistence.IRepositories
{
    public interface ISimilarArticleRepository
    {
        public void InsertSimilarArticles(IEnumerable<SimilarArticle> similarArticles);
    }
}