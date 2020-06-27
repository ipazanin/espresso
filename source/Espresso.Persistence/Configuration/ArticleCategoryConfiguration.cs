using Espresso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.Configuration
{
    public class ArticleCategoryConfiguration : IEntityTypeConfiguration<ArticleCategory>
    {
        public void Configure(EntityTypeBuilder<ArticleCategory> builder)
        {
            builder.HasOne(articleCategory => articleCategory.Article)
                .WithMany(article => article!.ArticleCategories)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(articleCategory => articleCategory.ArticleId);

            builder.HasOne(articleCategory => articleCategory.Category)
                .WithMany(category => category!.ArticleCategories)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(articleCategory => articleCategory.CategoryId);
        }
    }
}
