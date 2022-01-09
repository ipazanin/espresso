// 20211229104804_RemoveSimilarArticlesConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Espresso.Persistence.EspressoDatabaseMigrations;

public partial class RemoveSimilarArticlesConfiguration : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "SimilarArticle");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "SimilarArticle",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                MainArticleId = table.Column<Guid>(type: "uuid", nullable: false),
                SimilarityScore = table.Column<double>(type: "double precision", nullable: false),
                SubordinateArticleId = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_SimilarArticle", x => x.Id);
            });
    }
}
