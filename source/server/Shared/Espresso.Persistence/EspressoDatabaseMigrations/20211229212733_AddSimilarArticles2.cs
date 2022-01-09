// 20211229212733_AddSimilarArticles2.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Espresso.Persistence.EspressoDatabaseMigrations;

public partial class AddSimilarArticles2 : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_SimilarArticle_Articles_FirstArticleId",
            table: "SimilarArticle");

        migrationBuilder.DropForeignKey(
            name: "FK_SimilarArticle_Articles_SecondArticleId",
            table: "SimilarArticle");

        migrationBuilder.DropPrimaryKey(
            name: "PK_SimilarArticle",
            table: "SimilarArticle");

        migrationBuilder.RenameTable(
            name: "SimilarArticle",
            newName: "SimilarArticles");

        migrationBuilder.RenameIndex(
            name: "IX_SimilarArticle_SecondArticleId",
            table: "SimilarArticles",
            newName: "IX_SimilarArticles_SecondArticleId");

        migrationBuilder.RenameIndex(
            name: "IX_SimilarArticle_FirstArticleId",
            table: "SimilarArticles",
            newName: "IX_SimilarArticles_FirstArticleId");

        migrationBuilder.AddPrimaryKey(
            name: "PK_SimilarArticles",
            table: "SimilarArticles",
            column: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_SimilarArticles_Articles_FirstArticleId",
            table: "SimilarArticles",
            column: "FirstArticleId",
            principalTable: "Articles",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_SimilarArticles_Articles_SecondArticleId",
            table: "SimilarArticles",
            column: "SecondArticleId",
            principalTable: "Articles",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_SimilarArticles_Articles_FirstArticleId",
            table: "SimilarArticles");

        migrationBuilder.DropForeignKey(
            name: "FK_SimilarArticles_Articles_SecondArticleId",
            table: "SimilarArticles");

        migrationBuilder.DropPrimaryKey(
            name: "PK_SimilarArticles",
            table: "SimilarArticles");

        migrationBuilder.RenameTable(
            name: "SimilarArticles",
            newName: "SimilarArticle");

        migrationBuilder.RenameIndex(
            name: "IX_SimilarArticles_SecondArticleId",
            table: "SimilarArticle",
            newName: "IX_SimilarArticle_SecondArticleId");

        migrationBuilder.RenameIndex(
            name: "IX_SimilarArticles_FirstArticleId",
            table: "SimilarArticle",
            newName: "IX_SimilarArticle_FirstArticleId");

        migrationBuilder.AddPrimaryKey(
            name: "PK_SimilarArticle",
            table: "SimilarArticle",
            column: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_SimilarArticle_Articles_FirstArticleId",
            table: "SimilarArticle",
            column: "FirstArticleId",
            principalTable: "Articles",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_SimilarArticle_Articles_SecondArticleId",
            table: "SimilarArticle",
            column: "SecondArticleId",
            principalTable: "Articles",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
