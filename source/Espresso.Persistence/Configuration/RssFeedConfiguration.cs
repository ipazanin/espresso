using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Domain.ValueObjects.RssFeedValueObjects;
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
            ConfigureAmpConfiguration(builder);
            ConfigureSkipParseConfiguration(builder);
            ConfigureCategoryParseConfiguration(builder);
            ConfigureImageUrlParseConfiguration(builder);
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
            RssFeedDataSeed.Seed(builder: builder);
            #endregion
        }

        private void ConfigureAmpConfiguration(EntityTypeBuilder<RssFeed> builder)
        {
            var ampConfigurationBuilder = builder.OwnsOne(rssFeed => rssFeed.AmpConfiguration);

            ampConfigurationBuilder
                .Property(ampConfiguration => ampConfiguration!.TemplateUrl)
                .HasMaxLength(PropertyConstraintConstants.RssFeedAmpConfigurationTemplateUrlHasMaxLength);

            ampConfigurationBuilder
                .Property(ampConfiguration => ampConfiguration!.HasAmpArticles);
        }

        private void ConfigureSkipParseConfiguration(EntityTypeBuilder<RssFeed> builder)
        {

            var skipParseConfigurationBuilder = builder.OwnsOne(rssFeed => rssFeed.SkipParseConfiguration);
        }

        private void ConfigureCategoryParseConfiguration(EntityTypeBuilder<RssFeed> builder)
        {
            var categoryParseConfigurationBuilder = builder
                .OwnsOne(rssFeed => rssFeed.CategoryParseConfiguration);

            categoryParseConfigurationBuilder
                .Property(categoryParseConfiguration => categoryParseConfiguration.CategoryParseStrategy)
                .HasDefaultValue(CategoryParseConfiguration.CategoryParseStrategyDefaultValue);
        }

        private void ConfigureImageUrlParseConfiguration(EntityTypeBuilder<RssFeed> builder)
        {
            var imageUrlParseConfiguration = builder.OwnsOne(rssFeed => rssFeed.ImageUrlParseConfiguration);

            imageUrlParseConfiguration
                .Property(imageUrlConfig => imageUrlConfig.ImgElementXPath)
                .HasMaxLength(ImageUrlParseConfiguration.ImgElementXPathHasMaxLength)
                .HasDefaultValue(ImageUrlParseConfiguration.ImgElementXPathDefaultValue);

            imageUrlParseConfiguration
                .Property(imageUrlConfig => imageUrlConfig.ImageUrlParseStrategy)
                .HasDefaultValue(ImageUrlParseConfiguration.ImageUrlParseStrategyDefaultValue);

            imageUrlParseConfiguration
                .Property(imageUrlConfig => imageUrlConfig.ShouldImageUrlBeWebScraped)
                .HasDefaultValue(ImageUrlParseConfiguration.ShouldImageUrlBeWebScrapedDefaultValue);

            imageUrlParseConfiguration
                .Property(imageUrlConfig => imageUrlConfig.ImageUrlWebScrapeType)
                .HasDefaultValue(ImageUrlParseConfiguration.ImageUrlWebScrapeTypeDefaultValue);

            imageUrlParseConfiguration
                .Property(imageUrlConfig => imageUrlConfig.JsonWebScrapePropertyNames)
                .HasMaxLength(ImageUrlParseConfiguration.JsonWebScrapePropertyNamesHasMaxLength)
                .HasDefaultValue(ImageUrlParseConfiguration.JsonWebScrapePropertyNamesDefaultValue);

        }

    }
}
