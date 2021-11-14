// SimilarArticle.cs
//
// © 2021 Espresso News. All rights reserved.

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

#pragma warning disable SA1201 // Elements should appear in the correct order
        private SimilarArticle()
#pragma warning restore SA1201 // Elements should appear in the correct order
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimilarArticle"/> class.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="similarityScore"></param>
        /// <param name="mainArticleId"></param>
        /// <param name="mainArticle"></param>
        /// <param name="subordinateArticleId"></param>
        /// <param name="subordinateArticle"></param>
        public SimilarArticle(
            Guid id,
            double similarityScore,
            Guid mainArticleId,
            Article? mainArticle,
            Guid subordinateArticleId,
            Article? subordinateArticle)
        {
            Id = id;
            SimilarityScore = similarityScore;
            MainArticleId = mainArticleId;
            MainArticle = mainArticle;
            SubordinateArticleId = subordinateArticleId;
            SubordinateArticle = subordinateArticle;
        }

        public void SetMainArticle(Article mainArticle)
        {
            MainArticle = mainArticle;
            MainArticleId = mainArticle.Id;
        }

        public void SetSubordinateArticle(Article subordinateArticle)
        {
            SubordinateArticle = subordinateArticle;
            SubordinateArticleId = subordinateArticle.Id;
        }
    }
}
