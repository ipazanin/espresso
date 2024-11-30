// 20230108204849_AddWebScrapeRequestType.cs
//
// © 2022 Espresso News. All rights reserved.

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Espresso.Persistence.EspressoDatabaseMigrations;

/// <inheritdoc />
public partial class AddWebScrapeRequestType : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.AlterColumn<bool>(
            name: "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped",
            table: "RssFeeds",
            type: "boolean",
            nullable: false,
            defaultValue: false,
            oldClrType: typeof(bool),
            oldType: "boolean",
            oldNullable: true);

        _ = migrationBuilder.AddColumn<int>(
            name: "ImageUrlParseConfiguration_WebScrapeRequestType",
            table: "RssFeeds",
            type: "integer",
            nullable: false,
            defaultValue: 2);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.DropColumn(
            name: "ImageUrlParseConfiguration_WebScrapeRequestType",
            table: "RssFeeds");

        _ = migrationBuilder.AlterColumn<bool>(
            name: "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped",
            table: "RssFeeds",
            type: "boolean",
            nullable: true,
            oldClrType: typeof(bool),
            oldType: "boolean");
    }
}
