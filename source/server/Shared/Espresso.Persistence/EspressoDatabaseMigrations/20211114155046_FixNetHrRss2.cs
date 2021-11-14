// 20211114155046_FixNetHrRss2.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.EntityFrameworkCore.Migrations;

namespace Espresso.Persistence.EspressoDatabaseMigrations
{
    public partial class FixNetHrRss2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl" },
                values: new object?[] { false, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl" },
                values: new object[] { true, "https://net.hr/{1}{2}{3}amp" });
        }
    }
}
