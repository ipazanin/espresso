using Espresso.Domain.Entities;
using Espresso.Domain.Enums.RssFeedEnums;
using Espresso.Domain.ValueObjects.RssFeedValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.DataSeed
{
    internal static class RssFeedImageUrlParseConfigurationSeed
    {
        public static void Seed(
            EntityTypeBuilder<RssFeed> builder
        )
        {
            var imageUrlParseConfigurationBuilder = builder.OwnsOne(rssFeed => rssFeed.ImageUrlParseConfiguration);

            SeedImageUrlParseConfiguration(imageUrlParseConfigurationBuilder);
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

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Hcl,
                ImgElementXPath = "//div[contains(@class, 'article')]//img"
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.ProfitirajHr,
                ImgElementXPath = "//div[contains(@class, 'site-content')]//img"
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.MotoriHr,
                ImgElementXPath = "//div[contains(@class, 'content')]//img",
                ImageUrlParseStrategy = ImageUrlParseStrategy.FromSecondElementExtension
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.AutoportalHr,
                ImgElementXPath = "//div[contains(@class, 'td-post-content')]//img"
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.AutopressHr,
                ImgElementXPath = "//div[contains(@class, 'td-post-featured-image')]//img"
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.VozimHr,
                ImgElementXPath = "//div[contains(@class, 'intro-image-over')]//img"
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.AutoMotorSport,
                ImgElementXPath = "//main[contains(@class, 'main-content')]//img"
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Hoopster,
                ImgElementXPath = "//div[contains(@class, 'post-img')]//img"
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.PrvaHnl,
                ImgElementXPath = "//div[contains(@class, 'news')]//img"
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.AlJazeera,
                ImgElementXPath = "//div[contains(@class, 'field-items')]//img"
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.HifiMedia,
                ShouldImageUrlBeWebScraped = false,
                ImgElementXPath = "//figure[contains(@class, 'post-gallery')]//img"
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.GeekHr,
                ImgElementXPath = "//div[contains(@class, 'zox-post-main')]//img"
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.VizKultura,
                ImgElementXPath = "//div[contains(@class, 'content')]//img"
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.ZivotUmjetnosti,
                ShouldImageUrlBeWebScraped = false,
                ImgElementXPath = ""
            });

            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.SvijetKulture,
                ImgElementXPath = "//div[contains(@class, 'td-post-featured-image')]//img"
            });
        }
    }
}
