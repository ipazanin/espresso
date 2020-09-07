using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Espresso.Persistence.Migrations
{
    public partial class IPazanin_AddedAllLocalSources : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "NewsPortals",
                columns: new[] { "Id", "BaseUrl", "CategoryId", "CreatedAt", "IconUrl", "IsNewOverride", "Name", "RegionId" },
                values: new object?[,]
                {
                    { 65, "https://slobodnadalmacija.hr", 12, new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/SlobodnaDalmacija_Dalmacija.png", null, "Slobodna Dalmacija - Dalmacija", 2 },
                    { 98, "http://www.osijek031.com", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/Osijek031.png", null, "Osijek031", 7 },
                    { 97, "https://www.pozeska-kronika.hr", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/PozeskaKronika.png", null, "Požeška Kronika", 7 },
                    { 96, "https://sbplus.hr", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/SbPlusHr.png", null, "SBplus.hr", 7 },
                    { 95, "http://portal53.hr", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/Portal53.png", null, "Portal53", 7 },
                    { 94, "https://novosti.hr", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/NovostiHr.png", null, "Novosti.hr", 7 },
                    { 93, "https://www.novska.in", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/NovskaIn.png", null, "Novska.IN", 7 },
                    { 91, "https://www.zagorje.com", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/ZagorjeCom.png", null, "Zagorje.com", 6 },
                    { 90, "https://www.mnovine.hr", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/MedimurskeNovine.png", null, "Međimurske Novine", 6 },
                    { 89, "https://www.medjimurje.info", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/MedimurjeInfo.png", null, "Međimurje Info", 6 },
                    { 88, "https://www.glaspodravine.hr", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/GlasPodravineIPrigorja.png", null, "Glas Podravine i Prigorja", 6 },
                    { 87, "https://regionalni.com", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/PlusRegionalniTjednik.png", null, "7Plus Regionalni Tjednik", 6 },
                    { 86, "https://www.zagreb.hr/", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/GradZagreb.png", null, "Grad Zagreb", 5 },
                    { 85, "https://www.zgportal.com", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/ZgPortal.png", null, "ZG portal", 5 },
                    { 84, "https://www.zagrebacki.hr", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/ZagrebackiList.png", null, "Zagrebački List", 5 },
                    { 83, "https://www.gspress.net", 12, new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/GsPress.png", null, "GS Press", 4 },
                    { 92, "https://www.icv.hr", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/InformativniCentarVirovitica.png", null, "Informativni Centar Virovitica", 7 },
                    { 81, "https://www.fiuman.hr", 12, new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/Fiuman.png", null, "Fiuman", 3 },
                    { 66, "https://slobodnadalmacija.hr", 12, new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/SlobodnaDalmacija_Split.png", null, "Slobodna Dalmacija - Split", 2 },
                    { 67, "https://www.dubrovniknet.hr", 12, new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/DubrovnikNet.png", null, "Dubrovniknet.hr", 2 },
                    { 68, "https://makarska-danas.com", 12, new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/MakarskaDanas.png", null, "MakarskaDanas", 2 },
                    { 69, "https://makarska.hr", 12, new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/MakarskaHr.png", null, "Makarska", 2 },
                    { 70, "http://www.portaloko.hr", 12, new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/PortalOko.png", null, "Portaloko.hr", 2 },
                    { 71, "https://www.antenazadar.hr", 12, new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/AntenaZadar.png", null, "Antena portal Zadar", 2 },
                    { 82, "https://riportal.net.hr", 12, new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/Riportal.png", null, "Riportal", 3 },
                    { 72, "https://radioimotski.hr", 12, new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/RadioImotski.png", null, "Radio Imotski", 2 },
                    { 73, "https://imotskenovine.hr", 12, new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/ImotskeNovine.png", null, "Imotske Novine", 2 },
                    { 74, "http://www.kastela.org", 12, new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/PortalKastela.png", null, "Portal Grada Kaštela", 2 },
                    { 75, "https://huknet1.hr", 12, new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/HukNet.png", null, "Huknet", 2 },
                    { 76, "https://www.zadarskilist.hr", 12, new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/ZadarskiList.png", null, "Zadarski List", 2 },
                    { 77, "https://www.istriaterramagica.eu", 12, new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/IstraTerraMagica.png", null, "Istra Terra Magica", 3 },
                    { 78, "https://www.ipress.hr", 12, new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/IPress.png", null, "iPress", 3 },
                    { 80, "https://www.rijekadanas.com", 12, new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/RijekaDanas.png", null, "Rijeka Danas", 3 }
                });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'img-large loaded')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'img-large loaded')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'img-large loaded')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'img-large loaded')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'img-large loaded')]");

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
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//figure[contains(@class, 'media')]//img" });

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
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//div[contains(@class, 'mycontent')]//img" });

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

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object[] { 111, 12, 65, "https://slobodnadalmacija.hr/feed/category/246", 0, 5 });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url" },
                values: new object[,]
                {
                    { 141, 12, 96, "https://sbplus.hr/rss" },
                    { 140, 12, 95, "http://portal53.hr/feed" },
                    { 139, 12, 94, "https://novosti.hr/feed" },
                    { 138, 12, 93, "https://www.novska.in/feed" },
                    { 137, 12, 92, "https://www.icv.hr/feed/" },
                    { 136, 12, 91, "https://www.zagorje.com/rss" },
                    { 135, 12, 90, "https://www.mnovine.hr/feed" },
                    { 134, 12, 89, "https://www.medjimurje.info/feed" },
                    { 133, 12, 88, "https://www.glaspodravine.hr/feed" },
                    { 132, 12, 87, "https://regionalni.com/feed" },
                    { 131, 12, 86, "https://www.zagreb.hr/RssFeeds.aspx?id=17" },
                    { 130, 12, 85, "https://www.zgportal.com/feed" },
                    { 129, 12, 84, "https://www.zagrebacki.hr/feed" },
                    { 128, 12, 83, "https://www.gspress.net/feed" },
                    { 142, 12, 97, "https://www.pozeska-kronika.hr/component/fpss/module/292.feed?type=rss" },
                    { 127, 12, 82, "https://riportal.net.hr/feed" },
                    { 125, 12, 80, "https://www.rijekadanas.com/feed" },
                    { 124, 12, 78, "https://www.ipress.hr/index.php?format=feed&type=rss" },
                    { 123, 12, 77, "https://www.istriaterramagica.eu/feed" },
                    { 122, 12, 76, "https://www.zadarskilist.hr/rss.xml" },
                    { 121, 12, 75, "https://huknet1.hr/?feed=rss2" },
                    { 120, 12, 74, "http://www.kastela.org/?format=feed&type=rss" },
                    { 119, 12, 73, "https://imotskenovine.hr/feed" },
                    { 118, 12, 72, "https://radioimotski.hr/feed" },
                    { 117, 12, 71, "https://www.antenazadar.hr/feed" },
                    { 116, 12, 70, "http://www.portaloko.hr/rss/-1" },
                    { 115, 12, 69, "https://makarska.hr/rss" },
                    { 114, 12, 68, "https://makarska-danas.com/feed" },
                    { 113, 12, 67, "https://www.dubrovniknet.hr/feed" }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object[] { 112, 12, 66, "https://slobodnadalmacija.hr/feed/category/253", 0, 5 });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url" },
                values: new object[,]
                {
                    { 126, 12, 81, "https://www.fiuman.hr/feed" },
                    { 143, 12, 98, "http://www.osijek031.com/news_rss.php" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'img-large loaded')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'img-large loaded')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'img-large loaded')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'img-large loaded')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'img-large loaded')]");

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
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//figure[contains(@class, 'media')]//img" });

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
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//div[contains(@class, 'mycontent')]//img" });

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
        }
    }
}
