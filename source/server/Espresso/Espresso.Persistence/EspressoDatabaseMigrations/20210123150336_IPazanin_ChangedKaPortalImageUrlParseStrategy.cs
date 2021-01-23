using Microsoft.EntityFrameworkCore.Migrations;

namespace Espresso.Persistence.EspressoDatabaseMigrations
{
    public partial class IPazanin_ChangedKaPortalImageUrlParseStrategy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 172,
                column: "ImageUrlParseConfiguration_ImageUrlParseStrategy",
                value: 3);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
