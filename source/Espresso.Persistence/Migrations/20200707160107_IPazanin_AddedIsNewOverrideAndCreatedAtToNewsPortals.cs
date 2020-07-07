using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Espresso.Persistence.Migrations
{
    public partial class IPazanin_AddedIsNewOverrideAndCreatedAtToNewsPortals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "NewsPortals",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "NewsPortals",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsNewOverride",
                table: "NewsPortals",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Color", "KeyWordsRegexPattern", "Name" },
                values: new object?[] { 11, "#AC80A0", null, "Generalno" });

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { 2, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { 2, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { 2, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { 7, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { 5, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { 5, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { 5, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { 5, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { 7, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { 5, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { 4, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { 4, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { 4, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { 8, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { 1, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { 1, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { 1, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { 1, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { 1, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { 1, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 37,
                column: "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped",
                value: true);

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { 11, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { 11, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { 11, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { 11, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { 11, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { 11, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { 11, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { 11, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { 11, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { 11, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_NewsPortals_CategoryId",
                table: "NewsPortals",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsPortals_Categories_CategoryId",
                table: "NewsPortals",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsPortals_Categories_CategoryId",
                table: "NewsPortals");

            migrationBuilder.DropIndex(
                name: "IX_NewsPortals_CategoryId",
                table: "NewsPortals");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "NewsPortals");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "NewsPortals");

            migrationBuilder.DropColumn(
                name: "IsNewOverride",
                table: "NewsPortals");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 37,
                column: "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped",
                value: true);
        }
    }
}
