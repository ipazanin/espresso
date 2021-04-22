using Espresso.Domain.Entities;
using Espresso.Persistence.DataSeed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.Configuration
{
    public class NewsPortalConfiguration : IEntityTypeConfiguration<NewsPortal>
    {
        public void Configure(EntityTypeBuilder<NewsPortal> builder)
        {
            builder.Property(newsPortal => newsPortal.Name)
                .HasMaxLength(NewsPortal.NameMaxLength);

            builder.Property(newsPortal => newsPortal.BaseUrl)
                .HasMaxLength(NewsPortal.BaseUrlMaxLength);

            builder.Property(newsPortal => newsPortal.IconUrl)
                .HasMaxLength(NewsPortal.IconUrlMaxlength);

            builder.Property(newsPortal => newsPortal.IsEnabled)
                .HasDefaultValue(NewsPortal.IsEnabledDefaultValue);

            builder.HasMany(newsPortal => newsPortal.RssFeeds)
                .WithOne(rssFeed => rssFeed.NewsPortal!)
                .HasForeignKey(rssFeed => rssFeed.NewsPortalId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(newsPortal => newsPortal.Articles)
                .WithOne(article => article.NewsPortal!)
                .HasForeignKey(article => article.NewsPortalId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(newsPortal => newsPortal.Category)
                .WithMany(category => category!.NewsPortals)
                .HasForeignKey(newsPortal => newsPortal.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(newsPortal => newsPortal.Region)
                .WithMany(region => region!.NewsPortals)
                .HasForeignKey(newsPortal => newsPortal.RegionId)
                .OnDelete(DeleteBehavior.Cascade);

            NewsPortalDataSeed.SeedData(builder);
            LocalNewsPortalDataSeed.SeedData(builder);
        }
    }
}
