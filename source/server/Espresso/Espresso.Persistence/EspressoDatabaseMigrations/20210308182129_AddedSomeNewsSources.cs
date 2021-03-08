using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Espresso.Persistence.EspressoDatabaseMigrations
{
    public partial class AddedSomeNewsSources : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImageUrlParseConfiguration_ElementExtensionIndex",
                table: "RssFeeds",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute",
                table: "RssFeeds",
                type: "bit",
                nullable: true);

            migrationBuilder.InsertData(
                table: "NewsPortals",
                columns: new[] { "Id", "BaseUrl", "CategoryId", "CreatedAt", "IconUrl", "IsNewOverride", "Name", "RegionId" },
                values: new object?[,]
                {
                    { 129, "https://www.maxportal.hr", 1, new DateTime(2021, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/MaxPortal.png", null, "Maxportal", 1 },
                    { 17, "https://www.poslovni.hr", 7, new DateTime(2021, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/PoslovniDnevnik.png", null, "Poslovni dnevnik", 1 },
                    { 130, "https://www.gp1.hr", 8, new DateTime(2021, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/Gp1.png", null, "GP1", 1 },
                    { 131, "https://f1.pulsmedia.hr", 8, new DateTime(2021, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/F1PulsMedia.png", null, "F1.pulsmedia.hr", 1 },
                    { 132, "https://www.racunalo.com", 5, new DateTime(2021, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/Racunalo.png", null, "Racunalo.com", 1 },
                    { 133, "https://mob.hr", 5, new DateTime(2021, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/MobHr.png", null, "mob.hr", 1 },
                    { 134, "https://www.startnews.hr/", 1, new DateTime(2021, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/StartNews.png", null, "Startnews", 1 }
                });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute" },
                values: new object[] { 0, false });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute" },
                values: new object[] { 1, 3, true });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 153,
                columns: new[] { "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute" },
                values: new object[] { 1, 3, true });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object[,]
                {
                    { 174, 1, 129, 1, "https://www.maxportal.hr/feed", 0, 11 },
                    { 175, 7, 17, 1, "https://www.poslovni.hr/feed", 0, 5 },
                    { 176, 8, 130, 1, "https://www.gp1.hr/feed", 0, 11 },
                    { 177, 8, 131, 1, "https://f1.pulsmedia.hr/feed", 0, 7 },
                    { 178, 5, 132, 1, "https://www.racunalo.com/feed", 0, 11 }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object?[] { 179, 5, 133, 1, "https://mob.hr/feed", 2, 3, true, null, null, 0, 15 });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object[] { 180, 1, 134, 1, "https://www.startnews.hr/feeds/latest", 0, 21 });

            migrationBuilder.InsertData(
                table: "RssFeedContentModifier",
                columns: new[] { "Id", "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[,]
                {
                    { 25, "<notused>", 176, "<description>" },
                    { 26, "</notused>", 176, "</description>" },
                    { 27, "<description>", 176, "<content:encoded>" },
                    { 28, "</description>", 176, "</content:encoded>" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 177);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 178);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 179);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 180);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 176);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 130);

            migrationBuilder.DropColumn(
                name: "ImageUrlParseConfiguration_ElementExtensionIndex",
                table: "RssFeeds");

            migrationBuilder.DropColumn(
                name: "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute",
                table: "RssFeeds");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 82,
                column: "ImageUrlParseConfiguration_ImageUrlParseStrategy",
                value: 4);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 153,
                column: "ImageUrlParseConfiguration_ImageUrlParseStrategy",
                value: 4);
        }
    }
}
