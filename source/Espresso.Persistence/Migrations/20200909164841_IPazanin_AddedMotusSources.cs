using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Espresso.Persistence.Migrations
{
    public partial class IPazanin_AddedMotusSources : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 33,
                column: "CategoryId",
                value: 11);

            migrationBuilder.InsertData(
                table: "NewsPortals",
                columns: new[] { "Id", "BaseUrl", "CategoryId", "CreatedAt", "IconUrl", "IsNewOverride", "Name", "RegionId" },
                values: new object?[,]
                {
                    { 99, "https://otvoreno.hr", 11, new DateTime(2020, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/OtvorenoHr.png", null, "Otvoreno.hr", 1 },
                    { 100, "https://www.geopolitika.news", 1, new DateTime(2020, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/GeoPolitika.png", null, "Geopolitika News", 1 },
                    { 101, "https://povijest.hr", 1, new DateTime(2020, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/PovijestHr.png", null, "Povijest.hr", 1 },
                    { 102, "https://7dnevno.hr", 1, new DateTime(2020, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/Dnevno7.png", null, "7dnevno", 1 }
                });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "RssFeedId", "UrlRegex" },
                values: new object[] { 13, "globus" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 4, 13, "domidizajn" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 6, 13, "viral" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 3, 13, "spektakli" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 4, 13, "life" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 3, 13, "scena" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 3, 13, "spektakli" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CategoryId", "UrlRegex" },
                values: new object[] { 1, "vijesti" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 2, 37, "sport" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 3, 37, "showbiz" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "RssFeedId", "UrlRegex" },
                values: new object[] { 37, "lifestyle" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 7, 37, "biznis" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 5, 37, "techsci" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 8, 37, "automoto" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 9, 37, "kultura" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 1, 42, "vijesti" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 3, 42, "showbuzz" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "RssFeedId", "UrlRegex" },
                values: new object[] { 39, "politika-kriminal" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 4, 39, "zivot" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 9, 39, "kultura" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 2, 39, "na-prvu" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 25,
                column: "UrlRegex",
                value: "Vijesti");

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "CategoryId", "UrlRegex" },
                values: new object[] { 1, "Svijet" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "CategoryId", "UrlRegex" },
                values: new object[] { 1, "Znanost" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "CategoryId", "UrlRegex" },
                values: new object[] { 1, "Regija" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "CategoryId", "UrlRegex" },
                values: new object[] { 1, "Dnevnik" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "CategoryId", "UrlRegex" },
                values: new object[] { 1, "Info" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "CategoryId", "UrlRegex" },
                values: new object[] { 7, "Biznis" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "CategoryId", "UrlRegex" },
                values: new object[] { 4, "Lifestyle" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 4, 65, "Zdravlje" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "RssFeedId", "UrlRegex" },
                values: new object[] { 65, "Sport-Klub" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 3, 65, "Showbiz" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 5, 65, "Tehnologija" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 9, 65, "Kultura" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 1, 65, "Crna-Kronika" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 1, 65, "Video" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "RssFeedId", "UrlRegex" },
                values: new object[] { 66, "Hrvatska" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "RssFeedId", "UrlRegex" },
                values: new object[] { 66, "Sport" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 9, 66, "Kultura" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 1, 66, "svijet" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 1, 66, "koronovirus" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex", "UrlSegmentIndex" },
                values: new object[] { 4, 71, "zivot", 1 });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "RssFeedId", "UrlRegex", "UrlSegmentIndex" },
                values: new object[] { 71, "news", 1 });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex", "UrlSegmentIndex" },
                values: new object[] { 3, 71, "scena", 1 });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 4, 71, "bubble" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 1, 72, "vijesti" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 2, 72, "sport" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 1, 72, "domovina" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 3, 72, "magazin" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 4, 72, "zdravlje" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 1, 72, "korona-virus" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 8, 72, "auto-moto" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "CategoryId", "UrlRegex", "UrlSegmentIndex" },
                values: new object[] { 1, "hrvatska", 2 });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "CategoryId", "UrlRegex", "UrlSegmentIndex" },
                values: new object[] { 1, "crna-kronika", 2 });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "CategoryId", "UrlRegex", "UrlSegmentIndex" },
                values: new object[] { 1, "svijet", 2 });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 9, 14, "kultura" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 7, 14, "novac" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 5, 14, "znanost" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 2, 14, "sport" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 6, 14, "vic-dana" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 5, 14, "planet-x" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 6, 14, "fora-dana" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "RssFeedId", "UrlRegex" },
                values: new object[] { 14, "hot" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 4, 14, "magazin" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 5, 14, "tehnoklik" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 8, 14, "auto" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 4, 82, "lifestyle" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "CategoryId", "UrlRegex" },
                values: new object[] { 3, "vijesti" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "CategoryId", "UrlRegex" },
                values: new object[] { 9, "kultura" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "CategoryId", "UrlRegex" },
                values: new object[] { 3, "televizija" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "CategoryId", "UrlRegex" },
                values: new object[] { 4, "dogadjanja" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 1, 85, "top-news" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 77,
                column: "UrlRegex",
                value: "life");

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "CategoryId", "UrlRegex" },
                values: new object[] { 7, "ekonomix" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "CategoryId", "UrlRegex" },
                values: new object[] { 5, "tehno" });

            migrationBuilder.InsertData(
                table: "RssFeedCategory",
                columns: new[] { "Id", "CategoryId", "RssFeedId", "UrlRegex", "UrlSegmentIndex" },
                values: new object[] { 45, 1, 66, "svijet", 1 });

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
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[,]
                {
                    { 144, 1, 99, "https://otvoreno.hr/feed", 2, "//div[contains(@class, 'td-post-featured-image')]//img" },
                    { 145, 1, 100, "https://www.geopolitika.news/feed", 2, "//div[contains(@class, 'entry-image featured-image')]//img" }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 146, 1, 101, "https://povijest.hr/feed/", "//div[contains(@class, 'td-post-featured-image')]//img" });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 147, 1, 102, "https://7dnevno.hr/feed", 2, "//div[contains(@class, 'td-post-featured-image')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 1, 144, "vijesti" });

            migrationBuilder.InsertData(
                table: "RssFeedCategory",
                columns: new[] { "Id", "CategoryId", "RssFeedId", "UrlRegex", "UrlSegmentIndex" },
                values: new object[,]
                {
                    { 81, 7, 144, "gospodarstvo", 1 },
                    { 82, 3, 144, "magazin", 1 },
                    { 83, 9, 144, "kultura", 1 },
                    { 84, 2, 144, "sport", 1 },
                    { 85, 1, 144, "eu-i-svijet", 1 },
                    { 86, 1, 147, "vijesti", 1 },
                    { 87, 1, 147, "sport", 1 },
                    { 88, 1, 147, "domovina", 1 },
                    { 89, 1, 147, "kultura", 2 },
                    { 90, 1, 147, "zdravlje", 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 33,
                column: "CategoryId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "RssFeedId", "UrlRegex" },
                values: new object[] { 37, "vijesti" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 2, 37, "sport" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 3, 37, "showbiz" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 4, 37, "lifestyle" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 7, 37, "biznis" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 5, 37, "techsci" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 8, 37, "automoto" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CategoryId", "UrlRegex" },
                values: new object[] { 9, "kultura" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 1, 42, "vijesti" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 1, 13, "globus" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "RssFeedId", "UrlRegex" },
                values: new object[] { 13, "domidizajn" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 6, 13, "viral" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 3, 13, "spektakli" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 4, 13, "life" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 1, 39, "politika-kriminal" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 4, 39, "zivot" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 1, 65, "Vijesti" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "RssFeedId", "UrlRegex" },
                values: new object[] { 65, "Svijet" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 1, 65, "Znanost" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 1, 65, "Regija" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 1, 65, "Dnevnik" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 25,
                column: "UrlRegex",
                value: "Info");

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "CategoryId", "UrlRegex" },
                values: new object[] { 7, "Biznis" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "CategoryId", "UrlRegex" },
                values: new object[] { 4, "Lifestyle" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "CategoryId", "UrlRegex" },
                values: new object[] { 4, "Zdravlje" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "CategoryId", "UrlRegex" },
                values: new object[] { 2, "Sport-Klub" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "CategoryId", "UrlRegex" },
                values: new object[] { 3, "Showbiz" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "CategoryId", "UrlRegex" },
                values: new object[] { 5, "Tehnologija" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "CategoryId", "UrlRegex" },
                values: new object[] { 9, "Kultura" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 1, 66, "Hrvatska" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "RssFeedId", "UrlRegex" },
                values: new object[] { 66, "Sport" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 9, 66, "Kultura" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 4, 71, "zivot" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 1, 71, "news" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 3, 71, "scena" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 4, 71, "bubble" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "RssFeedId", "UrlRegex" },
                values: new object[] { 72, "vijesti" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "RssFeedId", "UrlRegex" },
                values: new object[] { 72, "sport" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 1, 72, "domovina" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 3, 72, "magazin" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 4, 72, "zdravlje" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex", "UrlSegmentIndex" },
                values: new object[] { 1, 14, "hrvatska", 2 });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "RssFeedId", "UrlRegex", "UrlSegmentIndex" },
                values: new object[] { 14, "crna-kronika", 2 });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex", "UrlSegmentIndex" },
                values: new object[] { 1, 14, "svijet", 2 });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 9, 14, "kultura" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 7, 14, "novac" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 5, 14, "znanost" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 2, 14, "sport" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 6, 14, "vic-dana" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 5, 14, "planet-x" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 6, 14, "fora-dana" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 3, 14, "hot" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "CategoryId", "UrlRegex", "UrlSegmentIndex" },
                values: new object[] { 4, "magazin", 1 });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "CategoryId", "UrlRegex", "UrlSegmentIndex" },
                values: new object[] { 5, "tehnoklik", 1 });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "CategoryId", "UrlRegex", "UrlSegmentIndex" },
                values: new object[] { 8, "auto", 1 });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 1, 65, "Crna-Kronika" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 1, 65, "Video" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 3, 42, "showbuzz" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 1, 66, "koronovirus" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 1, 66, "svijet" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 1, 72, "korona-virus" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 8, 72, "auto-moto" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "RssFeedId", "UrlRegex" },
                values: new object[] { 13, "scena" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 3, 13, "spektakli" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 9, 39, "kultura" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 1, 66, "svijet" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 2, 39, "na-prvu" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "CategoryId", "UrlRegex" },
                values: new object[] { 4, "lifestyle" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "CategoryId", "UrlRegex" },
                values: new object[] { 3, "vijesti" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "CategoryId", "UrlRegex" },
                values: new object[] { 9, "kultura" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "CategoryId", "UrlRegex" },
                values: new object[] { 3, "televizija" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[] { 4, 82, "dogadjanja" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 77,
                column: "UrlRegex",
                value: "top-news");

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "CategoryId", "UrlRegex" },
                values: new object[] { 1, "life" });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "CategoryId", "UrlRegex" },
                values: new object[] { 7, "ekonomix" });

            migrationBuilder.InsertData(
                table: "RssFeedCategory",
                columns: new[] { "Id", "CategoryId", "RssFeedId", "UrlRegex", "UrlSegmentIndex" },
                values: new object[] { 80, 5, 85, "tehno", 1 });

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
