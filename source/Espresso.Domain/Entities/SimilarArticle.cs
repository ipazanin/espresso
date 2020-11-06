using System;

namespace Espresso.Domain.Entities
{
    public class SimilarArticle
    {
        public Guid Id { get; private set; }

        public double SimilarityScore { get; private set; }

        public Guid MainArticleId { get; private set; }
        public Article? MainArticle { get; private set; }

        public Guid SubordinateArticleId { get; private set; }
        public Article? SubordinateArticle { get; private set; }

        private SimilarArticle()
        {
        }

        public SimilarArticle(
            Guid id,
            double similarityScore,
            Guid mainArticleId,
            Article? mainArticle,
            Guid subordinateArticleId,
            Article? subordinateArticle
        )
        {
            Id = id;
            SimilarityScore = similarityScore;
            MainArticleId = mainArticleId;
            MainArticle = mainArticle;
            SubordinateArticleId = subordinateArticleId;
            SubordinateArticle = subordinateArticle;
        }
    }
}