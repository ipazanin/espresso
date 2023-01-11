// 20230111222251_AddElementExtensionNameToImageUrlConfiguration.cs
//
// © 2022 Espresso News. All rights reserved.

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Espresso.Persistence.EspressoDatabaseMigrations;

/// <inheritdoc />
public partial class AddElementExtensionNameToImageUrlConfiguration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "ImageUrlParseConfiguration_ElementExtensionIndex",
            table: "RssFeeds");

        migrationBuilder.DropColumn(
            name: "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute",
            table: "RssFeeds");

        migrationBuilder.AddColumn<string>(
            name: "ImageUrlParseConfiguration_ElementExtensionAttributeName",
            table: "RssFeeds",
            type: "character varying(100)",
            maxLength: 100,
            nullable: false,
            defaultValue: string.Empty);

        migrationBuilder.AddColumn<string>(
            name: "ImageUrlParseConfiguration_ElementExtensionName",
            table: "RssFeeds",
            type: "character varying(100)",
            maxLength: 100,
            nullable: false,
            defaultValue: string.Empty);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "ImageUrlParseConfiguration_ElementExtensionAttributeName",
            table: "RssFeeds");

        migrationBuilder.DropColumn(
            name: "ImageUrlParseConfiguration_ElementExtensionName",
            table: "RssFeeds");

        migrationBuilder.AddColumn<int>(
            name: "ImageUrlParseConfiguration_ElementExtensionIndex",
            table: "RssFeeds",
            type: "integer",
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            name: "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute",
            table: "RssFeeds",
            type: "boolean",
            nullable: true);
    }
}
