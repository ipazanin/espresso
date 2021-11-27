// 20210419185735_FixedRssFeeds1.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.EntityFrameworkCore.Migrations;

namespace Espresso.Persistence.EspressoDatabaseMigrations;

/// <inheritdoc/>
public partial class FixedRssFeeds1 : Migration
{
    /// <inheritdoc/>
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "RssFeedContentModifier",
            keyColumn: "Id",
            keyValue: 30);
    }

    /// <inheritdoc/>
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "RssFeedContentModifier",
            columns: new[] { "Id", "OrderIndex", "ReplacementValue", "RssFeedId", "SourceValue" },
            values: new object[] { 30, 2, string.Empty, 63, "\t" });
    }
}
