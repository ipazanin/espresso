using Espresso.Domain.Entities;
using Espresso.Domain.ValueObjects.ArticleValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.Configuration
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            #region Property Mapping
            builder.Property(article => article.Summary)
                .HasMaxLength(Article.SummaryMaxLength);

            builder.Property(article => article.Title)
                .HasMaxLength(Article.TitleMaxLength);

            builder.Property(article => article.Url)
                .HasMaxLength(Article.UrlMaxLength);

            builder.Property(article => article.WebUrl)
                .HasMaxLength(Article.WebUrlMaxLength);

            builder.Property(article => article.Title)
                .HasMaxLength(Article.ImageUrlMaxLength);

            // TODO: get rid of these properties
            builder.Ignore(article => article.CreateArticleCategories);
            builder.Ignore(article => article.DeleteArticleCategories);
            #endregion

            #region ValueObjects
            var editorConfigurationBuilder = builder.OwnsOne(article => article.EditorConfiguration);

            editorConfigurationBuilder.Property(editorConfiguration => editorConfiguration.IsHidden)
                .HasDefaultValue(EditorConfiguration.IsHiddenDefaultValue);

            editorConfigurationBuilder.Property(editorConfiguration => editorConfiguration.IsFeatured)
                .HasDefaultValue(EditorConfiguration.IsFeaturedDefaultValue);

            editorConfigurationBuilder.Property(editorConfiguration => editorConfiguration.FeaturedPosition)
                .HasDefaultValue(EditorConfiguration.FeaturedPositionDefaultValue);
            #endregion

            #region Indices
            builder.HasIndex(article => article.PublishDateTime);
            #endregion

            #region Relationships
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
            #endregion
        }
    }
}
