// 20230105164826_AddNewsPortalImage.cs
//
// © 2022 Espresso News. All rights reserved.

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Espresso.Persistence.EspressoDatabaseMigrations;

/// <inheritdoc />
public partial class AddNewsPortalImage : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.CreateTable(
            name: "NewsPortalImages",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                ImageBytes = table.Column<byte[]>(type: "bytea", nullable: true),
                NewsPortalId = table.Column<int>(type: "integer", nullable: false),
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_NewsPortalImages", x => x.Id);
                _ = table.ForeignKey(
                    name: "FK_NewsPortalImages_NewsPortals_NewsPortalId",
                    column: x => x.NewsPortalId,
                    principalTable: "NewsPortals",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        _ = migrationBuilder.CreateIndex(
            name: "IX_NewsPortalImages_NewsPortalId",
            table: "NewsPortalImages",
            column: "NewsPortalId",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.DropTable(
            name: "NewsPortalImages");
    }
}
