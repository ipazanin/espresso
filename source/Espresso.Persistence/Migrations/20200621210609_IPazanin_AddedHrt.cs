using Microsoft.EntityFrameworkCore.Migrations;

namespace Espresso.Persistence.Migrations
{
    public partial class IPazanin_AddedHrt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "NewsPortals",
                columns: new[] { "Id", "BaseUrl", "IconUrl", "Name" },
                values: new object[] { 31, "https://www.hrt.hr", "Icons/Hrt.png", "HRT" });


            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames" },
                values: new object?[,]
                {
                    { 67, 1, 31, "https://www.hrt.hr/rss/vijesti/", 1, 1, 1, "//div[contains(@class, 'image-slider')]//img", null },
                    { 68, 2, 31, "https://www.hrt.hr/rss/sport/", 1, 1, 1, "//div[contains(@class, 'image-slider')]//img", null },
                    { 69, 3, 31, "https://magazin.hrt.hr/feed.xml", 1, 1, 1, "//div[contains(@class, 'image-slider')]//img", null },
                    { 70, 3, 31, "https://www.hrt.hr/rss/glazba/", 1, 1, 1, "//div[contains(@class, 'image-slider')]//img", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 31);
        }
    }
}
