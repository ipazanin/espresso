// SimilarArticleDto.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Linq.Expressions;
using System.Text.Json.Serialization;
using Espresso.Domain.Entities;

namespace Espresso.Application.DataTransferObjects.ArticleDataTransferObjects;

public record SimilarArticleDto
{
    /// <summary>
    /// Gets id.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets similarity score.
    /// </summary>
    public double SimilarityScore { get; private set; }

    /// <summary>
    /// Gets man article id.
    /// </summary>
    public Guid MainArticleId { get; private set; }

    /// <summary>
    /// Gets subordinate article id.
    /// </summary>
    public Guid SubordinateArticleId { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SimilarArticleDto"/> class.
    /// </summary>
    /// <param name="id">Id.</param>
    /// <param name="similarityScore">Similarity score.</param>
    /// <param name="mainArticleId">Main article id.</param>
    /// <param name="subordinateArticleId">Subordinate article id.</param>
    [JsonConstructor]
    public SimilarArticleDto(
        Guid id,
        double similarityScore,
        Guid mainArticleId,
        Guid subordinateArticleId)
    {
        Id = id;
        SimilarityScore = similarityScore;
        MainArticleId = mainArticleId;
        SubordinateArticleId = subordinateArticleId;
    }

    private SimilarArticleDto()
    {
    }

    /// <summary>
    /// Creates <see cref="SimilarArticle"/> to <see cref="SimilarArticleDto"/> projection.
    /// </summary>
    /// <returns><see cref="SimilarArticle"/> to <see cref="SimilarArticleDto"/> projection.</returns>
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
}
