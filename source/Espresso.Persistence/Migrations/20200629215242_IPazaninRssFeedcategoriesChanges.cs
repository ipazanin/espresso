using Microsoft.EntityFrameworkCore.Migrations;

namespace Espresso.Persistence.Migrations
{
    public partial class IPazaninRssFeedcategoriesChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 12,
                column: "UrlRegex",
                value: "vijesti");

            migrationBuilder.InsertData(
                table: "RssFeedCategory",
                columns: new[] { "Id", "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object[,]
                {
                    { 19, 4, 39, "zivot" },
                    { 70, 1, 66, "svijet" },
                    { 69, 9, 39, "kultura" },
                    { 68, 3, 13, "spektakli" },
                    { 67, 3, 13, "scena" },
                    { 66, 8, 72, "auto-moto" },
                    { 65, 1, 72, "korona-virus" },
                    { 64, 1, 66, "svijet" },
                    { 63, 1, 66, "koronovirus" },
                    { 62, 3, 42, "showbuzz" },
                    { 61, 1, 65, "Video" },
                    { 60, 1, 65, "Crna-Kronika" },
                    { 59, 8, 14, "auto" },
                    { 58, 5, 14, "tehnoklik" },
                    { 56, 3, 14, "hot" },
                    { 55, 6, 14, "fora-dana" },
                    { 54, 5, 14, "planet-x" },
                    { 53, 6, 14, "vic-dana" },
                    { 52, 2, 14, "sport" },
                    { 51, 5, 14, "znanost" },
                    { 50, 7, 14, "novac" },
                    { 49, 9, 14, "kultura" },
                    { 48, 1, 14, "svijet" },
                    { 47, 1, 14, "crna-kronika" },
                    { 46, 1, 14, "hrvatska" },
                    { 18, 1, 39, "politika-kriminal" },
                    { 57, 4, 14, "magazin" }
                });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 37,
                column: "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 12,
                column: "UrlRegex",
                value: "vijest");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 37,
                column: "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped",
                value: true);

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "CategoryParseConfiguration_UrlSegmentIndex", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames" },
                values: new object?[] { 41, 4, 9, "https://www.telegram.hr/feed/", 2, 1, 1, 1, "//div[contains(@class, 'thumb')]//img", null });

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 18,
                column: "RssFeedId",
                value: 41);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "RssFeedId", "UrlRegex" },
                values: new object[] { 41, "život" });
        }
    }
}
