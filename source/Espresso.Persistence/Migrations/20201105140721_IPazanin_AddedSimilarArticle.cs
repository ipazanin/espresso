using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Espresso.Persistence.Migrations
{
    public partial class IPazanin_AddedSimilarArticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SimilarArticles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SimilarityScore = table.Column<double>(type: "float", nullable: false),
                    MainArticleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubordinateArticleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimilarArticles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SimilarArticles_Articles_MainArticleId",
                        column: x => x.MainArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SimilarArticles_Articles_SubordinateArticleId",
                        column: x => x.SubordinateArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SimilarArticles_MainArticleId",
                table: "SimilarArticles",
                column: "MainArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_SimilarArticles_SubordinateArticleId",
                table: "SimilarArticles",
                column: "SubordinateArticleId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SimilarArticles");
        }
    }
}
