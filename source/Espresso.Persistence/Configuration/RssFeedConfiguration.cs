using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Persistence.DataSeed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.Configuration
{
    public class RssFeedConfiguration : IEntityTypeConfiguration<RssFeed>
    {
        public void Configure(EntityTypeBuilder<RssFeed> builder)
        {
            #region Property Mapping
            builder.Property(rssFeed => rssFeed.Url)
                .HasMaxLength(PropertyConstraintConstants.RssFeedUrlHasMaxLength);
            #endregion

            #region Value Object Mapping

            #region AmpConfiguration
            var ampConfigurationBuilder = builder.OwnsOne(rssFeed => rssFeed.AmpConfiguration);

            ampConfigurationBuilder
                .Property(ampConfiguration => ampConfiguration!.TemplateUrl)
                .HasMaxLength(PropertyConstraintConstants.RssFeedAmpConfigurationTemplateUrlHasMaxLength);

            ampConfigurationBuilder
                .Property(ampConfiguration => ampConfiguration!.HasAmpArticles);
            #endregion

            #region SkipParseConfiguration
            var skipParseConfigurationBuilder = builder.OwnsOne(rssFeed => rssFeed.SkipParseConfiguration);
            #endregion

            #region CategoryParseConfiguration
            var categoryParseConfigurationBuilder = builder
                .OwnsOne(rssFeed => rssFeed.CategoryParseConfiguration);

            categoryParseConfigurationBuilder
                .Property(categoryParseConfiguration => categoryParseConfiguration.CategoryParseStrategy);
            #endregion

            #region ImageUrlParseConfiguration
            var imageUrlParseConfiguration = builder.OwnsOne(rssFeed => rssFeed.ImageUrlParseConfiguration);

            imageUrlParseConfiguration.Property(imageUrlConfig => imageUrlConfig.ImgElementXPath)
                .HasMaxLength(PropertyConstraintConstants.RssFeedImgElementXPathHasMaxLength);

            imageUrlParseConfiguration
                .Property(imageUrlConfig => imageUrlConfig.ImageUrlParseStrategy);

            imageUrlParseConfiguration
                .Property(imageUrlConfig => imageUrlConfig.ShouldImageUrlBeWebScraped)
                .HasDefaultValue(false);
            #endregion

            #endregion

            #region Relations Mapping
            builder.HasOne(rssFeed => rssFeed.NewsPortal)
                .WithMany(newsPortal => newsPortal!.RssFeeds)
                .HasForeignKey(rssFeed => rssFeed.NewsPortalId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(rssFeed => rssFeed.Category)
                .WithMany(category => category!.RssFeeds)
                .HasForeignKey(rssFeed => rssFeed.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(rssFeed => rssFeed.RssFeedCategories)
                .WithOne(rssFeedCategory => rssFeedCategory.RssFeed!)
                .HasForeignKey(rssFeedCategory => rssFeedCategory.RssFeedId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(rssFeed => rssFeed.Articles)
                .WithOne(newArticle => newArticle.RssFeed!)
                .HasForeignKey(rssFeedCategory => rssFeedCategory.RssFeedId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Data Seed
            RssFeedDataSeed.Seed(
                builder: builder,
                ampConfigurationBuilder: ampConfigurationBuilder!,
                skipParseConfigurationBuilder: skipParseConfigurationBuilder!,
                categoryParseConfigurationBuilder: categoryParseConfigurationBuilder,
                imageUrlParseConfigurationBuilder: imageUrlParseConfiguration
            );
            #endregion
        }
    }
}
