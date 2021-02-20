using Microsoft.EntityFrameworkCore.Migrations;

namespace Espresso.Persistence.EspressoDatabaseMigrations
{
    public partial class IPazanin_UpdatedIndexAmpConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl" },
                values: new object[] { true, "https://www.index.hr/mobile/clanak?id={0}" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl" },
                values: new object[] { true, "https://www.index.hr/mobile/clanak?id={0}" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl" },
                values: new object[] { true, "https://www.index.hr/mobile/clanak?id={0}" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl" },
                values: new object[] { true, "https://www.index.hr/mobile/clanak?id={0}" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl" },
                values: new object[] { true, "https://www.index.hr/mobile/clanak?id={0}" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl" },
                values: new object[] { false, "https://amp.index.hr/article/{0}{3}" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl" },
                values: new object[] { false, "https://amp.index.hr/article/{0}{3}" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl" },
                values: new object[] { false, "https://amp.index.hr/article/{0}{3}" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl" },
                values: new object[] { false, "https://amp.index.hr/article/{0}{3}" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl" },
                values: new object[] { false, "https://amp.index.hr/article/{0}{3}" });
        }
    }
}
