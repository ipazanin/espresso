using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Espresso.Persistence.Migrations
{
    public partial class IPazanin_AddedSomeSources : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped",
                table: "RssFeeds",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrlParseConfiguration_ImgElementXPath",
                table: "RssFeeds",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "ImageUrlParseConfiguration_ImageUrlWebScrapeType",
                table: "RssFeeds",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "ImageUrlParseConfiguration_ImageUrlParseStrategy",
                table: "RssFeeds",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryParseConfiguration_CategoryParseStrategy",
                table: "RssFeeds",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValue: 1);

            migrationBuilder.InsertData(
                table: "NewsPortals",
                columns: new[] { "Id", "BaseUrl", "CategoryId", "CreatedAt", "IconUrl", "IsNewOverride", "Name", "RegionId" },
                values: new object?[,]
                {
                    { 114, "http://prvahnl.hr", 2, new DateTime(2020, 10, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/PrvaHnl.png", null, "Hrvatski Telekom Prva liga", 1 },
                    { 115, "http://balkans.aljazeera.net", 1, new DateTime(2020, 10, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/AlJazeera.png", null, "Al Jazeera Balkans", 1 },
                    { 116, "https://www.hifimedia.hr", 5, new DateTime(2020, 10, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/HifiMedia.png", null, "hifimedia", 1 },
                    { 117, "https://geek.hr", 5, new DateTime(2020, 10, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/GeekHr.png", null, "Geek.hr", 1 }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object?[] { 159, 2, 114, 1, "http://prvahnl.hr/rss", "//div[contains(@class, 'news')]//img", null, 0, 6 });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames" },
                values: new object?[] { 160, 1, 115, 1, "http://balkans.aljazeera.net/mobile/articles", "//div[contains(@class, 'field-items')]//img", null });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object?[,]
                {
                    { 161, 5, 116, 1, "https://www.hifimedia.hr/feed", "//figure[contains(@class, 'post-gallery')]//img", null, 0, 7 },
                    { 162, 5, 117, 1, "https://geek.hr/feed", "//figure[contains(@class, 'zox-post-main')]//img", null, 0, 5 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.AlterColumn<bool>(
                name: "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped",
                table: "RssFeeds",
                type: "bit",
                nullable: true,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrlParseConfiguration_ImgElementXPath",
                table: "RssFeeds",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "ImageUrlParseConfiguration_ImageUrlWebScrapeType",
                table: "RssFeeds",
                type: "int",
                nullable: true,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "ImageUrlParseConfiguration_ImageUrlParseStrategy",
                table: "RssFeeds",
                type: "int",
                nullable: true,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryParseConfiguration_CategoryParseStrategy",
                table: "RssFeeds",
                type: "int",
                nullable: true,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);
        }
    }
}
