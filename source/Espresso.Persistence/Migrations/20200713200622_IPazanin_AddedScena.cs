using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Espresso.Persistence.Migrations
{
    public partial class IPazanin_AddedScena : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "NewsPortals",
                columns: new[] { "Id", "BaseUrl", "CategoryId", "CreatedAt", "IconUrl", "IsNewOverride", "Name" },
                values: new object?[] { 36, "https://www.scena.hr/", 3, new DateTime(2020, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Icons/Scena.png", null, "Scena" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 37,
                column: "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped",
                value: true);

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames" },
                values: new object?[] { 82, 1, 36, "https://www.scena.hr/feed/", 2, 1, 1, "//div[contains(@class, 'mycontent')]//img", null });

            migrationBuilder.InsertData(
                table: "RssFeedCategory",
                columns: new[] { "Id", "CategoryId", "RssFeedId", "UrlRegex", "UrlSegmentIndex" },
                values: new object[,]
                {
                    { 72, 4, 82, "lifestyle", 1 },
                    { 73, 3, 82, "vijesti", 1 },
                    { 74, 9, 82, "kultura", 1 },
                    { 75, 3, 82, "televizija", 1 },
                    { 76, 4, 82, "dogadjanja", 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 37,
                column: "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped",
                value: true);
        }
    }
}
