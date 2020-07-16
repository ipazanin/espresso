using System.Collections.Generic;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.CategoryEnums;
using Espresso.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(category => category.Name)
                .HasMaxLength(PropertyConstraintConstants.CategoryNameHasMaxLenght)
                .IsRequired(PropertyConstraintConstants.CategoryNameIsRequired);

            builder.Property(category => category.Color)
                .HasMaxLength(PropertyConstraintConstants.CategoryNameHasMaxLenght)
                .IsRequired(PropertyConstraintConstants.CategoryNameIsRequired);

            builder.Property(category => category.KeyWordsRegexPattern)
                .HasMaxLength(PropertyConstraintConstants.CategoryKeyWordsRegexPatterHasMaxLenght)
                .IsRequired(PropertyConstraintConstants.CategoryKeyWordsRegexPatternIsRequired);

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

        private void Seed(EntityTypeBuilder<Category> builder)
        {
            var categories = new List<Category>
            {
                new Category(
                    id:(int)CategoryId.Vijesti,
                    name: CategoryId.Vijesti.GetDisplayName(),
                    color: "#E84855",
                    keyWordsRegexPattern: null,
                    sortIndex: 2
                ),
                new Category(
                    id:(int)CategoryId.Sport,
                    name: CategoryId.Sport.GetDisplayName(),
                    color: "#4CB944",
                    keyWordsRegexPattern: null,
                    sortIndex: 3
                ),
                new Category(
                    id:(int)CategoryId.Show,
                    name: CategoryId.Show.GetDisplayName(),
                    color:"#F4B100",
                    keyWordsRegexPattern: null,
                    sortIndex: 4
                ),
                new Category(
                    id:(int)CategoryId.Lifestyle,
                    name: CategoryId.Lifestyle.GetDisplayName(),
                    color: "#32936F",
                    keyWordsRegexPattern: null,
                    sortIndex: 5
                ),
                new Category(
                    id:(int)CategoryId.Tech,
                    name: CategoryId.Tech.GetDisplayName(),
                    color: "#2E86AB",
                    keyWordsRegexPattern: null,
                    sortIndex: 6
                ),
                new Category(
                    id:(int)CategoryId.Viral,
                    name: CategoryId.Viral.GetDisplayName(),
                    color: "#9055A2",
                    keyWordsRegexPattern: null,
                    sortIndex: 7
                ),
                new Category(
                    id:(int)CategoryId.Biznis,
                    name: CategoryId.Biznis.GetDisplayName(),
                    color: "#3185FC",
                    keyWordsRegexPattern: null,
                    sortIndex: 8
                ),
                new Category(
                    id:(int)CategoryId.AutoMoto,
                    name: CategoryId.AutoMoto.GetDisplayName(),
                    color: "#FC814A",
                    keyWordsRegexPattern: null,
                    sortIndex: 9
                ),
                new Category(
                    id:(int)CategoryId.Kultura,
                    name: CategoryId.Kultura.GetDisplayName(),
                    color: "#AC80A0",
                    keyWordsRegexPattern: null,
                    sortIndex: 10
                ),
                new Category(
                    id:(int)CategoryId.General,
                    name: CategoryId.General.GetDisplayName(),
                    color: "#AC80A0",
                    keyWordsRegexPattern: null,
                    sortIndex: 1
                ),
            };

            builder.HasData(categories);
        }
    }
}
