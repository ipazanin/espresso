// 20210422183837_FixedRssFeeds4.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.EntityFrameworkCore.Migrations;

namespace Espresso.Persistence.EspressoDatabaseMigrations;

/// <inheritdoc/>
public partial class FixedRssFeeds4 : Migration
{
    /// <inheritdoc/>
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.UpdateData(
            table: "NewsPortals",
            keyColumn: "Id",
            keyValue: 111,
            column: "IsEnabled",
            value: false);

        migrationBuilder.UpdateData(
            table: "RssFeeds",
            keyColumn: "Id",
            keyValue: 113,
            column: "RequestType",
            value: 2);
    }

    /// <inheritdoc/>
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.UpdateData(
            table: "NewsPortals",
            keyColumn: "Id",
            keyValue: 111,
            column: "IsEnabled",
            value: true);

        migrationBuilder.UpdateData(
            table: "RssFeeds",
            keyColumn: "Id",
            keyValue: 113,
            column: "RequestType",
            value: 1);
    }
}
