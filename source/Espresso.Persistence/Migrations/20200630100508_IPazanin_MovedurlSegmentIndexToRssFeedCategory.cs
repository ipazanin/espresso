using Microsoft.EntityFrameworkCore.Migrations;

namespace Espresso.Persistence.Migrations
{
    public partial class IPazanin_MovedurlSegmentIndexToRssFeedCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryParseConfiguration_UrlSegmentIndex",
                table: "RssFeeds");

            migrationBuilder.AddColumn<int>(
                name: "UrlSegmentIndex",
                table: "RssFeedCategory",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 1,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 2,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 3,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 4,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 5,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 6,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 7,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 8,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 9,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 10,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 11,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 12,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 13,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 14,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 15,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 16,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 17,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 18,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 19,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 20,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 21,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 22,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 23,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 24,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 25,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 26,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 27,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 28,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 29,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 30,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 31,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 32,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 33,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 34,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 35,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 36,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 37,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 38,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 39,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 40,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 41,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 42,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 43,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 44,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 46,
                column: "UrlSegmentIndex",
                value: 2);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 47,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 48,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 49,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 50,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 51,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 52,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 53,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 54,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 55,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 56,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 57,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 58,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 59,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 60,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 61,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 62,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 63,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 64,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 65,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 66,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 67,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 68,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 69,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedCategory",
                keyColumn: "Id",
                keyValue: 70,
                column: "UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 37,
                column: "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlSegmentIndex",
                table: "RssFeedCategory");

            migrationBuilder.AddColumn<int>(
                name: "CategoryParseConfiguration_UrlSegmentIndex",
                table: "RssFeeds",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 13,
                column: "CategoryParseConfiguration_UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 14,
                column: "CategoryParseConfiguration_UrlSegmentIndex",
                value: 2);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "CategoryParseConfiguration_UrlSegmentIndex", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped" },
                values: new object[] { 1, true });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 39,
                column: "CategoryParseConfiguration_UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 42,
                column: "CategoryParseConfiguration_UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 65,
                column: "CategoryParseConfiguration_UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 66,
                column: "CategoryParseConfiguration_UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 71,
                column: "CategoryParseConfiguration_UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 72,
                column: "CategoryParseConfiguration_UrlSegmentIndex",
                value: 1);
        }
    }
}
