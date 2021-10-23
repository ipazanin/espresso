// RssFeedImageUrlParseConfigurationSeed.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Espresso.Domain.Enums.RssFeedEnums;
using Espresso.Domain.ValueObjects.RssFeedValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.DataSeed
{
    /// <summary>
    /// <see cref="ImageUrlParseConfiguration"/> data seed.
    /// </summary>
    internal static class RssFeedImageUrlParseConfigurationSeed
    {
        /// <summary>
        /// Seeds entity data.
        /// </summary>
        /// <param name="builder">Entity builder.</param>
        public static void Seed(EntityTypeBuilder<RssFeed> builder)
        {
            var imageUrlParseConfigurationBuilder = builder.OwnsOne(rssFeed => rssFeed.ImageUrlParseConfiguration);

            SeedImageUrlParseConfiguration(imageUrlParseConfigurationBuilder);
        }

        private static void SeedImageUrlParseConfiguration(
            OwnedNavigationBuilder<RssFeed, ImageUrlParseConfiguration> imageUrlParseConfigurationBuilder)
        {
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Index_Vijesti, XPath = "//figure[contains(@class, 'img-container')]//img", ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Index_Sport, XPath = "//figure[contains(@class, 'img-container')]//img", ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Index_Magazin, XPath = "//figure[contains(@class, 'img-container')]//img", ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Index_Rogue, XPath = "//figure[contains(@class, 'img-container')]//img", ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Index_Auto, XPath = "//figure[contains(@class, 'img-container')]//img", ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary });

            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Vijesti, XPath = "//img[contains(@class, 'article__figure_img')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Show, XPath = "//img[contains(@class, 'article__figure_img')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Sport, XPath = "//img[contains(@class, 'article__figure_img')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Lifestyle, XPath = "//img[contains(@class, 'article__figure_img')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Tech, XPath = "//img[contains(@class, 'article__figure_img')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Viral, XPath = "//img[contains(@class, 'article__figure_img')]", });

            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SportskeNovosti_Sport, XPath = "//img[contains(@class, 'media-object adaptive lazy')]", });

            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.JutarnjiList, XPath = "//img[contains(@class, 'media-object adaptive lazy')]", });

            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.NetHr, XPath = "//div[contains(@class, 'featured-img')]//img", });

            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Novosti, XPath = "//img[contains(@class, 'card__image')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Sport, XPath = "//img[contains(@class, 'card__image')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Showbiz, XPath = "//img[contains(@class, 'card__image')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Trend, XPath = "//img[contains(@class, 'card__image')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_DrustvenaMreza, XPath = "//img[contains(@class, 'card__image')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Zivot, XPath = "//img[contains(@class, 'card__image')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Zdravlje, XPath = "//img[contains(@class, 'card__image')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Moda, XPath = "//img[contains(@class, 'card__image')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Ljepota, XPath = "//img[contains(@class, 'card__image')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Putovanja, XPath = "//img[contains(@class, 'card__image')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Gastro, XPath = "//img[contains(@class, 'card__image')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Dom, XPath = "//img[contains(@class, 'card__image')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Tehnologija, XPath = "//img[contains(@class, 'card__image')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Viral, XPath = "//img[contains(@class, 'card__image')]", });

            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_Vijesti, XPath = "//img[contains(@class, 'lateImage lateImageLoaded')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_Biznis, XPath = "//img[contains(@class, 'lateImage lateImageLoaded')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_Sport, XPath = "//img[contains(@class, 'lateImage lateImageLoaded')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_Tehno, XPath = "//img[contains(@class, 'lateImage lateImageLoaded')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_Showtime, XPath = "//img[contains(@class, 'lateImage lateImageLoaded')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_Lifestyle, XPath = "//img[contains(@class, 'lateImage lateImageLoaded')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_FunBox, XPath = "//img[contains(@class, 'lateImage lateImageLoaded')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_Kultura, XPath = "//img[contains(@class, 'lateImage lateImageLoaded')]", });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.VecernjiList,

                XPath = "//script[contains(@type, 'application/ld+json')]",
                ShouldImageUrlBeWebScraped = true,
                ImageUrlWebScrapeType = ImageUrlWebScrapeType.JsonObjectInScriptElement,
                JsonWebScrapePropertyNames = "image,url",
            });

            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Telegram, XPath = "//div[contains(@class, 'thumb')]//img", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Telegram_Telesport, XPath = "//div[contains(@class, 'featured-img')]//img", });

            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Dnevnik, XPath = "//figure[contains(@class, 'article-main-img')]//img", });

            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Gol_Sport, XPath = "//figure[contains(@class, 'article-image main-image')]//img", });

            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.RtlVijesti_Sport, XPath = "//img[contains(@class, 'naslovna')]", });

            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.NogometPlus_Nogomet, XPath = "//div[contains(@class, 'post-img')]//img", });

            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Lider_BiznisIPolitikaHrvatska, XPath = "//img[contains(@class, 'card__image')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Lider_BiznisIPolitikaSvijet, XPath = "//img[contains(@class, 'card__image')]", });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Lider_Trziste, XPath = "//img[contains(@class, 'card__image')]", });

            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Bug_TechVijesti, XPath = "//div[contains(@class, 'entry-content')]//img", });

            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.VidiHr_TechVijesti, XPath = "//div[contains(@class, 'attribute-image')]//img", });

            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Zimo_TechVijesti, XPath = "//div[contains(@class, 'img-holder')]//img", });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Netokracija,
                ImageUrlParseStrategy = ImageUrlParseStrategy.FromContent,
                XPath = "//div[contains(@class, 'post__hero')]//img",
            });

            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.PoslovniPuls, XPath = "//div[contains(@class, 'postFeaturedImg postFeaturedImg--single')]//img", });

            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.PcChip, XPath = "//div[contains(@class, 'td-post-featured-image')]//img", });

            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Cosmopolitan, XPath = "//div[contains(@class, 'first-image')]//img", });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.WallHr,
                ImageUrlParseStrategy = ImageUrlParseStrategy.FromContent,
                XPath = "//figure[contains(@class, 'dcms-image article-image')]//img",
            });

            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.LjepotaIZdravlje, XPath = "//div[contains(@class, 'post-thumbnail')]//img", });

            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.AutoNet, XPath = "//figure[contains(@class, 'figure')]//img", });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.N1,
                XPath = "//figure[contains(@class, 'media')]//img",
                ImageUrlParseStrategy = ImageUrlParseStrategy.FromElementExtension,
                ElementExtensionIndex = 0,
                IsSavedInHtmlElementWithSrcAttribute = false,
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.NarodHr,
                XPath = "//div[contains(@class, 'td-post-featured-image')]//img",
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Hrt_Vijesti,
                XPath = "//div[contains(@class, 'image-slider')]//img",
            });
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Hrt_Sport,
                XPath = "//div[contains(@class, 'image-slider')]//img",
            });
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Hrt_Magazin,
                XPath = "//div[contains(@class, 'image-slider')]//img",
            });
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Hrt_Glazba,
                XPath = "//div[contains(@class, 'image-slider')]//img",
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.StoPosto,
                XPath = "//picture[contains(@class, 'pic')]//img",
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Dnevno,
                XPath = "//div[contains(@class, 'img-holder inner')]//img",
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DirektnoHr_Direkt,
                XPath = "//div[contains(@class, 'pd-hero-image')]//img",
            });
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DirektnoHr_Direktno,
                XPath = "//div[contains(@class, 'pd-hero-image')]//img",
            });
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DirektnoHr_Domovina,
                XPath = "//div[contains(@class, 'pd-hero-image')]//img",
            });
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DirektnoHr_EuSvijet,
                XPath = "//div[contains(@class, 'pd-hero-image')]//img",
            });
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DirektnoHr_Kolumne,
                XPath = "//div[contains(@class, 'pd-hero-image')]//img",
            });
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DirektnoHr_Razvoj,
                XPath = "//div[contains(@class, 'pd-hero-image')]//img",
            });
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DirektnoHr_Sport,
                XPath = "//div[contains(@class, 'pd-hero-image')]//img",
            });
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DirektnoHr_Zivot,
                XPath = "//div[contains(@class, 'pd-hero-image')]//img",
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Scena,
                XPath = "//div[contains(@class, 'mycontent')]//img",
                ImageUrlParseStrategy = ImageUrlParseStrategy.FromElementExtension,
                ElementExtensionIndex = 1,
                IsSavedInHtmlElementWithSrcAttribute = true,
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Nacional,
                XPath = "//div[contains(@class, 'single-post-media')]//img",
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Express,
                XPath = "//img[contains(@class, 'article__figure_img')]",
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.OtvorenoHr,
                XPath = "//div[contains(@class, 'td-post-featured-image')]//img",
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.GeoPolitika,
                XPath = "//div[contains(@class, 'entry-image featured-image')]//img",
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.PovijestHr,
                XPath = "//div[contains(@class, 'td-post-featured-image')]//img",
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Dnevno7,
                XPath = "//div[contains(@class, 'td-post-featured-image')]//img",
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.BasketballHr,
                XPath = "//div[contains(@class, 'img')]//img",
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.IctBusiness,
                XPath = "//div[contains(@class, 'main-content')]//img",
                ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary,
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Hcl,
                XPath = "//div[contains(@class, 'article')]//img",
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.ProfitirajHr,
                XPath = "//div[contains(@class, 'site-content')]//img",
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.MotoriHr,
                XPath = "//div[contains(@class, 'content')]//img",
                ImageUrlParseStrategy = ImageUrlParseStrategy.FromElementExtension,
                ElementExtensionIndex = 1,
                IsSavedInHtmlElementWithSrcAttribute = true,
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.AutoportalHr,
                XPath = "//div[contains(@class, 'td-post-content')]//img",
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.AutopressHr,
                XPath = "//div[contains(@class, 'td-post-featured-image')]//img",
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.VozimHr,
                XPath = "//div[contains(@class, 'intro-image-over')]//img",
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.AutoMotorSport,
                XPath = "//main[contains(@class, 'main-content')]//img",
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Hoopster,
                XPath = "//div[contains(@class, 'post-img')]//img",
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.PrvaHnl,
                XPath = "//div[contains(@class, 'news')]//img",
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.AlJazeera,
                XPath = "//div[contains(@class, 'field-items')]//img",
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.HifiMedia,
                ShouldImageUrlBeWebScraped = false,
                XPath = "//figure[contains(@class, 'post-gallery')]//img",
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.GeekHr,
                XPath = "//div[contains(@class, 'zox-post-main')]//img",
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.VizKultura,
                XPath = "//div[contains(@class, 'content')]//img",
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.ZivotUmjetnosti,
                ShouldImageUrlBeWebScraped = false,
                XPath = string.Empty,
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.SvijetKulture,
                XPath = "//div[contains(@class, 'td-post-featured-image')]//img",
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.GamerHr,
                XPath = "//div[contains(@class, 'site-featured-image')]//img",
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.BitnoNet,
                XPath = "//section[contains(@class, 'article-content')]//picture[contains(@class, 'wp-caption')]//img[@data-lazy-src]",
                AttributeName = "data-lazy-src",
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.MobHr,
                ImageUrlParseStrategy = ImageUrlParseStrategy.FromElementExtension,
                ElementExtensionIndex = 2,
                IsSavedInHtmlElementWithSrcAttribute = true,
            });
        }
    }
}
