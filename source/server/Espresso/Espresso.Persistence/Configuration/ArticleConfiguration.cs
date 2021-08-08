// ArticleConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

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

            var editorConfigurationBuilder = builder.OwnsOne(article => article.EditorConfiguration);

            editorConfigurationBuilder.Property(editorConfiguration => editorConfiguration.IsHidden)
                .HasDefaultValue(EditorConfiguration.IsHiddenDefaultValue);

            editorConfigurationBuilder.Property(editorConfiguration => editorConfiguration.IsFeatured)
                .HasDefaultValue(EditorConfiguration.IsFeaturedDefaultValue);

            editorConfigurationBuilder.Property(editorConfiguration => editorConfiguration.FeaturedPosition)
                .HasDefaultValue(EditorConfiguration.FeaturedPositionDefaultValue);

            builder.HasIndex(article => article.PublishDateTime);

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

            builder.HasMany(article => article.SubordinateArticles)
                .WithOne(similarArticle => similarArticle!.MainArticle!)
                .HasForeignKey(similarArticle => similarArticle.MainArticleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(article => article.MainArticle)
                .WithOne(similarArticle => similarArticle!.SubordinateArticle!)
                .HasForeignKey<SimilarArticle>(similarArticle => similarArticle.SubordinateArticleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
