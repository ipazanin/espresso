using Espresso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.Configuration
{
    public class SimilarArticleConfiguration : IEntityTypeConfiguration<SimilarArticle>
    {
        public void Configure(EntityTypeBuilder<SimilarArticle> builder)
        {
            #region Relationships
            builder.HasOne(similarArticle => similarArticle.MainArticle)
                .WithMany(article => article!.SubordinateArticles)
                .HasForeignKey(similarArticle => similarArticle.MainArticleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(similarArticle => similarArticle.SubordinateArticle)
                .WithOne(article => article!.MainArticle!)
                .HasForeignKey<SimilarArticle>(similarArticle => similarArticle.SubordinateArticleId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}
