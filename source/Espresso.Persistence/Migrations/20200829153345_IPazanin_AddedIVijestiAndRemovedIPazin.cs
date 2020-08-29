using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Espresso.Persistence.Migrations
{
    public partial class IPazanin_AddedIVijestiAndRemovedIPazin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.InsertData(
                table: "NewsPortals",
                columns: new[] { "Id", "BaseUrl", "CategoryId", "CreatedAt", "IconUrl", "IsNewOverride", "Name", "RegionId" },
                values: new object?[] { 59, "https://ivijesti.hr/", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/IVijesti.png", null, "iVijesti", 3 });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 37,
                column: "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped",
                value: true);

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames" },
                values: new object?[] { 105, 12, 59, "https://ivijesti.hr/feed", 1, 1, 1, "", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.InsertData(
                table: "NewsPortals",
                columns: new[] { "Id", "BaseUrl", "CategoryId", "CreatedAt", "IconUrl", "IsNewOverride", "Name", "RegionId" },
                values: new object?[] { 43, "https://www.ipazin.net/", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/IPazin.png", null, "iPazin", 3 });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 37,
                column: "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped",
                value: true);

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames" },
                values: new object?[] { 89, 12, 43, "https://www.ipazin.net/feed", 1, 1, 1, "", null });
        }
    }
}
