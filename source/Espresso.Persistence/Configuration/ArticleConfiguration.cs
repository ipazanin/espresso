using Espresso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.Configuration
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            #region Property Mapping
            builder.Property(article => article.ArticleId)
                .HasMaxLength(Article.ArticleIdMaxLength);

            builder.Property(article => article.Summary)
                .HasMaxLength(Article.SummaryMaxLength);

            builder.Property(article => article.Title)
                .HasMaxLength(Article.TitleMaxLength);

            builder.Property(article => article.Url)
                .HasMaxLength(Article.UrlMaxLength);

            builder.Property(article => article.Title)
                .HasMaxLength(Article.ImageUrlMaxLength);

            builder.Property(article => article.IsHidden)
                .HasDefaultValue(Article.IsHiddenDefaultValue);

            builder.Property(article => article.IsHidden)
                .HasDefaultValue(Article.IsFeaturedDefaultValue);

            builder.Ignore(article => article.CreateArticleCategories);
            builder.Ignore(article => article.DeleteArticleCategories);
            #endregion

            builder.HasIndex(article => article.PublishDateTime);
            builder.HasIndex(article => article.TrendingScore);

            builder.HasOne(article => article.NewsPortal)
                .WithMany(newsPortal => newsPortal!.Articles)
                .HasForeignKey(article => article.NewsPortalId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(article => article.RssFeed)
                .WithMany(rssFeed => rssFeed!.Articles)
                .HasForeignKey(article => article.RssFeedId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(article => article.ArticleCategories)
                .WithOne(articleCatgory => articleCatgory!.Article!)
                .HasForeignKey(articleCategory => articleCategory.ArticleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
