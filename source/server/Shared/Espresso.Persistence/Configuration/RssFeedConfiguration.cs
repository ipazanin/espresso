// RssFeedConfiguration.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Espresso.Domain.ValueObjects.RssFeedValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.Configuration;

/// <summary>
/// <see cref="RssFeed"/> entity configuration.
/// </summary>
public class RssFeedConfiguration : IEntityTypeConfiguration<RssFeed>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<RssFeed> builder)
    {
        _ = builder.Property(rssFeed => rssFeed.Url)
            .HasMaxLength(RssFeed.UrlMaxLength);

        ConfigureAmpConfiguration(builder);
        ConfigureSkipParseConfiguration(builder);
        ConfigureCategoryParseConfiguration(builder);
        ConfigureImageUrlParseConfiguration(builder);

        _ = builder.HasOne(rssFeed => rssFeed.NewsPortal)
            .WithMany(newsPortal => newsPortal!.RssFeeds)
            .HasForeignKey(rssFeed => rssFeed.NewsPortalId)
            .OnDelete(DeleteBehavior.Restrict);

        _ = builder.HasOne(rssFeed => rssFeed.Category)
            .WithMany(category => category!.RssFeeds)
            .HasForeignKey(rssFeed => rssFeed.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        _ = builder.HasMany(rssFeed => rssFeed.RssFeedCategories)
            .WithOne(rssFeedCategory => rssFeedCategory.RssFeed!)
            .HasForeignKey(rssFeedCategory => rssFeedCategory.RssFeedId)
            .OnDelete(DeleteBehavior.Cascade);

        _ = builder.HasMany(rssFeed => rssFeed.Articles)
            .WithOne(newArticle => newArticle.RssFeed!)
            .HasForeignKey(rssFeedCategory => rssFeedCategory.RssFeedId)
            .OnDelete(DeleteBehavior.Cascade);

        _ = builder.HasMany(rssFeed => rssFeed.RssFeedContentModifiers)
            .WithOne(rssFeedContentModifier => rssFeedContentModifier.RssFeed!)
            .HasForeignKey(rssFeedContentModifier => rssFeedContentModifier.RssFeedId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private static void ConfigureAmpConfiguration(EntityTypeBuilder<RssFeed> builder)
    {
        var ampConfigurationBuilder = builder.OwnsOne(rssFeed => rssFeed.AmpConfiguration);

        _ = ampConfigurationBuilder
            .Property(ampConfiguration => ampConfiguration!.TemplateUrl)
            .HasMaxLength(RssFeed.AmpConfigurationTemplateUrlMaxLength);

        _ = ampConfigurationBuilder
            .Property(ampConfiguration => ampConfiguration!.HasAmpArticles);
    }

    private static void ConfigureSkipParseConfiguration(EntityTypeBuilder<RssFeed> builder)
    {
        _ = builder.OwnsOne(rssFeed => rssFeed.SkipParseConfiguration);
    }

    private static void ConfigureCategoryParseConfiguration(EntityTypeBuilder<RssFeed> builder)
    {
        var categoryParseConfigurationBuilder = builder
            .OwnsOne(rssFeed => rssFeed.CategoryParseConfiguration);

        _ = categoryParseConfigurationBuilder
            .Property(categoryParseConfiguration => categoryParseConfiguration.CategoryParseStrategy)
            .HasDefaultValue(CategoryParseConfiguration.CategoryParseStrategyDefaultValue);
    }

    private static void ConfigureImageUrlParseConfiguration(EntityTypeBuilder<RssFeed> builder)
    {
        var imageUrlParseConfiguration = builder.OwnsOne(rssFeed => rssFeed.ImageUrlParseConfiguration);

        _ = imageUrlParseConfiguration
            .Property(imageUrlConfig => imageUrlConfig.XPath)
            .HasMaxLength(ImageUrlParseConfiguration.ImgElementXPathHasMaxLength)
            .HasDefaultValue(ImageUrlParseConfiguration.ImgElementXPathDefaultValue);

        _ = imageUrlParseConfiguration
            .Property(imageUrlConfig => imageUrlConfig.AttributeName)
            .HasMaxLength(ImageUrlParseConfiguration.AttributeNameMaxLength)
            .HasDefaultValue(ImageUrlParseConfiguration.AttributeNameDefaultValue);

        _ = imageUrlParseConfiguration
            .Property(imageUrlConfig => imageUrlConfig.ImageUrlParseStrategy)
            .HasDefaultValue(ImageUrlParseConfiguration.ImageUrlParseStrategyDefaultValue);

        _ = imageUrlParseConfiguration
            .Property(imageUrlConfig => imageUrlConfig.ShouldImageUrlBeWebScraped)
            .HasDefaultValue(ImageUrlParseConfiguration.ShouldImageUrlBeWebScrapedDefaultValue);

        _ = imageUrlParseConfiguration
            .Property(imageUrlConfig => imageUrlConfig.ImageUrlWebScrapeType)
            .HasDefaultValue(ImageUrlParseConfiguration.ImageUrlWebScrapeTypeDefaultValue);

        _ = imageUrlParseConfiguration
            .Property(imageUrlConfig => imageUrlConfig.JsonWebScrapePropertyNames)
            .HasMaxLength(ImageUrlParseConfiguration.JsonWebScrapePropertyNamesHasMaxLength)
            .HasDefaultValue(ImageUrlParseConfiguration.JsonWebScrapePropertyNamesDefaultValue);

        _ = imageUrlParseConfiguration
            .Property(imageUrlConfig => imageUrlConfig.ElementExtensionName)
            .HasMaxLength(ImageUrlParseConfiguration.ElementExtensionNameMaxLength);

        _ = imageUrlParseConfiguration
            .Property(imageUrlConfig => imageUrlConfig.ElementExtensionAttributeName)
            .HasMaxLength(ImageUrlParseConfiguration.ElementExtensionAttributeNameMaxLength);

        _ = imageUrlParseConfiguration
            .Property(imageUrlConfig => imageUrlConfig.WebScrapeRequestType)
            .HasDefaultValue(ImageUrlParseConfiguration.WebScrapeRequestTypeDefaultValue);
    }
}
