using Microsoft.EntityFrameworkCore.Migrations;

namespace Espresso.Persistence.Migrations
{
    public partial class IPazanin_RemovedIndecies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Articles_PublishDateTime",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_TrendingScore",
                table: "Articles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Articles_PublishDateTime",
                table: "Articles",
                column: "PublishDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_TrendingScore",
                table: "Articles",
                column: "TrendingScore");
        }
    }
}
