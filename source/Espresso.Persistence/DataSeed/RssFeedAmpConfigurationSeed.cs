using Espresso.Domain.Entities;
using Espresso.Domain.Enums.RssFeedEnums;
using Espresso.Domain.ValueObjects.RssFeedValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.DataSeed
{
    internal static class RssFeedAmpConfigurationSeed
    {
        public static void Seed(
            EntityTypeBuilder<RssFeed> builder
        )
        {
            var ampConfigurationBuilder = builder.OwnsOne(rssFeed => rssFeed.AmpConfiguration);

            SeedAmpConfigurations(ampConfigurationBuilder!);
        }

        private static void SeedAmpConfigurations(OwnedNavigationBuilder<RssFeed, AmpConfiguration> ampConfigurationBuilder)
        {

            ampConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Index_Vijesti,
                HasAmpArticles = true,
                TemplateUrl = "https://amp.index.hr/article/{0}{3}"
            });
            ampConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Index_Sport,
                HasAmpArticles = true,
                TemplateUrl = "https://amp.index.hr/article/{0}{3}"
            });
            ampConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Index_Rogue,
                HasAmpArticles = true,
                TemplateUrl = "https://amp.index.hr/article/{0}{3}"
            });
            ampConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Index_Magazin,
                HasAmpArticles = true,
                TemplateUrl = "https://amp.index.hr/article/{0}{3}"
            });
            ampConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Index_Auto,
                HasAmpArticles = true,
                TemplateUrl = "https://amp.index.hr/article/{0}{3}"
            });



            //ampConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Lifestyle, HasAmpArticles = true, TemplateUrl = "https://m.24sata.hr/amp/{1}{2}" });
            //ampConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Show, HasAmpArticles = true, TemplateUrl = "https://m.24sata.hr/amp/{1}{2}" });
            //ampConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Sport, HasAmpArticles = true, TemplateUrl = "https://m.24sata.hr/amp/{1}{2}" });
            //ampConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Tech, HasAmpArticles = true, TemplateUrl = "https://m.24sata.hr/amp/{1}{2}" });
            //ampConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Vijesti, HasAmpArticles = true, TemplateUrl = "https://m.24sata.hr/amp/{1}{2}" });
            //ampConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Viral, HasAmpArticles = true, TemplateUrl = "https://m.24sata.hr/amp/{1}{2}" });
            ampConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Lifestyle,
                HasAmpArticles = false,
                TemplateUrl = (string?)null
            });
            ampConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Show,
                HasAmpArticles = false,
                TemplateUrl = (string?)null
            });
            ampConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Sport,
                HasAmpArticles = false,
                TemplateUrl = (string?)null
            });
            ampConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Tech,
                HasAmpArticles = false,
                TemplateUrl = (string?)null
            });
            ampConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Vijesti,
                HasAmpArticles = false,
                TemplateUrl = (string?)null
            });
            ampConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Viral,
                HasAmpArticles = false,
                TemplateUrl = (string?)null
            });



            ampConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.NetHr,
                HasAmpArticles = true,
                TemplateUrl = "https://net.hr/{1}{2}{3}amp"
            });



            ampConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.VecernjiList,
                HasAmpArticles = true,
                TemplateUrl = "https://m.vecernji.hr/amp/{1}{2}"
            });



            ampConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Dnevnik,
                HasAmpArticles = true,
                TemplateUrl = "https://dnevnik.hr/amp/{1}{2}{3}"
            });



            ampConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Gol_Sport,
                HasAmpArticles = true,
                TemplateUrl = "https://gol.dnevnik.hr/amp/{1}{2}{3}{4}"
            });



            ampConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Zimo_TechVijesti,
                HasAmpArticles = true,
                TemplateUrl = "https://zimo.dnevnik.hr/amp/clanak/{2}"
            });



            ampConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Netokracija,
                HasAmpArticles = true,
                TemplateUrl = "https://www.netokracija.com/{1}/amp"
            });

        }
    }
}
