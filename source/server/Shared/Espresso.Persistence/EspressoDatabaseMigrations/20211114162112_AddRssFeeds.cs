using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Espresso.Persistence.EspressoDatabaseMigrations
{
    public partial class AddRssFeeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "NewsPortals",
                columns: new[] { "Id", "BaseUrl", "CategoryId", "CreatedAt", "IconUrl", "IsEnabled", "IsNewOverride", "Name", "RegionId" },
                values: new object[,]
                {
                    { 136, "https://sportklub.hr", 2, new DateTime(2021, 11, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/SportKlub.png", true, null, "Sportklub", 1 },
                    { 137, "https://doktorehitno.hr", 4, new DateTime(2021, 11, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/DoktoreHitno.png", true, null, "Doktore, hitno!", 1 }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url" },
                values: new object[,]
                {
                    { 182, 2, 136, 1, "https://sportklub.hr/feed" },
                    { 183, 4, 137, 1, "https://doktorehitno.hr/rss" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 182);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 183);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 137);
        }
    }
}
