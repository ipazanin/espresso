using Microsoft.EntityFrameworkCore.Migrations;

namespace Espresso.Persistence.Migrations
{
    public partial class IPazaninAddedDirektnoHr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "NewsPortals",
                columns: new[] { "Id", "BaseUrl", "IconUrl", "Name" },
                values: new object[] { 35, "https://direktno.hr/", "Icons/DirektnoHr.png", "Direktno" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object[] { 0, 5 });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "CategoryParseConfiguration_UrlSegmentIndex", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames" },
                values: new object?[,]
                {
                    { 74, 1, 35, "https://direktno.hr/rss/publish/latest/direkt-50/", 1, null, 1, 1, "//div[contains(@class, 'pd-hero-image')]//img", null },
                    { 75, 1, 35, "https://direktno.hr/rss/publish/latest/domovina-10/", 1, null, 1, 1, "//div[contains(@class, 'pd-hero-image')]//img", null },
                    { 76, 1, 35, "https://direktno.hr/rss/publish/latest/eu_svijet/", 1, null, 1, 1, "//div[contains(@class, 'pd-hero-image')]//img", null },
                    { 77, 7, 35, "https://direktno.hr/rss/publish/latest/razvoj-110/", 1, null, 1, 1, "//div[contains(@class, 'pd-hero-image')]//img", null },
                    { 78, 2, 35, "https://direktno.hr/rss/publish/latest/sport-60/", 1, null, 1, 1, "//div[contains(@class, 'pd-hero-image')]//img", null },
                    { 79, 3, 35, "https://direktno.hr/rss/publish/latest/zivot-70/", 1, null, 1, 1, "//div[contains(@class, 'pd-hero-image')]//img", null },
                    { 80, 1, 35, "https://direktno.hr/rss/publish/latest/kolumne-80/", 1, null, 1, 1, "//div[contains(@class, 'pd-hero-image')]//img", null },
                    { 81, 1, 35, "https://direktno.hr/rss/publish/latest/direktnotv-100/", 1, null, 1, 1, "//div[contains(@class, 'pd-hero-image')]//img", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 35);
        }
    }
}
