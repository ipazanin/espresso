using Espresso.Domain.Entities;
using Espresso.Domain.Enums.RssFeedEnums;
using Espresso.Domain.ValueObjects.RssFeedValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.DataSeed
{
    internal static class RssFeedCategoryParseConfigurationSeed
    {
        public static void Seed(
            EntityTypeBuilder<RssFeed> builder
        )
        {
            var categoryParseConfigurationBuilder = builder.OwnsOne(rssFeed => rssFeed.CategoryParseConfiguration);

            SeedCategoryParseConfiguration(categoryParseConfigurationBuilder);
        }
        private static void SeedCategoryParseConfiguration(
            OwnedNavigationBuilder<RssFeed, CategoryParseConfiguration> categoryParseConfigurationBuilder
        )
        {
            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.JutarnjiList,
                CategoryParseStrategy = CategoryParseStrategy.FromUrl

            });

            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.NetHr,
                CategoryParseStrategy = CategoryParseStrategy.FromUrl

            });

            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.VecernjiList,
                CategoryParseStrategy = CategoryParseStrategy.FromUrl

            });

            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Telegram,
                CategoryParseStrategy = CategoryParseStrategy.FromUrl
            });

            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Dnevnik,
                CategoryParseStrategy = CategoryParseStrategy.FromUrl
            });



            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.N1,
                CategoryParseStrategy = CategoryParseStrategy.FromUrl
            });



            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.NarodHr,
                CategoryParseStrategy = CategoryParseStrategy.FromUrl
            });

            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.StoPosto,
                CategoryParseStrategy = CategoryParseStrategy.FromUrl

            });

            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Dnevno,
                CategoryParseStrategy = CategoryParseStrategy.FromUrl

            });

            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Scena,
                CategoryParseStrategy = CategoryParseStrategy.FromUrl,
            });

            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Express,
                CategoryParseStrategy = CategoryParseStrategy.FromUrl,
            });

            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.OtvorenoHr,
                CategoryParseStrategy = CategoryParseStrategy.FromUrl,
            });

            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.GeoPolitika,
                CategoryParseStrategy = CategoryParseStrategy.FromUrl,
            });

            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Dnevno7,
                CategoryParseStrategy = CategoryParseStrategy.FromUrl,
            });

        }

    }
}
