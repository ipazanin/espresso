using System;
using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.Application.DataTransferObjects
{
    public record SimilarArticleDto
    {
        #region Properties
        public Guid Id { get; init; }
        public Guid MainArticleId { get; init; }
        public Guid SubordinateArticleId { get; init; }
        public double SimilarityScore { get; init; }
        #endregion

        #region Methods
        public SimilarArticle CreateSimilarArticle(Article mainArticle, Article subordinateArticle)
        {
            return new SimilarArticle(
                id: Id,
                similarityScore: SimilarityScore,
                mainArticleId: MainArticleId,
                mainArticle: mainArticle,
                subordinateArticleId: SubordinateArticleId,
                subordinateArticle: subordinateArticle
            );
        }

        public static Expression<Func<SimilarArticle, SimilarArticleDto>> GetProjection()
        {
            return similarArticle => new SimilarArticleDto
            {
                Id = similarArticle.Id,
                SimilarityScore = similarArticle.SimilarityScore,
                MainArticleId = similarArticle.MainArticleId,
                SubordinateArticleId = similarArticle.SubordinateArticleId,
            };
        }
        #endregion
    }
}