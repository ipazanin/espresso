// CategoryConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Extensions;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.CategoryEnums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Espresso.Persistence.Configuration
{
    /// <summary>
    /// <see cref="Category"/> entity configuration.
    /// </summary>
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(category => category.Name)
                .HasMaxLength(Category.NameHasMaxLenght);

            builder.Property(category => category.Color)
                .HasMaxLength(Category.ColorHasMaxLenght);

            builder.Property(category => category.KeyWordsRegexPattern)
                .HasMaxLength(Category.KeyWordsRegexPatterHasMaxLenght);

            builder.Property(category => category.Url)
                .HasMaxLength(Category.UrlHasMaxLength);

            builder.HasMany(category => category.ArticleCategories)
                .WithOne(articleCategory => articleCategory.Category!)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(articleCategory => articleCategory.CategoryId);

            builder.HasMany(category => category.RssFeeds)
                .WithOne(rssFeed => rssFeed.Category!)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(articleCategory => articleCategory.CategoryId);

            builder.HasMany(category => category.NewsPortals)
                .WithOne(newsPortal => newsPortal.Category!)
                .HasForeignKey(newsPortal => newsPortal.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            Seed(builder);
        }

        private static void Seed(EntityTypeBuilder<Category> builder)
        {
            var categories = new List<Category>
            {
                new Category(
                    id: (int)CategoryId.Vijesti,
                    name: CategoryId.Vijesti.GetDisplayName(),
                    color: "#E84855",
                    keyWordsRegexPattern: null,
                    sortIndex: 2,
                    position: null,
                    categoryType: CategoryType.Normal,
                    categoryUrl: "/vijesti"
                ),
                new Category(
                    id: (int)CategoryId.Sport,
                    name: CategoryId.Sport.GetDisplayName(),
                    color: "#4CB944",
                    keyWordsRegexPattern: null,
                    sortIndex: 3,
                    position: null,
                    categoryType: CategoryType.Normal,
                    categoryUrl: "/sport"
                ),
                new Category(
                    id: (int)CategoryId.Show,
                    name: CategoryId.Show.GetDisplayName(),
                    color: "#F4B100",
                    keyWordsRegexPattern: null,
                    sortIndex: 4,
                    position: null,
                    categoryType: CategoryType.Normal,
                    categoryUrl: "/show"
                ),
                new Category(
                    id: (int)CategoryId.Lifestyle,
                    name: CategoryId.Lifestyle.GetDisplayName(),
                    color: "#32936F",
                    keyWordsRegexPattern: null,
                    sortIndex: 5,
                    position: null,
                    categoryType: CategoryType.Normal,
                    categoryUrl: "/lifestyle"
                ),
                new Category(
                    id: (int)CategoryId.Tech,
                    name: CategoryId.Tech.GetDisplayName(),
                    color: "#2E86AB",
                    keyWordsRegexPattern: null,
                    sortIndex: 6,
                    position: null,
                    categoryType: CategoryType.Normal,
                    categoryUrl: "/tech"
                ),
                new Category(
                    id: (int)CategoryId.Viral,
                    name: CategoryId.Viral.GetDisplayName(),
                    color: "#9055A2",
                    keyWordsRegexPattern: null,
                    sortIndex: 7,
                    position: null,
                    categoryType: CategoryType.Normal,
                    categoryUrl: "/viral"
                ),
                new Category(
                    id: (int)CategoryId.Biznis,
                    name: CategoryId.Biznis.GetDisplayName(),
                    color: "#3185FC",
                    keyWordsRegexPattern: null,
                    sortIndex: 8,
                    position: null,
                    categoryType: CategoryType.Normal,
                    categoryUrl: "/biznis"
                ),
                new Category(
                    id: (int)CategoryId.AutoMoto,
                    name: CategoryId.AutoMoto.GetDisplayName(),
                    color: "#FC814A",
                    keyWordsRegexPattern: null,
                    sortIndex: 9,
                    position: null,
                    categoryType: CategoryType.Normal,
                    categoryUrl: "/auto-moto"
                ),
                new Category(
                    id: (int)CategoryId.Kultura,
                    name: CategoryId.Kultura.GetDisplayName(),
                    color: "#AC80A0",
                    keyWordsRegexPattern: null,
                    sortIndex: 10,
                    position: null,
                    categoryType: CategoryType.Normal,
                    categoryUrl: "/kultura"
                ),
                new Category(
                    id: (int)CategoryId.General,
                    name: CategoryId.General.GetDisplayName(),
                    color: "#AC80A0",
                    keyWordsRegexPattern: null,
                    sortIndex: 1,
                    position: null,
                    categoryType: CategoryType.General,
                    categoryUrl: "/general"
                ),
                new Category(
                    id: (int)CategoryId.Local,
                    name: CategoryId.Local.GetDisplayName(),
                    color: "#AC80A0",
                    keyWordsRegexPattern: null,
                    sortIndex: null,
                    position: 3,
                    categoryType: CategoryType.Local,
                    categoryUrl: "/local"
                ),
            };

            builder.HasData(categories);
        }
    }
}
