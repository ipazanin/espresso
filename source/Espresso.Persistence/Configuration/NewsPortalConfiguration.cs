using System.Collections.Generic;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
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

            SeedData(builder);
        }

        private void SeedData(EntityTypeBuilder<NewsPortal> builder)
        {
            var newsPortals = new List<NewsPortal>
            {
                new NewsPortal((int)NewsPortalId.Index, NewsPortalId.Index.GetDisplayName(), "https://www.index.hr/", $"Icons/{NewsPortalId.Index}{FileExtensionConstants.Png}"),
                new NewsPortal((int)NewsPortalId.DvadesetCetiriSata, NewsPortalId.DvadesetCetiriSata.GetDisplayName(), "https://www.24sata.hr/", $"Icons/{NewsPortalId.DvadesetCetiriSata}{FileExtensionConstants.Png}"),
                new NewsPortal((int)NewsPortalId.SportskeNovosti, NewsPortalId.SportskeNovosti.GetDisplayName(), "https://sportske.jutarnji.hr/", $"Icons/{NewsPortalId.SportskeNovosti}{FileExtensionConstants.Png}"),
                new NewsPortal((int)NewsPortalId.JutarnjiList, NewsPortalId.JutarnjiList.GetDisplayName(), "https://sportske.jutarnji.hr/", $"Icons/{NewsPortalId.JutarnjiList}{FileExtensionConstants.Png}"),
                new NewsPortal((int)NewsPortalId.NetHr, NewsPortalId.NetHr.GetDisplayName(), "https://net.hr/", $"Icons/{NewsPortalId.NetHr}{FileExtensionConstants.Png}"),
                new NewsPortal((int)NewsPortalId.SlobodnaDalmacija, NewsPortalId.SlobodnaDalmacija.GetDisplayName(), "https://slobodnadalmacija.hr/", $"Icons/{NewsPortalId.SlobodnaDalmacija}{FileExtensionConstants.Png}"),
                new NewsPortal((int)NewsPortalId.TPortal, NewsPortalId.TPortal.GetDisplayName(), "https://www.tportal.hr/", $"Icons/{NewsPortalId.TPortal}{FileExtensionConstants.Png}"),
                new NewsPortal((int)NewsPortalId.VecernjiList, NewsPortalId.VecernjiList.GetDisplayName(), "https://www.vecernji.hr/", $"Icons/{NewsPortalId.VecernjiList}{FileExtensionConstants.Png}"),
                new NewsPortal((int)NewsPortalId.Telegram, NewsPortalId.Telegram.GetDisplayName(), "https://www.telegram.hr/", $"Icons/{NewsPortalId.Telegram}{FileExtensionConstants.Png}"),
                new NewsPortal((int)NewsPortalId.Dnevnik, NewsPortalId.Dnevnik.GetDisplayName(), "https://dnevnik.hr/", $"Icons/{NewsPortalId.Dnevnik}{FileExtensionConstants.Png}"),
                new NewsPortal((int)NewsPortalId.Gol, NewsPortalId.Gol.GetDisplayName(), "https://gol.dnevnik.hr/", $"Icons/{NewsPortalId.Gol}{FileExtensionConstants.Png}"),
                new NewsPortal((int)NewsPortalId.RtlVijesti, NewsPortalId.RtlVijesti.GetDisplayName(), "https://sportnet.rtl.hr/", $"Icons/{NewsPortalId.RtlVijesti}{FileExtensionConstants.Png}"),
                //new NewsPortal((int)NewsPortalId.Sprdex, NewsPortalId.Sprdex.GetDisplayName(), "https://sprdex.com/", $"Icons/{NewsPortalId.Sprdex}{FileExtensionConstants.Png}"),
                new NewsPortal((int)NewsPortalId.NogometPlus, NewsPortalId.NogometPlus.GetDisplayName(), "http://www.nogometplus.net/", $"Icons/{NewsPortalId.NogometPlus}{FileExtensionConstants.Png}"), // Nemaju SLS LUL
                new NewsPortal((int)NewsPortalId.Lider, NewsPortalId.Lider.GetDisplayName(), "https://lider.media/", $"Icons/{NewsPortalId.Lider}{FileExtensionConstants.Png}"),
                new NewsPortal((int)NewsPortalId.Bug, NewsPortalId.Bug.GetDisplayName(), "https://www.bug.hr/", $"Icons/{NewsPortalId.Bug}{FileExtensionConstants.Png}"),
                new NewsPortal((int)NewsPortalId.VidiHr, NewsPortalId.VidiHr.GetDisplayName(), "https://www.vidi.hr/", $"Icons/{NewsPortalId.VidiHr}{FileExtensionConstants.Png}"),
                new NewsPortal((int)NewsPortalId.Zimo, NewsPortalId.Zimo.GetDisplayName(), "https://zimo.dnevnik.hr/", $"Icons/{NewsPortalId.Zimo}{FileExtensionConstants.Png}"),
                new NewsPortal((int)NewsPortalId.Netokracija, NewsPortalId.Netokracija.GetDisplayName(), "https://www.netokracija.com/", $"Icons/{NewsPortalId.Netokracija}{FileExtensionConstants.Png}"),
                new NewsPortal((int)NewsPortalId.PoslovniPuls, NewsPortalId.PoslovniPuls.GetDisplayName(), "https://poslovnipuls.com/", $"Icons/{NewsPortalId.PoslovniPuls}{FileExtensionConstants.Png}"),
                new NewsPortal((int)NewsPortalId.PcChip, NewsPortalId.PcChip.GetDisplayName(), "https://pcchip.hr/", $"Icons/{NewsPortalId.PcChip}{FileExtensionConstants.Png}"),
                new NewsPortal((int)NewsPortalId.Cosmopolitan, NewsPortalId.Cosmopolitan.GetDisplayName(), "http://www.cosmopolitan.hr/", $"Icons/{NewsPortalId.Cosmopolitan}{FileExtensionConstants.Png}"), // Nemaju SLS LUL
                new NewsPortal((int)NewsPortalId.WallHr, NewsPortalId.WallHr.GetDisplayName(), "https://wall.hr/", $"Icons/{NewsPortalId.WallHr}{FileExtensionConstants.Png}"),
                new NewsPortal((int)NewsPortalId.LjepotaIZdravlje, NewsPortalId.LjepotaIZdravlje.GetDisplayName(), "http://www.ljepotaizdravlje.hr/", $"Icons/{NewsPortalId.LjepotaIZdravlje}{FileExtensionConstants.Png}"), // Nemaju SLS LUL
                new NewsPortal((int)NewsPortalId.Autonet, NewsPortalId.Autonet.GetDisplayName(), "https://www.autonet.hr/", $"Icons/{NewsPortalId.Autonet}{FileExtensionConstants.Png}"),
                new NewsPortal((int)NewsPortalId.N1, NewsPortalId.N1.GetDisplayName(), "https://hr.n1info.com/", $"Icons/{NewsPortalId.N1}{FileExtensionConstants.Png}"),
                new NewsPortal((int)NewsPortalId.NarodHr, NewsPortalId.NarodHr.GetDisplayName(), "https://narod.hr/", $"Icons/{NewsPortalId.NarodHr}{FileExtensionConstants.Png}"),

                new NewsPortal(
                    id: (int)NewsPortalId.Hrt,
                    name: NewsPortalId.Hrt.GetDisplayName(),
                    baseUrl: "https://www.hrt.hr",
                    iconUrl: $"Icons/{NewsPortalId.Hrt}{FileExtensionConstants.Png}"
                ),

                new NewsPortal(
                    id: (int)NewsPortalId.StoPosto,
                    name: NewsPortalId.StoPosto.GetDisplayName(),
                    baseUrl: "https://100posto.jutarnji.hr/",
                    iconUrl: $"Icons/{NewsPortalId.StoPosto}{FileExtensionConstants.Png}"
                ),
            };

            builder.HasData(newsPortals);
        }
    }
}
