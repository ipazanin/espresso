using Microsoft.EntityFrameworkCore.Migrations;

namespace Espresso.Persistence.Migrations
{
    public partial class IPazanin_RefactoredRssFeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RequestType",
                table: "RssFeeds",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RssFeedContentModifier",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceValue = table.Column<string>(maxLength: 1000, nullable: false),
                    ReplacementValue = table.Column<string>(maxLength: 1000, nullable: false),
                    RssFeedId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RssFeedContentModifier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RssFeedContentModifier_RssFeeds_RssFeedId",
                        column: x => x.RssFeedId,
                        principalTable: "RssFeeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "RssFeedContentModifier",
                columns: new[] { "Id", "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object[,]
                {
                    { 1, "<a10:content>", 5, "<content>" },
                    { 2, "</a10:content>", 5, "</content>" },
                    { 3, "<a10:content>", 3, "<content>" },
                    { 4, "</a10:content>", 3, "</content>" },
                    { 5, "<a10:content>", 4, "<content>" },
                    { 6, "</a10:content>", 4, "</content>" },
                    { 7, "<a10:content>", 2, "<content>" },
                    { 8, "</a10:content>", 2, "</content>" },
                    { 9, "<a10:content>", 1, "<content>" },
                    { 10, "</a10:content>", 1, "</content>" },
                    { 11, "<a10:content>", 96, "<content>" },
                    { 12, "</a10:content>", 96, "</content>" }
                });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, 2, "//figure[contains(@class, 'img-container')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, 2, "//figure[contains(@class, 'img-container')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, 2, "//figure[contains(@class, 'img-container')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, 2, "//figure[contains(@class, 'img-container')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, 2, "//figure[contains(@class, 'img-container')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//img[contains(@class, 'article__figure_img')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//img[contains(@class, 'article__figure_img')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//img[contains(@class, 'article__figure_img')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "RequestType", "Url", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "https://www.24sata.hr/feeds/Lifestyle.xml", "//img[contains(@class, 'article__figure_img')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//img[contains(@class, 'article__figure_img')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//img[contains(@class, 'article__figure_img')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//img[contains(@class, 'media-object adaptive lazy')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "RequestType", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, 2, "//img[contains(@class, 'media-object adaptive lazy')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "RequestType", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, 2, "//div[contains(@class, 'featured-img')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//img[contains(@class, 'card__image')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//img[contains(@class, 'card__image')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//img[contains(@class, 'card__image')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//img[contains(@class, 'card__image')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//img[contains(@class, 'card__image')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//img[contains(@class, 'card__image')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//img[contains(@class, 'card__image')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//img[contains(@class, 'card__image')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//img[contains(@class, 'card__image')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//img[contains(@class, 'card__image')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//img[contains(@class, 'card__image')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//img[contains(@class, 'card__image')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//img[contains(@class, 'card__image')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//img[contains(@class, 'card__image')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//img[contains(@class, 'lateImage lateImageLoaded')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//img[contains(@class, 'lateImage lateImageLoaded')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//img[contains(@class, 'lateImage lateImageLoaded')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//img[contains(@class, 'lateImage lateImageLoaded')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//img[contains(@class, 'lateImage lateImageLoaded')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "RequestType", "Url", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "https://www.tportal.hr/rss-Lifestyle.xml", "//img[contains(@class, 'lateImage lateImageLoaded')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//img[contains(@class, 'lateImage lateImageLoaded')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//img[contains(@class, 'lateImage lateImageLoaded')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "RequestType", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped" },
                values: new object[] { 1, 2, 2, "//script[contains(@type, 'application/ld+json')]", "image,url", true });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "RequestType", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, 2, "//div[contains(@class, 'thumb')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//div[contains(@class, 'featured-img')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "RequestType", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, 2, "//figure[contains(@class, 'article-main-img')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//figure[contains(@class, 'article-image main-image')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//img[contains(@class, 'naslovna')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//div[contains(@class, 'post-img')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//img[contains(@class, 'card__image')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//img[contains(@class, 'card__image')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//img[contains(@class, 'card__image')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//div[contains(@class, 'entry-content')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//div[contains(@class, 'attribute-image')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//div[contains(@class, 'img-holder')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//div[contains(@class, 'post__hero')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//div[contains(@class, 'postFeaturedImg postFeaturedImg--single')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//div[contains(@class, 'td-post-featured-image')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//div[contains(@class, 'first-image')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, 2, "//figure[contains(@class, 'dcms-image article-image')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//div[contains(@class, 'post-thumbnail')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//figure[contains(@class, 'figure')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "RequestType", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, 2, "//figure[contains(@class, 'media')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "RequestType", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, 2, "//div[contains(@class, 'td-post-featured-image')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//div[contains(@class, 'image-slider')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//div[contains(@class, 'image-slider')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//div[contains(@class, 'image-slider')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//div[contains(@class, 'image-slider')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "RequestType", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, 2, "//picture[contains(@class, 'pic')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "RequestType", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, 2, "//div[contains(@class, 'img-holder inner')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//div[contains(@class, 'pd-hero-image')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//div[contains(@class, 'pd-hero-image')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//div[contains(@class, 'pd-hero-image')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//div[contains(@class, 'pd-hero-image')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//div[contains(@class, 'pd-hero-image')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//div[contains(@class, 'pd-hero-image')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//div[contains(@class, 'pd-hero-image')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//div[contains(@class, 'pd-hero-image')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "RequestType", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, 2, "//div[contains(@class, 'mycontent')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//div[contains(@class, 'td-full-screen-header-image-wrap')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//div[contains(@class, 'single-post-media')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "RequestType", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, 2, "//img[contains(@class, 'article__figure_img')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 86,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 87,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 90,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 91,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 92,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 93,
                column: "RequestType",
                value: 2);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 94,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 95,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, 2, "//figure[contains(@class, 'img-container')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 97,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 98,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 99,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 100,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 101,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 102,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 103,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 104,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 105,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 106,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 107,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 108,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 109,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 110,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 111,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 112,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 113,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 114,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 115,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 116,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 117,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 118,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 119,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 120,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 121,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 122,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 123,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 124,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 125,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 126,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 127,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 128,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 129,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 130,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 131,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 132,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 133,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 134,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 135,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 136,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 137,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 138,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 139,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 140,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 141,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 142,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 143,
                column: "RequestType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 144,
                columns: new[] { "RequestType", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, 2, "//div[contains(@class, 'td-post-featured-image')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 145,
                columns: new[] { "RequestType", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, 2, "//div[contains(@class, 'entry-image featured-image')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 146,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//div[contains(@class, 'td-post-featured-image')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 147,
                columns: new[] { "RequestType", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, 2, "//div[contains(@class, 'td-post-featured-image')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 148,
                columns: new[] { "RequestType", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 1, "//div[contains(@class, 'img')]//img" });

            migrationBuilder.CreateIndex(
                name: "IX_RssFeedContentModifier_RssFeedId",
                table: "RssFeedContentModifier",
                column: "RssFeedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RssFeedContentModifier");

            migrationBuilder.DropColumn(
                name: "RequestType",
                table: "RssFeeds");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//figure[contains(@class, 'img-container')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//figure[contains(@class, 'img-container')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//figure[contains(@class, 'img-container')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//figure[contains(@class, 'img-container')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//figure[contains(@class, 'img-container')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'article__figure_img')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'article__figure_img')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'article__figure_img')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Url", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { "https://www.24sata.hr/feeds/lifestyle.xml", "//img[contains(@class, 'article__figure_img')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'article__figure_img')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 11,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'article__figure_img')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 12,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'media-object adaptive lazy')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//img[contains(@class, 'media-object adaptive lazy')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//div[contains(@class, 'featured-img')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 15,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 16,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 17,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 18,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 19,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 20,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 21,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 22,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 23,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 24,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 25,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 26,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 27,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 28,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 29,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'lateImage lateImageLoaded')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 30,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'lateImage lateImageLoaded')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 31,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'lateImage lateImageLoaded')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 32,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'lateImage lateImageLoaded')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 33,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'lateImage lateImageLoaded')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "Url", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { "https://www.tportal.hr/rss-lifestyle.xml", "//img[contains(@class, 'lateImage lateImageLoaded')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 35,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'lateImage lateImageLoaded')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 36,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'lateImage lateImageLoaded')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped" },
                values: new object[] { 2, 2, "//script[contains(@type, 'application/ld+json')]", "image,url", true });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//div[contains(@class, 'thumb')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 40,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'featured-img')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//figure[contains(@class, 'article-main-img')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 43,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//figure[contains(@class, 'article-image main-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 44,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'naslovna')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 47,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'post-img')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 48,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 49,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 50,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//img[contains(@class, 'card__image')]");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 54,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'entry-content')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 55,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'attribute-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 56,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'img-holder')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 57,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'post__hero')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 58,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'postFeaturedImg postFeaturedImg--single')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 59,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'td-post-featured-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 61,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'first-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//figure[contains(@class, 'dcms-image article-image')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 63,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'post-thumbnail')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 64,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//figure[contains(@class, 'figure')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//figure[contains(@class, 'media')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//div[contains(@class, 'td-post-featured-image')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 67,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'image-slider')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 68,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'image-slider')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 69,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'image-slider')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 70,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'image-slider')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//picture[contains(@class, 'pic')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//div[contains(@class, 'img-holder inner')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 74,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'pd-hero-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 75,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'pd-hero-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 76,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'pd-hero-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 77,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'pd-hero-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 78,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'pd-hero-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 79,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'pd-hero-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 80,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'pd-hero-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 81,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'pd-hero-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//div[contains(@class, 'mycontent')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 83,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'td-full-screen-header-image-wrap')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 84,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'single-post-media')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//img[contains(@class, 'article__figure_img')]" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//figure[contains(@class, 'img-container')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 144,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//div[contains(@class, 'td-post-featured-image')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 145,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//div[contains(@class, 'entry-image featured-image')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 146,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'td-post-featured-image')]//img");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 147,
                columns: new[] { "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath" },
                values: new object[] { 2, "//div[contains(@class, 'td-post-featured-image')]//img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 148,
                column: "ImageUrlParseConfiguration_ImgElementXPath",
                value: "//div[contains(@class, 'img')]//img");
        }
    }
}
