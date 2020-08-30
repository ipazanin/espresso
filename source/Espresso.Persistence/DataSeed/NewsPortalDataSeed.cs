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
    internal static class NewsPortalDataSeed
    {

        public static void SeedData(EntityTypeBuilder<NewsPortal> builder)
        {
            SeedNewsPortals(builder);
            SeedLocalNewsPortals(builder);
        }

        private static void SeedNewsPortals(EntityTypeBuilder<NewsPortal> builder)
        {
            var newsPortals = new List<NewsPortal>
            {
                new NewsPortal(
                    id:(int)NewsPortalId.Index,
                    name: NewsPortalId.Index.GetDisplayName(),
                    baseUrl: "https://www.index.hr/",
                    iconUrl: $"Icons/{NewsPortalId.Index}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.General,
                    regionId: (int)RegionId.Global
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.DvadesetCetiriSata,
                    name: NewsPortalId.DvadesetCetiriSata.GetDisplayName(),
                    baseUrl: "https://www.24sata.hr/",
                    iconUrl: $"Icons/{NewsPortalId.DvadesetCetiriSata}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.General,
                    regionId: (int)RegionId.Global
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.SportskeNovosti,
                    name: NewsPortalId.SportskeNovosti.GetDisplayName(),
                    baseUrl: "https://sportske.jutarnji.hr/",
                    iconUrl: $"Icons/{NewsPortalId.SportskeNovosti}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Sport,
                    regionId: (int)RegionId.Global
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.JutarnjiList,
                    name: NewsPortalId.JutarnjiList.GetDisplayName(),
                    baseUrl: "https://sportske.jutarnji.hr/",
                    iconUrl: $"Icons/{NewsPortalId.JutarnjiList}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.General,
                    regionId: (int)RegionId.Global
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.NetHr,
                    name: NewsPortalId.NetHr.GetDisplayName(),
                    baseUrl: "https://net.hr/",
                    iconUrl: $"Icons/{NewsPortalId.NetHr}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.General,
                    regionId: (int)RegionId.Global
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.SlobodnaDalmacija,
                    name: NewsPortalId.SlobodnaDalmacija.GetDisplayName(),
                    baseUrl: "https://slobodnadalmacija.hr/",
                    iconUrl: $"Icons/{NewsPortalId.SlobodnaDalmacija}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.General,
                    regionId: (int)RegionId.Global
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.TPortal,
                    name: NewsPortalId.TPortal.GetDisplayName(),
                    baseUrl: "https://www.tportal.hr/",
                    iconUrl: $"Icons/{NewsPortalId.TPortal}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.General,
                    regionId: (int)RegionId.Global
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.VecernjiList,
                    name: NewsPortalId.VecernjiList.GetDisplayName(),
                    baseUrl: "https://www.vecernji.hr/",
                    iconUrl: $"Icons/{NewsPortalId.VecernjiList}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.General,
                    regionId: (int)RegionId.Global
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.Telegram,
                    name: NewsPortalId.Telegram.GetDisplayName(),
                    baseUrl: "https://www.telegram.hr/",
                    iconUrl: $"Icons/{NewsPortalId.Telegram}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.General,
                    regionId: (int)RegionId.Global
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.Dnevnik,
                    name: NewsPortalId.Dnevnik.GetDisplayName(),
                    baseUrl: "https://dnevnik.hr/",
                    iconUrl: $"Icons/{NewsPortalId.Dnevnik}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.General,
                    regionId: (int)RegionId.Global
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.Gol,
                    name: NewsPortalId.Gol.GetDisplayName(),
                    baseUrl: "https://gol.dnevnik.hr/",
                    iconUrl: $"Icons/{NewsPortalId.Gol}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Sport,
                    regionId: (int)RegionId.Global
                ),

                new NewsPortal(
                    id:(int)NewsPortalId.RtlVijesti,
                    name: NewsPortalId.RtlVijesti.GetDisplayName(),
                    baseUrl: "https://sportnet.rtl.hr/",
                    iconUrl: $"Icons/{NewsPortalId.RtlVijesti}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.General,
                    regionId: (int)RegionId.Global
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.NogometPlus,
                    name: NewsPortalId.NogometPlus.GetDisplayName(),
                    baseUrl: "http://www.nogometplus.net/",
                    iconUrl: $"Icons/{NewsPortalId.NogometPlus}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Sport,
                    regionId: (int)RegionId.Global
                ), // Nemaju SLS LUL
                new NewsPortal(
                    id:(int)NewsPortalId.Lider,
                    name: NewsPortalId.Lider.GetDisplayName(),
                    baseUrl: "https://lider.media/",
                    iconUrl: $"Icons/{NewsPortalId.Lider}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc), categoryId: (int)CategoryId.Biznis,
                    regionId: (int)RegionId.Global
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.Bug,
                    name: NewsPortalId.Bug.GetDisplayName(),
                    baseUrl: "https://www.bug.hr/",
                    iconUrl: $"Icons/{NewsPortalId.Bug}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Tech,
                    regionId: (int)RegionId.Global
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.VidiHr,
                    name: NewsPortalId.VidiHr.GetDisplayName(),
                    baseUrl: "https://www.vidi.hr/",
                    iconUrl: $"Icons/{NewsPortalId.VidiHr}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Tech,
                    regionId: (int)RegionId.Global
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.Zimo,
                    name: NewsPortalId.Zimo.GetDisplayName(),
                    baseUrl: "https://zimo.dnevnik.hr/",
                    iconUrl: $"Icons/{NewsPortalId.Zimo}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Tech,
                    regionId: (int)RegionId.Global
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.Netokracija,
                    name: NewsPortalId.Netokracija.GetDisplayName(),
                    baseUrl: "https://www.netokracija.com/",
                    iconUrl: $"Icons/{NewsPortalId.Netokracija}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Tech,
                    regionId: (int)RegionId.Global
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.PoslovniPuls,
                    name: NewsPortalId.PoslovniPuls.GetDisplayName(),
                    baseUrl: "https://poslovnipuls.com/",
                    iconUrl: $"Icons/{NewsPortalId.PoslovniPuls}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Biznis,
                    regionId: (int)RegionId.Global
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.PcChip,
                    name: NewsPortalId.PcChip.GetDisplayName(),
                    baseUrl: "https://pcchip.hr/",
                    iconUrl: $"Icons/{NewsPortalId.PcChip}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Tech,
                    regionId: (int)RegionId.Global
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.Cosmopolitan,
                    name: NewsPortalId.Cosmopolitan.GetDisplayName(),
                    baseUrl: "http://www.cosmopolitan.hr/",
                    iconUrl: $"Icons/{NewsPortalId.Cosmopolitan}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Lifestyle,
                    regionId: (int)RegionId.Global
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.WallHr,
                    name: NewsPortalId.WallHr.GetDisplayName(),
                    baseUrl: "https://wall.hr/",
                    iconUrl: $"Icons/{NewsPortalId.WallHr}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Lifestyle,
                    regionId: (int)RegionId.Global
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.LjepotaIZdravlje,
                    name: NewsPortalId.LjepotaIZdravlje.GetDisplayName(),
                    baseUrl: "http://www.ljepotaizdravlje.hr/",
                    iconUrl: $"Icons/{NewsPortalId.LjepotaIZdravlje}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Lifestyle,
                    regionId: (int)RegionId.Global
                ), // Nemaju SLS LUL
                new NewsPortal(
                    id:(int)NewsPortalId.Autonet,
                    name: NewsPortalId.Autonet.GetDisplayName(),
                    baseUrl: "https://www.autonet.hr/",
                    iconUrl: $"Icons/{NewsPortalId.Autonet}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.AutoMoto,
                    regionId: (int)RegionId.Global
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.N1,
                    name:NewsPortalId.N1.GetDisplayName(),
                    baseUrl:"https://hr.n1info.com/",
                    iconUrl:$"Icons/{NewsPortalId.N1}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Vijesti,
                    regionId: (int)RegionId.Global
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.NarodHr,
                    name:NewsPortalId.NarodHr.GetDisplayName(),
                    baseUrl:"https://narod.hr/",
                    iconUrl: $"Icons/{NewsPortalId.NarodHr}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt:new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId:(int)CategoryId.Vijesti,
                    regionId: (int)RegionId.Global
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.Hrt,
                    name: NewsPortalId.Hrt.GetDisplayName(),
                    baseUrl: "https://www.hrt.hr/",
                    iconUrl: $"Icons/{NewsPortalId.Hrt}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 6, 25, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Vijesti,
                    regionId: (int)RegionId.Global
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.StoPosto,
                    name: NewsPortalId.StoPosto.GetDisplayName(),
                    baseUrl: "https://100posto.jutarnji.hr/",
                    iconUrl: $"Icons/{NewsPortalId.StoPosto}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 6, 28, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Vijesti,
                    regionId: (int)RegionId.Global
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.Dnevno,
                    name: NewsPortalId.Dnevno.GetDisplayName(),
                    baseUrl: "https://www.dnevno.hr/",
                    iconUrl: $"Icons/{NewsPortalId.Dnevno}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 6, 28, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Vijesti,
                    regionId: (int)RegionId.Global
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
                    createdAt: new DateTime(2020, 7, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Vijesti,
                    regionId: (int)RegionId.Global
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.Scena,
                    name: NewsPortalId.Scena.GetDisplayName(),
                    baseUrl: "https://www.scena.hr/",
                    iconUrl: $"Icons/{NewsPortalId.Scena}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 7, 13, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Show,
                    regionId: (int)RegionId.Global
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.Nacional,
                    name: NewsPortalId.Nacional.GetDisplayName(),
                    baseUrl: "https://www.nacional.hr/",
                    iconUrl: $"Icons/{NewsPortalId.Nacional}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Vijesti,
                    regionId: (int)RegionId.Global
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.Express,
                    name: NewsPortalId.Express.GetDisplayName(),
                    baseUrl: "https://express.24sata.hr/",
                    iconUrl: $"Icons/{NewsPortalId.Express}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Vijesti,
                    regionId: (int)RegionId.Global
                ),
            };

            builder.HasData(newsPortals);
        }

        private static void SeedLocalNewsPortals(EntityTypeBuilder<NewsPortal> builder)
        {
            var localNewsPortals = new List<NewsPortal>
            {
                #region Dalmacija
                new NewsPortal(
                    id:(int)NewsPortalId.DalmacijaDanas,
                    name: NewsPortalId.DalmacijaDanas.GetDisplayName(),
                    baseUrl: "https://www.dalmacijadanas.hr/",
                    iconUrl: $"Icons/{NewsPortalId.DalmacijaDanas}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Dalmacija
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.DalmacijaNews,
                    name: NewsPortalId.DalmacijaNews.GetDisplayName(),
                    baseUrl: "https://www.dalmacijanews.hr/",
                    iconUrl: $"Icons/{NewsPortalId.DalmacijaDanas}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Dalmacija
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.DalmatinskiPortal,
                    name: NewsPortalId.DalmatinskiPortal.GetDisplayName(),
                    baseUrl: "https://dalmatinskiportal.hr/",
                    iconUrl: $"Icons/{NewsPortalId.DalmatinskiPortal}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Dalmacija
                ),                
                #endregion

                #region Istra i Kvarner
                // new NewsPortal(
                //     id:(int)NewsPortalId.IPazin,
                //     name: NewsPortalId.IPazin.GetDisplayName(),
                //     baseUrl: "https://www.ipazin.net/",
                //     iconUrl: $"Icons/{NewsPortalId.IPazin}{FileExtensionConstants.Png}",
                //     isNewOverride: null,
                //     createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                //     categoryId: (int)CategoryId.Local,
                //     regionId: (int)RegionId.Istra
                // ),
                new NewsPortal(
                    id:(int)NewsPortalId.NoviList,
                    name: NewsPortalId.NoviList.GetDisplayName(),
                    baseUrl: "https://www.novilist.hr/",
                    iconUrl: $"Icons/{NewsPortalId.NoviList}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Istra
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.Parentium,
                    name: NewsPortalId.Parentium.GetDisplayName(),
                    baseUrl: "https://www.parentium.com/",
                    iconUrl: $"Icons/{NewsPortalId.Parentium}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Istra
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.IVijesti,
                    name: NewsPortalId.IVijesti.GetDisplayName(),
                    baseUrl: "https://ivijesti.hr/",
                    iconUrl: $"Icons/{NewsPortalId.IVijesti}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Istra
                ),                
                #endregion

                #region Lika
                new NewsPortal(
                    id:(int)NewsPortalId.LikaKlub,
                    name: NewsPortalId.LikaKlub.GetDisplayName(),
                    baseUrl: "https://likaclub.eu/",
                    iconUrl: $"Icons/{NewsPortalId.LikaKlub}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Lika
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.LikaExpress,
                    name: NewsPortalId.LikaExpress.GetDisplayName(),
                    baseUrl: "http://www.lika-express.hr/",
                    iconUrl: $"Icons/{NewsPortalId.LikaExpress}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Lika
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.LikaOnline,
                    name: NewsPortalId.LikaOnline.GetDisplayName(),
                    baseUrl: "https://www.lika-online.com/",
                    iconUrl: $"Icons/{NewsPortalId.LikaOnline}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Lika
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.LikaPlus,
                    name: NewsPortalId.LikaPlus.GetDisplayName(),
                    baseUrl: "http://www.likaplus.hr/",
                    iconUrl: $"Icons/{NewsPortalId.LikaPlus}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Lika
                ),
                #endregion

                #region Zagreb
                new NewsPortal(
                    id:(int)NewsPortalId.IndexHrZagreb,
                    name: NewsPortalId.IndexHrZagreb.GetDisplayName(),
                    baseUrl: "https://www.index.hr/",
                    iconUrl: $"Icons/{NewsPortalId.IndexHrZagreb}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Zagreb
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.ZagrebInfo,
                    name: NewsPortalId.ZagrebInfo.GetDisplayName(),
                    baseUrl: "https://www.zagreb.info/",
                    iconUrl: $"Icons/{NewsPortalId.ZagrebInfo}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Zagreb
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.Zagrebancija,
                    name: NewsPortalId.Zagrebancija.GetDisplayName(),
                    baseUrl: "https://www.zagrebancija.com/",
                    iconUrl: $"Icons/{NewsPortalId.Zagrebancija}{FileExtensionConstants.Png}",
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
                    baseUrl: "https://sjever.hr/",
                    iconUrl: $"Icons/{NewsPortalId.SjeverHr}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.SjevernaHrvatska
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.PrigorskiHr,
                    name: NewsPortalId.PrigorskiHr.GetDisplayName(),
                    baseUrl: "https://prigorski.hr/",
                    iconUrl: $"Icons/{NewsPortalId.PrigorskiHr}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.SjevernaHrvatska
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.PodravinaHr,
                    name: NewsPortalId.PodravinaHr.GetDisplayName(),
                    baseUrl: "https://epodravina.hr/",
                    iconUrl: $"Icons/{NewsPortalId.PodravinaHr}{FileExtensionConstants.Png}",
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
                    baseUrl: "https://www.baranjainfo.hr/",
                    iconUrl: $"Icons/{NewsPortalId.BaranjaInfo}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Slavonija
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.GlasSlavonije,
                    name: NewsPortalId.GlasSlavonije.GetDisplayName(),
                    baseUrl: "https://www.glas-slavonije.hr/",
                    iconUrl: $"Icons/{NewsPortalId.GlasSlavonije}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Local,
                    regionId: (int)RegionId.Slavonija
                ),
                new NewsPortal(
                    id:(int)NewsPortalId.SlavonskiHr,
                    name: NewsPortalId.SlavonskiHr.GetDisplayName(),
                    baseUrl: "https://slavonski.hr/",
                    iconUrl: $"Icons/{NewsPortalId.SlavonskiHr}{FileExtensionConstants.Png}",
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