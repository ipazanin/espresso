using System;
using System.Collections.Generic;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.CategoryEnums;
using Espresso.Domain.Enums.NewsPortalEnums;
using Espresso.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.Configuration
{
    public class NewsPortalConfiguration : IEntityTypeConfiguration<NewsPortal>
    {
        public void Configure(EntityTypeBuilder<NewsPortal> builder)
        {
            builder.Property(newsPortal => newsPortal.Name)
                .HasMaxLength(PropertyConstraintConstants.NEWSPORTAL_NAME_HASMAXLENGHT)
                .IsRequired(PropertyConstraintConstants.NEWSPORTAL_NAME_ISREQUIRED);

            builder.Property(newsPortal => newsPortal.BaseUrl)
                .HasMaxLength(PropertyConstraintConstants.NEWSPORTAL_BASEURL_HASMAXLENGHT)
                .IsRequired(PropertyConstraintConstants.NEWSPORTAL_BASEURL_ISREQUIRED);

            builder.Property(newsPortal => newsPortal.IconUrl)
                .HasMaxLength(PropertyConstraintConstants.NEWSPORTAL_ICONURL_HASMAXLENGHT)
                .IsRequired(PropertyConstraintConstants.NEWSPORTAL_ICONURL_ISREQUIRED);

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

            SeedData(builder);
        }

        private void SeedData(EntityTypeBuilder<NewsPortal> builder)
        {
            var newsPortals = new List<NewsPortal>
            {
                new NewsPortal(
                    id:(int)NewsPortalId.Index,
                    name: NewsPortalId.Index.GetDisplayName(),
                    baseUrl: "https://www.index.hr/",
                    iconUrl: $"Icons/{NewsPortalId.Index}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1),
                    categoryId: (int)CategoryId.General
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.DvadesetCetiriSata,
                    name: NewsPortalId.DvadesetCetiriSata.GetDisplayName(),
                    baseUrl: "https://www.24sata.hr/",
                    iconUrl: $"Icons/{NewsPortalId.DvadesetCetiriSata}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1),
                    categoryId: (int)CategoryId.General
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.SportskeNovosti,
                    name: NewsPortalId.SportskeNovosti.GetDisplayName(),
                    baseUrl: "https://sportske.jutarnji.hr/",
                    iconUrl: $"Icons/{NewsPortalId.SportskeNovosti}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1),
                    categoryId: (int)CategoryId.Sport),
                new NewsPortal(
                    id:(int)NewsPortalId.JutarnjiList,
                    name: NewsPortalId.JutarnjiList.GetDisplayName(),
                    baseUrl: "https://sportske.jutarnji.hr/",
                    iconUrl: $"Icons/{NewsPortalId.JutarnjiList}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1),
                    categoryId: (int)CategoryId.General
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.NetHr,
                    name: NewsPortalId.NetHr.GetDisplayName(),
                    baseUrl: "https://net.hr/",
                    iconUrl: $"Icons/{NewsPortalId.NetHr}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1),
                    categoryId: (int)CategoryId.General
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.SlobodnaDalmacija,
                    name: NewsPortalId.SlobodnaDalmacija.GetDisplayName(),
                    baseUrl: "https://slobodnadalmacija.hr/",
                    iconUrl: $"Icons/{NewsPortalId.SlobodnaDalmacija}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1),
                    categoryId: (int)CategoryId.General
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.TPortal,
                    name: NewsPortalId.TPortal.GetDisplayName(),
                    baseUrl: "https://www.tportal.hr/",
                    iconUrl: $"Icons/{NewsPortalId.TPortal}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1),
                    categoryId: (int)CategoryId.General
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.VecernjiList,
                    name: NewsPortalId.VecernjiList.GetDisplayName(),
                    baseUrl: "https://www.vecernji.hr/",
                    iconUrl: $"Icons/{NewsPortalId.VecernjiList}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1),
                    categoryId: (int)CategoryId.General
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.Telegram,
                    name: NewsPortalId.Telegram.GetDisplayName(),
                    baseUrl: "https://www.telegram.hr/",
                    iconUrl: $"Icons/{NewsPortalId.Telegram}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1),
                    categoryId: (int)CategoryId.General
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.Dnevnik,
                    name: NewsPortalId.Dnevnik.GetDisplayName(),
                    baseUrl: "https://dnevnik.hr/",
                    iconUrl: $"Icons/{NewsPortalId.Dnevnik}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1),
                    categoryId: (int)CategoryId.General
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.Gol,
                    name: NewsPortalId.Gol.GetDisplayName(),
                    baseUrl: "https://gol.dnevnik.hr/",
                    iconUrl: $"Icons/{NewsPortalId.Gol}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1),
                    categoryId: (int)CategoryId.Sport
                ),

                new NewsPortal(
                    id:(int)NewsPortalId.RtlVijesti,
                    name: NewsPortalId.RtlVijesti.GetDisplayName(),
                    baseUrl: "https://sportnet.rtl.hr/",
                    iconUrl: $"Icons/{NewsPortalId.RtlVijesti}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1),
                    categoryId: (int)CategoryId.General
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.NogometPlus,
                    name: NewsPortalId.NogometPlus.GetDisplayName(),
                    baseUrl: "http://www.nogometplus.net/",
                    iconUrl: $"Icons/{NewsPortalId.NogometPlus}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1),
                    categoryId: (int)CategoryId.Sport
                ), // Nemaju SLS LUL
                new NewsPortal(
                    id:(int)NewsPortalId.Lider,
                    name: NewsPortalId.Lider.GetDisplayName(),
                    baseUrl: "https://lider.media/",
                    iconUrl: $"Icons/{NewsPortalId.Lider}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1), categoryId: (int)CategoryId.Biznis),
                new NewsPortal(
                    id:(int)NewsPortalId.Bug,
                    name: NewsPortalId.Bug.GetDisplayName(),
                    baseUrl: "https://www.bug.hr/",
                    iconUrl: $"Icons/{NewsPortalId.Bug}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1),
                    categoryId: (int)CategoryId.Tech
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.VidiHr,
                    name: NewsPortalId.VidiHr.GetDisplayName(),
                    baseUrl: "https://www.vidi.hr/",
                    iconUrl: $"Icons/{NewsPortalId.VidiHr}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1),
                    categoryId: (int)CategoryId.Tech
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.Zimo,
                    name: NewsPortalId.Zimo.GetDisplayName(),
                    baseUrl: "https://zimo.dnevnik.hr/",
                    iconUrl: $"Icons/{NewsPortalId.Zimo}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1),
                    categoryId: (int)CategoryId.Tech
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.Netokracija,
                    name: NewsPortalId.Netokracija.GetDisplayName(),
                    baseUrl: "https://www.netokracija.com/",
                    iconUrl: $"Icons/{NewsPortalId.Netokracija}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1),
                    categoryId: (int)CategoryId.Tech
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.PoslovniPuls,
                    name: NewsPortalId.PoslovniPuls.GetDisplayName(),
                    baseUrl: "https://poslovnipuls.com/",
                    iconUrl: $"Icons/{NewsPortalId.PoslovniPuls}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1),
                    categoryId: (int)CategoryId.Biznis
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.PcChip,
                    name: NewsPortalId.PcChip.GetDisplayName(),
                    baseUrl: "https://pcchip.hr/",
                    iconUrl: $"Icons/{NewsPortalId.PcChip}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1),
                    categoryId: (int)CategoryId.Tech
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.Cosmopolitan,
                    name: NewsPortalId.Cosmopolitan.GetDisplayName(),
                    baseUrl: "http://www.cosmopolitan.hr/",
                    iconUrl: $"Icons/{NewsPortalId.Cosmopolitan}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1),
                    categoryId: (int)CategoryId.Lifestyle
                ), // Nemaju SLS LUL
                new NewsPortal(
                    id:(int)NewsPortalId.WallHr,
                    name: NewsPortalId.WallHr.GetDisplayName(),
                    baseUrl: "https://wall.hr/",
                    iconUrl: $"Icons/{NewsPortalId.WallHr}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1),
                    categoryId: (int)CategoryId.Lifestyle
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.LjepotaIZdravlje,
                    name: NewsPortalId.LjepotaIZdravlje.GetDisplayName(),
                    baseUrl: "http://www.ljepotaizdravlje.hr/",
                    iconUrl: $"Icons/{NewsPortalId.LjepotaIZdravlje}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1),
                    categoryId: (int)CategoryId.Lifestyle
                ), // Nemaju SLS LUL
                new NewsPortal(
                    id:(int)NewsPortalId.Autonet,
                    name: NewsPortalId.Autonet.GetDisplayName(),
                    baseUrl: "https://www.autonet.hr/",
                    iconUrl: $"Icons/{NewsPortalId.Autonet}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1),
                    categoryId: (int)CategoryId.AutoMoto
                ),

                new NewsPortal(
                    id:(int)NewsPortalId.N1,
                    name:NewsPortalId.N1.GetDisplayName(),
                    baseUrl:"https://hr.n1info.com/",
                    iconUrl:$"Icons/{NewsPortalId.N1}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1),
                    categoryId: (int)CategoryId.Vijesti
                ),

                new NewsPortal(
                    id:(int)NewsPortalId.NarodHr,
                    name:NewsPortalId.NarodHr.GetDisplayName(),
                    baseUrl:"https://narod.hr/",
                    iconUrl: $"Icons/{NewsPortalId.NarodHr}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt:new DateTime(2020, 5, 1),
                    categoryId:(int)CategoryId.Vijesti
                ),

                new NewsPortal(
                    id: (int)NewsPortalId.Hrt,
                    name: NewsPortalId.Hrt.GetDisplayName(),
                    baseUrl: "https://www.hrt.hr/",
                    iconUrl: $"Icons/{NewsPortalId.Hrt}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1),
                    categoryId: (int)CategoryId.Vijesti
                ),

                new NewsPortal(
                    id: (int)NewsPortalId.StoPosto,
                    name: NewsPortalId.StoPosto.GetDisplayName(),
                    baseUrl: "https://100posto.jutarnji.hr/",
                    iconUrl: $"Icons/{NewsPortalId.StoPosto}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1),
                    categoryId: (int)CategoryId.Vijesti
                ),

                new NewsPortal(
                    id: (int)NewsPortalId.Dnevno,
                    name: NewsPortalId.Dnevno.GetDisplayName(),
                    baseUrl: "https://www.dnevno.hr/",
                    iconUrl: $"Icons/{NewsPortalId.Dnevno}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1),
                    categoryId: (int)CategoryId.Vijesti
                ),

                // new NewsPortal(
                //     id: (int)NewsPortalId.AutomobiliHr,
                //     name: NewsPortalId.AutomobiliHr.GetDisplayName(),
                //     baseUrl: "https://automobili.klik.hr/",
                //     iconUrl: $"Icons/{NewsPortalId.AutomobiliHr}{FileExtensionConstants.Png}", isNewOverride: null, createdAt: new DateTime(2020, 5, 1), categoryId: (int)CategoryId.General
                // ),

                new NewsPortal(
                    id: (int)NewsPortalId.DirektnoHr,
                    name: NewsPortalId.DirektnoHr.GetDisplayName(),
                    baseUrl: "https://direktno.hr/",
                    iconUrl: $"Icons/{NewsPortalId.DirektnoHr}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1), categoryId: (int)CategoryId.Vijesti
                ),
            };

            builder.HasData(newsPortals);
        }
    }
}
