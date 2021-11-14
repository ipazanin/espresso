// 20211114152316_FixNetHrRss.cs
//
// © 2021 Espresso News. All rights reserved.

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Espresso.Persistence.EspressoDatabaseMigrations
{
    public partial class FixNetHrRss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 14,
                column: "Url",
                value: "https://net.hr/feed");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 14,
                column: "Url",
                value: "https://net.hr/feed/");
        }
    }
}
