// 20210422193726_FixedRssFeeds5.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.EntityFrameworkCore.Migrations;

namespace Espresso.Persistence.EspressoDatabaseMigrations
{
    public partial class FixedRssFeeds5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ReplacementValue", "SourceValue" },
                values: new object[] { "<notused>", "<description>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ReplacementValue", "SourceValue" },
                values: new object[] { "</notused>", "</description>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "OrderIndex", "RssFeedId" },
                values: new object[] { 3, 5 });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "OrderIndex", "RssFeedId" },
                values: new object[] { 4, 5 });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { "<notused>", 3, "<description>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { "</notused>", 3, "</description>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "OrderIndex", "RssFeedId" },
                values: new object[] { 3, 3 });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "OrderIndex", "RssFeedId" },
                values: new object[] { 4, 3 });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { "<notused>", 4, "<description>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { "</notused>", 4, "</description>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "OrderIndex", "RssFeedId" },
                values: new object[] { 3, 4 });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "OrderIndex", "RssFeedId" },
                values: new object[] { 4, 4 });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { "<notused>", 2, "<description>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { "</notused>", 2, "</description>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "OrderIndex", "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { 3, "<description>", 2, "<content>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "OrderIndex", "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { 4, "</description>", 2, "</content>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { "<description>", 1, "<content>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { "</description>", 1, "</content>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "OrderIndex", "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { 3, "<description>", 1, "<content>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "OrderIndex", "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { 4, "</description>", 1, "</content>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { "<description>", 96, "<content>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { "</description>", 96, "</content>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "RssFeedId", "SourceValue" },
                values: new object[] { 96, "<content>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "RssFeedId", "SourceValue" },
                values: new object[] { 96, "</content>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { "<link>", 70, "<thumb>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { "</link>", 70, "</thumb>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "OrderIndex", "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { 1, "<link>", 69, "<thumb>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "OrderIndex", "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { 2, "</link>", 69, "</thumb>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { "<link>", 68, "<thumb>" });

            migrationBuilder.InsertData(
                table: "RssFeedContentModifier",
                columns: new[] { "Id", "OrderIndex", "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[,]
                {
                    { 40, 4, "</description>", 176, "</content:encoded>" },
                    { 39, 3, "<description>", 176, "<content:encoded>" },
                    { 38, 2, "</notused>", 176, "</description>" },
                    { 37, 1, "<notused>", 176, "<description>" },
                    { 36, 4, "</description>", 57, "</content:encoded>" },
                    { 35, 3, "<description>", 57, "<content:encoded>" },
                    { 34, 2, "</notused>", 57, "</description>" },
                    { 33, 1, "<notused>", 57, "<description>" },
                    { 32, 2, "</link>", 67, "</thumb>" },
                    { 31, 1, "<link>", 67, "<thumb>" },
                    { 41, 1, string.Empty, 63, "\n" },
                    { 30, 2, "</link>", 68, "</thumb>" },
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ReplacementValue", "SourceValue" },
                values: new object[] { "<description>", "<content>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ReplacementValue", "SourceValue" },
                values: new object[] { "</description>", "</content>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "OrderIndex", "RssFeedId" },
                values: new object[] { 1, 3 });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "OrderIndex", "RssFeedId" },
                values: new object[] { 2, 3 });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { "<description>", 4, "<content>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { "</description>", 4, "</content>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "OrderIndex", "RssFeedId" },
                values: new object[] { 1, 2 });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "OrderIndex", "RssFeedId" },
                values: new object[] { 2, 2 });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { "<description>", 1, "<content>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { "</description>", 1, "</content>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "OrderIndex", "RssFeedId" },
                values: new object[] { 1, 96 });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "OrderIndex", "RssFeedId" },
                values: new object[] { 2, 96 });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { "<link>", 70, "<thumb>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { "</link>", 70, "</thumb>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "OrderIndex", "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { 1, "<link>", 69, "<thumb>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "OrderIndex", "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { 2, "</link>", 69, "</thumb>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { "<link>", 68, "<thumb>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { "</link>", 68, "</thumb>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "OrderIndex", "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { 1, "<link>", 67, "<thumb>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "OrderIndex", "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { 2, "</link>", 67, "</thumb>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { "<notused>", 57, "<description>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { "</notused>", 57, "</description>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "RssFeedId", "SourceValue" },
                values: new object[] { 57, "<content:encoded>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "RssFeedId", "SourceValue" },
                values: new object[] { 57, "</content:encoded>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { "<notused>", 176, "<description>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { "</notused>", 176, "</description>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "OrderIndex", "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { 3, "<description>", 176, "<content:encoded>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "OrderIndex", "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { 4, "</description>", 176, "</content:encoded>" });

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { string.Empty, 63, "\n" });
        }
    }
}
