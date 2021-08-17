﻿// NewsPortalDataSeed.cs
//
// © 2021 Espresso News. All rights reserved.

using System;
using System.Collections.Generic;
using Espresso.Common.Constants;
using Espresso.Common.Extensions;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.CategoryEnums;
using Espresso.Domain.Enums.NewsPortalEnums;
using Espresso.Domain.Enums.RegionEnums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.DataSeed
{
    /// <summary>
    /// <see cref="NewsPortal"/> data seed.
    /// </summary>
    internal static class NewsPortalDataSeed
    {
        /// <summary>
        /// Seeds entity data.
        /// </summary>
        /// <param name="builder">Entity builder.</param>
        public static void SeedData(EntityTypeBuilder<NewsPortal> builder)
        {
            SeedNewsPortals(builder);
        }

        private static void SeedNewsPortals(EntityTypeBuilder<NewsPortal> builder)
        {
            var newsPortals = new List<NewsPortal>
            {
                new NewsPortal(
                    id: (int)NewsPortalId.Index,
                    name: NewsPortalId.Index.GetDisplayName(),
                    baseUrl: "https://www.index.hr",
                    iconUrl: $"Icons/{NewsPortalId.Index}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.General,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.DvadesetCetiriSata,
                    name: NewsPortalId.DvadesetCetiriSata.GetDisplayName(),
                    baseUrl: "https://www.24sata.hr",
                    iconUrl: $"Icons/{NewsPortalId.DvadesetCetiriSata}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.General,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.SportskeNovosti,
                    name: NewsPortalId.SportskeNovosti.GetDisplayName(),
                    baseUrl: "https://sportske.jutarnji.hr",
                    iconUrl: $"Icons/{NewsPortalId.SportskeNovosti}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Sport,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.JutarnjiList,
                    name: NewsPortalId.JutarnjiList.GetDisplayName(),
                    baseUrl: "https://sportske.jutarnji.hr",
                    iconUrl: $"Icons/{NewsPortalId.JutarnjiList}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.General,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.NetHr,
                    name: NewsPortalId.NetHr.GetDisplayName(),
                    baseUrl: "https://net.hr",
                    iconUrl: $"Icons/{NewsPortalId.NetHr}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.General,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.SlobodnaDalmacija,
                    name: NewsPortalId.SlobodnaDalmacija.GetDisplayName(),
                    baseUrl: "https://slobodnadalmacija.hr",
                    iconUrl: $"Icons/{NewsPortalId.SlobodnaDalmacija}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.General,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.TPortal,
                    name: NewsPortalId.TPortal.GetDisplayName(),
                    baseUrl: "https://www.tportal.hr",
                    iconUrl: $"Icons/{NewsPortalId.TPortal}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.General,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.VecernjiList,
                    name: NewsPortalId.VecernjiList.GetDisplayName(),
                    baseUrl: "https://www.vecernji.hr",
                    iconUrl: $"Icons/{NewsPortalId.VecernjiList}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.General,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.Telegram,
                    name: NewsPortalId.Telegram.GetDisplayName(),
                    baseUrl: "https://www.telegram.hr",
                    iconUrl: $"Icons/{NewsPortalId.Telegram}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.General,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.Dnevnik,
                    name: NewsPortalId.Dnevnik.GetDisplayName(),
                    baseUrl: "https://dnevnik.hr",
                    iconUrl: $"Icons/{NewsPortalId.Dnevnik}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.General,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.Gol,
                    name: NewsPortalId.Gol.GetDisplayName(),
                    baseUrl: "https://gol.dnevnik.hr",
                    iconUrl: $"Icons/{NewsPortalId.Gol}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Sport,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),

                new NewsPortal(
                    id: (int)NewsPortalId.RtlVijesti,
                    name: NewsPortalId.RtlVijesti.GetDisplayName(),
                    baseUrl: "https://sportnet.rtl.hr",
                    iconUrl: $"Icons/{NewsPortalId.RtlVijesti}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.General,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.NogometPlus,
                    name: NewsPortalId.NogometPlus.GetDisplayName(),
                    baseUrl: "http://www.nogometplus.net",
                    iconUrl: $"Icons/{NewsPortalId.NogometPlus}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Sport,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ), // Nemaju SLS LUL
                new NewsPortal(
                    id: (int)NewsPortalId.Lider,
                    name: NewsPortalId.Lider.GetDisplayName(),
                    baseUrl: "https://lider.media",
                    iconUrl: $"Icons/{NewsPortalId.Lider}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Biznis,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.Bug,
                    name: NewsPortalId.Bug.GetDisplayName(),
                    baseUrl: "https://www.bug.hr",
                    iconUrl: $"Icons/{NewsPortalId.Bug}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Tech,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.VidiHr,
                    name: NewsPortalId.VidiHr.GetDisplayName(),
                    baseUrl: "https://www.vidi.hr",
                    iconUrl: $"Icons/{NewsPortalId.VidiHr}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Tech,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.Zimo,
                    name: NewsPortalId.Zimo.GetDisplayName(),
                    baseUrl: "https://zimo.dnevnik.hr",
                    iconUrl: $"Icons/{NewsPortalId.Zimo}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Tech,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.Netokracija,
                    name: NewsPortalId.Netokracija.GetDisplayName(),
                    baseUrl: "https://www.netokracija.com",
                    iconUrl: $"Icons/{NewsPortalId.Netokracija}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Tech,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.PoslovniPuls,
                    name: NewsPortalId.PoslovniPuls.GetDisplayName(),
                    baseUrl: "https://poslovnipuls.com",
                    iconUrl: $"Icons/{NewsPortalId.PoslovniPuls}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Biznis,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.PcChip,
                    name: NewsPortalId.PcChip.GetDisplayName(),
                    baseUrl: "https://pcchip.hr",
                    iconUrl: $"Icons/{NewsPortalId.PcChip}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Tech,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.Cosmopolitan,
                    name: NewsPortalId.Cosmopolitan.GetDisplayName(),
                    baseUrl: "http://www.cosmopolitan.hr",
                    iconUrl: $"Icons/{NewsPortalId.Cosmopolitan}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Lifestyle,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.WallHr,
                    name: NewsPortalId.WallHr.GetDisplayName(),
                    baseUrl: "https://wall.hr",
                    iconUrl: $"Icons/{NewsPortalId.WallHr}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Lifestyle,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.LjepotaIZdravlje,
                    name: NewsPortalId.LjepotaIZdravlje.GetDisplayName(),
                    baseUrl: "http://www.ljepotaizdravlje.hr",
                    iconUrl: $"Icons/{NewsPortalId.LjepotaIZdravlje}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Lifestyle,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ), // Nemaju SLS LUL
                new NewsPortal(
                    id: (int)NewsPortalId.Autonet,
                    name: NewsPortalId.Autonet.GetDisplayName(),
                    baseUrl: "https://www.autonet.hr",
                    iconUrl: $"Icons/{NewsPortalId.Autonet}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.AutoMoto,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.N1,
                    name: NewsPortalId.N1.GetDisplayName(),
                    baseUrl: "https://hr.n1info.com",
                    iconUrl: $"Icons/{NewsPortalId.N1}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Vijesti,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.NarodHr,
                    name: NewsPortalId.NarodHr.GetDisplayName(),
                    baseUrl: "https://narod.hr",
                    iconUrl: $"Icons/{NewsPortalId.NarodHr}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Vijesti,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.Hrt,
                    name: NewsPortalId.Hrt.GetDisplayName(),
                    baseUrl: "https://www.hrt.hr",
                    iconUrl: $"Icons/{NewsPortalId.Hrt}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 6, 25, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Vijesti,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.StoPosto,
                    name: NewsPortalId.StoPosto.GetDisplayName(),
                    baseUrl: "https://100posto.jutarnji.hr",
                    iconUrl: $"Icons/{NewsPortalId.StoPosto}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 6, 28, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Vijesti,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.Dnevno,
                    name: NewsPortalId.Dnevno.GetDisplayName(),
                    baseUrl: "https://www.dnevno.hr",
                    iconUrl: $"Icons/{NewsPortalId.Dnevno}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 6, 28, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.General,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.DirektnoHr,
                    name: NewsPortalId.DirektnoHr.GetDisplayName(),
                    baseUrl: "https://direktno.hr",
                    iconUrl: $"Icons/{NewsPortalId.DirektnoHr}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 7, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Vijesti,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.Scena,
                    name: NewsPortalId.Scena.GetDisplayName(),
                    baseUrl: "https://www.scena.hr",
                    iconUrl: $"Icons/{NewsPortalId.Scena}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 7, 13, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Show,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.Nacional,
                    name: NewsPortalId.Nacional.GetDisplayName(),
                    baseUrl: "https://www.nacional.hr",
                    iconUrl: $"Icons/{NewsPortalId.Nacional}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Vijesti,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.Express,
                    name: NewsPortalId.Express.GetDisplayName(),
                    baseUrl: "https://express.24sata.hr",
                    iconUrl: $"Icons/{NewsPortalId.Express}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Vijesti,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.OtvorenoHr,
                    name: NewsPortalId.OtvorenoHr.GetDisplayName(),
                    baseUrl: "https://otvoreno.hr",
                    iconUrl: $"Icons/{NewsPortalId.OtvorenoHr}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 10, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Vijesti,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.GeoPolitika,
                    name: NewsPortalId.GeoPolitika.GetDisplayName(),
                    baseUrl: "https://www.geopolitika.news",
                    iconUrl: $"Icons/{NewsPortalId.GeoPolitika}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 10, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Vijesti,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.PovijestHr,
                    name: NewsPortalId.PovijestHr.GetDisplayName(),
                    baseUrl: "https://povijest.hr",
                    iconUrl: $"Icons/{NewsPortalId.PovijestHr}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 10, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Vijesti,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.Dnevno7,
                    name: NewsPortalId.Dnevno7.GetDisplayName(),
                    baseUrl: "https://7dnevno.hr",
                    iconUrl: $"Icons/{NewsPortalId.Dnevno7}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 10, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Vijesti,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.BasketballHr,
                    name: NewsPortalId.BasketballHr.GetDisplayName(),
                    baseUrl: "https://basketball.hr",
                    iconUrl: $"Icons/{NewsPortalId.BasketballHr}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 14, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Sport,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.JoomBoos,
                    name: NewsPortalId.JoomBoos.GetDisplayName(),
                    baseUrl: "https://joomboos.24sata.hr",
                    iconUrl: $"Icons/{NewsPortalId.JoomBoos}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 21, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Viral,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.IctBusiness,
                    name: NewsPortalId.IctBusiness.GetDisplayName(),
                    baseUrl: "https://www.ictbusiness.info",
                    iconUrl: $"Icons/{NewsPortalId.IctBusiness}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 21, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Tech,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.Hcl,
                    name: NewsPortalId.Hcl.GetDisplayName(),
                    baseUrl: "https://www.hcl.hr",
                    iconUrl: $"Icons/{NewsPortalId.Hcl}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 23, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Tech,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.ProfitirajHr,
                    name: NewsPortalId.ProfitirajHr.GetDisplayName(),
                    baseUrl: "https://profitiraj.hr",
                    iconUrl: $"Icons/{NewsPortalId.ProfitirajHr}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 23, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Biznis,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.MotoriHr,
                    name: NewsPortalId.MotoriHr.GetDisplayName(),
                    baseUrl: "https://www.motori.hr/",
                    iconUrl: $"Icons/{NewsPortalId.MotoriHr}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 24, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.AutoMoto,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.AutoportalHr,
                    name: NewsPortalId.AutoportalHr.GetDisplayName(),
                    baseUrl: "https://autoportal.hr",
                    iconUrl: $"Icons/{NewsPortalId.AutoportalHr}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 24, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.AutoMoto,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.AutopressHr,
                    name: NewsPortalId.AutopressHr.GetDisplayName(),
                    baseUrl: "https://www.autopress.hr",
                    iconUrl: $"Icons/{NewsPortalId.AutopressHr}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 24, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.AutoMoto,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.VozimHr,
                    name: NewsPortalId.VozimHr.GetDisplayName(),
                    baseUrl: "https://vozim.hr",
                    iconUrl: $"Icons/{NewsPortalId.VozimHr}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 24, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.AutoMoto,
                    regionId: (int)RegionId.Global,
                    isEnabled: false
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.AutoMotorSport,
                    name: NewsPortalId.AutoMotorSport.GetDisplayName(),
                    baseUrl: "https://ams.hr",
                    iconUrl: $"Icons/{NewsPortalId.AutoMotorSport}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 24, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.AutoMoto,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.Hoopster,
                    name: NewsPortalId.Hoopster.GetDisplayName(),
                    baseUrl: "http://hoopster.hr",
                    iconUrl: $"Icons/{NewsPortalId.Hoopster}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 9, 30, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Sport,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.PrvaHnl,
                    name: NewsPortalId.PrvaHnl.GetDisplayName(),
                    baseUrl: "http://prvahnl.hr",
                    iconUrl: $"Icons/{NewsPortalId.PrvaHnl}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 10, 6, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Sport,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.AlJazeera,
                    name: NewsPortalId.AlJazeera.GetDisplayName(),
                    baseUrl: "http://balkans.aljazeera.net",
                    iconUrl: $"Icons/{NewsPortalId.AlJazeera}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 10, 6, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Vijesti,
                    regionId: (int)RegionId.Global,
                    isEnabled: false
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.HifiMedia,
                    name: NewsPortalId.HifiMedia.GetDisplayName(),
                    baseUrl: "https://www.hifimedia.hr",
                    iconUrl: $"Icons/{NewsPortalId.HifiMedia}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 10, 6, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Tech,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.GeekHr,
                    name: NewsPortalId.GeekHr.GetDisplayName(),
                    baseUrl: "https://geek.hr",
                    iconUrl: $"Icons/{NewsPortalId.GeekHr}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 10, 6, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Tech,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.VizKultura,
                    name: NewsPortalId.VizKultura.GetDisplayName(),
                    baseUrl: "https://vizkultura.hr",
                    iconUrl: $"Icons/{NewsPortalId.VizKultura}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 10, 8, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Kultura,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.ZivotUmjetnosti,
                    name: NewsPortalId.ZivotUmjetnosti.GetDisplayName(),
                    baseUrl: "https://zivotumjetnosti.ipu.hr",
                    iconUrl: $"Icons/{NewsPortalId.ZivotUmjetnosti}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 10, 8, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Kultura,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.SvijetKulture,
                    name: NewsPortalId.SvijetKulture.GetDisplayName(),
                    baseUrl: "https://svijetkulture.com",
                    iconUrl: $"Icons/{NewsPortalId.SvijetKulture}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 10, 8, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Kultura,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.GamerHr,
                    name: NewsPortalId.GamerHr.GetDisplayName(),
                    baseUrl: "http://www.gamer.hr",
                    iconUrl: $"Icons/{NewsPortalId.GamerHr}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2020, 10, 10, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Tech,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.BitnoNet,
                    name: NewsPortalId.BitnoNet.GetDisplayName(),
                    baseUrl: "https://www.bitno.net",
                    iconUrl: $"Icons/{NewsPortalId.BitnoNet}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2021, 1, 16, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Vijesti,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.MaxPortal,
                    name: NewsPortalId.MaxPortal.GetDisplayName(),
                    baseUrl: "https://www.maxportal.hr",
                    iconUrl: $"Icons/{NewsPortalId.MaxPortal}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2021, 3, 7, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Vijesti,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.PoslovniDnevnik,
                    name: NewsPortalId.PoslovniDnevnik.GetDisplayName(),
                    baseUrl: "https://www.poslovni.hr",
                    iconUrl: $"Icons/{NewsPortalId.PoslovniDnevnik}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2021, 3, 7, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Biznis,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.Gp1,
                    name: NewsPortalId.Gp1.GetDisplayName(),
                    baseUrl: "https://www.gp1.hr",
                    iconUrl: $"Icons/{NewsPortalId.Gp1}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2021, 3, 7, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.AutoMoto,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.F1PulsMedia,
                    name: NewsPortalId.F1PulsMedia.GetDisplayName(),
                    baseUrl: "https://f1.pulsmedia.hr",
                    iconUrl: $"Icons/{NewsPortalId.F1PulsMedia}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2021, 3, 7, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.AutoMoto,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.Racunalo,
                    name: NewsPortalId.Racunalo.GetDisplayName(),
                    baseUrl: "https://www.racunalo.com",
                    iconUrl: $"Icons/{NewsPortalId.Racunalo}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2021, 3, 7, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Tech,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.MobHr,
                    name: NewsPortalId.MobHr.GetDisplayName(),
                    baseUrl: "https://mob.hr",
                    iconUrl: $"Icons/{NewsPortalId.MobHr}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2021, 3, 7, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Tech,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.StartNews,
                    name: NewsPortalId.StartNews.GetDisplayName(),
                    baseUrl: "https://www.startnews.hr",
                    iconUrl: $"Icons/{NewsPortalId.StartNews}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2021, 3, 7, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Vijesti,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
                new NewsPortal(
                    id: (int)NewsPortalId.Viral,
                    name: NewsPortalId.Viral.GetDisplayName(),
                    baseUrl: "https://viral.hr",
                    iconUrl: $"Icons/{NewsPortalId.Viral}{FileExtensionConstants.Png}",
                    isNewOverride: null,
                    createdAt: new DateTime(2021, 4, 21, 0, 0, 0, DateTimeKind.Utc),
                    categoryId: (int)CategoryId.Viral,
                    regionId: (int)RegionId.Global,
                    isEnabled: true
                ),
            };

            builder.HasData(newsPortals);
        }
    }
}