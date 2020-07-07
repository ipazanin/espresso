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
                new Category((int)CategoryId.Vijesti, CategoryId.Vijesti.GetDisplayName(), "#E84855", null),
                new Category((int)CategoryId.Sport, CategoryId.Sport.GetDisplayName(), "#4CB944", null),
                new Category((int)CategoryId.Show, CategoryId.Show.GetDisplayName(), "#F4B100", null),
                new Category((int)CategoryId.Lifestyle, CategoryId.Lifestyle.GetDisplayName(), "#32936F", null),
                new Category((int)CategoryId.Tech, CategoryId.Tech.GetDisplayName(), "#2E86AB", null),
                new Category((int)CategoryId.Viral, CategoryId.Viral.GetDisplayName(), "#9055A2", null),
                new Category((int)CategoryId.Biznis, CategoryId.Biznis.GetDisplayName(), "#3185FC", null),
                new Category((int)CategoryId.AutoMoto, CategoryId.AutoMoto.GetDisplayName(), "#FC814A", null),
                new Category((int)CategoryId.Kultura, CategoryId.Kultura.GetDisplayName(), "#AC80A0", null),
                new Category((int)CategoryId.General, CategoryId.General.GetDisplayName(), "#AC80A0", null),
            };

            builder.HasData(categories);
        }
    }
}
