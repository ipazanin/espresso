using System;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using Espresso.Domain.Entities;

namespace Espresso.Application.DataTransferObjects.ArticleDataTransferObjects
{
    public record SimilarArticleDto
    {
        #region Properties
        public Guid Id { get; private set; }
        public double SimilarityScore { get; private set; }
        public Guid MainArticleId { get; private set; }
        public Guid SubordinateArticleId { get; private set; }
        #endregion

        #region Constructors
        [JsonConstructor]
        public SimilarArticleDto(
            Guid id,
            double similarityScore,
            Guid mainArticleId,
            Guid subordinateArticleId
        )
        {
            Id = id;
            SimilarityScore = similarityScore;
            MainArticleId = mainArticleId;
            SubordinateArticleId = subordinateArticleId;
        }

        private SimilarArticleDto()
        {
        }
        #endregion

        #region Methods
        public static Expression<Func<SimilarArticle, SimilarArticleDto>> GetProjection()
        {
            return similarArticle => new SimilarArticleDto
            {
                Id = similarArticle.Id,
                SimilarityScore = similarArticle.SimilarityScore,
                MainArticleId = similarArticle.MainArticleId,
                SubordinateArticleId = similarArticle.SubordinateArticleId
            };
        }
        #endregion
    }
}