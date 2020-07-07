using Microsoft.EntityFrameworkCore.Migrations;

namespace Espresso.Persistence.Migrations
{
    public partial class IPazanin_ChangedIsNewOverrideToBeNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsNewOverride",
                table: "NewsPortals",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsNewOverride",
                value: null);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsNewOverride",
                value: null);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsNewOverride",
                value: null);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 4,
                column: "IsNewOverride",
                value: null);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 5,
                column: "IsNewOverride",
                value: null);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 6,
                column: "IsNewOverride",
                value: null);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 7,
                column: "IsNewOverride",
                value: null);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 8,
                column: "IsNewOverride",
                value: null);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 9,
                column: "IsNewOverride",
                value: null);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 10,
                column: "IsNewOverride",
                value: null);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 11,
                column: "IsNewOverride",
                value: null);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 12,
                column: "IsNewOverride",
                value: null);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 15,
                column: "IsNewOverride",
                value: null);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 16,
                column: "IsNewOverride",
                value: null);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 18,
                column: "IsNewOverride",
                value: null);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 19,
                column: "IsNewOverride",
                value: null);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 20,
                column: "IsNewOverride",
                value: null);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 21,
                column: "IsNewOverride",
                value: null);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 22,
                column: "IsNewOverride",
                value: null);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 23,
                column: "IsNewOverride",
                value: null);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 25,
                column: "IsNewOverride",
                value: null);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 26,
                column: "IsNewOverride",
                value: null);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 27,
                column: "IsNewOverride",
                value: null);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 28,
                column: "IsNewOverride",
                value: null);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 29,
                column: "IsNewOverride",
                value: null);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 30,
                column: "IsNewOverride",
                value: null);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 31,
                column: "IsNewOverride",
                value: null);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 32,
                column: "IsNewOverride",
                value: null);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 33,
                column: "IsNewOverride",
                value: null);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 35,
                column: "IsNewOverride",
                value: null);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 37,
                column: "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsNewOverride",
                table: "NewsPortals",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsNewOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsNewOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsNewOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 4,
                column: "IsNewOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 5,
                column: "IsNewOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 6,
                column: "IsNewOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 7,
                column: "IsNewOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 8,
                column: "IsNewOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 9,
                column: "IsNewOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 10,
                column: "IsNewOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 11,
                column: "IsNewOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 12,
                column: "IsNewOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 15,
                column: "IsNewOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 16,
                column: "IsNewOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 18,
                column: "IsNewOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 19,
                column: "IsNewOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 20,
                column: "IsNewOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 21,
                column: "IsNewOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 22,
                column: "IsNewOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 23,
                column: "IsNewOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 25,
                column: "IsNewOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 26,
                column: "IsNewOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 27,
                column: "IsNewOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 28,
                column: "IsNewOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 29,
                column: "IsNewOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 30,
                column: "IsNewOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 31,
                column: "IsNewOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 32,
                column: "IsNewOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 33,
                column: "IsNewOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 35,
                column: "IsNewOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 37,
                column: "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped",
                value: true);
        }
    }
}
