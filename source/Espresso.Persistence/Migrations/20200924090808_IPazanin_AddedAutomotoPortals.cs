using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Espresso.Persistence.Migrations
{
    public partial class IPazanin_AddedAutomotoPortals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "NewsPortals",
                columns: new[] { "Id", "BaseUrl", "CategoryId", "CreatedAt", "IconUrl", "IsNewOverride", "Name", "RegionId" },
                values: new object?[,]
                {
                    { 108, "https://www.motori.hr/", 8, new DateTime(2020, 9, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/MotoriHr.png", null, "motori.hr", 1 },
                    { 109, "https://autoportal.hr", 8, new DateTime(2020, 9, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/AutoportalHr.png", null, "Autoportal.hr", 1 },
                    { 110, "https://www.autopress.hr", 8, new DateTime(2020, 9, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/AutopressHr.png", null, "AutopressHR", 1 },
                    { 111, "https://vozim.hr", 8, new DateTime(2020, 9, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/VozimHr.png", null, "Vozim.HR", 1 },
                    { 112, "https://ams.hr", 8, new DateTime(2020, 9, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/AutoMotorSport.png", null, "AUTO MOTOR I SPORT", 1 }
                });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//figure[contains(@class, 'img-container')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//figure[contains(@class, 'img-container')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//figure[contains(@class, 'img-container')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//figure[contains(@class, 'img-container')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//figure[contains(@class, 'img-container')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'article__figure_img')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'article__figure_img')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'article__figure_img')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'article__figure_img')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'article__figure_img')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 11,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'article__figure_img')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 12,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'media-object adaptive lazy')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//img[contains(@class, 'media-object adaptive lazy')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//div[contains(@class, 'featured-img')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 15,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 16,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 17,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 18,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 19,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 20,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 21,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 22,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 23,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 24,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 25,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 26,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 27,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 28,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 29,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'lateImage lateImageLoaded')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 30,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'lateImage lateImageLoaded')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 31,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'lateImage lateImageLoaded')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 32,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'lateImage lateImageLoaded')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 33,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'lateImage lateImageLoaded')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 34,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'lateImage lateImageLoaded')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 35,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'lateImage lateImageLoaded')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 36,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'lateImage lateImageLoaded')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped" },
                values: new object[] { 2, 2, "//script[contains(@type, 'application/ld+json')]", "image,url", true });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//div[contains(@class, 'thumb')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 40,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'featured-img')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//figure[contains(@class, 'article-main-img')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 43,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//figure[contains(@class, 'article-image main-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 44,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'naslovna')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 47,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'post-img')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 48,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 49,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 50,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 54,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'entry-content')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 55,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'attribute-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 56,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'img-holder')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 57,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'post__hero')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 58,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'postFeaturedImg postFeaturedImg--single')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 59,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'td-post-featured-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 61,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'first-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//figure[contains(@class, 'dcms-image article-image')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 63,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'post-thumbnail')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 64,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//figure[contains(@class, 'figure')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, 3, "//figure[contains(@class, 'media')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//div[contains(@class, 'td-post-featured-image')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 67,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'image-slider')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 68,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'image-slider')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 69,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'image-slider')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 70,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'image-slider')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//picture[contains(@class, 'pic')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//div[contains(@class, 'img-holder inner')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 74,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'pd-hero-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 75,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'pd-hero-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 76,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'pd-hero-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 77,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'pd-hero-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 78,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'pd-hero-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 79,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'pd-hero-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 80,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'pd-hero-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 81,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'pd-hero-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, 4, "//div[contains(@class, 'mycontent')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 83,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'td-full-screen-header-image-wrap')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 84,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'single-post-media')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//img[contains(@class, 'article__figure_img')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//figure[contains(@class, 'img-container')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 144,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//div[contains(@class, 'td-post-featured-image')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 145,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//div[contains(@class, 'entry-image featured-image')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 146,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'td-post-featured-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 147,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//div[contains(@class, 'td-post-featured-image')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 148,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'img')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 150,
                columns: new[] { "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//div[contains(@class, 'main-content')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 151,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'article')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 152,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'site-content')]//img");

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ImgElementXPath", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object[,]
                {
                    { 153, 8, 108, 1, "https://www.motori.hr/feed", "//div[contains(@class, 'content')]//img", 0, 17 },
                    { 154, 8, 109, 1, "https://autoportal.hr/feed", "//div[contains(@class, 'td-post-content')]//img", 0, 3 },
                    { 155, 8, 110, 1, "https://www.autopress.hr/feed", "//div[contains(@class, 'td-post-featured-image')]//img", 0, 9 },
                    { 156, 8, 111, 1, "https://vozim.hr/feed", "//div[contains(@class, 'intro-image-over')]//img", 0, 8 },
                    { 157, 8, 112, 1, "https://ams.hr/feed", "//main[contains(@class, 'main-content')]//img", 0, 17 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//figure[contains(@class, 'img-container')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//figure[contains(@class, 'img-container')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//figure[contains(@class, 'img-container')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//figure[contains(@class, 'img-container')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//figure[contains(@class, 'img-container')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'article__figure_img')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'article__figure_img')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'article__figure_img')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'article__figure_img')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'article__figure_img')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 11,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'article__figure_img')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 12,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'media-object adaptive lazy')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//img[contains(@class, 'media-object adaptive lazy')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//div[contains(@class, 'featured-img')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 15,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 16,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 17,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 18,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 19,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 20,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 21,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 22,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 23,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 24,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 25,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 26,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 27,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 28,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 29,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'lateImage lateImageLoaded')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 30,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'lateImage lateImageLoaded')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 31,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'lateImage lateImageLoaded')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 32,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'lateImage lateImageLoaded')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 33,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'lateImage lateImageLoaded')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 34,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'lateImage lateImageLoaded')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 35,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'lateImage lateImageLoaded')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 36,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'lateImage lateImageLoaded')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped" },
                values: new object[] { 2, 2, "//script[contains(@type, 'application/ld+json')]", "image,url", true });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//div[contains(@class, 'thumb')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 40,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'featured-img')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//figure[contains(@class, 'article-main-img')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 43,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//figure[contains(@class, 'article-image main-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 44,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'naslovna')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 47,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'post-img')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 48,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 49,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 50,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 54,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'entry-content')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 55,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'attribute-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 56,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'img-holder')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 57,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'post__hero')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 58,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'postFeaturedImg postFeaturedImg--single')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 59,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'td-post-featured-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 61,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'first-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//figure[contains(@class, 'dcms-image article-image')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 63,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'post-thumbnail')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 64,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//figure[contains(@class, 'figure')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, 3, "//figure[contains(@class, 'media')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//div[contains(@class, 'td-post-featured-image')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 67,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'image-slider')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 68,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'image-slider')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 69,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'image-slider')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 70,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'image-slider')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//picture[contains(@class, 'pic')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//div[contains(@class, 'img-holder inner')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 74,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'pd-hero-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 75,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'pd-hero-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 76,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'pd-hero-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 77,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'pd-hero-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 78,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'pd-hero-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 79,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'pd-hero-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 80,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'pd-hero-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 81,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'pd-hero-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, 4, "//div[contains(@class, 'mycontent')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 83,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'td-full-screen-header-image-wrap')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 84,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'single-post-media')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//img[contains(@class, 'article__figure_img')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//figure[contains(@class, 'img-container')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 144,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//div[contains(@class, 'td-post-featured-image')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 145,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//div[contains(@class, 'entry-image featured-image')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 146,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'td-post-featured-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 147,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//div[contains(@class, 'td-post-featured-image')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 148,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'img')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 150,
                columns: new[] { "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//div[contains(@class, 'main-content')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 151,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'article')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 152,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'site-content')]//img");
        }
    }
}
