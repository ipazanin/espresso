using Microsoft.EntityFrameworkCore.Migrations;

namespace Espresso.Persistence.EspressoDatabaseMigrations
{
    public partial class IPazanin_UpdatedIndexAmpConfiguration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 1,
                column: "AmpConfiguration_TemplateUrl",
                value: "https://www.index.hr/mobile/clanak.aspx?id={0}");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 2,
                column: "AmpConfiguration_TemplateUrl",
                value: "https://www.index.hr/mobile/clanak.aspx?id={0}");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 3,
                column: "AmpConfiguration_TemplateUrl",
                value: "https://www.index.hr/mobile/clanak.aspx?id={0}");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 4,
                column: "AmpConfiguration_TemplateUrl",
                value: "https://www.index.hr/mobile/clanak.aspx?id={0}");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 5,
                column: "AmpConfiguration_TemplateUrl",
                value: "https://www.index.hr/mobile/clanak.aspx?id={0}");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 1,
                column: "AmpConfiguration_TemplateUrl",
                value: "https://www.index.hr/mobile/clanak?id={0}");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 2,
                column: "AmpConfiguration_TemplateUrl",
                value: "https://www.index.hr/mobile/clanak?id={0}");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 3,
                column: "AmpConfiguration_TemplateUrl",
                value: "https://www.index.hr/mobile/clanak?id={0}");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 4,
                column: "AmpConfiguration_TemplateUrl",
                value: "https://www.index.hr/mobile/clanak?id={0}");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 5,
                column: "AmpConfiguration_TemplateUrl",
                value: "https://www.index.hr/mobile/clanak?id={0}");
        }
    }
}
