using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Espresso.Persistence.Migrations
{
    public partial class InitialCreate : Migration
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
                    KeyWordsRegexPattern = table.Column<string>(maxLength: 1000, nullable: true),
                    SortIndex = table.Column<int>(nullable: true),
                    Position = table.Column<int>(nullable: true),
                    CategoryType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PushNotifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InternalName = table.Column<string>(maxLength: 1000, nullable: false),
                    Title = table.Column<string>(maxLength: 1000, nullable: false),
                    Message = table.Column<string>(maxLength: 1000, nullable: false),
                    Topic = table.Column<string>(maxLength: 1000, nullable: false),
                    ArticleUrl = table.Column<string>(maxLength: 5000, nullable: false),
                    IsSoundEnabled = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PushNotifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsPortals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    BaseUrl = table.Column<string>(maxLength: 100, nullable: false),
                    IconUrl = table.Column<string>(maxLength: 100, nullable: false),
                    IsNewOverride = table.Column<bool>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    RegionId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsPortals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsPortals_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsPortals_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Title = table.Column<string>(maxLength: 500, nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    UpdateDateTime = table.Column<DateTime>(nullable: false),
                    PublishDateTime = table.Column<DateTime>(nullable: false),
                    NumberOfClicks = table.Column<int>(nullable: false),
                    TrendingScore = table.Column<decimal>(nullable: false),
                    IsHidden = table.Column<bool>(nullable: false, defaultValue: false),
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
                    UrlSegmentIndex = table.Column<int>(nullable: false),
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
                columns: new[] { "Id", "CategoryType", "Color", "KeyWordsRegexPattern", "Name", "Position", "SortIndex" },
                values: new object?[,]
                {
                    { 1, 1, "#E84855", null, "Vijesti", null, 2 },
                    { 12, 2, "#AC80A0", null, "Lokalno", 3, null },
                    { 11, 3, "#AC80A0", null, "Generalno", null, 1 },
                    { 8, 1, "#FC814A", null, "Auto/Moto", null, 9 },
                    { 7, 1, "#3185FC", null, "Biznis", null, 8 },
                    { 9, 1, "#AC80A0", null, "Kultura", null, 10 },
                    { 5, 1, "#2E86AB", null, "Tech", null, 6 },
                    { 4, 1, "#32936F", null, "Lifestyle", null, 5 },
                    { 3, 1, "#F4B100", null, "Show", null, 4 },
                    { 2, 1, "#4CB944", null, "Sport", null, 3 },
                    { 6, 1, "#9055A2", null, "Viral", null, 7 }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Name" },
                values: new object?[,]
                {
                    { 7, "Slavonija & Baranja" },
                    { 1, "Global" },
                    { 2, "Dalmacija" },
                    { 3, "Istra & Kvarner" },
                    { 4, "Lika" },
                    { 6, "Sjeverna Hrvatska" },
                    { 5, "Zagreb" }
                });

            migrationBuilder.InsertData(
                table: "NewsPortals",
                columns: new[] { "Id", "BaseUrl", "CategoryId", "CreatedAt", "IconUrl", "IsNewOverride", "Name", "RegionId" },
                values: new object?[,]
                {
                    { 1, "https://www.index.hr/", 11, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Icons/Index.png", null, "Index.hr", 1 },
                    { 35, "https://direktno.hr/", 1, new DateTime(2020, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Icons/DirektnoHr.png", null, "Direktno", 1 },
                    { 33, "https://www.dnevno.hr/", 1, new DateTime(2020, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Icons/Dnevno.png", null, "Dnevno.Hr", 1 },
                    { 32, "https://100posto.jutarnji.hr/", 1, new DateTime(2020, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Icons/StoPosto.png", null, "100posto", 1 },
                    { 31, "https://www.hrt.hr/", 1, new DateTime(2020, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Icons/Hrt.png", null, "HRT", 1 },
                    { 30, "https://narod.hr/", 1, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Icons/NarodHr.png", null, "Narod HR", 1 },
                    { 29, "https://hr.n1info.com/", 1, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Icons/N1.png", null, "N1", 1 },
                    { 28, "https://www.autonet.hr/", 8, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Icons/Autonet.png", null, "Autonet", 1 },
                    { 27, "http://www.ljepotaizdravlje.hr/", 4, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Icons/LjepotaIZdravlje.png", null, "Ljepota i zdravlje", 1 },
                    { 26, "https://wall.hr/", 4, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Icons/WallHr.png", null, "Wall.hr", 1 },
                    { 25, "http://www.cosmopolitan.hr/", 4, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Icons/Cosmopolitan.png", null, "Cosmopolitan", 1 },
                    { 23, "https://pcchip.hr/", 5, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Icons/PcChip.png", null, "PCchip", 1 },
                    { 22, "https://poslovnipuls.com/", 7, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Icons/PoslovniPuls.png", null, "Poslovni Puls", 1 },
                    { 21, "https://www.netokracija.com/", 5, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Icons/Netokracija.png", null, "Netokracija", 1 },
                    { 20, "https://zimo.dnevnik.hr/", 5, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Icons/Zimo.png", null, "Zimo", 1 },
                    { 19, "https://www.vidi.hr/", 5, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Icons/VidiHr.png", null, "Vidi.hr", 1 },
                    { 18, "https://www.bug.hr/", 5, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Icons/Bug.png", null, "Bug", 1 },
                    { 16, "https://lider.media/", 7, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Icons/Lider.png", null, "Lider", 1 },
                    { 15, "http://www.nogometplus.net/", 2, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Icons/NogometPlus.png", null, "Nogomet Plus", 1 },
                    { 12, "https://sportnet.rtl.hr/", 11, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Icons/RtlVijesti.png", null, "RTL Vijesti", 1 },
                    { 11, "https://gol.dnevnik.hr/", 2, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Icons/Gol.png", null, "Gol", 1 },
                    { 10, "https://dnevnik.hr/", 11, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Icons/Dnevnik.png", null, "Dnevnik", 1 },
                    { 9, "https://www.telegram.hr/", 11, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Icons/Telegram.png", null, "Telegram", 1 },
                    { 8, "https://www.vecernji.hr/", 11, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Icons/VecernjiList.png", null, "Večernji List", 1 },
                    { 7, "https://www.tportal.hr/", 11, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Icons/TPortal.png", null, "tportal", 1 },
                    { 6, "https://slobodnadalmacija.hr/", 11, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Icons/SlobodnaDalmacija.png", null, "Slobodna Dalmacija", 1 },
                    { 5, "https://net.hr/", 11, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Icons/NetHr.png", null, "Net.hr", 1 },
                    { 4, "https://sportske.jutarnji.hr/", 11, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Icons/JutarnjiList.png", null, "Jutarnji List", 1 },
                    { 3, "https://sportske.jutarnji.hr/", 2, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Icons/SportskeNovosti.png", null, "Sportske Novosti", 1 },
                    { 2, "https://www.24sata.hr/", 11, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Icons/DvadesetCetiriSata.png", null, "24 sata", 1 },
                    { 36, "https://www.scena.hr/", 3, new DateTime(2020, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Icons/Scena.png", null, "Scena", 1 },
                    { 37, "https://www.dalmacijadanas.hr/", 12, new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Icons/DalmacijaDanas.png", null, "Dalmacija Danas", 2 }
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
                    { 61, 4, 25, "http://www.cosmopolitan.hr/feed", 1, 1, 1, "//div[contains(@class, 'first-image')]//img", null },
                    { 59, 5, 23, "http://pcchip.hr/feed/", 1, 1, 1, "//div[contains(@class, 'td-post-featured-image')]//img", null }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object?[] { 58, 7, 22, "http://www.poslovnipuls.com/feed/", 1, 1, 1, "//div[contains(@class, 'postFeaturedImg postFeaturedImg--single')]//img", null, 0, 10 });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames" },
                values: new object?[,]
                {
                    { 57, 5, 21, "http://www.netokracija.com/feed", true, "https://www.netokracija.com/{1}/amp", 1, 1, 1, "//div[contains(@class, 'post__hero')]//img", null },
                    { 56, 5, 20, "https://zimo.dnevnik.hr/assets/feed/articles", true, "https://zimo.dnevnik.hr/amp/clanak/{2}", 1, 1, 1, "//div[contains(@class, 'img-holder')]//img", null }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames" },
                values: new object?[,]
                {
                    { 55, 5, 19, "http://www.vidi.hr/rss/feed/vidi", 1, 1, 1, "//div[contains(@class, 'attribute-image')]//img", null },
                    { 62, 4, 26, "http://wall.hr/cdn/feed.xml", 1, 2, 1, "//figure[contains(@class, 'dcms-image article-image')]//img", null },
                    { 54, 5, 18, "http://www.bug.hr/rss/vijesti/", 1, 1, 1, "//div[contains(@class, 'entry-content')]//img", null },
                    { 49, 7, 16, "http://lider.media/feed/", 1, 1, 1, "//img[contains(@class, 'card__image')]", null },
                    { 48, 7, 16, "http://lider.media/feed/", 1, 1, 1, "//img[contains(@class, 'card__image')]", null },
                    { 47, 2, 15, "http://www.nogometplus.net/index.php/feed/", 1, 1, 1, "//div[contains(@class, 'post-img')]//img", null },
                    { 44, 2, 12, "https://sportnet.rtl.hr/rss/", 1, 1, 1, "//img[contains(@class, 'naslovna')]", null }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames" },
                values: new object?[,]
                {
                    { 43, 2, 11, "https://gol.dnevnik.hr/assets/feed/articles", true, "https://gol.dnevnik.hr/amp/{1}{2}{3}{4}", 1, 1, 1, "//figure[contains(@class, 'article-image main-image')]//img", null },
                    { 42, 1, 10, "https://dnevnik.hr/assets/feed/articles/", true, "https://dnevnik.hr/amp/{1}{2}{3}", 2, 1, 1, "//figure[contains(@class, 'article-main-img')]//img", null }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames" },
                values: new object?[,]
                {
                    { 50, 7, 16, "http://lider.media/feed/", 1, 1, 1, "//img[contains(@class, 'card__image')]", null },
                    { 63, 4, 27, "http://www.ljepotaizdravlje.hr/feed", 1, 1, 1, "//div[contains(@class, 'post-thumbnail')]//img", null },
                    { 64, 8, 28, "https://www.autonet.hr/feed/", 1, 1, 1, "//figure[contains(@class, 'figure')]//img", null },
                    { 65, 1, 29, "http://hr.n1info.com/rss/249/Naslovna", 2, 1, 1, "//figure[contains(@class, 'media')]//img", null },
                    { 81, 1, 35, "https://direktno.hr/rss/publish/latest/direktnotv-100/", 1, 1, 1, "//div[contains(@class, 'pd-hero-image')]//img", null },
                    { 80, 1, 35, "https://direktno.hr/rss/publish/latest/kolumne-80/", 1, 1, 1, "//div[contains(@class, 'pd-hero-image')]//img", null },
                    { 79, 3, 35, "https://direktno.hr/rss/publish/latest/zivot-70/", 1, 1, 1, "//div[contains(@class, 'pd-hero-image')]//img", null },
                    { 78, 2, 35, "https://direktno.hr/rss/publish/latest/sport-60/", 1, 1, 1, "//div[contains(@class, 'pd-hero-image')]//img", null },
                    { 77, 7, 35, "https://direktno.hr/rss/publish/latest/razvoj-110/", 1, 1, 1, "//div[contains(@class, 'pd-hero-image')]//img", null },
                    { 76, 1, 35, "https://direktno.hr/rss/publish/latest/eu_svijet/", 1, 1, 1, "//div[contains(@class, 'pd-hero-image')]//img", null },
                    { 75, 1, 35, "https://direktno.hr/rss/publish/latest/domovina-10/", 1, 1, 1, "//div[contains(@class, 'pd-hero-image')]//img", null },
                    { 74, 1, 35, "https://direktno.hr/rss/publish/latest/direkt-50/", 1, 1, 1, "//div[contains(@class, 'pd-hero-image')]//img", null },
                    { 72, 1, 33, "https://www.dnevno.hr/feed/", 2, 1, 1, "//div[contains(@class, 'img-holder inner')]//img", null }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object?[] { 71, 1, 32, "https://100posto.jutarnji.hr/rss", 2, 1, 1, "//picture[contains(@class, 'pic')]//img", null, 0, 5 });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames" },
                values: new object?[,]
                {
                    { 70, 3, 31, "https://www.hrt.hr/rss/glazba/", 1, 1, 1, "//div[contains(@class, 'image-slider')]//img", null },
                    { 69, 3, 31, "https://magazin.hrt.hr/feed.xml", 1, 1, 1, "//div[contains(@class, 'image-slider')]//img", null },
                    { 68, 2, 31, "https://www.hrt.hr/rss/sport/", 1, 1, 1, "//div[contains(@class, 'image-slider')]//img", null },
                    { 67, 1, 31, "https://www.hrt.hr/rss/vijesti/", 1, 1, 1, "//div[contains(@class, 'image-slider')]//img", null },
                    { 66, 1, 30, "https://narod.hr/feed", 2, 1, 1, "//div[contains(@class, 'td-post-featured-image')]//img", null },
                    { 40, 2, 9, "https://telesport.telegram.hr/feed/", 1, 1, 1, "//div[contains(@class, 'featured-img')]//img", null },
                    { 39, 1, 9, "https://www.telegram.hr/feed/", 2, 1, 1, "//div[contains(@class, 'thumb')]//img", null }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped" },
                values: new object?[] { 37, 1, 8, "https://www.vecernji.hr/feeds/latest", true, "https://m.vecernji.hr/amp/{1}{2}", 2, 1, 2, "//script[contains(@type, 'application/ld+json')]", "image,url", true });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames" },
                values: new object?[] { 36, 9, 7, "https://www.tportal.hr/rss-kultura.xml", 1, 1, 1, "//img[contains(@class, 'lateImage lateImageLoaded')]", null });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object?[,]
                {
                    { 16, 2, 6, "https://www.slobodnadalmacija.hr/feed/category/255", 1, 1, 1, "//img[contains(@class, 'card__image')]", null, 0, 5 },
                    { 15, 1, 6, "https://www.slobodnadalmacija.hr/feed/category/119", 1, 1, 1, "//img[contains(@class, 'card__image')]", null, 0, 5 }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames" },
                values: new object?[] { 14, 1, 5, "https://net.hr/feed/", true, "https://net.hr/{1}{2}{3}amp", 2, 1, 1, "//div[contains(@class, 'featured-img')]//img", null });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object?[] { 13, 1, 4, "https://www.jutarnji.hr/feed", 2, 1, 1, "//img[contains(@class, 'media-object adaptive lazy')]", null, 0, 5 });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames" },
                values: new object?[] { 12, 2, 3, "http://sportske.jutarnji.hr/sn/feed", 1, 1, 1, "//img[contains(@class, 'media-object adaptive lazy')]", null });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames" },
                values: new object?[,]
                {
                    { 11, 6, 2, "https://www.24sata.hr/feeds/fun.xml", false, null, 1, 1, 1, "//img[contains(@class, 'article__figure_img')]", null },
                    { 10, 5, 2, "https://www.24sata.hr/feeds/tech.xml", false, null, 1, 1, 1, "//img[contains(@class, 'article__figure_img')]", null },
                    { 9, 4, 2, "https://www.24sata.hr/feeds/lifestyle.xml", false, null, 1, 1, 1, "//img[contains(@class, 'article__figure_img')]", null },
                    { 8, 2, 2, "https://www.24sata.hr/feeds/sport.xml", false, null, 1, 1, 1, "//img[contains(@class, 'article__figure_img')]", null },
                    { 7, 3, 2, "https://www.24sata.hr/feeds/show.xml", false, null, 1, 1, 1, "//img[contains(@class, 'article__figure_img')]", null },
                    { 6, 1, 2, "https://www.24sata.hr/feeds/news.xml", false, null, 1, 1, 1, "//img[contains(@class, 'article__figure_img')]", null },
                    { 5, 8, 1, "https://www.index.hr/rss/auto", true, "https://amp.index.hr/article/{0}{3}", 1, 1, 1, "//img[contains(@class, 'img-large loaded')]", null },
                    { 4, 4, 1, "https://www.index.hr/rss/rouge", true, "https://amp.index.hr/article/{0}{3}", 1, 1, 1, "//img[contains(@class, 'img-large loaded')]", null },
                    { 3, 3, 1, "https://www.index.hr/rss/magazin", true, "https://amp.index.hr/article/{0}{3}", 1, 1, 1, "//img[contains(@class, 'img-large loaded')]", null },
                    { 2, 2, 1, "https://www.index.hr/rss/sport", true, "https://amp.index.hr/article/{0}{3}", 1, 1, 1, "//img[contains(@class, 'img-large loaded')]", null }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object?[] { 17, 3, 6, "https://www.slobodnadalmacija.hr/feed/category/262", 1, 1, 1, "//img[contains(@class, 'card__image')]", null, 0, 5 });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames" },
                values: new object?[] { 82, 1, 36, "https://www.scena.hr/feed/", 2, 1, 1, "//div[contains(@class, 'mycontent')]//img", null });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object?[,]
                {
                    { 18, 3, 6, "https://www.slobodnadalmacija.hr/feed/category/375", 1, 1, 1, "//img[contains(@class, 'card__image')]", null, 0, 5 },
                    { 20, 4, 6, "https://www.slobodnadalmacija.hr/feed/category/264", 1, 1, 1, "//img[contains(@class, 'card__image')]", null, 0, 5 }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames" },
                values: new object?[,]
                {
                    { 35, 6, 7, "https://www.tportal.hr/rss-funbox.xml", 1, 1, 1, "//img[contains(@class, 'lateImage lateImageLoaded')]", null },
                    { 34, 4, 7, "https://www.tportal.hr/rss-lifestyle.xml", 1, 1, 1, "//img[contains(@class, 'lateImage lateImageLoaded')]", null },
                    { 33, 3, 7, "https://www.tportal.hr/rss-showtime.xml", 1, 1, 1, "//img[contains(@class, 'lateImage lateImageLoaded')]", null },
                    { 32, 5, 7, "https://www.tportal.hr/rss-tehno.xml", 1, 1, 1, "//img[contains(@class, 'lateImage lateImageLoaded')]", null },
                    { 31, 2, 7, "https://www.tportal.hr/rss-sport.xml", 1, 1, 1, "//img[contains(@class, 'lateImage lateImageLoaded')]", null },
                    { 30, 1, 7, "https://www.tportal.hr/rss-biznis.xml", 1, 1, 1, "//img[contains(@class, 'lateImage lateImageLoaded')]", null },
                    { 29, 1, 7, "https://www.tportal.hr/rss-vijesti.xml", 1, 1, 1, "//img[contains(@class, 'lateImage lateImageLoaded')]", null }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object?[,]
                {
                    { 28, 6, 6, "https://www.slobodnadalmacija.hr/feed/category/274", 1, 1, 1, "//img[contains(@class, 'card__image')]", null, 0, 5 },
                    { 27, 5, 6, "https://www.slobodnadalmacija.hr/feed/category/269", 1, 1, 1, "//img[contains(@class, 'card__image')]", null, 0, 5 },
                    { 26, 4, 6, "https://www.slobodnadalmacija.hr/feed/category/271", 1, 1, 1, "//img[contains(@class, 'card__image')]", null, 0, 5 },
                    { 25, 4, 6, "https://www.slobodnadalmacija.hr/feed/category/270", 1, 1, 1, "//img[contains(@class, 'card__image')]", null, 0, 5 },
                    { 24, 4, 6, "https://www.slobodnadalmacija.hr/feed/category/268", 1, 1, 1, "//img[contains(@class, 'card__image')]", null, 0, 5 },
                    { 23, 4, 6, "https://www.slobodnadalmacija.hr/feed/category/267", 1, 1, 1, "//img[contains(@class, 'card__image')]", null, 0, 5 },
                    { 22, 4, 6, "https://www.slobodnadalmacija.hr/feed/category/266", 1, 1, 1, "//img[contains(@class, 'card__image')]", null, 0, 5 },
                    { 21, 4, 6, "https://www.slobodnadalmacija.hr/feed/category/265", 1, 1, 1, "//img[contains(@class, 'card__image')]", null, 0, 5 },
                    { 19, 3, 6, "https://www.slobodnadalmacija.hr/feed/category/263", 1, 1, 1, "//img[contains(@class, 'card__image')]", null, 0, 5 }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames" },
                values: new object?[] { 83, 12, 37, "https://www.dalmacijadanas.hr/feed/", 1, 1, 1, "//div[contains(@class, 'td-full-screen-header-image-wrap')]//img", null });

            migrationBuilder.InsertData(
                table: "RssFeedCategory",
                columns: new[] { "Id", "CategoryId", "RssFeedId", "UrlRegex", "UrlSegmentIndex" },
                values: new object?[,]
                {
                    { 1, 1, 13, "vijesti", 1 },
                    { 33, 1, 66, "Hrvatska", 1 },
                    { 61, 1, 65, "Video", 1 },
                    { 60, 1, 65, "Crna-Kronika", 1 },
                    { 32, 9, 65, "Kultura", 1 },
                    { 31, 5, 65, "Tehnologija", 1 },
                    { 30, 3, 65, "Showbiz", 1 },
                    { 29, 2, 65, "Sport-Klub", 1 },
                    { 28, 4, 65, "Zdravlje", 1 },
                    { 27, 4, 65, "Lifestyle", 1 },
                    { 26, 7, 65, "Biznis", 1 },
                    { 25, 1, 65, "Info", 1 },
                    { 24, 1, 65, "Dnevnik", 1 },
                    { 23, 1, 65, "Regija", 1 },
                    { 22, 1, 65, "Znanost", 1 },
                    { 21, 1, 65, "Svijet", 1 },
                    { 34, 2, 66, "Sport", 1 },
                    { 20, 1, 65, "Vijesti", 1 },
                    { 35, 9, 66, "Kultura", 1 },
                    { 64, 1, 66, "svijet", 1 },
                    { 74, 9, 82, "kultura", 1 },
                    { 73, 3, 82, "vijesti", 1 },
                    { 72, 4, 82, "lifestyle", 1 },
                    { 66, 8, 72, "auto-moto", 1 },
                    { 65, 1, 72, "korona-virus", 1 },
                    { 44, 4, 72, "zdravlje", 1 },
                    { 43, 3, 72, "magazin", 1 },
                    { 42, 1, 72, "domovina", 1 },
                    { 41, 2, 72, "sport", 1 },
                    { 40, 1, 72, "vijesti", 1 },
                    { 39, 4, 71, "bubble", 1 },
                    { 38, 3, 71, "scena", 1 },
                    { 37, 1, 71, "news", 1 },
                    { 36, 4, 71, "zivot", 1 },
                    { 70, 1, 66, "svijet", 1 },
                    { 63, 1, 66, "koronovirus", 1 },
                    { 75, 3, 82, "televizija", 1 },
                    { 62, 3, 42, "showbuzz", 1 },
                    { 71, 2, 39, "na-prvu", 1 },
                    { 51, 5, 14, "znanost", 1 },
                    { 50, 7, 14, "novac", 1 },
                    { 49, 9, 14, "kultura", 1 },
                    { 48, 1, 14, "svijet", 2 },
                    { 47, 1, 14, "crna-kronika", 2 },
                    { 46, 1, 14, "hrvatska", 2 },
                    { 68, 3, 13, "spektakli", 1 },
                    { 67, 3, 13, "scena", 1 },
                    { 17, 4, 13, "life", 1 },
                    { 16, 3, 13, "spektakli", 1 },
                    { 15, 6, 13, "viral", 1 },
                    { 14, 4, 13, "domidizajn", 1 },
                    { 13, 1, 13, "globus", 1 },
                    { 3, 9, 13, "kultura", 1 },
                    { 2, 8, 13, "autoklub", 1 },
                    { 52, 2, 14, "sport", 1 },
                    { 12, 1, 42, "vijesti", 1 },
                    { 53, 6, 14, "vic-dana", 1 },
                    { 55, 6, 14, "fora-dana", 1 },
                    { 69, 9, 39, "kultura", 1 },
                    { 19, 4, 39, "zivot", 1 },
                    { 18, 1, 39, "politika-kriminal", 1 },
                    { 11, 9, 37, "kultura", 1 },
                    { 10, 8, 37, "automoto", 1 },
                    { 9, 5, 37, "techsci", 1 },
                    { 8, 7, 37, "biznis", 1 },
                    { 7, 4, 37, "lifestyle", 1 },
                    { 6, 3, 37, "showbiz", 1 },
                    { 5, 2, 37, "sport", 1 },
                    { 4, 1, 37, "vijesti", 1 },
                    { 59, 8, 14, "auto", 1 },
                    { 58, 5, 14, "tehnoklik", 1 },
                    { 57, 4, 14, "magazin", 1 },
                    { 56, 3, 14, "hot", 1 },
                    { 54, 5, 14, "planet-x", 1 },
                    { 76, 4, 82, "dogadjanja", 1 }
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
                name: "IX_NewsPortals_CategoryId",
                table: "NewsPortals",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsPortals_RegionId",
                table: "NewsPortals",
                column: "RegionId");

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
                name: "PushNotifications");

            migrationBuilder.DropTable(
                name: "RssFeedCategory");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "RssFeeds");

            migrationBuilder.DropTable(
                name: "NewsPortals");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
