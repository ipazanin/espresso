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
            SeedLocalRssFeeds(builder);
            SeedAmpConfigurations(ampConfigurationBuilder!);
            SeedSkipParseConfiguration(skipParseConfigurationBuilder!);
            SeedCategoryParseConfiguration(categoryParseConfigurationBuilder);
            SeedImageUrlParseConfiguration(imageUrlParseConfigurationBuilder);
            SeedLocalImageUrlParseConfiguration(imageUrlParseConfigurationBuilder);
        }

        private static void SeedRssFeeds(EntityTypeBuilder<RssFeed> builder)
        {
            var rssFeeds = new List<RssFeed>
            {
                #region Index
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
                #endregion

                #region 24 sata
                new RssFeed((int)RssFeedId.DvadesetCetiriSata_Vijesti, "https://www.24sata.hr/feeds/news.xml", (int)NewsPortalId.DvadesetCetiriSata, (int)CategoryId.Vijesti, requestType: RequestType.Normal),
                new RssFeed((int)RssFeedId.DvadesetCetiriSata_Show, "https://www.24sata.hr/feeds/show.xml", (int)NewsPortalId.DvadesetCetiriSata, (int)CategoryId.Show, requestType: RequestType.Normal),
                new RssFeed((int)RssFeedId.DvadesetCetiriSata_Sport, "https://www.24sata.hr/feeds/sport.xml", (int)NewsPortalId.DvadesetCetiriSata, (int)CategoryId.Sport, requestType: RequestType.Normal),
                new RssFeed((int)RssFeedId.DvadesetCetiriSata_Lifestyle, "https://www.24sata.hr/feeds/Lifestyle.xml", (int)NewsPortalId.DvadesetCetiriSata, (int)CategoryId.Lifestyle, requestType: RequestType.Normal),
                new RssFeed((int)RssFeedId.DvadesetCetiriSata_Tech, "https://www.24sata.hr/feeds/tech.xml", (int)NewsPortalId.DvadesetCetiriSata, (int)CategoryId.Tech, requestType: RequestType.Normal),
                new RssFeed((int)RssFeedId.DvadesetCetiriSata_Viral, "https://www.24sata.hr/feeds/fun.xml", (int)NewsPortalId.DvadesetCetiriSata, (int)CategoryId.Viral, requestType: RequestType.Normal),
                #endregion

                #region Sportske Novosti
                new RssFeed((int)RssFeedId.SportskeNovosti_Sport, "http://sportske.jutarnji.hr/sn/feed", (int)NewsPortalId.SportskeNovosti, (int)CategoryId.Sport, requestType: RequestType.Browser),
                #endregion

                #region Jutarnji
                new RssFeed((int)RssFeedId.JutarnjiList, "https://www.jutarnji.hr/feed", (int)NewsPortalId.JutarnjiList, (int)CategoryId.Vijesti, requestType: RequestType.Browser),
                #endregion

                #region Net.hr
                new RssFeed((int)RssFeedId.NetHr, "https://net.hr/feed/", (int)NewsPortalId.NetHr, (int)CategoryId.Vijesti, requestType: RequestType.Normal),
                #endregion

                #region Slobodna Dalmacija
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
                #endregion 

                #region TPortal
                new RssFeed((int)RssFeedId.TPortal_Vijesti, "https://www.tportal.hr/rss-vijesti.xml", (int)NewsPortalId.TPortal, (int)CategoryId.Vijesti, requestType: RequestType.Normal),
                new RssFeed((int)RssFeedId.TPortal_Biznis, "https://www.tportal.hr/rss-biznis.xml", (int)NewsPortalId.TPortal, (int)CategoryId.Vijesti, requestType: RequestType.Normal),
                new RssFeed((int)RssFeedId.TPortal_Sport, "https://www.tportal.hr/rss-sport.xml", (int)NewsPortalId.TPortal, (int)CategoryId.Sport, requestType: RequestType.Normal),
                new RssFeed((int)RssFeedId.TPortal_Tehno, "https://www.tportal.hr/rss-tehno.xml", (int)NewsPortalId.TPortal, (int)CategoryId.Tech, requestType: RequestType.Normal),
                new RssFeed((int)RssFeedId.TPortal_Showtime, "https://www.tportal.hr/rss-showtime.xml", (int)NewsPortalId.TPortal, (int)CategoryId.Show, requestType: RequestType.Normal),
                new RssFeed((int)RssFeedId.TPortal_Lifestyle, "https://www.tportal.hr/rss-Lifestyle.xml", (int)NewsPortalId.TPortal, (int)CategoryId.Lifestyle, requestType: RequestType.Normal),
                new RssFeed((int)RssFeedId.TPortal_FunBox, "https://www.tportal.hr/rss-funbox.xml", (int)NewsPortalId.TPortal, (int)CategoryId.Viral, requestType: RequestType.Normal),
                new RssFeed((int)RssFeedId.TPortal_Kultura, "https://www.tportal.hr/rss-kultura.xml", (int)NewsPortalId.TPortal, (int)CategoryId.Kultura, requestType: RequestType.Normal),
                #endregion

                #region Večernji List
                new RssFeed((int)RssFeedId.VecernjiList, "https://www.vecernji.hr/feeds/latest", (int)NewsPortalId.VecernjiList, (int)CategoryId.Vijesti, requestType: RequestType.Normal),
                #endregion

                #region Telegram
                new RssFeed((int)RssFeedId.Telegram,"https://www.telegram.hr/feed/", (int)NewsPortalId.Telegram, (int)CategoryId.Vijesti, requestType: RequestType.Normal),
                new RssFeed((int)RssFeedId.Telegram_Telesport, "https://telesport.telegram.hr/feed/", (int)NewsPortalId.Telegram, (int)CategoryId.Sport, requestType: RequestType.Normal),
                #endregion

                // Dnevnik
                new RssFeed((int)RssFeedId.Dnevnik, "https://dnevnik.hr/assets/feed/articles/", (int)NewsPortalId.Dnevnik, (int)CategoryId.Vijesti, requestType: RequestType.Normal),

                // Gol
                new RssFeed((int)RssFeedId.Gol_Sport, "https://gol.dnevnik.hr/assets/feed/articles", (int)NewsPortalId.Gol, (int)CategoryId.Sport, requestType: RequestType.Normal),

                // Rtl Vijesti
                new RssFeed((int)RssFeedId.RtlVijesti_Sport, "https://sportnet.rtl.hr/rss/", (int)NewsPortalId.RtlVijesti, (int)CategoryId.Sport, requestType: RequestType.Normal),
                
                // Sprdex
                //new RssFeed((int)RssFeedId.Sprdex_ZabavnaSatira, "http://sprdex.net.hr/feed/", (int)NewsPortalId.Sprdex, (int)CategoryId.Viral, requestType: RequestType.Normal),
                
                // Nogometni Plus
                new RssFeed((int)RssFeedId.NogometPlus_Nogomet, "http://www.nogometplus.net/index.php/feed/", (int)NewsPortalId.NogometPlus, (int)CategoryId.Sport, requestType: RequestType.Normal),
                
                // Lider
                new RssFeed((int)RssFeedId.Lider_BiznisIPolitikaHrvatska, "http://lider.media/feed/", (int)NewsPortalId.Lider, (int)CategoryId.Biznis, requestType: RequestType.Normal),
                new RssFeed((int)RssFeedId.Lider_BiznisIPolitikaSvijet, "http://lider.media/feed/", (int)NewsPortalId.Lider, (int)CategoryId.Biznis, requestType: RequestType.Normal),
                new RssFeed((int)RssFeedId.Lider_Trziste, "http://lider.media/feed/", (int)NewsPortalId.Lider, (int)CategoryId.Biznis, requestType: RequestType.Normal),
                
                // Bug
                new RssFeed((int)RssFeedId.Bug_TechVijesti, "http://www.bug.hr/rss/vijesti/", (int)NewsPortalId.Bug, (int)CategoryId.Tech, requestType: RequestType.Normal),

                // Vidi.Hr
                new RssFeed((int)RssFeedId.VidiHr_TechVijesti, "http://www.vidi.hr/rss/feed/vidi", (int)NewsPortalId.VidiHr, (int)CategoryId.Tech, requestType: RequestType.Normal),

                // Zimo
                new RssFeed((int)RssFeedId.Zimo_TechVijesti, "https://zimo.dnevnik.hr/assets/feed/articles", (int)NewsPortalId.Zimo, (int)CategoryId.Tech, requestType: RequestType.Normal),

                // Netokracija
                new RssFeed((int)RssFeedId.Netokracija, "http://www.netokracija.com/feed", (int)NewsPortalId.Netokracija, (int)CategoryId.Tech, requestType: RequestType.Normal),

                // Poslovni Plus
                new RssFeed((int)RssFeedId.PoslovniPuls, "http://www.poslovnipuls.com/feed/", (int)NewsPortalId.PoslovniPuls, (int)CategoryId.Biznis, requestType: RequestType.Browser),
                
                // PCChip
                new RssFeed((int)RssFeedId.PcChip, "http://pcchip.hr/feed/", (int)NewsPortalId.PcChip, (int)CategoryId.Tech, requestType: RequestType.Normal),
                
                // Cosmopolitan
                new RssFeed((int)RssFeedId.Cosmopolitan, "http://www.cosmopolitan.hr/feed", (int)NewsPortalId.Cosmopolitan, (int)CategoryId.Lifestyle, requestType: RequestType.Normal),
                
                #region Wall.hr
                new RssFeed((int)RssFeedId.WallHr, "http://wall.hr/cdn/feed.xml", (int)NewsPortalId.WallHr, (int)CategoryId.Lifestyle, requestType: RequestType.Normal),
                #endregion
                
                #region Ljepota i zdravlje
                new RssFeed((int)RssFeedId.LjepotaIZdravlje, "http://www.ljepotaizdravlje.hr/feed", (int)NewsPortalId.LjepotaIZdravlje, (int)CategoryId.Lifestyle, requestType: RequestType.Normal),
                #endregion

                #region Autonet
                new RssFeed((int)RssFeedId.AutoNet, "https://www.autonet.hr/feed/", (int)NewsPortalId.Autonet, (int)CategoryId.AutoMoto, requestType: RequestType.Normal),
                #endregion

                #region N1
                new RssFeed((int)RssFeedId.N1, "http://hr.n1info.com/rss/249/Naslovna", (int)NewsPortalId.N1, (int)CategoryId.Vijesti, requestType: RequestType.Browser),
                #endregion

                #region NarodHr
                new RssFeed((int)RssFeedId.NarodHr, "https://narod.hr/feed", (int)NewsPortalId.NarodHr, (int)CategoryId.Vijesti, requestType: RequestType.Browser),
                #endregion

                #region Hrt
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
                #endregion

                #region  100posto
                new RssFeed(
                    id: (int)RssFeedId.StoPosto,
                    url: "https://100posto.jutarnji.hr/rss",
                    newsPortalId: (int)NewsPortalId.StoPosto,
                    categoryId: (int)CategoryId.Vijesti,
                    requestType: RequestType.Normal
                ),
                #endregion

                #region  Dnevno
                new RssFeed(
                    id: (int)RssFeedId.Dnevno,
                    url: "https://www.dnevno.hr/feed/",
                    newsPortalId: (int)NewsPortalId.Dnevno,
                    categoryId: (int)CategoryId.Vijesti, requestType: RequestType.Normal
                ),
                #endregion

                #region AutomobiliHr
                // new RssFeed(
                //     id: (int)RssFeedId.AutomobiliHr,
                //     url: "https://klik.hr/rss",
                //     newsPortalId: (int)NewsPortalId.AutomobiliHr,
                //     categoryId: (int)CategoryId.AutoMoto, requestType: RequestType.Normal
                // ),
                #endregion

                #region DirektnoHr
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
                #endregion

                #region Scena
                new RssFeed(
                    id: (int)RssFeedId.Scena,
                    url: "https://www.scena.hr/feed/",
                    newsPortalId: (int)NewsPortalId.Scena,
                    categoryId: (int)CategoryId.Vijesti, requestType: RequestType.Normal
                ),
                #endregion

                #region Nacional
                new RssFeed(
                    id: (int)RssFeedId.Nacional,
                    url: "https://www.nacional.hr/feed/",
                    newsPortalId: (int)NewsPortalId.Nacional,
                    categoryId: (int)CategoryId.Vijesti, requestType: RequestType.Normal
                ),
                #endregion

                #region Express
                new RssFeed(
                    id: (int)RssFeedId.Express,
                    url: "https://express.24sata.hr/feeds/placeholder-head/rss_feed",
                    newsPortalId: (int)NewsPortalId.Express,
                    categoryId: (int)CategoryId.Vijesti, requestType: RequestType.Normal
                ),
                #endregion

                #region OtvorenoHr
                new RssFeed(
                    id: (int)RssFeedId.OtvorenoHr,
                    url: "https://otvoreno.hr/feed",
                    newsPortalId: (int)NewsPortalId.OtvorenoHr,
                    categoryId: (int)CategoryId.Vijesti, requestType: RequestType.Normal
                ),
                #endregion   

                #region GeoPolitika
                new RssFeed(
                    id: (int)RssFeedId.GeoPolitika,
                    url: "https://www.geopolitika.news/feed",
                    newsPortalId: (int)NewsPortalId.GeoPolitika,
                    categoryId: (int)CategoryId.Vijesti, requestType: RequestType.Normal
                ),
                #endregion 

                #region PovijestHr
                new RssFeed(
                    id: (int)RssFeedId.PovijestHr,
                    url: "https://povijest.hr/feed/",
                    newsPortalId: (int)NewsPortalId.PovijestHr,
                    categoryId: (int)CategoryId.Vijesti, requestType: RequestType.Normal
                ),
                #endregion 

                #region 7Dnevno
                new RssFeed(
                    id: (int)RssFeedId.Dnevno7,
                    url: "https://7dnevno.hr/feed",
                    newsPortalId: (int)NewsPortalId.Dnevno7,
                    categoryId: (int)CategoryId.Vijesti, requestType: RequestType.Normal
                ),
                #endregion   

                #region Basketball.Hr
                new RssFeed(
                    id: (int)RssFeedId.BasketballHr,
                    url: "https://basketball.hr/vijesti.xml",
                    newsPortalId: (int)NewsPortalId.BasketballHr,
                    categoryId: (int)CategoryId.Sport, requestType: RequestType.Normal
                ),
                #endregion   
            };

            builder.HasData(rssFeeds);
        }

        private static void SeedLocalRssFeeds(EntityTypeBuilder<RssFeed> builder)
        {
            var localRssFeeds = new List<RssFeed>
            {
                #region Dalmacija
                new RssFeed(
                    id: (int)RssFeedId.DalmacijaDanas,
                    url: "https://www.dalmacijadanas.hr/feed/",
                    newsPortalId: (int)NewsPortalId.DalmacijaDanas,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.DalmacijaNews,
                    url: "https://www.dalmacijanews.hr/rss",
                    newsPortalId: (int)NewsPortalId.DalmacijaNews,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.DalmatinskiPortal,
                    url: "https://dalmatinskiportal.hr/sadrzaj/rss/vijesti.xml",
                    newsPortalId: (int)NewsPortalId.DalmatinskiPortal,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.DubrovackiDnevnik,
                    url: "https://dubrovackidnevnik.net.hr/rss",
                    newsPortalId: (int)NewsPortalId.DubrovackiDnevnik,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.SlobodnaDalmacija_Dalmacija,
                    url: "https://slobodnadalmacija.hr/feed/category/246",
                    newsPortalId: (int)NewsPortalId.SlobodnaDalmacija_Dalmacija,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.SlobodnaDalmacija_Split,
                    url: "https://slobodnadalmacija.hr/feed/category/253",
                    newsPortalId: (int)NewsPortalId.SlobodnaDalmacija_Split,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.DubrovnikNet,
                    url: "https://www.dubrovniknet.hr/feed",
                    newsPortalId: (int)NewsPortalId.DubrovnikNet,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.MakarskaDanas,
                    url: "https://makarska-danas.com/feed",
                    newsPortalId: (int)NewsPortalId.MakarskaDanas,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.MakarskaHr,
                    url: "https://makarska.hr/rss",
                    newsPortalId: (int)NewsPortalId.MakarskaHr,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.PortalOko,
                    url: "http://www.portaloko.hr/rss/-1",
                    newsPortalId: (int)NewsPortalId.PortalOko,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.AntenaZadar,
                    url: "https://www.antenazadar.hr/feed",
                    newsPortalId: (int)NewsPortalId.AntenaZadar,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.RadioImotski,
                    url: "https://radioimotski.hr/feed",
                    newsPortalId: (int)NewsPortalId.RadioImotski,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.ImotskeNovine,
                    url: "https://imotskenovine.hr/feed",
                    newsPortalId: (int)NewsPortalId.ImotskeNovine,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.PortalKastela,
                    url: "http://www.kastela.org/?format=feed&type=rss",
                    newsPortalId: (int)NewsPortalId.PortalKastela,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.HukNet,
                    url: "https://huknet1.hr/?feed=rss2",
                    newsPortalId: (int)NewsPortalId.HukNet,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.ZadarskiList,
                    url: "https://www.zadarskilist.hr/rss.xml",
                    newsPortalId: (int)NewsPortalId.ZadarskiList,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),                                                 
                                                                                                          
                #endregion

                #region Istra i Kvarner
                new RssFeed(
                    id: (int)RssFeedId.IVijesti,
                    url: "https://ivijesti.hr/feed",
                    newsPortalId: (int)NewsPortalId.IVijesti,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.NoviList,
                    url: "https://www.novilist.hr/feed",
                    newsPortalId: (int)NewsPortalId.NoviList,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.Parentium,
                    url: "https://www.parentium.com/rssfeed.asp",
                    newsPortalId: (int)NewsPortalId.Parentium,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.IstarskaZupanija,
                    url: "http://www.istra-istria.hr/index.php?id=2415&type=100",
                    newsPortalId: (int)NewsPortalId.IstarskaZupanija,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.IstraTerraMagica,
                    url: "https://www.istriaterramagica.eu/feed",
                    newsPortalId: (int)NewsPortalId.IstraTerraMagica,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
               new RssFeed(
                    id: (int)RssFeedId.IPress,
                    url: "https://www.ipress.hr/index.php?format=feed&type=rss",
                    newsPortalId: (int)NewsPortalId.IPress,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
               new RssFeed(
                    id: (int)RssFeedId.RijekaDanas,
                    url: "https://www.rijekadanas.com/feed",
                    newsPortalId: (int)NewsPortalId.RijekaDanas,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
               new RssFeed(
                    id: (int)RssFeedId.Fiuman,
                    url: "https://www.fiuman.hr/feed",
                    newsPortalId: (int)NewsPortalId.Fiuman,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
               new RssFeed(
                    id: (int)RssFeedId.Riportal,
                    url: "https://riportal.net.hr/feed",
                    newsPortalId: (int)NewsPortalId.Riportal,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),                                                                                               
                #endregion

                #region Lika
                new RssFeed(
                    id: (int)RssFeedId.LikaKlub,
                    url: "https://likaclub.eu/feed",
                    newsPortalId: (int)NewsPortalId.LikaKlub,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.LikaExpress,
                    url: "http://www.lika-express.hr/feed",
                    newsPortalId: (int)NewsPortalId.LikaExpress,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Browser
                ),
                new RssFeed(
                    id: (int)RssFeedId.LikaOnline,
                    url: "https://www.lika-online.com/feed",
                    newsPortalId: (int)NewsPortalId.LikaOnline,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.LikaPlus,
                    url: "http://www.likaplus.hr/rss",
                    newsPortalId: (int)NewsPortalId.LikaPlus,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
               new RssFeed(
                    id: (int)RssFeedId.GsPress,
                    url: "https://www.gspress.net/feed",
                    newsPortalId: (int)NewsPortalId.GsPress,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),                  
                #endregion

                #region Zagreb
                new RssFeed(
                    id: (int)RssFeedId.IndexHrZagreb,
                    url: "https://www.index.hr/rss/vijesti-zagreb",
                    newsPortalId: (int)NewsPortalId.IndexHrZagreb,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.ZagrebInfo,
                    url: "https://www.zagreb.info/feed",
                    newsPortalId: (int)NewsPortalId.ZagrebInfo,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.Zagrebancija,
                    url: "https://www.zagrebancija.com/feed",
                    newsPortalId: (int)NewsPortalId.Zagrebancija,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.ZagrebOnline,
                    url: "https://www.zagrebonline.hr/feed",
                    newsPortalId: (int)NewsPortalId.ZagrebOnline,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.ZagrebackiList,
                    url: "https://www.zagrebacki.hr/feed",
                    newsPortalId: (int)NewsPortalId.ZagrebackiList,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.ZgPortal,
                    url: "https://www.zgportal.com/feed",
                    newsPortalId: (int)NewsPortalId.ZgPortal,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.GradZagreb,
                    url: "https://www.zagreb.hr/RssFeeds.aspx?id=17",
                    newsPortalId: (int)NewsPortalId.GradZagreb,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),                                                
                #endregion

                #region Sjeverna Hrvatska
                new RssFeed(
                    id: (int)RssFeedId.SjeverHr,
                    url: "https://sjever.hr/feed",
                    newsPortalId: (int)NewsPortalId.SjeverHr,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.PrigorskiHr,
                    url: "https://prigorski.hr/feed",
                    newsPortalId: (int)NewsPortalId.PrigorskiHr,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.PodravinaHr,
                    url: "https://epodravina.hr/feed",
                    newsPortalId: (int)NewsPortalId.PodravinaHr,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.SisakInfo,
                    url: "https://www.sisak.info/feed",
                    newsPortalId: (int)NewsPortalId.SisakInfo,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.PlusRegionalniTjednik,
                    url: "https://regionalni.com/feed",
                    newsPortalId: (int)NewsPortalId.PlusRegionalniTjednik,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.GlasPodravineIPrigorja,
                    url: "https://www.glaspodravine.hr/feed",
                    newsPortalId: (int)NewsPortalId.GlasPodravineIPrigorja,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.MedimurjeInfo,
                    url: "https://www.medjimurje.info/feed",
                    newsPortalId: (int)NewsPortalId.MedimurjeInfo,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.MedimurskeNovine,
                    url: "https://www.mnovine.hr/feed",
                    newsPortalId: (int)NewsPortalId.MedimurskeNovine,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.ZagorjeCom,
                    url: "https://www.zagorje.com/rss",
                    newsPortalId: (int)NewsPortalId.ZagorjeCom,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),                                                                                
                #endregion

                #region Slavonija
                new RssFeed(
                    id: (int)RssFeedId.BaranjaInfo,
                    url: "https://www.baranjainfo.hr/feed",
                    newsPortalId: (int)NewsPortalId.BaranjaInfo,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.GlasSlavonije,
                    url: "https://www.glas-slavonije.hr/rss",
                    newsPortalId: (int)NewsPortalId.GlasSlavonije,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.SlavonskiHr,
                    url: "https://slavonski.hr/feed",
                    newsPortalId: (int)NewsPortalId.SlavonskiHr,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.OsijekNews,
                    url: "https://osijeknews.hr/feed",
                    newsPortalId: (int)NewsPortalId.OsijekNews,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.InformativniCentarVirovitica,
                    url: "https://www.icv.hr/feed/",
                    newsPortalId: (int)NewsPortalId.InformativniCentarVirovitica,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.NovskaIn,
                    url: "https://www.novska.in/feed",
                    newsPortalId: (int)NewsPortalId.NovskaIn,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.NovostiHr,
                    url: "https://novosti.hr/feed",
                    newsPortalId: (int)NewsPortalId.NovostiHr,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.Portal53,
                    url: "http://portal53.hr/feed",
                    newsPortalId: (int)NewsPortalId.Portal53,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.SbPlusHr,
                    url: "https://sbplus.hr/rss",
                    newsPortalId: (int)NewsPortalId.SbPlusHr,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.PozeskaKronika,
                    url: "https://www.pozeska-kronika.hr/component/fpss/module/292.feed?type=rss",
                    newsPortalId: (int)NewsPortalId.PozeskaKronika,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.Osijek031,
                    url: "http://www.osijek031.com/news_rss.php",
                    newsPortalId: (int)NewsPortalId.Osijek031,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),                                                                                              
                #endregion

            };

            builder.HasData(localRssFeeds);
        }

        private static void SeedAmpConfigurations(OwnedNavigationBuilder<RssFeed, AmpConfiguration> ampConfigurationBuilder)
        {
            #region Index
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
            #endregion

            #region 24 Sata
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
            #endregion

            #region NetHr
            ampConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.NetHr,
                HasAmpArticles = true,
                TemplateUrl = "https://net.hr/{1}{2}{3}amp"
            });
            #endregion

            #region Vecernji List
            ampConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.VecernjiList,
                HasAmpArticles = true,
                TemplateUrl = "https://m.vecernji.hr/amp/{1}{2}"
            });
            #endregion

            #region Dnevnik
            ampConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Dnevnik,
                HasAmpArticles = true,
                TemplateUrl = "https://dnevnik.hr/amp/{1}{2}{3}"
            });
            #endregion

            #region  Gol
            ampConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Gol_Sport,
                HasAmpArticles = true,
                TemplateUrl = "https://gol.dnevnik.hr/amp/{1}{2}{3}{4}"
            });
            #endregion

            #region  Zimo
            ampConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Zimo_TechVijesti,
                HasAmpArticles = true,
                TemplateUrl = "https://zimo.dnevnik.hr/amp/clanak/{2}"
            });
            #endregion

            #region Netokracija
            ampConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Netokracija,
                HasAmpArticles = true,
                TemplateUrl = "https://www.netokracija.com/{1}/amp"
            });
            #endregion
        }

        private static void SeedSkipParseConfiguration(OwnedNavigationBuilder<RssFeed, SkipParseConfiguration> skipParseConfigurationBuilder)
        {
            #region Jutarnji List
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.JutarnjiList,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            #endregion

            #region Slobodna Dalmacija
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
            #endregion

            #region Poslovni Puls
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.PoslovniPuls,
                NumberOfSkips = 10,
                CurrentSkip = 0
            });
            #endregion

            #region 100posto
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.StoPosto,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            #endregion

            #region Express
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Express,
                NumberOfSkips = 10,
                CurrentSkip = 0
            });
            #endregion

            #region HRT
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
            #endregion

            #region SlobodnaDalmacija_Dalmacija
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Dalmacija,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            #endregion

            #region SlobodnaDalmacija_Split
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Split,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            #endregion

            #region Baskteball.Hr
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.BasketballHr,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            #endregion            
        }

        private static void SeedCategoryParseConfiguration(
            OwnedNavigationBuilder<RssFeed, CategoryParseConfiguration> categoryParseConfigurationBuilder
        )
        {
            #region  Jutarnji
            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.JutarnjiList,
                CategoryParseStrategy = CategoryParseStrategy.FromUrl

            });
            #endregion

            #region  Net.hr
            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.NetHr,
                CategoryParseStrategy = CategoryParseStrategy.FromUrl

            });
            #endregion

            #region Večernji List
            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.VecernjiList,
                CategoryParseStrategy = CategoryParseStrategy.FromUrl

            });
            #endregion

            #region Telegram
            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Telegram,
                CategoryParseStrategy = CategoryParseStrategy.FromUrl
            });
            #endregion

            // Dnevnik
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Dnevnik, CategoryParseStrategy = CategoryParseStrategy.FromUrl });

            #region N1
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.N1, CategoryParseStrategy = CategoryParseStrategy.FromUrl });
            #endregion

            #region NarodHr
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.NarodHr, CategoryParseStrategy = CategoryParseStrategy.FromUrl });
            #endregion

            #region 100posto
            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.StoPosto,
                CategoryParseStrategy = CategoryParseStrategy.FromUrl

            });
            #endregion

            #region Dnevno
            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Dnevno,
                CategoryParseStrategy = CategoryParseStrategy.FromUrl

            });
            #endregion

            #region Scena
            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Scena,
                CategoryParseStrategy = CategoryParseStrategy.FromUrl,
            });
            #endregion

            #region Express
            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Express,
                CategoryParseStrategy = CategoryParseStrategy.FromUrl,
            });
            #endregion

            #region Otvorenohr
            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.OtvorenoHr,
                CategoryParseStrategy = CategoryParseStrategy.FromUrl,
            });
            #endregion

            #region GeoPolitika
            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.GeoPolitika,
                CategoryParseStrategy = CategoryParseStrategy.FromUrl,
            });
            #endregion

            #region 7Dnevno
            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Dnevno7,
                CategoryParseStrategy = CategoryParseStrategy.FromUrl,
            });
            #endregion                      
        }

        private static void SeedImageUrlParseConfiguration(
            OwnedNavigationBuilder<RssFeed, ImageUrlParseConfiguration> imageUrlParseConfigurationBuilder
        )
        {
            #region Index
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Index_Vijesti, ImgElementXPath = "//figure[contains(@class, 'img-container')]//img", ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Index_Sport, ImgElementXPath = "//figure[contains(@class, 'img-container')]//img", ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Index_Magazin, ImgElementXPath = "//figure[contains(@class, 'img-container')]//img", ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Index_Rogue, ImgElementXPath = "//figure[contains(@class, 'img-container')]//img", ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Index_Auto, ImgElementXPath = "//figure[contains(@class, 'img-container')]//img", ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary });
            #endregion

            #region 24 sata
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Vijesti, ImgElementXPath = "//img[contains(@class, 'article__figure_img')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Show, ImgElementXPath = "//img[contains(@class, 'article__figure_img')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Sport, ImgElementXPath = "//img[contains(@class, 'article__figure_img')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Lifestyle, ImgElementXPath = "//img[contains(@class, 'article__figure_img')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Tech, ImgElementXPath = "//img[contains(@class, 'article__figure_img')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Viral, ImgElementXPath = "//img[contains(@class, 'article__figure_img')]", });
            #endregion

            #region Sportske Novosti
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SportskeNovosti_Sport, ImgElementXPath = "//img[contains(@class, 'media-object adaptive lazy')]", });
            #endregion

            #region Jutarnji
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.JutarnjiList, ImgElementXPath = "//img[contains(@class, 'media-object adaptive lazy')]", });
            #endregion

            #region Net.hr
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.NetHr, ImgElementXPath = "//div[contains(@class, 'featured-img')]//img", });
            #endregion

            #region Slobodna Dalmacija
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
            #endregion

            #region TPortal
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_Vijesti, ImgElementXPath = "//img[contains(@class, 'lateImage lateImageLoaded')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_Biznis, ImgElementXPath = "//img[contains(@class, 'lateImage lateImageLoaded')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_Sport, ImgElementXPath = "//img[contains(@class, 'lateImage lateImageLoaded')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_Tehno, ImgElementXPath = "//img[contains(@class, 'lateImage lateImageLoaded')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_Showtime, ImgElementXPath = "//img[contains(@class, 'lateImage lateImageLoaded')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_Lifestyle, ImgElementXPath = "//img[contains(@class, 'lateImage lateImageLoaded')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_FunBox, ImgElementXPath = "//img[contains(@class, 'lateImage lateImageLoaded')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_Kultura, ImgElementXPath = "//img[contains(@class, 'lateImage lateImageLoaded')]", });
            #endregion

            #region Večernji List
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.VecernjiList,

                ImgElementXPath = "//script[contains(@type, 'application/ld+json')]",
                ShouldImageUrlBeWebScraped = true,
                ImageUrlWebScrapeType = ImageUrlWebScrapeType.JsonObjectInScriptElement,
                JsonWebScrapePropertyNames = "image,url"
            });
            #endregion

            #region  Telegram
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Telegram, ImgElementXPath = "//div[contains(@class, 'thumb')]//img", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Telegram_Telesport, ImgElementXPath = "//div[contains(@class, 'featured-img')]//img", });
            #endregion

            #region Dnevnik
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Dnevnik, ImgElementXPath = "//figure[contains(@class, 'article-main-img')]//img", });
            #endregion

            #region  Gol
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Gol_Sport, ImgElementXPath = "//figure[contains(@class, 'article-image main-image')]//img", });
            #endregion

            #region Rtl Vijesti
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.RtlVijesti_Sport, ImgElementXPath = "//img[contains(@class, 'naslovna')]", });
            #endregion

            #region Nogometni Plus
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.NogometPlus_Nogomet, ImgElementXPath = "//div[contains(@class, 'post-img')]//img", });
            #endregion

            #region  Lider
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Lider_BiznisIPolitikaHrvatska, ImgElementXPath = "//img[contains(@class, 'card__image')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Lider_BiznisIPolitikaSvijet, ImgElementXPath = "//img[contains(@class, 'card__image')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Lider_Trziste, ImgElementXPath = "//img[contains(@class, 'card__image')]", });
            #endregion

            #region  Bug
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Bug_TechVijesti, ImgElementXPath = "//div[contains(@class, 'entry-content')]//img", });
            #endregion

            #region Vidi.Hr
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.VidiHr_TechVijesti, ImgElementXPath = "//div[contains(@class, 'attribute-image')]//img", });
            #endregion

            #region Zimo
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Zimo_TechVijesti, ImgElementXPath = "//div[contains(@class, 'img-holder')]//img", });
            #endregion

            #region Netokracija
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Netokracija, ImgElementXPath = "//div[contains(@class, 'post__hero')]//img", });
            #endregion

            #region Poslovni Plus
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.PoslovniPuls, ImgElementXPath = "//div[contains(@class, 'postFeaturedImg postFeaturedImg--single')]//img", });
            #endregion

            #region PCChip
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.PcChip, ImgElementXPath = "//div[contains(@class, 'td-post-featured-image')]//img", });
            #endregion

            #region Cosmopolitan
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Cosmopolitan, ImgElementXPath = "//div[contains(@class, 'first-image')]//img", });
            #endregion

            #region Wall.hr
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.WallHr, ImageUrlParseStrategy = ImageUrlParseStrategy.FromContent, ImgElementXPath = "//figure[contains(@class, 'dcms-image article-image')]//img", });
            #endregion

            #region  Ljepota i zdravlje
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.LjepotaIZdravlje, ImgElementXPath = "//div[contains(@class, 'post-thumbnail')]//img", });
            #endregion

            #region Autonet
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.AutoNet, ImgElementXPath = "//figure[contains(@class, 'figure')]//img", });
            #endregion

            #region N1        
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.N1,
                ImgElementXPath = "//figure[contains(@class, 'media')]//img",
                ImageUrlParseStrategy = ImageUrlParseStrategy.FromElementExtension
            });
            #endregion

            #region NarodHr        
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.NarodHr,
                ImgElementXPath = "//div[contains(@class, 'td-post-featured-image')]//img",
            });
            #endregion

            #region Hrt        
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
            #endregion

            #region 100posto
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.StoPosto,
                ImgElementXPath = "//picture[contains(@class, 'pic')]//img",
            });
            #endregion

            #region Dnevno
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Dnevno,
                ImgElementXPath = "//div[contains(@class, 'img-holder inner')]//img",
            });
            #endregion

            #region DirektHr
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
            #endregion

            #region Scena
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Scena,
                ImgElementXPath = "//div[contains(@class, 'mycontent')]//img",
            });
            #endregion

            #region Nacional
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Nacional,
                ImgElementXPath = "//div[contains(@class, 'single-post-media')]//img",
            });
            #endregion

            #region Express
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Express,
                ImgElementXPath = "//img[contains(@class, 'article__figure_img')]",
            });
            #endregion

            #region OtvorenoHr
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.OtvorenoHr,
                ImgElementXPath = "//div[contains(@class, 'td-post-featured-image')]//img",
            });
            #endregion   

            #region GeoPolitika
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.GeoPolitika,
                ImgElementXPath = "//div[contains(@class, 'entry-image featured-image')]//img",
            });
            #endregion

            #region Povijesthr
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.PovijestHr,
                ImgElementXPath = "//div[contains(@class, 'td-post-featured-image')]//img",
            });
            #endregion

            #region 7Dnevno
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Dnevno7,
                ImgElementXPath = "//div[contains(@class, 'td-post-featured-image')]//img",
            });
            #endregion     

            #region Baskteball.Hr
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.BasketballHr,
                ImgElementXPath = "//div[contains(@class, 'img')]//img",
            });
            #endregion                                    
        }

        private static void SeedLocalImageUrlParseConfiguration(
            OwnedNavigationBuilder<RssFeed, ImageUrlParseConfiguration> imageUrlParseConfigurationBuilder
        )
        {
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DalmacijaDanas,
                ImgElementXPath = "//div[contains(@class, 'td-full-screen-header-image-wrap')]//img",
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.IndexHrZagreb,
                ImgElementXPath = "//figure[contains(@class, 'img-container')]//img",
                ImageUrlParseStrategy = ImageUrlParseStrategy.FromContent
            });
        }
    }
}
