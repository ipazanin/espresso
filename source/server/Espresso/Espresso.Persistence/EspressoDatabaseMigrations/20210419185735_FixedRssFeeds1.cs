using Microsoft.EntityFrameworkCore.Migrations;

namespace Espresso.Persistence.EspressoDatabaseMigrations
{
    public partial class FixedRssFeeds1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 30);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RssFeedContentModifier",
                columns: new[] { "Id", "OrderIndex", "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[] { 30, 2, "", 63, "	" });
        }
    }
}
