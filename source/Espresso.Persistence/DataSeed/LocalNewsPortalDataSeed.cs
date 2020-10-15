using System;
using System.Collections.Generic;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.CategoryEnums;
using Espresso.Domain.Enums.NewsPortalEnums;
using Espresso.Domain.Enums.RegionEnums;
using Espresso.Domain.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.DataSeed
{
    internal static class LocalNewsPortalDataSeed
    {

        public static void SeedData(EntityTypeBuilder<NewsPortal> builder)
        {
            SeedLocalNewsPortals(builder);
        }

        private static void SeedLocalNewsPortals(EntityTypeBuilder<NewsPortal> builder)
        {
            var localNewsPortals = new List<NewsPortal>
            {
                #region Dalmacija
                new NewsPortal(
                    id:(int)NewsPortalId.DalmacijaDanas,
                    name: NewsPortalId.DalmacijaDanas.GetDisplayName(),
                    baseUrl: "https://www.dalmacijadanas.hr",
                    iconUrl: $"Icons/{NewsPortalId.DalmacijaDanas}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Dalmacija
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.DalmacijaNews,
                    name: NewsPortalId.DalmacijaNews.GetDisplayName(),
                    baseUrl: "https://www.dalmacijanews.hr",
                    iconUrl: $"Icons/{NewsPortalId.DalmacijaNews}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Dalmacija
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.DalmatinskiPortal,
                    name: NewsPortalId.DalmatinskiPortal.GetDisplayName(),
                    baseUrl: "https://dalmatinskiportal.hr",
                    iconUrl: $"Icons/{NewsPortalId.DalmatinskiPortal}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Dalmacija
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.DubrovackiDnevnik,
                    name: NewsPortalId.DubrovackiDnevnik.GetDisplayName(),
                    baseUrl: "https://dubrovackidnevnik.net.hr",
                    iconUrl: $"Icons/{NewsPortalId.DubrovackiDnevnik}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Dalmacija
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.SlobodnaDalmacija_Dalmacija,
                    name: NewsPortalId.SlobodnaDalmacija_Dalmacija.GetDisplayName(),
                    baseUrl: "https://slobodnadalmacija.hr",
                    iconUrl: $"Icons/{NewsPortalId.SlobodnaDalmacija_Dalmacija}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 8, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Dalmacija
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.SlobodnaDalmacija_Split,
                    name: NewsPortalId.SlobodnaDalmacija_Split.GetDisplayName(),
                    baseUrl: "https://slobodnadalmacija.hr",
                    iconUrl: $"Icons/{NewsPortalId.SlobodnaDalmacija_Split}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 8, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Dalmacija
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.DubrovnikNet,
                    name: NewsPortalId.DubrovnikNet.GetDisplayName(),
                    baseUrl: "https://www.dubrovniknet.hr",
                    iconUrl: $"Icons/{NewsPortalId.DubrovnikNet}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 8, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Dalmacija
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.MakarskaDanas,
                    name: NewsPortalId.MakarskaDanas.GetDisplayName(),
                    baseUrl: "https://makarska-danas.com",
                    iconUrl: $"Icons/{NewsPortalId.MakarskaDanas}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 8, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Dalmacija
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.MakarskaHr,
                    name: NewsPortalId.MakarskaHr.GetDisplayName(),
                    baseUrl: "https://makarska.hr",
                    iconUrl: $"Icons/{NewsPortalId.MakarskaHr}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 8, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Dalmacija
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.PortalOko,
                    name: NewsPortalId.PortalOko.GetDisplayName(),
                    baseUrl: "http://www.portaloko.hr",
                    iconUrl: $"Icons/{NewsPortalId.PortalOko}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 8, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Dalmacija
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.AntenaZadar,
                    name: NewsPortalId.AntenaZadar.GetDisplayName(),
                    baseUrl: "https://www.antenazadar.hr",
                    iconUrl: $"Icons/{NewsPortalId.AntenaZadar}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 8, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Dalmacija
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.RadioImotski,
                    name: NewsPortalId.RadioImotski.GetDisplayName(),
                    baseUrl: "https://radioimotski.hr",
                    iconUrl: $"Icons/{NewsPortalId.RadioImotski}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 8, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Dalmacija
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.ImotskeNovine,
                    name: NewsPortalId.ImotskeNovine.GetDisplayName(),
                    baseUrl: "https://imotskenovine.hr",
                    iconUrl: $"Icons/{NewsPortalId.ImotskeNovine}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 8, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Dalmacija
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.PortalKastela,
                    name: NewsPortalId.PortalKastela.GetDisplayName(),
                    baseUrl: "http://www.kastela.org",
                    iconUrl: $"Icons/{NewsPortalId.PortalKastela}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 8, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Dalmacija
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.HukNet,
                    name: NewsPortalId.HukNet.GetDisplayName(),
                    baseUrl: "https://huknet1.hr",
                    iconUrl: $"Icons/{NewsPortalId.HukNet}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 8, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Dalmacija
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.ZadarskiList,
                    name: NewsPortalId.ZadarskiList.GetDisplayName(),
                    baseUrl: "https://www.zadarskilist.hr",
                    iconUrl: $"Icons/{NewsPortalId.ZadarskiList}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 8, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Dalmacija
                ),                                                                                                                                                                                                                                       
                #endregion

                #region Istra i Kvarner
                // new NewsPortal(
                //     id:(int)NewsPortalId.IPazin,
                //     name: NewsPortalId.IPazin.GetDisplayName(),
                //     baseUrl: "https://www.ipazin.net",
                //     iconUrl: $"Icons/{NewsPortalId.IPazin}{FileExtensionConstants.Png}",
                //     isNewOverride: null,
                //     createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                //     categoryId: (int)CategoryId.Local,
                //     regionId: (int)RegionId.Istra
                // ),
                new NewsPortal(
                    id:(int)NewsPortalId.NoviList,
                    name: NewsPortalId.NoviList.GetDisplayName(),
                    baseUrl: "https://www.novilist.hr",
                    iconUrl: $"Icons/{NewsPortalId.NoviList}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Istra
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.Parentium,
                    name: NewsPortalId.Parentium.GetDisplayName(),
                    baseUrl: "https://www.parentium.com",
                    iconUrl: $"Icons/{NewsPortalId.Parentium}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Istra
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.IVijesti,
                    name: NewsPortalId.IVijesti.GetDisplayName(),
                    baseUrl: "https://ivijesti.hr",
                    iconUrl: $"Icons/{NewsPortalId.IVijesti}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Istra
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.IstarskaZupanija,
                    name: NewsPortalId.IstarskaZupanija.GetDisplayName(),
                    baseUrl: "http://www.istra-istria.hr",
                    iconUrl: $"Icons/{NewsPortalId.IstarskaZupanija}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Istra
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.IstraTerraMagica,
                    name: NewsPortalId.IstraTerraMagica.GetDisplayName(),
                    baseUrl: "https://www.istriaterramagica.eu",
                    iconUrl: $"Icons/{NewsPortalId.IstraTerraMagica}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 8, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Istra
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.IPress,
                    name: NewsPortalId.IPress.GetDisplayName(),
                    baseUrl: "https://www.ipress.hr",
                    iconUrl: $"Icons/{NewsPortalId.IPress}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 8, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Istra
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.RijekaDanas,
                    name: NewsPortalId.RijekaDanas.GetDisplayName(),
                    baseUrl: "https://www.rijekadanas.com",
                    iconUrl: $"Icons/{NewsPortalId.RijekaDanas}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 8, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Istra
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.Fiuman,
                    name: NewsPortalId.Fiuman.GetDisplayName(),
                    baseUrl: "https://www.fiuman.hr",
                    iconUrl: $"Icons/{NewsPortalId.Fiuman}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 8, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Istra
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.Riportal,
                    name: NewsPortalId.Riportal.GetDisplayName(),
                    baseUrl: "https://riportal.net.hr",
                    iconUrl: $"Icons/{NewsPortalId.Riportal}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 8, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Istra
                ),                                                                                                       
                #endregion

                #region Lika
                new NewsPortal(
                    id:(int)NewsPortalId.LikaKlub,
                    name: NewsPortalId.LikaKlub.GetDisplayName(),
                    baseUrl: "https://likaclub.eu",
                    iconUrl: $"Icons/{NewsPortalId.LikaKlub}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Lika
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.LikaExpress,
                    name: NewsPortalId.LikaExpress.GetDisplayName(),
                    baseUrl: "http://www.lika-express.hr",
                    iconUrl: $"Icons/{NewsPortalId.LikaExpress}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Lika
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.LikaOnline,
                    name: NewsPortalId.LikaOnline.GetDisplayName(),
                    baseUrl: "https://www.lika-online.com",
                    iconUrl: $"Icons/{NewsPortalId.LikaOnline}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Lika
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.LikaPlus,
                    name: NewsPortalId.LikaPlus.GetDisplayName(),
                    baseUrl: "http://www.likaplus.hr",
                    iconUrl: $"Icons/{NewsPortalId.LikaPlus}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Lika
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.GsPress,
                    name: NewsPortalId.GsPress.GetDisplayName(),
                    baseUrl: "https://www.gspress.net",
                    iconUrl: $"Icons/{NewsPortalId.GsPress}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 8, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Lika
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.OgPortal,
                    name: NewsPortalId.OgPortal.GetDisplayName(),
                    baseUrl: "https://ogportal.com",
                    iconUrl: $"Icons/{NewsPortalId.OgPortal}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 10, 15, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Lika
                ),                                  
                #endregion

                #region Zagreb
                new NewsPortal(
                    id:(int)NewsPortalId.IndexHrZagreb,
                    name: NewsPortalId.IndexHrZagreb.GetDisplayName(),
                    baseUrl: "https://www.index.hr",
                    iconUrl: $"Icons/{NewsPortalId.IndexHrZagreb}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Zagreb
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.ZagrebInfo,
                    name: NewsPortalId.ZagrebInfo.GetDisplayName(),
                    baseUrl: "https://www.zagreb.info",
                    iconUrl: $"Icons/{NewsPortalId.ZagrebInfo}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Zagreb
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.Zagrebancija,
                    name: NewsPortalId.Zagrebancija.GetDisplayName(),
                    baseUrl: "https://www.zagrebancija.com",
                    iconUrl: $"Icons/{NewsPortalId.Zagrebancija}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Zagreb
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.ZagrebOnline,
                    name: NewsPortalId.ZagrebOnline.GetDisplayName(),
                    baseUrl: "https://www.zagrebonline.hr",
                    iconUrl: $"Icons/{NewsPortalId.ZagrebOnline}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Zagreb
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.ZagrebackiList,
                    name: NewsPortalId.ZagrebackiList.GetDisplayName(),
                    baseUrl: "https://www.zagrebacki.hr",
                    iconUrl: $"Icons/{NewsPortalId.ZagrebackiList}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Zagreb
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.ZgPortal,
                    name: NewsPortalId.ZgPortal.GetDisplayName(),
                    baseUrl: "https://www.zgportal.com",
                    iconUrl: $"Icons/{NewsPortalId.ZgPortal}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Zagreb
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.GradZagreb,
                    name: NewsPortalId.GradZagreb.GetDisplayName(),
                    baseUrl: "https://www.zagreb.hr/",
                    iconUrl: $"Icons/{NewsPortalId.GradZagreb}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Zagreb
                ),                                                                                               
                #endregion

                #region Sjeverna Hrvatska
                new NewsPortal(
                    id:(int)NewsPortalId.SjeverHr,
                    name: NewsPortalId.SjeverHr.GetDisplayName(),
                    baseUrl: "https://sjever.hr",
                    iconUrl: $"Icons/{NewsPortalId.SjeverHr}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.SjevernaHrvatska
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.PrigorskiHr,
                    name: NewsPortalId.PrigorskiHr.GetDisplayName(),
                    baseUrl: "https://prigorski.hr",
                    iconUrl: $"Icons/{NewsPortalId.PrigorskiHr}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.SjevernaHrvatska
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.PodravinaHr,
                    name: NewsPortalId.PodravinaHr.GetDisplayName(),
                    baseUrl: "https://epodravina.hr",
                    iconUrl: $"Icons/{NewsPortalId.PodravinaHr}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.SjevernaHrvatska
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.SisakInfo,
                    name: NewsPortalId.SisakInfo.GetDisplayName(),
                    baseUrl: "https://www.sisak.info",
                    iconUrl: $"Icons/{NewsPortalId.SisakInfo}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.SjevernaHrvatska
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.PlusRegionalniTjednik,
                    name: NewsPortalId.PlusRegionalniTjednik.GetDisplayName(),
                    baseUrl: "https://regionalni.com",
                    iconUrl: $"Icons/{NewsPortalId.PlusRegionalniTjednik}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.SjevernaHrvatska
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.GlasPodravineIPrigorja,
                    name: NewsPortalId.GlasPodravineIPrigorja.GetDisplayName(),
                    baseUrl: "https://www.glaspodravine.hr",
                    iconUrl: $"Icons/{NewsPortalId.GlasPodravineIPrigorja}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.SjevernaHrvatska
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.MedimurjeInfo,
                    name: NewsPortalId.MedimurjeInfo.GetDisplayName(),
                    baseUrl: "https://www.medjimurje.info",
                    iconUrl: $"Icons/{NewsPortalId.MedimurjeInfo}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.SjevernaHrvatska
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.MedimurskeNovine,
                    name: NewsPortalId.MedimurskeNovine.GetDisplayName(),
                    baseUrl: "https://www.mnovine.hr",
                    iconUrl: $"Icons/{NewsPortalId.MedimurskeNovine}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.SjevernaHrvatska
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.ZagorjeCom,
                    name: NewsPortalId.ZagorjeCom.GetDisplayName(),
                    baseUrl: "https://www.zagorje.com",
                    iconUrl: $"Icons/{NewsPortalId.ZagorjeCom}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.SjevernaHrvatska
                ),                                                                                                                       
                #endregion


                #region Slavonija
                new NewsPortal(
                    id:(int)NewsPortalId.BaranjaInfo,
                    name: NewsPortalId.BaranjaInfo.GetDisplayName(),
                    baseUrl: "https://www.baranjainfo.hr",
                    iconUrl: $"Icons/{NewsPortalId.BaranjaInfo}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Slavonija
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.GlasSlavonije,
                    name: NewsPortalId.GlasSlavonije.GetDisplayName(),
                    baseUrl: "https://www.glas-slavonije.hr",
                    iconUrl: $"Icons/{NewsPortalId.GlasSlavonije}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Slavonija
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.SlavonskiHr,
                    name: NewsPortalId.SlavonskiHr.GetDisplayName(),
                    baseUrl: "https://slavonski.hr",
                    iconUrl: $"Icons/{NewsPortalId.SlavonskiHr}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Slavonija
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.OsijekNews,
                    name: NewsPortalId.OsijekNews.GetDisplayName(),
                    baseUrl: "https://osijeknews.hr",
                    iconUrl: $"Icons/{NewsPortalId.OsijekNews}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Slavonija
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.InformativniCentarVirovitica,
                    name: NewsPortalId.InformativniCentarVirovitica.GetDisplayName(),
                    baseUrl: "https://www.icv.hr",
                    iconUrl: $"Icons/{NewsPortalId.InformativniCentarVirovitica}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Slavonija
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.NovskaIn,
                    name: NewsPortalId.NovskaIn.GetDisplayName(),
                    baseUrl: "https://www.novska.in",
                    iconUrl: $"Icons/{NewsPortalId.NovskaIn}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Slavonija
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.NovostiHr,
                    name: NewsPortalId.NovostiHr.GetDisplayName(),
                    baseUrl: "https://novosti.hr",
                    iconUrl: $"Icons/{NewsPortalId.NovostiHr}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Slavonija
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.Portal53,
                    name: NewsPortalId.Portal53.GetDisplayName(),
                    baseUrl: "http://portal53.hr",
                    iconUrl: $"Icons/{NewsPortalId.Portal53}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Slavonija
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.SbPlusHr,
                    name: NewsPortalId.SbPlusHr.GetDisplayName(),
                    baseUrl: "https://sbplus.hr",
                    iconUrl: $"Icons/{NewsPortalId.SbPlusHr}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Slavonija
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.PozeskaKronika,
                    name: NewsPortalId.PozeskaKronika.GetDisplayName(),
                    baseUrl: "https://www.pozeska-kronika.hr",
                    iconUrl: $"Icons/{NewsPortalId.PozeskaKronika}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Slavonija
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.Osijek031,
                    name: NewsPortalId.Osijek031.GetDisplayName(),
                    baseUrl: "http://www.osijek031.com",
                    iconUrl: $"Icons/{NewsPortalId.Osijek031}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Slavonija
                ),                                                                                                                                                              
                #endregion                                            
            };

            builder.HasData(localNewsPortals);
        }

    }
}
