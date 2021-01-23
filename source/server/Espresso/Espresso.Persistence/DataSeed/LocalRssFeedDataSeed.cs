using System.Collections.Generic;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.CategoryEnums;
using Espresso.Domain.Enums.NewsPortalEnums;
using Espresso.Domain.Enums.RssFeedEnums;
using Espresso.Domain.ValueObjects.RssFeedValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.DataSeed
{
    public static class LocalRssFeedDataSeed
    {
        public static void Seed(EntityTypeBuilder<RssFeed> builder)
        {
            var ampConfigurationBuilder = builder.OwnsOne(rssFeed => rssFeed.AmpConfiguration);
            var skipParseConfigurationBuilder = builder.OwnsOne(rssFeed => rssFeed.SkipParseConfiguration);
            var categoryParseConfigurationBuilder = builder.OwnsOne(rssFeed => rssFeed.CategoryParseConfiguration);
            var imageUrlParseConfigurationBuilder = builder.OwnsOne(rssFeed => rssFeed.ImageUrlParseConfiguration);

            SeedLocalRssFeeds(builder);
            SeedSkipParseConfiguration(skipParseConfigurationBuilder!);
            SeedLocalImageUrlParseConfiguration(imageUrlParseConfigurationBuilder);
        }

        private static void SeedLocalRssFeeds(EntityTypeBuilder<RssFeed> builder)
        {
            var localRssFeeds = new List<RssFeed>
            {

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
               new RssFeed(
                    id: (int)RssFeedId.OgPortal,
                    url: "https://ogportal.com/feed",
                    newsPortalId: (int)NewsPortalId.OgPortal,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),


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
                new RssFeed(
                    id: (int)RssFeedId.Zupanjac,
                    url: "https://zupanjac.net/feed",
                    newsPortalId: (int)NewsPortalId.Zupanjac,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.Press032,
                    url: "https://press032.com/feed",
                    newsPortalId: (int)NewsPortalId.Press032,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.PloceOnline,
                    url: "https://ploce.com.hr/feed",
                    newsPortalId: (int)NewsPortalId.PloceOnline,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.KaPortalHr,
                    url: "https://kaportal.net.hr/feed",
                    newsPortalId: (int)NewsPortalId.KaPortalHr,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
                new RssFeed(
                    id: (int)RssFeedId.RadioMreznica,
                    url: "https://radio-mreznica.hr/feed",
                    newsPortalId: (int)NewsPortalId.RadioMreznica,
                    categoryId: (int)CategoryId.Local,
                    requestType: RequestType.Normal
                ),
            };

            builder.HasData(localRssFeeds);
        }

        private static void SeedSkipParseConfiguration(
            OwnedNavigationBuilder<RssFeed, SkipParseConfiguration> skipParseConfigurationBuilder
        )
        {
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Dalmacija,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Split,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.OgPortal,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Zupanjac,
                NumberOfSkips = 4,
                CurrentSkip = 0
            });

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Press032,
                NumberOfSkips = 7,
                CurrentSkip = 0
            });

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.PloceOnline,
                NumberOfSkips = 3,
                CurrentSkip = 0
            });
        }

        private static void SeedLocalImageUrlParseConfiguration(
            OwnedNavigationBuilder<RssFeed, ImageUrlParseConfiguration> imageUrlParseConfigurationBuilder
        )
        {
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DalmacijaDanas,
                XPath = "//div[contains(@class, 'td-full-screen-header-image-wrap')]//img",
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.IndexHrZagreb,
                XPath = "//figure[contains(@class, 'img-container')]//img",
                ImageUrlParseStrategy = ImageUrlParseStrategy.FromContent
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Zupanjac,
                XPath = "//div[contains(@class, 'feature-img')]//img"
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.KaPortalHr,
                ImageUrlParseStrategy = ImageUrlParseStrategy.FromFirstElementExtension
            });
        }

    }
}
