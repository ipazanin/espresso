// 20211229104718_RemoveSimilarArticles.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Espresso.Persistence.EspressoDatabaseMigrations;

public partial class RemoveSimilarArticles : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_SimilarArticles_Articles_MainArticleId",
            table: "SimilarArticles");

        migrationBuilder.DropForeignKey(
            name: "FK_SimilarArticles_Articles_SubordinateArticleId",
            table: "SimilarArticles");

        migrationBuilder.DropPrimaryKey(
            name: "PK_SimilarArticles",
            table: "SimilarArticles");

        migrationBuilder.DropIndex(
            name: "IX_SimilarArticles_MainArticleId",
            table: "SimilarArticles");

        migrationBuilder.DropIndex(
            name: "IX_SimilarArticles_SubordinateArticleId",
            table: "SimilarArticles");

        migrationBuilder.RenameTable(
            name: "SimilarArticles",
            newName: "SimilarArticle");

        migrationBuilder.AddPrimaryKey(
            name: "PK_SimilarArticle",
            table: "SimilarArticle",
            column: "Id");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropPrimaryKey(
            name: "PK_SimilarArticle",
            table: "SimilarArticle");

        migrationBuilder.RenameTable(
            name: "SimilarArticle",
            newName: "SimilarArticles");

        migrationBuilder.AddPrimaryKey(
            name: "PK_SimilarArticles",
            table: "SimilarArticles",
            column: "Id");

        migrationBuilder.CreateIndex(
            name: "IX_SimilarArticles_MainArticleId",
            table: "SimilarArticles",
            column: "MainArticleId");

        migrationBuilder.CreateIndex(
            name: "IX_SimilarArticles_SubordinateArticleId",
            table: "SimilarArticles",
            column: "SubordinateArticleId",
            unique: true);

        migrationBuilder.AddForeignKey(
            name: "FK_SimilarArticles_Articles_MainArticleId",
            table: "SimilarArticles",
            column: "MainArticleId",
            principalTable: "Articles",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);

        migrationBuilder.AddForeignKey(
            name: "FK_SimilarArticles_Articles_SubordinateArticleId",
            table: "SimilarArticles",
            column: "SubordinateArticleId",
            principalTable: "Articles",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);
    }
}
