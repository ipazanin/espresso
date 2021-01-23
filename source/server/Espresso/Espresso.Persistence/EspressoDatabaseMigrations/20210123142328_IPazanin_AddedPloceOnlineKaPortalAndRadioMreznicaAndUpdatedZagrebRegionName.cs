using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Espresso.Persistence.EspressoDatabaseMigrations
{
    public partial class IPazanin_AddedPloceOnlineKaPortalAndRadioMreznicaAndUpdatedZagrebRegionName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "NewsPortals",
                columns: new[] { "Id", "BaseUrl", "CategoryId", "CreatedAt", "IconUrl", "IsNewOverride", "Name", "RegionId" },
                values: new object?[,]
                {
                    { 126, "https://ploce.com.hr", 12, new DateTime(2021, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/PloceOnline.png", null, "Ploče Online", 2 },
                    { 127, "https://kaportal.net.hr", 12, new DateTime(2021, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/KaPortalHr.png", null, "KAportal.hr", 5 },
                    { 128, "https://radio-mreznica.hr", 12, new DateTime(2021, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/RadioMreznica.png", null, "Radio Mrežnica", 5 }
                });

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Zagreb i okolica");

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object[] { 171, 12, 126, 1, "https://ploce.com.hr/feed", 0, 3 });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url" },
                values: new object[] { 172, 12, 127, 1, "https://kaportal.net.hr/feed" });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url" },
                values: new object[] { 173, 12, 128, 1, "https://radio-mreznica.hr/feed" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 128);

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Zagreb");
        }
    }
}
