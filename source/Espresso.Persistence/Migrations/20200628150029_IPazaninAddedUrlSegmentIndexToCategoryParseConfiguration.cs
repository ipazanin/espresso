using Microsoft.EntityFrameworkCore.Migrations;

namespace Espresso.Persistence.Migrations
{
    public partial class IPazaninAddedUrlSegmentIndexToCategoryParseConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryParseConfiguration_UrlSegmentIndex",
                table: "RssFeeds",
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
                keyValue: 37,
                column: "CategoryParseConfiguration_UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 39,
                column: "CategoryParseConfiguration_UrlSegmentIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 41,
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryParseConfiguration_UrlSegmentIndex",
                table: "RssFeeds");
        }
    }
}
