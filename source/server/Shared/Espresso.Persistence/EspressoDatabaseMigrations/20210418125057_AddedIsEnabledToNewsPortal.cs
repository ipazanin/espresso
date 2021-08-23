// 20210418125057_AddedIsEnabledToNewsPortal.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.EntityFrameworkCore.Migrations;

namespace Espresso.Persistence.EspressoDatabaseMigrations
{
    /// <inheritdoc/>
    public partial class AddedIsEnabledToNewsPortal : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "NewsPortals",
                type: "boolean",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "NewsPortals");
        }
    }
}
