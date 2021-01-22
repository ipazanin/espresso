using Microsoft.EntityFrameworkCore.Migrations;

namespace Espresso.Persistence.Migrations
{
    public partial class IPazanin_UpdatedBitnoNet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrlParseConfiguration_ImgElementXPath",
                table: "RssFeeds",
                newName: "ImageUrlParseConfiguration_XPath");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrlParseConfiguration_AttributeName",
                table: "RssFeeds",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "src");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 170,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_AttributeName", "ImageUrlParseConfiguration_XPath", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object[] { 2, "data-lazy-src", "//section[contains(@class, 'article-content')]//picture[contains(@class, 'wp-caption')]//img[@data-lazy-src]", 0, 12 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrlParseConfiguration_AttributeName",
                table: "RssFeeds");

            migrationBuilder.RenameColumn(
                name: "ImageUrlParseConfiguration_XPath",
                table: "RssFeeds",
                newName: "ImageUrlParseConfiguration_ImgElementXPath");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 170,
                column: "RequestType",
                value: 1);
        }
    }
}
