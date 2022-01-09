// SimilarArticle.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.Domain.Entities;

public class SimilarArticle
{
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
        FirstArticleId = mainArticleId;
        FirstArticle = mainArticle;
        SecondArticleId = subordinateArticleId;
        SecondArticle = subordinateArticle;
    }

    private SimilarArticle()
    {
    }

    public Guid Id { get; private set; }

    public double SimilarityScore { get; private set; }

    public Guid FirstArticleId { get; private set; }

    public Article? FirstArticle { get; private set; }

    public Guid SecondArticleId { get; private set; }

    public Article? SecondArticle { get; private set; }

    public void SetFirstArticle(Article firstArticle)
    {
        FirstArticle = firstArticle;
        FirstArticleId = firstArticle.Id;
    }

    public void SetSecondArticle(Article secondArticle)
    {
        SecondArticle = secondArticle;
        SecondArticleId = secondArticle.Id;
    }
}
