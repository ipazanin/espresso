// 20210418200211_ChangedSomeRssfeedsToBrowserContentLoad.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.EntityFrameworkCore.Migrations;

namespace Espresso.Persistence.EspressoDatabaseMigrations
{
    public partial class ChangedSomeRssfeedsToBrowserContentLoad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 44,
                column: "RequestType",
                value: 2);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 159,
                column: "RequestType",
                value: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 44,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 159,
                column: "RequestType",
                value: 1);
        }
    }
}
