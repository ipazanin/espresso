using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Espresso.Persistence.Migrations
{
    public partial class IPazanin_AddedKulturaSources : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "NewsPortals",
                columns: new[] { "Id", "BaseUrl", "CategoryId", "CreatedAt", "IconUrl", "IsNewOverride", "Name", "RegionId" },
                values: new object?[,]
                {
                    { 118, "https://vizkultura.hr", 9, new DateTime(2020, 10, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/VizKultura.png", null, "vizkultura.hr", 1 },
                    { 119, "https://zivotumjetnosti.ipu.hr", 9, new DateTime(2020, 10, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/ZivotUmjetnosti.png", null, "Život umjetnosti", 1 },
                    { 120, "https://svijetkulture.com", 9, new DateTime(2020, 10, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/SvijetKulture.png", null, "SVIJET KULTURE", 1 }
                });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 161,
                column: "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped",
                value: false);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 162,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'zox-post-main')]//img");

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object?[] { 163, 9, 118, 1, "https://vizkultura.hr/feed", "//div[contains(@class, 'content')]//img", null, null, 0, 7 });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object?[] { 164, 9, 119, 1, "https://zivotumjetnosti.ipu.hr/feed", "", null, false, 0, 27 });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object?[] { 165, 9, 120, 1, "https://svijetkulture.com/feed", "//div[contains(@class, 'td-post-featured-image')]//img", null, null, 0, 7 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 161,
                column: "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped",
                value: null);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 162,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//figure[contains(@class, 'zox-post-main')]//img");
        }
    }
}
