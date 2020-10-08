using System.Collections.Generic;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.CategoryEnums;
using Espresso.Domain.Enums.NewsPortalEnums;
using Espresso.Domain.Enums.RssFeedEnums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.DataSeed
{
    internal static class RssFeedDataSeed
    {
        public static void Seed(
            EntityTypeBuilder<RssFeed> builder
        )
        {
            SeedRssFeeds(builder);
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

                new RssFeed(
                    id: (int)RssFeedId.Hcl,
                    url: "https://www.hcl.hr/feed",
                    newsPortalId: (int)NewsPortalId.Hcl,
                    categoryId: (int)CategoryId.Tech,
                    requestType: RequestType.Normal
                ),

                new RssFeed(
                    id: (int)RssFeedId.ProfitirajHr,
                    url: "https://profitiraj.hr/feed",
                    newsPortalId: (int)NewsPortalId.ProfitirajHr,
                    categoryId: (int)CategoryId.Biznis,
                    requestType: RequestType.Normal
                ),

                new RssFeed(
                    id: (int)RssFeedId.MotoriHr,
                    url: "https://www.motori.hr/feed",
                    newsPortalId: (int)NewsPortalId.MotoriHr,
                    categoryId: (int)CategoryId.AutoMoto,
                    requestType: RequestType.Normal
                ),

                new RssFeed(
                    id: (int)RssFeedId.AutoportalHr,
                    url: "https://autoportal.hr/feed",
                    newsPortalId: (int)NewsPortalId.AutoportalHr,
                    categoryId: (int)CategoryId.AutoMoto,
                    requestType: RequestType.Normal
                ),

                new RssFeed(
                    id: (int)RssFeedId.AutopressHr,
                    url: "https://www.autopress.hr/feed",
                    newsPortalId: (int)NewsPortalId.AutopressHr,
                    categoryId: (int)CategoryId.AutoMoto,
                    requestType: RequestType.Normal
                ),

                new RssFeed(
                    id: (int)RssFeedId.VozimHr,
                    url: "https://vozim.hr/feed",
                    newsPortalId: (int)NewsPortalId.VozimHr,
                    categoryId: (int)CategoryId.AutoMoto,
                    requestType: RequestType.Normal
                ),

                new RssFeed(
                    id: (int)RssFeedId.AutoMotorSport,
                    url: "https://ams.hr/feed",
                    newsPortalId: (int)NewsPortalId.AutoMotorSport,
                    categoryId: (int)CategoryId.AutoMoto,
                    requestType: RequestType.Normal
                ),

                new RssFeed(
                    id: (int)RssFeedId.Hoopster,
                    url: "http://hoopster.hr/feed",
                    newsPortalId: (int)NewsPortalId.Hoopster,
                    categoryId: (int)CategoryId.Sport,
                    requestType: RequestType.Normal
                ),

                new RssFeed(
                    id: (int)RssFeedId.PrvaHnl,
                    url: "http://prvahnl.hr/rss",
                    newsPortalId: (int)NewsPortalId.PrvaHnl,
                    categoryId: (int)CategoryId.Sport,
                    requestType: RequestType.Normal
                ),

                new RssFeed(
                    id: (int)RssFeedId.AlJazeera,
                    url: "http://balkans.aljazeera.net/mobile/articles",
                    newsPortalId: (int)NewsPortalId.AlJazeera,
                    categoryId: (int)CategoryId.Vijesti,
                    requestType: RequestType.Normal
                ),

                new RssFeed(
                    id: (int)RssFeedId.HifiMedia,
                    url: "https://www.hifimedia.hr/feed",
                    newsPortalId: (int)NewsPortalId.HifiMedia,
                    categoryId: (int)CategoryId.Tech,
                    requestType: RequestType.Browser
                ),

                new RssFeed(
                    id: (int)RssFeedId.GeekHr,
                    url: "https://geek.hr/feed",
                    newsPortalId: (int)NewsPortalId.GeekHr,
                    categoryId: (int)CategoryId.Tech,
                    requestType: RequestType.Normal
                ),

                new RssFeed(
                    id: (int)RssFeedId.VizKultura,
                    url: "https://vizkultura.hr/feed",
                    newsPortalId: (int)NewsPortalId.VizKultura,
                    categoryId: (int)CategoryId.Kultura,
                    requestType: RequestType.Normal
                ),

                new RssFeed(
                    id: (int)RssFeedId.ZivotUmjetnosti,
                    url: "https://zivotumjetnosti.ipu.hr/feed",
                    newsPortalId: (int)NewsPortalId.ZivotUmjetnosti,
                    categoryId: (int)CategoryId.Kultura,
                    requestType: RequestType.Normal
                ),

                new RssFeed(
                    id: (int)RssFeedId.SvijetKulture,
                    url: "https://svijetkulture.com/feed",
                    newsPortalId: (int)NewsPortalId.SvijetKulture,
                    categoryId: (int)CategoryId.Kultura,
                    requestType: RequestType.Normal
                ),

            };

            builder.HasData(rssFeeds);
        }
    }
}
