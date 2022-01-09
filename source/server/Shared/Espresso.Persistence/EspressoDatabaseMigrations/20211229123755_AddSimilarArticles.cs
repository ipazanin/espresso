// 20211229123755_AddSimilarArticles.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Espresso.Persistence.EspressoDatabaseMigrations;

public partial class AddSimilarArticles : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "SimilarArticle",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                SimilarityScore = table.Column<double>(type: "double precision", nullable: false),
                FirstArticleId = table.Column<Guid>(type: "uuid", nullable: false),
                SecondArticleId = table.Column<Guid>(type: "uuid", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_SimilarArticle", x => x.Id);
                table.ForeignKey(
                    name: "FK_SimilarArticle_Articles_FirstArticleId",
                    column: x => x.FirstArticleId,
                    principalTable: "Articles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_SimilarArticle_Articles_SecondArticleId",
                    column: x => x.SecondArticleId,
                    principalTable: "Articles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_SimilarArticle_FirstArticleId",
            table: "SimilarArticle",
            column: "FirstArticleId");

        migrationBuilder.CreateIndex(
            name: "IX_SimilarArticle_SecondArticleId",
            table: "SimilarArticle",
            column: "SecondArticleId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "SimilarArticle");
    }
}
