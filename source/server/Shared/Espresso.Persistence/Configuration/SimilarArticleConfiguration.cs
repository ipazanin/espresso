// SimilarArticleConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.Configuration;

/// <summary>
/// <see cref="SimilarArticle"/> entity configuration.
/// </summary>
public class SimilarArticleConfiguration : IEntityTypeConfiguration<SimilarArticle>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<SimilarArticle> builder)
    {
        builder.HasOne(similarArticle => similarArticle.FirstArticle)
            .WithMany(article => article!.FirstSimilarArticles)
            .HasForeignKey(similarArticle => similarArticle.FirstArticleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(similarArticle => similarArticle.SecondArticle)
            .WithMany(article => article!.SecondSimilarArticles)
            .HasForeignKey(similarArticle => similarArticle.SecondArticleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
