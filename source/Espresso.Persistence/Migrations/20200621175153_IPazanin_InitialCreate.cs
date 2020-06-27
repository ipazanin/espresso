using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Espresso.Persistence.Migrations
{
    public partial class IPazanin_InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationDownload",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WebApiVersion = table.Column<string>(maxLength: 10, nullable: false),
                    MobileAppVersion = table.Column<string>(maxLength: 20, nullable: false),
                    DownloadedTime = table.Column<DateTime>(nullable: false),
                    MobileDeviceType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationDownload", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Color = table.Column<string>(maxLength: 20, nullable: false),
                    KeyWordsRegexPattern = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsPortals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    BaseUrl = table.Column<string>(maxLength: 100, nullable: false),
                    IconUrl = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsPortals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RssFeeds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(maxLength: 300, nullable: false),
                    AmpConfiguration_HasAmpArticles = table.Column<bool>(nullable: true),
                    AmpConfiguration_TemplateUrl = table.Column<string>(maxLength: 300, nullable: true),
                    CategoryParseConfiguration_CategoryParseStrategy = table.Column<int>(nullable: true),
                    ImageUrlParseConfiguration_ImageUrlParseStrategy = table.Column<int>(nullable: true),
                    ImageUrlParseConfiguration_ImgElementXPath = table.Column<string>(maxLength: 300, nullable: true),
                    ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped = table.Column<bool>(nullable: true, defaultValue: false),
                    ImageUrlParseConfiguration_ImageUrlWebScrapeType = table.Column<int>(nullable: true),
                    ImageUrlParseConfiguration_JsonWebScrapePropertyNames = table.Column<string>(nullable: true),
                    SkipParseConfiguration_NumberOfSkips = table.Column<int>(nullable: true),
                    SkipParseConfiguration_CurrentSkip = table.Column<int>(nullable: true),
                    NewsPortalId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RssFeeds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RssFeeds_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RssFeeds_NewsPortals_NewsPortalId",
                        column: x => x.NewsPortalId,
                        principalTable: "NewsPortals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ArticleId = table.Column<string>(maxLength: 500, nullable: false),
                    Url = table.Column<string>(maxLength: 500, nullable: false),
                    Summary = table.Column<string>(maxLength: 2000, nullable: false),
                    Title = table.Column<string>(maxLength: 500, nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    UpdateDateTime = table.Column<DateTime>(nullable: false),
                    PublishDateTime = table.Column<DateTime>(nullable: false),
                    NumberOfClicks = table.Column<int>(nullable: false),
                    TrendingScore = table.Column<decimal>(nullable: false),
                    NewsPortalId = table.Column<int>(nullable: false),
                    RssFeedId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_NewsPortals_NewsPortalId",
                        column: x => x.NewsPortalId,
                        principalTable: "NewsPortals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Articles_RssFeeds_RssFeedId",
                        column: x => x.RssFeedId,
                        principalTable: "RssFeeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RssFeedCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrlRegex = table.Column<string>(maxLength: 100, nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    RssFeedId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RssFeedCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RssFeedCategory_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RssFeedCategory_RssFeeds_RssFeedId",
                        column: x => x.RssFeedId,
                        principalTable: "RssFeeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticleCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ArticleId = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleCategories_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Color", "KeyWordsRegexPattern", "Name" },
                values: new object?[,]
                {
                    { 1, "#E84855", null, "Vijesti" },
                    { 2, "#4CB944", null, "Sport" },
                    { 3, "#F4B100", null, "Show" },
                    { 4, "#32936F", null, "Lifestyle" },
                    { 5, "#2E86AB", null, "Tech" },
                    { 6, "#9055A2", null, "Viral" },
                    { 7, "#3185FC", null, "Biznis" },
                    { 8, "#FC814A", null, "Auto/Moto" },
                    { 9, "#AC80A0", null, "Kultura" }
                });

            migrationBuilder.InsertData(
                table: "NewsPortals",
                columns: new[] { "Id", "BaseUrl", "IconUrl", "Name" },
                values: new object?[,]
                {
                    { 18, "https://www.bug.hr/", "Icons/Bug.png", "Bug" },
                    { 19, "https://www.vidi.hr/", "Icons/VidiHr.png", "Vidi.hr" },
                    { 20, "https://zimo.dnevnik.hr/", "Icons/Zimo.png", "Zimo" },
                    { 21, "https://www.netokracija.com/", "Icons/Netokracija.png", "Netokracija" },
                    { 22, "https://poslovnipuls.com/", "Icons/PoslovniPuls.png", "Poslovni Puls" },
                    { 27, "http://www.ljepotaizdravlje.hr/", "Icons/LjepotaIZdravlje.png", "Ljepota i zdravlje" },
                    { 25, "http://www.cosmopolitan.hr/", "Icons/Cosmopolitan.png", "Cosmopolitan" },
                    { 26, "https://wall.hr/", "Icons/WallHr.png", "Wall.hr" },
                    { 16, "https://lider.media/", "Icons/Lider.png", "Lider" },
                    { 28, "https://www.autonet.hr/", "Icons/Autonet.png", "Autonet" },
                    { 23, "https://pcchip.hr/", "Icons/PcChip.png", "PCchip" },
                    { 15, "http://www.nogometplus.net/", "Icons/NogometPlus.png", "Nogomet Plus" },
                    { 9, "https://www.telegram.hr/", "Icons/Telegram.png", "Telegram" },
                    { 11, "https://gol.dnevnik.hr/", "Icons/Gol.png", "Gol" },
                    { 10, "https://dnevnik.hr/", "Icons/Dnevnik.png", "Dnevnik" },
                    { 29, "https://hr.n1info.com/", "Icons/N1.png", "N1" },
                    { 8, "https://www.vecernji.hr/", "Icons/VecernjiList.png", "Večernji List" },
                    { 7, "https://www.tportal.hr/", "Icons/TPortal.png", "tportal" },
                    { 6, "https://slobodnadalmacija.hr/", "Icons/SlobodnaDalmacija.png", "Slobodna Dalmacija" },
                    { 5, "https://net.hr/", "Icons/NetHr.png", "Net.hr" },
                    { 4, "https://sportske.jutarnji.hr/", "Icons/JutarnjiList.png", "Jutarnji List" },
                    { 3, "https://sportske.jutarnji.hr/", "Icons/SportskeNovosti.png", "Sportske Novosti" },
                    { 2, "https://www.24sata.hr/", "Icons/DvadesetCetiriSata.png", "24 sata" },
                    { 1, "https://www.index.hr/", "Icons/Index.png", "Index.hr" },
                    { 12, "https://sportnet.rtl.hr/", "Icons/RtlVijesti.png", "RTL Vijesti" },
                    { 30, "https://narod.hr/", "Icons/NarodHr.png", "Narod HR" }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames" },
                values: new object?[] { 1, 1, 1, "https://www.index.hr/rss/vijesti", true, "https://amp.index.hr/article/{0}{3}", 1, 1, 1, "//img[contains(@class, 'img-large loaded')]", null });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames" },
                values: new object?[,]
                {
                    { 32, 5, 7, "https://www.tportal.hr/rss-tehno.xml", 1, 1, 1, "//img[contains(@class, 'lateImage lateImageLoaded')]", null },
                    { 33, 3, 7, "https://www.tportal.hr/rss-showtime.xml", 1, 1, 1, "//img[contains(@class, 'lateImage lateImageLoaded')]", null },
                    { 34, 4, 7, "https://www.tportal.hr/rss-lifestyle.xml", 1, 1, 1, "//img[contains(@class, 'lateImage lateImageLoaded')]", null },
                    { 35, 6, 7, "https://www.tportal.hr/rss-funbox.xml", 1, 1, 1, "//img[contains(@class, 'lateImage lateImageLoaded')]", null },
                    { 36, 9, 7, "https://www.tportal.hr/rss-kultura.xml", 1, 1, 1, "//img[contains(@class, 'lateImage lateImageLoaded')]", null }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped" },
                values: new object?[] { 37, 1, 8, "https://www.vecernji.hr/feeds/latest", true, "https://m.vecernji.hr/amp/{1}{2}", 2, 1, 2, "//script[contains(@type, 'application/ld+json')]", "image,url", true });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames" },
                values: new object?[,]
                {
                    { 39, 1, 9, "https://www.telegram.hr/feed/", 2, 1, 1, "//div[contains(@class, 'thumb')]//img", null },
                    { 40, 2, 9, "https://telesport.telegram.hr/feed/", 1, 1, 1, "//div[contains(@class, 'featured-img')]//img", null },
                    { 41, 4, 9, "https://www.telegram.hr/feed/", 2, 1, 1, "//div[contains(@class, 'thumb')]//img", null }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames" },
                values: new object?[,]
                {
                    { 42, 1, 10, "https://dnevnik.hr/assets/feed/articles/", true, "https://dnevnik.hr/amp/{1}{2}{3}", 2, 1, 1, "//figure[contains(@class, 'article-main-img')]//img", null },
                    { 43, 2, 11, "https://gol.dnevnik.hr/assets/feed/articles", true, "https://gol.dnevnik.hr/amp/{1}{2}{3}{4}", 1, 1, 1, "//figure[contains(@class, 'article-image main-image')]//img", null }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames" },
                values: new object?[,]
                {
                    { 44, 2, 12, "https://sportnet.rtl.hr/rss/", 1, 1, 1, "//img[contains(@class, 'naslovna')]", null },
                    { 31, 2, 7, "https://www.tportal.hr/rss-sport.xml", 1, 1, 1, "//img[contains(@class, 'lateImage lateImageLoaded')]", null },
                    { 47, 2, 15, "http://www.nogometplus.net/index.php/feed/", 1, 1, 1, "//div[contains(@class, 'post-img')]//img", null },
                    { 49, 7, 16, "http://lider.media/feed/", 1, 1, 1, "//img[contains(@class, 'card__image')]", null },
                    { 50, 7, 16, "http://lider.media/feed/", 1, 1, 1, "//img[contains(@class, 'card__image')]", null },
                    { 54, 5, 18, "http://www.bug.hr/rss/vijesti/", 1, 1, 1, "//div[contains(@class, 'entry-content')]//img", null },
                    { 55, 5, 19, "http://www.vidi.hr/rss/feed/vidi", 1, 1, 1, "//div[contains(@class, 'attribute-image')]//img", null }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames" },
                values: new object?[,]
                {
                    { 56, 5, 20, "https://zimo.dnevnik.hr/assets/feed/articles", true, "https://zimo.dnevnik.hr/amp/clanak/{2}", 1, 1, 1, "//div[contains(@class, 'img-holder')]//img", null },
                    { 57, 5, 21, "http://www.netokracija.com/feed", true, "https://www.netokracija.com/{1}/amp", 1, 1, 1, "//div[contains(@class, 'post__hero')]//img", null }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object?[] { 58, 7, 22, "http://www.poslovnipuls.com/feed/", 1, 1, 1, "//div[contains(@class, 'postFeaturedImg postFeaturedImg--single')]//img", null, 0, 10 });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames" },
                values: new object?[,]
                {
                    { 59, 5, 23, "http://pcchip.hr/feed/", 1, 1, 1, "//div[contains(@class, 'td-post-featured-image')]//img", null },
                    { 61, 4, 25, "http://www.cosmopolitan.hr/feed", 1, 1, 1, "//div[contains(@class, 'first-image')]//img", null },
                    { 62, 4, 26, "http://wall.hr/cdn/feed.xml", 1, 2, 1, "//figure[contains(@class, 'dcms-image article-image')]//img", null },
                    { 63, 4, 27, "http://www.ljepotaizdravlje.hr/feed", 1, 1, 1, "//div[contains(@class, 'post-thumbnail')]//img", null },
                    { 64, 8, 28, "https://www.autonet.hr/feed/", 1, 1, 1, "//figure[contains(@class, 'figure')]//img", null },
                    { 48, 7, 16, "http://lider.media/feed/", 1, 1, 1, "//img[contains(@class, 'card__image')]", null },
                    { 65, 1, 29, "http://hr.n1info.com/rss/249/Naslovna", 2, 1, 1, "//figure[contains(@class, 'media')]//img", null },
                    { 30, 1, 7, "https://www.tportal.hr/rss-biznis.xml", 1, 1, 1, "//img[contains(@class, 'lateImage lateImageLoaded')]", null }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object?[] { 28, 6, 6, "https://www.slobodnadalmacija.hr/feed/category/274", 1, 1, 1, "//img[contains(@class, 'card__image')]", null, 0, 5 });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames" },
                values: new object?[,]
                {
                    { 2, 2, 1, "https://www.index.hr/rss/sport", true, "https://amp.index.hr/article/{0}{3}", 1, 1, 1, "//img[contains(@class, 'img-large loaded')]", null },
                    { 3, 3, 1, "https://www.index.hr/rss/magazin", true, "https://amp.index.hr/article/{0}{3}", 1, 1, 1, "//img[contains(@class, 'img-large loaded')]", null },
                    { 4, 4, 1, "https://www.index.hr/rss/rouge", true, "https://amp.index.hr/article/{0}{3}", 1, 1, 1, "//img[contains(@class, 'img-large loaded')]", null },
                    { 5, 8, 1, "https://www.index.hr/rss/auto", true, "https://amp.index.hr/article/{0}{3}", 1, 1, 1, "//img[contains(@class, 'img-large loaded')]", null },
                    { 6, 1, 2, "https://www.24sata.hr/feeds/news.xml", false, null, 1, 1, 1, "//img[contains(@class, 'article__figure_img')]", null },
                    { 7, 3, 2, "https://www.24sata.hr/feeds/show.xml", false, null, 1, 1, 1, "//img[contains(@class, 'article__figure_img')]", null },
                    { 8, 2, 2, "https://www.24sata.hr/feeds/sport.xml", false, null, 1, 1, 1, "//img[contains(@class, 'article__figure_img')]", null },
                    { 9, 4, 2, "https://www.24sata.hr/feeds/lifestyle.xml", false, null, 1, 1, 1, "//img[contains(@class, 'article__figure_img')]", null },
                    { 10, 5, 2, "https://www.24sata.hr/feeds/tech.xml", false, null, 1, 1, 1, "//img[contains(@class, 'article__figure_img')]", null },
                    { 11, 6, 2, "https://www.24sata.hr/feeds/fun.xml", false, null, 1, 1, 1, "//img[contains(@class, 'article__figure_img')]", null }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames" },
                values: new object?[] { 12, 2, 3, "http://sportske.jutarnji.hr/sn/feed", 1, 1, 1, "//img[contains(@class, 'media-object adaptive lazy')]", null });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object?[] { 13, 1, 4, "https://www.jutarnji.hr/feed", 2, 1, 1, "//img[contains(@class, 'media-object adaptive lazy')]", null, 0, 5 });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames" },
                values: new object?[] { 29, 1, 7, "https://www.tportal.hr/rss-vijesti.xml", 1, 1, 1, "//img[contains(@class, 'lateImage lateImageLoaded')]", null });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames" },
                values: new object?[] { 14, 1, 5, "https://net.hr/feed/", true, "https://net.hr/{1}{2}{3}amp", 1, 1, 1, "//div[contains(@class, 'featured-img')]//img", null });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object?[,]
                {
                    { 16, 2, 6, "https://www.slobodnadalmacija.hr/feed/category/255", 1, 1, 1, "//img[contains(@class, 'card__image')]", null, 0, 5 },
                    { 17, 3, 6, "https://www.slobodnadalmacija.hr/feed/category/262", 1, 1, 1, "//img[contains(@class, 'card__image')]", null, 0, 5 },
                    { 18, 3, 6, "https://www.slobodnadalmacija.hr/feed/category/375", 1, 1, 1, "//img[contains(@class, 'card__image')]", null, 0, 5 },
                    { 19, 3, 6, "https://www.slobodnadalmacija.hr/feed/category/263", 1, 1, 1, "//img[contains(@class, 'card__image')]", null, 0, 5 },
                    { 20, 4, 6, "https://www.slobodnadalmacija.hr/feed/category/264", 1, 1, 1, "//img[contains(@class, 'card__image')]", null, 0, 5 },
                    { 21, 4, 6, "https://www.slobodnadalmacija.hr/feed/category/265", 1, 1, 1, "//img[contains(@class, 'card__image')]", null, 0, 5 },
                    { 22, 4, 6, "https://www.slobodnadalmacija.hr/feed/category/266", 1, 1, 1, "//img[contains(@class, 'card__image')]", null, 0, 5 },
                    { 23, 4, 6, "https://www.slobodnadalmacija.hr/feed/category/267", 1, 1, 1, "//img[contains(@class, 'card__image')]", null, 0, 5 },
                    { 24, 4, 6, "https://www.slobodnadalmacija.hr/feed/category/268", 1, 1, 1, "//img[contains(@class, 'card__image')]", null, 0, 5 },
                    { 25, 4, 6, "https://www.slobodnadalmacija.hr/feed/category/270", 1, 1, 1, "//img[contains(@class, 'card__image')]", null, 0, 5 },
                    { 26, 4, 6, "https://www.slobodnadalmacija.hr/feed/category/271", 1, 1, 1, "//img[contains(@class, 'card__image')]", null, 0, 5 },
                    { 27, 5, 6, "https://www.slobodnadalmacija.hr/feed/category/269", 1, 1, 1, "//img[contains(@class, 'card__image')]", null, 0, 5 },
                    { 15, 1, 6, "https://www.slobodnadalmacija.hr/feed/category/119", 1, 1, 1, "//img[contains(@class, 'card__image')]", null, 0, 5 }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames" },
                values: new object?[] { 66, 1, 30, "https://narod.hr/feed", 2, 1, 1, "//div[contains(@class, 'td-post-featured-image')]//img", null });

            migrationBuilder.InsertData(
                table: "RssFeedCategory",
                columns: new[] { "Id", "CategoryId", "RssFeedId", "UrlRegex" },
                values: new object?[,]
                {
                    { 1, 1, 13, "vijesti" },
                    { 20, 1, 65, "Vijesti" },
                    { 21, 1, 65, "Svijet" },
                    { 22, 1, 65, "Znanost" },
                    { 23, 1, 65, "Regija" },
                    { 24, 1, 65, "Dnevnik" },
                    { 25, 1, 65, "Info" },
                    { 12, 1, 42, "vijest" },
                    { 26, 7, 65, "Biznis" },
                    { 28, 4, 65, "Zdravlje" },
                    { 29, 2, 65, "Sport-Klub" },
                    { 30, 3, 65, "Showbiz" },
                    { 31, 5, 65, "Tehnologija" },
                    { 32, 9, 65, "Kultura" },
                    { 33, 1, 66, "Hrvatska" },
                    { 27, 4, 65, "Lifestyle" },
                    { 34, 2, 66, "Sport" },
                    { 19, 4, 41, "život" },
                    { 11, 9, 37, "kultura" },
                    { 2, 8, 13, "autoklub" },
                    { 3, 9, 13, "kultura" },
                    { 13, 1, 13, "globus" },
                    { 14, 4, 13, "domidizajn" },
                    { 15, 6, 13, "viral" },
                    { 16, 3, 13, "spektakli" },
                    { 18, 1, 41, "politika-kriminal" },
                    { 17, 4, 13, "life" },
                    { 5, 2, 37, "sport" },
                    { 6, 3, 37, "showbiz" },
                    { 7, 4, 37, "lifestyle" },
                    { 8, 7, 37, "biznis" },
                    { 9, 5, 37, "techsci" },
                    { 10, 8, 37, "automoto" },
                    { 4, 1, 37, "vijesti" },
                    { 35, 9, 66, "Kultura" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleCategories_ArticleId",
                table: "ArticleCategories",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleCategories_CategoryId",
                table: "ArticleCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_NewsPortalId",
                table: "Articles",
                column: "NewsPortalId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_PublishDateTime",
                table: "Articles",
                column: "PublishDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_RssFeedId",
                table: "Articles",
                column: "RssFeedId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_TrendingScore",
                table: "Articles",
                column: "TrendingScore");

            migrationBuilder.CreateIndex(
                name: "IX_RssFeedCategory_CategoryId",
                table: "RssFeedCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RssFeedCategory_RssFeedId",
                table: "RssFeedCategory",
                column: "RssFeedId");

            migrationBuilder.CreateIndex(
                name: "IX_RssFeeds_CategoryId",
                table: "RssFeeds",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RssFeeds_NewsPortalId",
                table: "RssFeeds",
                column: "NewsPortalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationDownload");

            migrationBuilder.DropTable(
                name: "ArticleCategories");

            migrationBuilder.DropTable(
                name: "RssFeedCategory");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "RssFeeds");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "NewsPortals");
        }
    }
}
