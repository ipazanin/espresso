using System.Collections.Generic;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.CategoryEnums;
using Espresso.Domain.Enums.NewsPortalEnums;
using Espresso.Domain.Enums.RssFeedEnums;
using Espresso.Domain.ValueObjects.RssFeedValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.DataSeed
{
    internal static class RssFeedDataSeed
    {
        public static void Seed(
            EntityTypeBuilder<RssFeed> builder
        )
        {
            var ampConfigurationBuilder = builder.OwnsOne(rssFeed => rssFeed.AmpConfiguration);
            var skipParseConfigurationBuilder = builder.OwnsOne(rssFeed => rssFeed.SkipParseConfiguration);
            var categoryParseConfigurationBuilder = builder.OwnsOne(rssFeed => rssFeed.CategoryParseConfiguration);
            var imageUrlParseConfigurationBuilder = builder.OwnsOne(rssFeed => rssFeed.ImageUrlParseConfiguration);

            SeedRssFeeds(builder);
            SeedAmpConfigurations(ampConfigurationBuilder!);
            SeedSkipParseConfiguration(skipParseConfigurationBuilder!);
            SeedCategoryParseConfiguration(categoryParseConfigurationBuilder);
            SeedImageUrlParseConfiguration(imageUrlParseConfigurationBuilder);
        }

        private static void SeedRssFeeds(EntityTypeBuilder<RssFeed> builder)
        {
            var rssFeeds = new List<RssFeed>
            {
                new RssFeed(
                    id: (int)RssFeedId.Index_Vijesti,
                    url: "https://www.index.hr/rss/vijesti",
                    newsPortalId: (int)NewsPortalId.Index,
                    categoryId: (int)CategoryId.Vijesti,
                    requestType: RequestType.Normal
                ),
                new RssFeed((int)RssFeedId.Index_Sport, "https://www.index.hr/rss/sport", (int)NewsPortalId.Index, (int)CategoryId.Sport, requestType: RequestType.Normal),
                new RssFeed((int)RssFeedId.Index_Magazin, "https://www.index.hr/rss/magazin", (int)NewsPortalId.Index, (int)CategoryId.Show, requestType: RequestType.Normal),
                new RssFeed((int)RssFeedId.Index_Rogue,  "https://www.index.hr/rss/rouge", (int)NewsPortalId.Index, (int)CategoryId.Lifestyle, requestType: RequestType.Normal),
                new RssFeed((int)RssFeedId.Index_Auto, "https://www.index.hr/rss/auto", (int)NewsPortalId.Index, (int)CategoryId.AutoMoto, requestType: RequestType.Normal),


                new RssFeed((int)RssFeedId.DvadesetCetiriSata_Vijesti, "https://www.24sata.hr/feeds/news.xml", (int)NewsPortalId.DvadesetCetiriSata, (int)CategoryId.Vijesti, requestType: RequestType.Normal),
                new RssFeed((int)RssFeedId.DvadesetCetiriSata_Show, "https://www.24sata.hr/feeds/show.xml", (int)NewsPortalId.DvadesetCetiriSata, (int)CategoryId.Show, requestType: RequestType.Normal),
                new RssFeed((int)RssFeedId.DvadesetCetiriSata_Sport, "https://www.24sata.hr/feeds/sport.xml", (int)NewsPortalId.DvadesetCetiriSata, (int)CategoryId.Sport, requestType: RequestType.Normal),
                new RssFeed((int)RssFeedId.DvadesetCetiriSata_Lifestyle, "https://www.24sata.hr/feeds/lifestyle.xml", (int)NewsPortalId.DvadesetCetiriSata, (int)CategoryId.Lifestyle, requestType: RequestType.Normal),
                new RssFeed((int)RssFeedId.DvadesetCetiriSata_Tech, "https://www.24sata.hr/feeds/tech.xml", (int)NewsPortalId.DvadesetCetiriSata, (int)CategoryId.Tech, requestType: RequestType.Normal),
                new RssFeed((int)RssFeedId.DvadesetCetiriSata_Viral, "https://www.24sata.hr/feeds/fun.xml", (int)NewsPortalId.DvadesetCetiriSata, (int)CategoryId.Viral, requestType: RequestType.Normal),

                new RssFeed((int)RssFeedId.SportskeNovosti_Sport, "http://sportske.jutarnji.hr/sn/feed", (int)NewsPortalId.SportskeNovosti, (int)CategoryId.Sport, requestType: RequestType.Browser),

                new RssFeed((int)RssFeedId.JutarnjiList, "https://www.jutarnji.hr/feed", (int)NewsPortalId.JutarnjiList, (int)CategoryId.Vijesti, requestType: RequestType.Browser),

                new RssFeed((int)RssFeedId.NetHr, "https://net.hr/feed/", (int)NewsPortalId.NetHr, (int)CategoryId.Vijesti, requestType: RequestType.Normal),

                new RssFeed((int)RssFeedId.SlobodnaDalmacija_Novosti, "https://www.slobodnadalmacija.hr/feed/category/119", (int)NewsPortalId.SlobodnaDalmacija, (int)CategoryId.Vijesti, requestType: RequestType.Browser),
                new RssFeed((int)RssFeedId.SlobodnaDalmacija_Sport, "https://www.slobodnadalmacija.hr/feed/category/255", (int)NewsPortalId.SlobodnaDalmacija, (int)CategoryId.Sport, requestType: RequestType.Browser),
                new RssFeed((int)RssFeedId.SlobodnaDalmacija_Showbiz, "https://www.slobodnadalmacija.hr/feed/category/262", (int)NewsPortalId.SlobodnaDalmacija, (int)CategoryId.Show, requestType: RequestType.Browser),
                new RssFeed((int)RssFeedId.SlobodnaDalmacija_Trend, "https://www.slobodnadalmacija.hr/feed/category/375", (int)NewsPortalId.SlobodnaDalmacija, (int)CategoryId.Show, requestType: RequestType.Browser),
                new RssFeed((int)RssFeedId.SlobodnaDalmacija_DrustvenaMreza, "https://www.slobodnadalmacija.hr/feed/category/263", (int)NewsPortalId.SlobodnaDalmacija, (int)CategoryId.Show, requestType: RequestType.Browser),
                new RssFeed((int)RssFeedId.SlobodnaDalmacija_Zivot, "https://www.slobodnadalmacija.hr/feed/category/264", (int)NewsPortalId.SlobodnaDalmacija, (int)CategoryId.Lifestyle, requestType: RequestType.Browser),
                new RssFeed((int)RssFeedId.SlobodnaDalmacija_Zdravlje, "https://www.slobodnadalmacija.hr/feed/category/265", (int)NewsPortalId.SlobodnaDalmacija, (int)CategoryId.Lifestyle, requestType: RequestType.Browser),
                new RssFeed((int)RssFeedId.SlobodnaDalmacija_Moda, "https://www.slobodnadalmacija.hr/feed/category/266", (int)NewsPortalId.SlobodnaDalmacija, (int)CategoryId.Lifestyle, requestType: RequestType.Browser),
                new RssFeed((int)RssFeedId.SlobodnaDalmacija_Ljepota, "https://www.slobodnadalmacija.hr/feed/category/267", (int)NewsPortalId.SlobodnaDalmacija, (int)CategoryId.Lifestyle, requestType: RequestType.Browser),
                new RssFeed((int)RssFeedId.SlobodnaDalmacija_Putovanja, "https://www.slobodnadalmacija.hr/feed/category/268", (int)NewsPortalId.SlobodnaDalmacija, (int)CategoryId.Lifestyle, requestType: RequestType.Browser),
                new RssFeed((int)RssFeedId.SlobodnaDalmacija_Gastro, "https://www.slobodnadalmacija.hr/feed/category/270", (int)NewsPortalId.SlobodnaDalmacija, (int)CategoryId.Lifestyle, requestType: RequestType.Browser),
                new RssFeed((int)RssFeedId.SlobodnaDalmacija_Dom, "https://www.slobodnadalmacija.hr/feed/category/271", (int)NewsPortalId.SlobodnaDalmacija, (int)CategoryId.Lifestyle, requestType: RequestType.Browser),
                new RssFeed((int)RssFeedId.SlobodnaDalmacija_Tehnologija, "https://www.slobodnadalmacija.hr/feed/category/269", (int)NewsPortalId.SlobodnaDalmacija, (int)CategoryId.Tech, requestType: RequestType.Browser),
                new RssFeed((int)RssFeedId.SlobodnaDalmacija_Viral, "https://www.slobodnadalmacija.hr/feed/category/274", (int)NewsPortalId.SlobodnaDalmacija, (int)CategoryId.Viral, requestType: RequestType.Browser),



                new RssFeed((int)RssFeedId.TPortal_Vijesti, "https://www.tportal.hr/rss-vijesti.xml", (int)NewsPortalId.TPortal, (int)CategoryId.Vijesti, requestType: RequestType.Normal),
                new RssFeed((int)RssFeedId.TPortal_Biznis, "https://www.tportal.hr/rss-biznis.xml", (int)NewsPortalId.TPortal, (int)CategoryId.Vijesti, requestType: RequestType.Normal),
                new RssFeed((int)RssFeedId.TPortal_Sport, "https://www.tportal.hr/rss-sport.xml", (int)NewsPortalId.TPortal, (int)CategoryId.Sport, requestType: RequestType.Normal),
                new RssFeed((int)RssFeedId.TPortal_Tehno, "https://www.tportal.hr/rss-tehno.xml", (int)NewsPortalId.TPortal, (int)CategoryId.Tech, requestType: RequestType.Normal),
                new RssFeed((int)RssFeedId.TPortal_Showtime, "https://www.tportal.hr/rss-showtime.xml", (int)NewsPortalId.TPortal, (int)CategoryId.Show, requestType: RequestType.Normal),
                new RssFeed((int)RssFeedId.TPortal_Lifestyle, "https://www.tportal.hr/rss-Lifestyle.xml", (int)NewsPortalId.TPortal, (int)CategoryId.Lifestyle, requestType: RequestType.Normal),
                new RssFeed((int)RssFeedId.TPortal_FunBox, "https://www.tportal.hr/rss-funbox.xml", (int)NewsPortalId.TPortal, (int)CategoryId.Viral, requestType: RequestType.Normal),
                new RssFeed((int)RssFeedId.TPortal_Kultura, "https://www.tportal.hr/rss-kultura.xml", (int)NewsPortalId.TPortal, (int)CategoryId.Kultura, requestType: RequestType.Normal),

                new RssFeed((int)RssFeedId.VecernjiList, "https://www.vecernji.hr/feeds/latest", (int)NewsPortalId.VecernjiList, (int)CategoryId.Vijesti, requestType: RequestType.Normal),

                new RssFeed((int)RssFeedId.Telegram,"https://www.telegram.hr/feed/", (int)NewsPortalId.Telegram, (int)CategoryId.Vijesti, requestType: RequestType.Normal),
                new RssFeed((int)RssFeedId.Telegram_Telesport, "https://telesport.telegram.hr/feed/", (int)NewsPortalId.Telegram, (int)CategoryId.Sport, requestType: RequestType.Normal),

                new RssFeed((int)RssFeedId.Dnevnik, "https://dnevnik.hr/assets/feed/articles/", (int)NewsPortalId.Dnevnik, (int)CategoryId.Vijesti, requestType: RequestType.Normal),

                new RssFeed((int)RssFeedId.Gol_Sport, "https://gol.dnevnik.hr/assets/feed/articles", (int)NewsPortalId.Gol, (int)CategoryId.Sport, requestType: RequestType.Normal),

                new RssFeed((int)RssFeedId.RtlVijesti_Sport, "https://sportnet.rtl.hr/rss/", (int)NewsPortalId.RtlVijesti, (int)CategoryId.Sport, requestType: RequestType.Normal),

                new RssFeed((int)RssFeedId.NogometPlus_Nogomet, "http://www.nogometplus.net/index.php/feed/", (int)NewsPortalId.NogometPlus, (int)CategoryId.Sport, requestType: RequestType.Normal),

                new RssFeed((int)RssFeedId.Lider_BiznisIPolitikaHrvatska, "http://lider.media/feed/", (int)NewsPortalId.Lider, (int)CategoryId.Biznis, requestType: RequestType.Normal),
                new RssFeed((int)RssFeedId.Lider_BiznisIPolitikaSvijet, "http://lider.media/feed/", (int)NewsPortalId.Lider, (int)CategoryId.Biznis, requestType: RequestType.Normal),
                new RssFeed((int)RssFeedId.Lider_Trziste, "http://lider.media/feed/", (int)NewsPortalId.Lider, (int)CategoryId.Biznis, requestType: RequestType.Normal),

                new RssFeed((int)RssFeedId.Bug_TechVijesti, "http://www.bug.hr/rss/vijesti/", (int)NewsPortalId.Bug, (int)CategoryId.Tech, requestType: RequestType.Normal),

                new RssFeed((int)RssFeedId.VidiHr_TechVijesti, "http://www.vidi.hr/rss/feed/vidi", (int)NewsPortalId.VidiHr, (int)CategoryId.Tech, requestType: RequestType.Normal),

                new RssFeed((int)RssFeedId.Zimo_TechVijesti, "https://zimo.dnevnik.hr/assets/feed/articles", (int)NewsPortalId.Zimo, (int)CategoryId.Tech, requestType: RequestType.Normal),

                new RssFeed((int)RssFeedId.Netokracija, "http://www.netokracija.com/feed", (int)NewsPortalId.Netokracija, (int)CategoryId.Tech, requestType: RequestType.Normal),

                new RssFeed((int)RssFeedId.PoslovniPuls, "http://www.poslovnipuls.com/feed/", (int)NewsPortalId.PoslovniPuls, (int)CategoryId.Biznis, requestType: RequestType.Browser),

                new RssFeed((int)RssFeedId.PcChip, "http://pcchip.hr/feed/", (int)NewsPortalId.PcChip, (int)CategoryId.Tech, requestType: RequestType.Normal),

                new RssFeed((int)RssFeedId.Cosmopolitan, "http://www.cosmopolitan.hr/feed", (int)NewsPortalId.Cosmopolitan, (int)CategoryId.Lifestyle, requestType: RequestType.Normal),

                new RssFeed((int)RssFeedId.WallHr, "http://wall.hr/cdn/feed.xml", (int)NewsPortalId.WallHr, (int)CategoryId.Lifestyle, requestType: RequestType.Normal),

                new RssFeed((int)RssFeedId.LjepotaIZdravlje, "http://www.ljepotaizdravlje.hr/feed", (int)NewsPortalId.LjepotaIZdravlje, (int)CategoryId.Lifestyle, requestType: RequestType.Normal),

                new RssFeed((int)RssFeedId.AutoNet, "https://www.autonet.hr/feed/", (int)NewsPortalId.Autonet, (int)CategoryId.AutoMoto, requestType: RequestType.Normal),

                new RssFeed((int)RssFeedId.N1, "http://hr.n1info.com/rss/249/Naslovna", (int)NewsPortalId.N1, (int)CategoryId.Vijesti, requestType: RequestType.Browser),

                new RssFeed((int)RssFeedId.NarodHr, "https://narod.hr/feed", (int)NewsPortalId.NarodHr, (int)CategoryId.Vijesti, requestType: RequestType.Browser),

                new RssFeed(
                    id: (int)RssFeedId.Hrt_Vijesti,
                    url: "https://www.hrt.hr/rss/vijesti/",
                    newsPortalId: (int)NewsPortalId.Hrt,
                    categoryId: (int)CategoryId.Vijesti, requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.Hrt_Sport,
                    url: "https://www.hrt.hr/rss/sport/",
                    newsPortalId: (int)NewsPortalId.Hrt,
                    categoryId: (int)CategoryId.Sport, requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.Hrt_Magazin,
                    url: "https://magazin.hrt.hr/feed.xml",
                    newsPortalId: (int)NewsPortalId.Hrt,
                    categoryId: (int)CategoryId.Show, requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.Hrt_Glazba,
                    url: "https://www.hrt.hr/rss/glazba/",
                    newsPortalId: (int)NewsPortalId.Hrt,
                    categoryId: (int)CategoryId.Show, requestType: RequestType.Normal
                ),



                new RssFeed(
                    id: (int)RssFeedId.StoPosto,
                    url: "https://100posto.jutarnji.hr/rss",
                    newsPortalId: (int)NewsPortalId.StoPosto,
                    categoryId: (int)CategoryId.Vijesti,
                    requestType: RequestType.Normal
                ),



                new RssFeed(
                    id: (int)RssFeedId.Dnevno,
                    url: "https://www.dnevno.hr/feed/",
                    newsPortalId: (int)NewsPortalId.Dnevno,
                    categoryId: (int)CategoryId.Vijesti, requestType: RequestType.Normal
                ),



                new RssFeed(
                    id: (int)RssFeedId.DirektnoHr_Direkt,
                    url: "https://direktno.hr/rss/publish/latest/direkt-50/",
                    newsPortalId: (int)NewsPortalId.DirektnoHr,
                    categoryId: (int)CategoryId.Vijesti, requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.DirektnoHr_Domovina,
                    url: "https://direktno.hr/rss/publish/latest/domovina-10/",
                    newsPortalId: (int)NewsPortalId.DirektnoHr,
                    categoryId: (int)CategoryId.Vijesti, requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.DirektnoHr_EuSvijet,
                    url: "https://direktno.hr/rss/publish/latest/eu_svijet/",
                    newsPortalId: (int)NewsPortalId.DirektnoHr,
                    categoryId: (int)CategoryId.Vijesti, requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.DirektnoHr_Razvoj,
                    url: "https://direktno.hr/rss/publish/latest/razvoj-110/",
                    newsPortalId: (int)NewsPortalId.DirektnoHr,
                    categoryId: (int)CategoryId.Biznis, requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.DirektnoHr_Sport,
                    url: "https://direktno.hr/rss/publish/latest/sport-60/",
                    newsPortalId: (int)NewsPortalId.DirektnoHr,
                    categoryId: (int)CategoryId.Sport, requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.DirektnoHr_Zivot,
                    url: "https://direktno.hr/rss/publish/latest/zivot-70/",
                    newsPortalId: (int)NewsPortalId.DirektnoHr,
                    categoryId: (int)CategoryId.Show, requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.DirektnoHr_Kolumne,
                    url: "https://direktno.hr/rss/publish/latest/kolumne-80/",
                    newsPortalId: (int)NewsPortalId.DirektnoHr,
                    categoryId: (int)CategoryId.Vijesti, requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.DirektnoHr_Direktno,
                    url: "https://direktno.hr/rss/publish/latest/direktnotv-100/",
                    newsPortalId: (int)NewsPortalId.DirektnoHr,
                    categoryId: (int)CategoryId.Vijesti, requestType: RequestType.Normal
                ),



                new RssFeed(
                    id: (int)RssFeedId.Scena,
                    url: "https://www.scena.hr/feed/",
                    newsPortalId: (int)NewsPortalId.Scena,
                    categoryId: (int)CategoryId.Vijesti, requestType: RequestType.Browser
                ),



                new RssFeed(
                    id: (int)RssFeedId.Nacional,
                    url: "https://www.nacional.hr/feed/",
                    newsPortalId: (int)NewsPortalId.Nacional,
                    categoryId: (int)CategoryId.Vijesti, requestType: RequestType.Normal
                ),



                new RssFeed(
                    id: (int)RssFeedId.Express,
                    url: "https://express.24sata.hr/feeds/placeholder-head/rss_feed",
                    newsPortalId: (int)NewsPortalId.Express,
                    categoryId: (int)CategoryId.Vijesti, requestType: RequestType.Normal
                ),



                new RssFeed(
                    id: (int)RssFeedId.OtvorenoHr,
                    url: "https://otvoreno.hr/feed",
                    newsPortalId: (int)NewsPortalId.OtvorenoHr,
                    categoryId: (int)CategoryId.Vijesti, requestType: RequestType.Normal
                ),



                new RssFeed(
                    id: (int)RssFeedId.GeoPolitika,
                    url: "https://www.geopolitika.news/feed",
                    newsPortalId: (int)NewsPortalId.GeoPolitika,
                    categoryId: (int)CategoryId.Vijesti, requestType: RequestType.Normal
                ),



                new RssFeed(
                    id: (int)RssFeedId.PovijestHr,
                    url: "https://povijest.hr/feed/",
                    newsPortalId: (int)NewsPortalId.PovijestHr,
                    categoryId: (int)CategoryId.Vijesti,
                    requestType: RequestType.Normal
                ),



                new RssFeed(
                    id: (int)RssFeedId.Dnevno7,
                    url: "https://7dnevno.hr/feed",
                    newsPortalId: (int)NewsPortalId.Dnevno7,
                    categoryId: (int)CategoryId.Vijesti,
                    requestType: RequestType.Normal
                ),



                new RssFeed(
                    id: (int)RssFeedId.BasketballHr,
                    url: "https://basketball.hr/vijesti.xml",
                    newsPortalId: (int)NewsPortalId.BasketballHr,
                    categoryId: (int)CategoryId.Sport,
                    requestType: RequestType.Normal
                ),



                new RssFeed(
                    id: (int)RssFeedId.JoomBoos,
                    url: "https://joomboos.24sata.hr/feeds/axiom-feed/tes-partnerski",
                    newsPortalId: (int)NewsPortalId.JoomBoos,
                    categoryId: (int)CategoryId.Viral,
                    requestType: RequestType.Normal
                ),



                new RssFeed(
                    id: (int)RssFeedId.IctBusiness,
                    url: "https://www.ictbusiness.info/rss2.xml",
                    newsPortalId: (int)NewsPortalId.IctBusiness,
                    categoryId: (int)CategoryId.Tech,
                    requestType: RequestType.Normal
                ),

            };

            builder.HasData(rssFeeds);
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

        private static void SeedSkipParseConfiguration(OwnedNavigationBuilder<RssFeed, SkipParseConfiguration> skipParseConfigurationBuilder)
        {

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.JutarnjiList,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Novosti,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Sport,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Showbiz,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Trend,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.SlobodnaDalmacija_DrustvenaMreza,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Zivot,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Zdravlje,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Moda,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Ljepota,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Putovanja,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Gastro,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Dom,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Tehnologija,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Viral,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.PoslovniPuls,
                NumberOfSkips = 10,
                CurrentSkip = 0
            });

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.StoPosto,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Express,
                NumberOfSkips = 10,
                CurrentSkip = 0
            });

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Hrt_Glazba,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Hrt_Magazin,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Hrt_Sport,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Hrt_Vijesti,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.BasketballHr,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.JoomBoos,
                NumberOfSkips = 7,
                CurrentSkip = 0
            });

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.IctBusiness,
                NumberOfSkips = 8,
                CurrentSkip = 0
            });

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Scena,
                NumberOfSkips = 6,
                CurrentSkip = 0
            });

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

        private static void SeedImageUrlParseConfiguration(
            OwnedNavigationBuilder<RssFeed, ImageUrlParseConfiguration> imageUrlParseConfigurationBuilder
        )
        {

            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Index_Vijesti, ImgElementXPath = "//figure[contains(@class, 'img-container')]//img", ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Index_Sport, ImgElementXPath = "//figure[contains(@class, 'img-container')]//img", ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Index_Magazin, ImgElementXPath = "//figure[contains(@class, 'img-container')]//img", ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Index_Rogue, ImgElementXPath = "//figure[contains(@class, 'img-container')]//img", ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Index_Auto, ImgElementXPath = "//figure[contains(@class, 'img-container')]//img", ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary });

            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Vijesti, ImgElementXPath = "//img[contains(@class, 'article__figure_img')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Show, ImgElementXPath = "//img[contains(@class, 'article__figure_img')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Sport, ImgElementXPath = "//img[contains(@class, 'article__figure_img')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Lifestyle, ImgElementXPath = "//img[contains(@class, 'article__figure_img')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Tech, ImgElementXPath = "//img[contains(@class, 'article__figure_img')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Viral, ImgElementXPath = "//img[contains(@class, 'article__figure_img')]", });

            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SportskeNovosti_Sport, ImgElementXPath = "//img[contains(@class, 'media-object adaptive lazy')]", });

            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.JutarnjiList, ImgElementXPath = "//img[contains(@class, 'media-object adaptive lazy')]", });

            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.NetHr, ImgElementXPath = "//div[contains(@class, 'featured-img')]//img", });

            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Novosti, ImgElementXPath = "//img[contains(@class, 'card__image')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Sport, ImgElementXPath = "//img[contains(@class, 'card__image')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Showbiz, ImgElementXPath = "//img[contains(@class, 'card__image')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Trend, ImgElementXPath = "//img[contains(@class, 'card__image')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_DrustvenaMreza, ImgElementXPath = "//img[contains(@class, 'card__image')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Zivot, ImgElementXPath = "//img[contains(@class, 'card__image')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Zdravlje, ImgElementXPath = "//img[contains(@class, 'card__image')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Moda, ImgElementXPath = "//img[contains(@class, 'card__image')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Ljepota, ImgElementXPath = "//img[contains(@class, 'card__image')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Putovanja, ImgElementXPath = "//img[contains(@class, 'card__image')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Gastro, ImgElementXPath = "//img[contains(@class, 'card__image')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Dom, ImgElementXPath = "//img[contains(@class, 'card__image')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Tehnologija, ImgElementXPath = "//img[contains(@class, 'card__image')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Viral, ImgElementXPath = "//img[contains(@class, 'card__image')]", });



            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_Vijesti, ImgElementXPath = "//img[contains(@class, 'lateImage lateImageLoaded')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_Biznis, ImgElementXPath = "//img[contains(@class, 'lateImage lateImageLoaded')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_Sport, ImgElementXPath = "//img[contains(@class, 'lateImage lateImageLoaded')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_Tehno, ImgElementXPath = "//img[contains(@class, 'lateImage lateImageLoaded')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_Showtime, ImgElementXPath = "//img[contains(@class, 'lateImage lateImageLoaded')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_Lifestyle, ImgElementXPath = "//img[contains(@class, 'lateImage lateImageLoaded')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_FunBox, ImgElementXPath = "//img[contains(@class, 'lateImage lateImageLoaded')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_Kultura, ImgElementXPath = "//img[contains(@class, 'lateImage lateImageLoaded')]", });



            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.VecernjiList,

                ImgElementXPath = "//script[contains(@type, 'application/ld+json')]",
                ShouldImageUrlBeWebScraped = true,
                ImageUrlWebScrapeType = ImageUrlWebScrapeType.JsonObjectInScriptElement,
                JsonWebScrapePropertyNames = "image,url"
            });



            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Telegram, ImgElementXPath = "//div[contains(@class, 'thumb')]//img", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Telegram_Telesport, ImgElementXPath = "//div[contains(@class, 'featured-img')]//img", });



            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Dnevnik, ImgElementXPath = "//figure[contains(@class, 'article-main-img')]//img", });



            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Gol_Sport, ImgElementXPath = "//figure[contains(@class, 'article-image main-image')]//img", });



            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.RtlVijesti_Sport, ImgElementXPath = "//img[contains(@class, 'naslovna')]", });



            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.NogometPlus_Nogomet, ImgElementXPath = "//div[contains(@class, 'post-img')]//img", });



            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Lider_BiznisIPolitikaHrvatska, ImgElementXPath = "//img[contains(@class, 'card__image')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Lider_BiznisIPolitikaSvijet, ImgElementXPath = "//img[contains(@class, 'card__image')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Lider_Trziste, ImgElementXPath = "//img[contains(@class, 'card__image')]", });



            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Bug_TechVijesti, ImgElementXPath = "//div[contains(@class, 'entry-content')]//img", });



            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.VidiHr_TechVijesti, ImgElementXPath = "//div[contains(@class, 'attribute-image')]//img", });



            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Zimo_TechVijesti, ImgElementXPath = "//div[contains(@class, 'img-holder')]//img", });



            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Netokracija, ImgElementXPath = "//div[contains(@class, 'post__hero')]//img", });



            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.PoslovniPuls, ImgElementXPath = "//div[contains(@class, 'postFeaturedImg postFeaturedImg--single')]//img", });



            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.PcChip, ImgElementXPath = "//div[contains(@class, 'td-post-featured-image')]//img", });



            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Cosmopolitan, ImgElementXPath = "//div[contains(@class, 'first-image')]//img", });



            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.WallHr, ImageUrlParseStrategy = ImageUrlParseStrategy.FromContent, ImgElementXPath = "//figure[contains(@class, 'dcms-image article-image')]//img", });



            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.LjepotaIZdravlje, ImgElementXPath = "//div[contains(@class, 'post-thumbnail')]//img", });



            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.AutoNet, ImgElementXPath = "//figure[contains(@class, 'figure')]//img", });



            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.N1,
                ImgElementXPath = "//figure[contains(@class, 'media')]//img",
                ImageUrlParseStrategy = ImageUrlParseStrategy.FromFirstElementExtension
            });



            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.NarodHr,
                ImgElementXPath = "//div[contains(@class, 'td-post-featured-image')]//img",
            });



            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Hrt_Vijesti,
                ImgElementXPath = "//div[contains(@class, 'image-slider')]//img",
            });
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Hrt_Sport,
                ImgElementXPath = "//div[contains(@class, 'image-slider')]//img",
            });
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Hrt_Magazin,
                ImgElementXPath = "//div[contains(@class, 'image-slider')]//img",
            });
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Hrt_Glazba,
                ImgElementXPath = "//div[contains(@class, 'image-slider')]//img",
            });



            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.StoPosto,
                ImgElementXPath = "//picture[contains(@class, 'pic')]//img",
            });



            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Dnevno,
                ImgElementXPath = "//div[contains(@class, 'img-holder inner')]//img",
            });



            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DirektnoHr_Direkt,
                ImgElementXPath = "//div[contains(@class, 'pd-hero-image')]//img",
            });
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DirektnoHr_Direktno,
                ImgElementXPath = "//div[contains(@class, 'pd-hero-image')]//img",
            });
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DirektnoHr_Domovina,
                ImgElementXPath = "//div[contains(@class, 'pd-hero-image')]//img",
            });
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DirektnoHr_EuSvijet,
                ImgElementXPath = "//div[contains(@class, 'pd-hero-image')]//img",
            });
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DirektnoHr_Kolumne,
                ImgElementXPath = "//div[contains(@class, 'pd-hero-image')]//img",
            });
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DirektnoHr_Razvoj,
                ImgElementXPath = "//div[contains(@class, 'pd-hero-image')]//img",
            });
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DirektnoHr_Sport,
                ImgElementXPath = "//div[contains(@class, 'pd-hero-image')]//img",
            });
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DirektnoHr_Zivot,
                ImgElementXPath = "//div[contains(@class, 'pd-hero-image')]//img",
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Scena,
                ImgElementXPath = "//div[contains(@class, 'mycontent')]//img",
                ImageUrlParseStrategy = ImageUrlParseStrategy.FromSecondElementExtension
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Nacional,
                ImgElementXPath = "//div[contains(@class, 'single-post-media')]//img",
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Express,
                ImgElementXPath = "//img[contains(@class, 'article__figure_img')]",
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.OtvorenoHr,
                ImgElementXPath = "//div[contains(@class, 'td-post-featured-image')]//img",
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.GeoPolitika,
                ImgElementXPath = "//div[contains(@class, 'entry-image featured-image')]//img",
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.PovijestHr,
                ImgElementXPath = "//div[contains(@class, 'td-post-featured-image')]//img",
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Dnevno7,
                ImgElementXPath = "//div[contains(@class, 'td-post-featured-image')]//img",
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.BasketballHr,
                ImgElementXPath = "//div[contains(@class, 'img')]//img",
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.IctBusiness,
                ImgElementXPath = "//div[contains(@class, 'main-content')]//img",
                ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary
            });
        }
    }
}
