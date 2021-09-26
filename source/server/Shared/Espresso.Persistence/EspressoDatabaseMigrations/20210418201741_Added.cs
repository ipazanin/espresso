// 20210418201741_Added.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Espresso.Persistence.EspressoDatabaseMigrations
{
    /// <inheritdoc/>
    public partial class Added : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 134,
                column: "BaseUrl",
                value: "https://www.startnews.hr");

            migrationBuilder.InsertData(
                table: "NewsPortals",
                columns: new[] { "Id", "BaseUrl", "CategoryId", "CreatedAt", "IconUrl", "IsNewOverride", "Name", "RegionId" },
                values: new object?[] { 135, "https://viral.hr", 6, new DateTime(2021, 4, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/Viral.png", null, "Viral.hr", 1 });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 152,
                column: "SkipParseConfiguration_NumberOfSkips",
                value: 5);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 153,
                column: "SkipParseConfiguration_NumberOfSkips",
                value: 6);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 157,
                column: "SkipParseConfiguration_NumberOfSkips",
                value: 8);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 158,
                column: "SkipParseConfiguration_NumberOfSkips",
                value: 7);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 164,
                column: "SkipParseConfiguration_NumberOfSkips",
                value: 6);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 166,
                column: "SkipParseConfiguration_NumberOfSkips",
                value: 5);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 170,
                column: "SkipParseConfiguration_NumberOfSkips",
                value: 5);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 174,
                column: "SkipParseConfiguration_NumberOfSkips",
                value: 3);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 176,
                column: "SkipParseConfiguration_NumberOfSkips",
                value: 6);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 178,
                column: "SkipParseConfiguration_NumberOfSkips",
                value: 5);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 179,
                column: "SkipParseConfiguration_NumberOfSkips",
                value: 9);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 180,
                column: "SkipParseConfiguration_NumberOfSkips",
                value: 7);

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object[] { 181, 6, 135, 1, "https://viral.hr/feed", 0, 3 });
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 181);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 135);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 134,
                column: "BaseUrl",
                value: "https://www.startnews.hr/");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 152,
                column: "SkipParseConfiguration_NumberOfSkips",
                value: 11);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 153,
                column: "SkipParseConfiguration_NumberOfSkips",
                value: 17);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 157,
                column: "SkipParseConfiguration_NumberOfSkips",
                value: 17);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 158,
                column: "SkipParseConfiguration_NumberOfSkips",
                value: 19);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 164,
                column: "SkipParseConfiguration_NumberOfSkips",
                value: 27);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 166,
                column: "SkipParseConfiguration_NumberOfSkips",
                value: 31);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 170,
                column: "SkipParseConfiguration_NumberOfSkips",
                value: 12);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 174,
                column: "SkipParseConfiguration_NumberOfSkips",
                value: 11);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 176,
                column: "SkipParseConfiguration_NumberOfSkips",
                value: 11);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 178,
                column: "SkipParseConfiguration_NumberOfSkips",
                value: 11);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 179,
                column: "SkipParseConfiguration_NumberOfSkips",
                value: 15);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 180,
                column: "SkipParseConfiguration_NumberOfSkips",
                value: 21);
        }
    }
}
