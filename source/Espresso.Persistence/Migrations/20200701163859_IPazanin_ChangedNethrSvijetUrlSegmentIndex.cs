using Microsoft.EntityFrameworkCore.Migrations;

namespace Espresso.Persistence.Migrations
{
    public partial class IPazanin_ChangedNethrSvijetUrlSegmentIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 48,
                column: "UrlSegmentIndex",
                value: 2);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 37,
                column: "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 48,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 37,
                column: "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped",
                value: true);
        }
    }
}
