// 20220319141439_InitialCreate.cs
//
// © 2022 Espresso News. All rights reserved.

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable
#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
#pragma warning disable CA1861 // Avoid constant arrays as arguments

namespace Espresso.Persistence.EspressoDatabaseMigrations;

public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.CreateTable(
            name: "ApplicationDownload",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                WebApiVersion = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                MobileAppVersion = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                DownloadedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                MobileDeviceType = table.Column<int>(type: "integer", nullable: false),
            },
            constraints: table => _ = table.PrimaryKey("PK_ApplicationDownload", x => x.Id));

        _ = migrationBuilder.CreateTable(
            name: "Categories",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                Color = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                KeyWordsRegexPattern = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                SortIndex = table.Column<int>(type: "integer", nullable: true),
                Position = table.Column<int>(type: "integer", nullable: true),
                CategoryType = table.Column<int>(type: "integer", nullable: false),
                Url = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
            },
            constraints: table => _ = table.PrimaryKey("PK_Categories", x => x.Id));

        _ = migrationBuilder.CreateTable(
            name: "PushNotifications",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                InternalName = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                Title = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                Message = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                Topic = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                ArticleUrl = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: false),
                IsSoundEnabled = table.Column<bool>(type: "boolean", nullable: false),
                CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
            },
            constraints: table => _ = table.PrimaryKey("PK_PushNotifications", x => x.Id));

        _ = migrationBuilder.CreateTable(
            name: "Regions",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                Subtitle = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
            },
            constraints: table => _ = table.PrimaryKey("PK_Regions", x => x.Id));

        _ = migrationBuilder.CreateTable(
            name: "Settings",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                SettingsRevision = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                ArticleSetting_MaxAgeOfTrendingArticleInMiliseconds = table.Column<long>(type: "bigint", nullable: false),
                ArticleSetting_MaxAgeOfFeaturedArticleInMiliseconds = table.Column<long>(type: "bigint", nullable: false),
                ArticleSetting_MaxAgeOfArticleInMiliseconds = table.Column<long>(type: "bigint", nullable: false),
                ArticleSetting_FeaturedArticlesTake = table.Column<int>(type: "integer", nullable: false),
                NewsPortalSetting_MaxAgeOfNewNewsPortalInMiliseconds = table.Column<long>(type: "bigint", nullable: false),
                NewsPortalSetting_NewNewsPortalsPosition = table.Column<int>(type: "integer", nullable: false),
                JobsSetting_AnalyticsCronExpression = table.Column<string>(type: "text", nullable: false),
                JobsSetting_WebApiReportCronExpression = table.Column<string>(type: "text", nullable: false),
                JobsSetting_ParseArticlesCronExpression = table.Column<string>(type: "text", nullable: false),
                SimilarArticleSetting_SimilarityScoreThreshold = table.Column<double>(type: "double precision", nullable: false),
                SimilarArticleSetting_ArticlePublishDateTimeDifferenceThreshol = table.Column<long>(type: "bigint", name: "SimilarArticleSetting_ArticlePublishDateTimeDifferenceThreshol~", nullable: false),
                SimilarArticleSetting_MaxAgeOfSimilarArticleCheckingInMiliseco = table.Column<long>(type: "bigint", name: "SimilarArticleSetting_MaxAgeOfSimilarArticleCheckingInMiliseco~", nullable: false),
                SimilarArticleSetting_MinimalNumberOfWordsForArticleToBeCompar = table.Column<int>(type: "integer", name: "SimilarArticleSetting_MinimalNumberOfWordsForArticleToBeCompar~", nullable: false),
            },
            constraints: table => _ = table.PrimaryKey("PK_Settings", x => x.Id));

        _ = migrationBuilder.CreateTable(
            name: "NewsPortals",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                BaseUrl = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                IconUrl = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                IsNewOverride = table.Column<bool>(type: "boolean", nullable: true),
                CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                IsEnabled = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                RegionId = table.Column<int>(type: "integer", nullable: false),
                CategoryId = table.Column<int>(type: "integer", nullable: false),
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_NewsPortals", x => x.Id);
                _ = table.ForeignKey(
                    name: "FK_NewsPortals_Categories_CategoryId",
                    column: x => x.CategoryId,
                    principalTable: "Categories",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                _ = table.ForeignKey(
                    name: "FK_NewsPortals_Regions_RegionId",
                    column: x => x.RegionId,
                    principalTable: "Regions",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        _ = migrationBuilder.CreateTable(
            name: "RssFeeds",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Url = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                RequestType = table.Column<int>(type: "integer", nullable: false),
                AmpConfiguration_HasAmpArticles = table.Column<bool>(type: "boolean", nullable: true),
                AmpConfiguration_TemplateUrl = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                CategoryParseConfiguration_CategoryParseStrategy = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                ImageUrlParseConfiguration_ImageUrlParseStrategy = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                ImageUrlParseConfiguration_XPath = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false, defaultValue: string.Empty),
                ImageUrlParseConfiguration_AttributeName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, defaultValue: "src"),
                ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped = table.Column<bool>(type: "boolean", nullable: true),
                ImageUrlParseConfiguration_ImageUrlWebScrapeType = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                ImageUrlParseConfiguration_JsonWebScrapePropertyNames = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                ImageUrlParseConfiguration_ElementExtensionIndex = table.Column<int>(type: "integer", nullable: true),
                ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute = table.Column<bool>(type: "boolean", nullable: true),
                SkipParseConfiguration_NumberOfSkips = table.Column<int>(type: "integer", nullable: true),
                SkipParseConfiguration_CurrentSkip = table.Column<int>(type: "integer", nullable: true),
                NewsPortalId = table.Column<int>(type: "integer", nullable: false),
                CategoryId = table.Column<int>(type: "integer", nullable: false),
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_RssFeeds", x => x.Id);
                _ = table.ForeignKey(
                    name: "FK_RssFeeds_Categories_CategoryId",
                    column: x => x.CategoryId,
                    principalTable: "Categories",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                _ = table.ForeignKey(
                    name: "FK_RssFeeds_NewsPortals_NewsPortalId",
                    column: x => x.NewsPortalId,
                    principalTable: "NewsPortals",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        _ = migrationBuilder.CreateTable(
            name: "Articles",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                WebUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                Summary = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                Title = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                ImageUrl = table.Column<string>(type: "text", nullable: true),
                CreateDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                UpdateDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                PublishDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                NumberOfClicks = table.Column<int>(type: "integer", nullable: false),
                TrendingScore = table.Column<decimal>(type: "numeric", nullable: false),
                EditorConfiguration_IsHidden = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                EditorConfiguration_IsFeatured = table.Column<bool>(type: "boolean", nullable: true),
                EditorConfiguration_FeaturedPosition = table.Column<int>(type: "integer", nullable: true),
                NewsPortalId = table.Column<int>(type: "integer", nullable: false),
                RssFeedId = table.Column<int>(type: "integer", nullable: false),
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_Articles", x => x.Id);
                _ = table.ForeignKey(
                    name: "FK_Articles_NewsPortals_NewsPortalId",
                    column: x => x.NewsPortalId,
                    principalTable: "NewsPortals",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                _ = table.ForeignKey(
                    name: "FK_Articles_RssFeeds_RssFeedId",
                    column: x => x.RssFeedId,
                    principalTable: "RssFeeds",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        _ = migrationBuilder.CreateTable(
            name: "RssFeedCategory",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                UrlRegex = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                UrlSegmentIndex = table.Column<int>(type: "integer", nullable: false),
                CategoryId = table.Column<int>(type: "integer", nullable: false),
                RssFeedId = table.Column<int>(type: "integer", nullable: false),
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_RssFeedCategory", x => x.Id);
                _ = table.ForeignKey(
                    name: "FK_RssFeedCategory_Categories_CategoryId",
                    column: x => x.CategoryId,
                    principalTable: "Categories",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                _ = table.ForeignKey(
                    name: "FK_RssFeedCategory_RssFeeds_RssFeedId",
                    column: x => x.RssFeedId,
                    principalTable: "RssFeeds",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        _ = migrationBuilder.CreateTable(
            name: "RssFeedContentModifier",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                SourceValue = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                ReplacementValue = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                OrderIndex = table.Column<int>(type: "integer", nullable: false),
                RssFeedId = table.Column<int>(type: "integer", nullable: false),
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_RssFeedContentModifier", x => x.Id);
                _ = table.ForeignKey(
                    name: "FK_RssFeedContentModifier_RssFeeds_RssFeedId",
                    column: x => x.RssFeedId,
                    principalTable: "RssFeeds",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        _ = migrationBuilder.CreateTable(
            name: "ArticleCategories",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                ArticleId = table.Column<Guid>(type: "uuid", nullable: false),
                CategoryId = table.Column<int>(type: "integer", nullable: false),
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_ArticleCategories", x => x.Id);
                _ = table.ForeignKey(
                    name: "FK_ArticleCategories_Articles_ArticleId",
                    column: x => x.ArticleId,
                    principalTable: "Articles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                _ = table.ForeignKey(
                    name: "FK_ArticleCategories_Categories_CategoryId",
                    column: x => x.CategoryId,
                    principalTable: "Categories",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        _ = migrationBuilder.CreateTable(
            name: "SimilarArticles",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                SimilarityScore = table.Column<double>(type: "double precision", nullable: false),
                FirstArticleId = table.Column<Guid>(type: "uuid", nullable: false),
                SecondArticleId = table.Column<Guid>(type: "uuid", nullable: false),
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_SimilarArticles", x => x.Id);
                _ = table.ForeignKey(
                    name: "FK_SimilarArticles_Articles_FirstArticleId",
                    column: x => x.FirstArticleId,
                    principalTable: "Articles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                _ = table.ForeignKey(
                    name: "FK_SimilarArticles_Articles_SecondArticleId",
                    column: x => x.SecondArticleId,
                    principalTable: "Articles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        _ = migrationBuilder.InsertData(
            table: "Categories",
            columns: ["Id", "CategoryType", "Color", "KeyWordsRegexPattern", "Name", "Position", "SortIndex", "Url"],
            values: new object[,]
            {
                { 1, 1, "#E84855", null, "Vijesti", null, 2, "/vijesti" },
                { 2, 1, "#4CB944", null, "Sport", null, 3, "/sport" },
                { 3, 1, "#F4B100", null, "Show", null, 4, "/show" },
                { 4, 1, "#32936F", null, "Lifestyle", null, 5, "/lifestyle" },
                { 5, 1, "#2E86AB", null, "Tech", null, 6, "/tech" },
                { 6, 1, "#9055A2", null, "Viral", null, 7, "/viral" },
                { 7, 1, "#3185FC", null, "Biznis", null, 8, "/biznis" },
                { 8, 1, "#FC814A", null, "Auto/Moto", null, 9, "/auto-moto" },
                { 9, 1, "#AC80A0", null, "Kultura", null, 10, "/kultura" },
                { 11, 3, "#AC80A0", null, "Generalno", null, 1, "/general" },
                { 12, 2, "#AC80A0", null, "Lokalno", 3, null, "/local" },
            });

        _ = migrationBuilder.InsertData(
            table: "Regions",
            columns: ["Id", "Name", "Subtitle"],
            values: new object[,]
            {
                { 1, "Global", "Global" },
                { 2, "Dalmacija", "Split, Zadar, Dubrovnik, Šibenik, Kaštela, Imotski..." },
                { 3, "Istra & Kvarner", "Rijeka, Pula, Opatija, Pazin, Umag, Poreč, Rovinj..." },
                { 4, "Lika", "Lokalne vijesti iz Ličko-Senjske županije" },
                { 5, "Zagreb i okolica", "Lokalne vijesti iz grada Zagreba i okolice" },
                { 6, "Sjeverna Hrvatska", "Međimurje, Podravina, Sisak, Zagorje..." },
                { 7, "Slavonija & Baranja", "Osijek, Vinkovci, Slavonski Brod, Vukovar, Požega..." },
            });

        _ = migrationBuilder.InsertData(
            table: "NewsPortals",
            columns: ["Id", "BaseUrl", "CategoryId", "CreatedAt", "IconUrl", "IsEnabled", "IsNewOverride", "Name", "RegionId"],
            values: new object[,]
            {
                { 1, "https://www.index.hr", 11, new DateTimeOffset(new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/Index.png", true, null, "Index.hr", 1 },
                { 2, "https://www.24sata.hr", 11, new DateTimeOffset(new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/DvadesetCetiriSata.png", true, null, "24 sata", 1 },
                { 3, "https://sportske.jutarnji.hr", 2, new DateTimeOffset(new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/SportskeNovosti.png", true, null, "Sportske Novosti", 1 },
                { 4, "https://sportske.jutarnji.hr", 11, new DateTimeOffset(new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/JutarnjiList.png", true, null, "Jutarnji List", 1 },
                { 5, "https://net.hr", 11, new DateTimeOffset(new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/NetHr.png", true, null, "Net.hr", 1 },
                { 6, "https://slobodnadalmacija.hr", 11, new DateTimeOffset(new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/SlobodnaDalmacija.png", true, null, "Slobodna Dalmacija", 1 },
                { 7, "https://www.tportal.hr", 11, new DateTimeOffset(new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/TPortal.png", true, null, "tportal", 1 },
                { 8, "https://www.vecernji.hr", 11, new DateTimeOffset(new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/VecernjiList.png", true, null, "Večernji List", 1 },
                { 9, "https://www.telegram.hr", 11, new DateTimeOffset(new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/Telegram.png", true, null, "Telegram", 1 },
                { 10, "https://dnevnik.hr", 11, new DateTimeOffset(new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/Dnevnik.png", true, null, "Dnevnik", 1 },
                { 11, "https://gol.dnevnik.hr", 2, new DateTimeOffset(new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/Gol.png", true, null, "Gol", 1 },
                { 12, "https://sportnet.rtl.hr", 11, new DateTimeOffset(new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/RtlVijesti.png", true, null, "RTL Vijesti", 1 },
                { 15, "http://www.nogometplus.net", 2, new DateTimeOffset(new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/NogometPlus.png", true, null, "Nogomet Plus", 1 },
                { 16, "https://lider.media", 7, new DateTimeOffset(new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/Lider.png", true, null, "Lider", 1 },
                { 17, "https://www.poslovni.hr", 7, new DateTimeOffset(new DateTime(2021, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/PoslovniDnevnik.png", true, null, "Poslovni dnevnik", 1 },
                { 18, "https://www.bug.hr", 5, new DateTimeOffset(new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/Bug.png", true, null, "Bug", 1 },
                { 19, "https://www.vidi.hr", 5, new DateTimeOffset(new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/VidiHr.png", true, null, "Vidi.hr", 1 },
                { 20, "https://zimo.dnevnik.hr", 5, new DateTimeOffset(new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/Zimo.png", true, null, "Zimo", 1 },
                { 21, "https://www.netokracija.com", 5, new DateTimeOffset(new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/Netokracija.png", true, null, "Netokracija", 1 },
                { 22, "https://poslovnipuls.com", 7, new DateTimeOffset(new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/PoslovniPuls.png", true, null, "Poslovni Puls", 1 },
                { 23, "https://pcchip.hr", 5, new DateTimeOffset(new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/PcChip.png", true, null, "PCchip", 1 },
                { 25, "http://www.cosmopolitan.hr", 4, new DateTimeOffset(new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/Cosmopolitan.png", true, null, "Cosmopolitan", 1 },
                { 26, "https://wall.hr", 4, new DateTimeOffset(new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/WallHr.png", true, null, "Wall.hr", 1 },
                { 27, "http://www.ljepotaizdravlje.hr", 4, new DateTimeOffset(new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/LjepotaIZdravlje.png", true, null, "Ljepota i zdravlje", 1 },
                { 28, "https://www.autonet.hr", 8, new DateTimeOffset(new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/Autonet.png", true, null, "Autonet", 1 },
                { 29, "https://hr.n1info.com", 1, new DateTimeOffset(new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/N1.png", true, null, "N1", 1 },
                { 30, "https://narod.hr", 1, new DateTimeOffset(new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/NarodHr.png", true, null, "Narod HR", 1 },
                { 31, "https://www.hrt.hr", 1, new DateTimeOffset(new DateTime(2020, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/Hrt.png", true, null, "HRT", 1 },
                { 32, "https://100posto.jutarnji.hr", 1, new DateTimeOffset(new DateTime(2020, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/StoPosto.png", true, null, "100posto", 1 },
                { 33, "https://www.dnevno.hr", 11, new DateTimeOffset(new DateTime(2020, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/Dnevno.png", true, null, "Dnevno.Hr", 1 },
                { 35, "https://direktno.hr", 1, new DateTimeOffset(new DateTime(2020, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/DirektnoHr.png", true, null, "Direktno", 1 },
                { 36, "https://www.scena.hr", 3, new DateTimeOffset(new DateTime(2020, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/Scena.png", true, null, "Scena", 1 },
                { 37, "https://www.dalmacijadanas.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/DalmacijaDanas.png", true, null, "Dalmacija Danas", 2 },
                { 38, "https://www.nacional.hr", 1, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/Nacional.png", true, null, "Nacional", 1 },
                { 39, "https://express.24sata.hr", 1, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/Express.png", true, null, "Express", 1 },
                { 40, "https://www.dalmacijanews.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/DalmacijaNews.png", true, null, "Dalmacija News", 2 },
                { 41, "https://dalmatinskiportal.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/DalmatinskiPortal.png", true, null, "Dalmatinski Portal", 2 },
                { 44, "https://www.novilist.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/NoviList.png", true, null, "Novi list", 3 },
                { 45, "https://www.parentium.com", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/Parentium.png", true, null, "Parentium", 3 },
                { 46, "https://likaclub.eu", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/LikaKlub.png", true, null, "Lika Klub", 4 },
                { 47, "http://www.lika-express.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/LikaExpress.png", true, null, "Lika express", 4 },
                { 48, "https://www.lika-online.com", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/LikaOnline.png", true, null, "Lika Online", 4 },
                { 49, "http://www.likaplus.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/LikaPlus.png", true, null, "Lika plus", 4 },
                { 50, "https://www.index.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/IndexHrZagreb.png", true, null, "Index.Hr - Zagreb", 5 },
                { 51, "https://www.zagreb.info", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/ZagrebInfo.png", true, null, "Zagreb Info", 5 },
                { 52, "https://www.zagrebancija.com", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/Zagrebancija.png", true, null, "Zagrebancija", 5 },
                { 53, "https://sjever.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/SjeverHr.png", true, null, "Sjever.hr", 6 },
                { 54, "https://prigorski.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/PrigorskiHr.png", true, null, "Prigorski.hr", 6 },
                { 55, "https://epodravina.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/PodravinaHr.png", true, null, "ePodravina.hr", 6 },
                { 56, "https://www.baranjainfo.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/BaranjaInfo.png", true, null, "Baranja info", 7 },
                { 57, "https://www.glas-slavonije.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/GlasSlavonije.png", true, null, "Glas Slavonije", 7 },
                { 58, "https://slavonski.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/SlavonskiHr.png", true, null, "Slavonski Hr", 7 },
                { 59, "https://ivijesti.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/IVijesti.png", true, null, "iVijesti", 3 },
                { 60, "https://dubrovackidnevnik.net.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/DubrovackiDnevnik.png", true, null, "Dubrovački Dnevnik.hr", 2 },
                { 61, "http://www.istra-istria.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/IstarskaZupanija.png", true, null, "Istarska Županija", 3 },
                { 62, "https://www.zagrebonline.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/ZagrebOnline.png", true, null, "Zagreb Online", 5 },
                { 63, "https://www.sisak.info", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/SisakInfo.png", true, null, "Sisak.Info", 6 },
                { 64, "https://osijeknews.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/OsijekNews.png", true, null, "Osijek NEWS", 7 },
                { 65, "https://slobodnadalmacija.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/SlobodnaDalmacija_Dalmacija.png", true, null, "Slobodna Dalmacija - Dalmacija", 2 },
                { 66, "https://slobodnadalmacija.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/SlobodnaDalmacija_Split.png", true, null, "Slobodna Dalmacija - Split", 2 },
                { 67, "https://www.dubrovniknet.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/DubrovnikNet.png", true, null, "Dubrovniknet.hr", 2 },
                { 68, "https://makarska-danas.com", 12, new DateTimeOffset(new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/MakarskaDanas.png", true, null, "MakarskaDanas", 2 },
                { 69, "https://makarska.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/MakarskaHr.png", true, null, "Makarska", 2 },
                { 70, "http://www.portaloko.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/PortalOko.png", true, null, "Portaloko.hr", 2 },
                { 71, "https://www.antenazadar.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/AntenaZadar.png", true, null, "Antena portal Zadar", 2 },
                { 72, "https://radioimotski.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/RadioImotski.png", true, null, "Radio Imotski", 2 },
                { 73, "https://imotskenovine.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/ImotskeNovine.png", true, null, "Imotske Novine", 2 },
                { 74, "http://www.kastela.org", 12, new DateTimeOffset(new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/PortalKastela.png", true, null, "Portal Grada Kaštela", 2 },
                { 75, "https://huknet1.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/HukNet.png", true, null, "Huknet", 2 },
                { 76, "https://www.zadarskilist.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/ZadarskiList.png", true, null, "Zadarski List", 2 },
                { 77, "https://www.istriaterramagica.eu", 12, new DateTimeOffset(new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/IstraTerraMagica.png", true, null, "Istra Terra Magica", 3 },
                { 78, "https://www.ipress.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/IPress.png", true, null, "iPress", 3 },
                { 80, "https://www.rijekadanas.com", 12, new DateTimeOffset(new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/RijekaDanas.png", true, null, "Rijeka Danas", 3 },
                { 81, "https://www.fiuman.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/Fiuman.png", true, null, "Fiuman", 3 },
                { 82, "https://riportal.net.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/Riportal.png", true, null, "Riportal", 3 },
                { 83, "https://www.gspress.net", 12, new DateTimeOffset(new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/GsPress.png", true, null, "GS Press", 4 },
                { 84, "https://www.zagrebacki.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/ZagrebackiList.png", true, null, "Zagrebački List", 5 },
                { 85, "https://www.zgportal.com", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/ZgPortal.png", true, null, "ZG portal", 5 },
                { 86, "https://www.zagreb.hr/", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/GradZagreb.png", true, null, "Grad Zagreb", 5 },
                { 87, "https://regionalni.com", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/PlusRegionalniTjednik.png", true, null, "7Plus Regionalni Tjednik", 6 },
                { 88, "https://www.glaspodravine.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/GlasPodravineIPrigorja.png", true, null, "Glas Podravine i Prigorja", 6 },
                { 89, "https://www.medjimurje.info", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/MedimurjeInfo.png", true, null, "Međimurje Info", 6 },
                { 90, "https://www.mnovine.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/MedimurskeNovine.png", true, null, "Međimurske Novine", 6 },
                { 91, "https://www.zagorje.com", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/ZagorjeCom.png", true, null, "Zagorje.com", 6 },
                { 92, "https://www.icv.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/InformativniCentarVirovitica.png", true, null, "Informativni Centar Virovitica", 7 },
                { 93, "https://www.novska.in", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/NovskaIn.png", true, null, "Novska.IN", 7 },
                { 94, "https://novosti.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/NovostiHr.png", true, null, "Novosti.hr", 7 },
                { 95, "http://portal53.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/Portal53.png", true, null, "Portal53", 7 },
                { 96, "https://sbplus.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/SbPlusHr.png", true, null, "SBplus.hr", 7 },
                { 97, "https://www.pozeska-kronika.hr", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/PozeskaKronika.png", true, null, "Požeška Kronika", 7 },
                { 98, "http://www.osijek031.com", 12, new DateTimeOffset(new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/Osijek031.png", true, null, "Osijek031", 7 },
                { 99, "https://otvoreno.hr", 1, new DateTimeOffset(new DateTime(2020, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/OtvorenoHr.png", true, null, "Otvoreno.hr", 1 },
                { 100, "https://www.geopolitika.news", 1, new DateTimeOffset(new DateTime(2020, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/GeoPolitika.png", true, null, "Geopolitika News", 1 },
                { 101, "https://povijest.hr", 1, new DateTimeOffset(new DateTime(2020, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/PovijestHr.png", true, null, "Povijest.hr", 1 },
                { 102, "https://7dnevno.hr", 1, new DateTimeOffset(new DateTime(2020, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/Dnevno7.png", true, null, "7dnevno", 1 },
                { 103, "https://basketball.hr", 2, new DateTimeOffset(new DateTime(2020, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/BasketballHr.png", true, null, "Basketball.hr", 1 },
                { 104, "https://joomboos.24sata.hr", 6, new DateTimeOffset(new DateTime(2020, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/JoomBoos.png", true, null, "JoomBoos", 1 },
                { 105, "https://www.ictbusiness.info", 5, new DateTimeOffset(new DateTime(2020, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/IctBusiness.png", true, null, "ICT Business", 1 },
                { 106, "https://www.hcl.hr", 5, new DateTimeOffset(new DateTime(2020, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/Hcl.png", true, null, "HCL Gaming Portal", 1 },
                { 107, "https://profitiraj.hr", 7, new DateTimeOffset(new DateTime(2020, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/ProfitirajHr.png", true, null, "Profitiraj.hr", 1 },
                { 108, "https://www.motori.hr/", 8, new DateTimeOffset(new DateTime(2020, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/MotoriHr.png", true, null, "motori.hr", 1 },
                { 109, "https://autoportal.hr", 8, new DateTimeOffset(new DateTime(2020, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/AutoportalHr.png", true, null, "Autoportal.hr", 1 },
                { 110, "https://www.autopress.hr", 8, new DateTimeOffset(new DateTime(2020, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/AutopressHr.png", true, null, "AutopressHR", 1 },
            });

        _ = migrationBuilder.InsertData(
            table: "NewsPortals",
            columns: ["Id", "BaseUrl", "CategoryId", "CreatedAt", "IconUrl", "IsNewOverride", "Name", "RegionId"],
            values: [111, "https://vozim.hr", 8, new DateTimeOffset(new DateTime(2020, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/VozimHr.png", null, "Vozim.HR", 1]);

        _ = migrationBuilder.InsertData(
            table: "NewsPortals",
            columns: ["Id", "BaseUrl", "CategoryId", "CreatedAt", "IconUrl", "IsEnabled", "IsNewOverride", "Name", "RegionId"],
            values: new object[,]
            {
                { 112, "https://ams.hr", 8, new DateTimeOffset(new DateTime(2020, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/AutoMotorSport.png", true, null, "AUTO MOTOR I SPORT", 1 },
                { 113, "http://hoopster.hr", 2, new DateTimeOffset(new DateTime(2020, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/Hoopster.png", true, null, "Hoopster.hr", 1 },
                { 114, "http://prvahnl.hr", 2, new DateTimeOffset(new DateTime(2020, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/PrvaHnl.png", true, null, "Hrvatski Telekom Prva liga", 1 },
            });

        _ = migrationBuilder.InsertData(
            table: "NewsPortals",
            columns: ["Id", "BaseUrl", "CategoryId", "CreatedAt", "IconUrl", "IsNewOverride", "Name", "RegionId"],
            values: [115, "http://balkans.aljazeera.net", 1, new DateTimeOffset(new DateTime(2020, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/AlJazeera.png", null, "Al Jazeera Balkans", 1]);

        _ = migrationBuilder.InsertData(
            table: "NewsPortals",
            columns: ["Id", "BaseUrl", "CategoryId", "CreatedAt", "IconUrl", "IsEnabled", "IsNewOverride", "Name", "RegionId"],
            values: new object[,]
            {
                { 116, "https://www.hifimedia.hr", 5, new DateTimeOffset(new DateTime(2020, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/HifiMedia.png", true, null, "hifimedia", 1 },
                { 117, "https://geek.hr", 5, new DateTimeOffset(new DateTime(2020, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/GeekHr.png", true, null, "Geek.hr", 1 },
                { 118, "https://vizkultura.hr", 9, new DateTimeOffset(new DateTime(2020, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/VizKultura.png", true, null, "vizkultura.hr", 1 },
                { 119, "https://zivotumjetnosti.ipu.hr", 9, new DateTimeOffset(new DateTime(2020, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/ZivotUmjetnosti.png", true, null, "Život umjetnosti", 1 },
                { 120, "https://svijetkulture.com", 9, new DateTimeOffset(new DateTime(2020, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/SvijetKulture.png", true, null, "SVIJET KULTURE", 1 },
                { 121, "http://www.gamer.hr", 5, new DateTimeOffset(new DateTime(2020, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/GamerHr.png", true, null, "Gamer.hr", 1 },
                { 122, "https://ogportal.com", 12, new DateTimeOffset(new DateTime(2020, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/OgPortal.png", true, null, "OG Portal", 4 },
                { 123, "https://zupanjac.net", 12, new DateTimeOffset(new DateTime(2020, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/Zupanjac.png", true, null, "Županjac.net", 7 },
                { 124, "https://press032.com", 12, new DateTimeOffset(new DateTime(2021, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/Press032.png", true, null, "PRESS 032", 7 },
                { 125, "https://www.bitno.net", 1, new DateTimeOffset(new DateTime(2021, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/BitnoNet.png", true, null, "Bitno.net", 1 },
                { 126, "https://ploce.com.hr", 12, new DateTimeOffset(new DateTime(2021, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/PloceOnline.png", true, null, "Ploče Online", 2 },
                { 127, "https://kaportal.net.hr", 12, new DateTimeOffset(new DateTime(2021, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/KaPortalHr.png", true, null, "KAportal.hr", 5 },
                { 128, "https://radio-mreznica.hr", 12, new DateTimeOffset(new DateTime(2021, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/RadioMreznica.png", true, null, "Radio Mrežnica", 5 },
                { 129, "https://www.maxportal.hr", 1, new DateTimeOffset(new DateTime(2021, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/MaxPortal.png", true, null, "Maxportal", 1 },
                { 130, "https://www.gp1.hr", 8, new DateTimeOffset(new DateTime(2021, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/Gp1.png", true, null, "GP1", 1 },
                { 131, "https://f1.pulsmedia.hr", 8, new DateTimeOffset(new DateTime(2021, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/F1PulsMedia.png", true, null, "F1.pulsmedia.hr", 1 },
                { 132, "https://www.racunalo.com", 5, new DateTimeOffset(new DateTime(2021, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/Racunalo.png", true, null, "Racunalo.com", 1 },
                { 133, "https://mob.hr", 5, new DateTimeOffset(new DateTime(2021, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/MobHr.png", true, null, "mob.hr", 1 },
                { 134, "https://www.startnews.hr", 1, new DateTimeOffset(new DateTime(2021, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/StartNews.png", true, null, "Startnews", 1 },
                { 135, "https://viral.hr", 6, new DateTimeOffset(new DateTime(2021, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/Viral.png", true, null, "Viral.hr", 1 },
                { 136, "https://sportklub.hr", 2, new DateTimeOffset(new DateTime(2021, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/SportKlub.png", true, null, "Sportklub", 1 },
                { 137, "https://doktorehitno.hr", 4, new DateTimeOffset(new DateTime(2021, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Icons/DoktoreHitno.png", true, null, "Doktore, hitno!", 1 },
            });

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath"],
            values: new object[,]
            {
                { 1, 1, 1, 1, "https://www.index.hr/rss/vijesti", true, "https://www.index.hr/mobile/clanak.aspx?id={0}", null, 1, null, null, null, "//figure[contains(@class, 'img-container')]//img" },
                { 2, 2, 1, 1, "https://www.index.hr/rss/sport", true, "https://www.index.hr/mobile/clanak.aspx?id={0}", null, 1, null, null, null, "//figure[contains(@class, 'img-container')]//img" },
                { 3, 3, 1, 1, "https://www.index.hr/rss/magazin", true, "https://www.index.hr/mobile/clanak.aspx?id={0}", null, 1, null, null, null, "//figure[contains(@class, 'img-container')]//img" },
                { 4, 4, 1, 1, "https://www.index.hr/rss/rouge", true, "https://www.index.hr/mobile/clanak.aspx?id={0}", null, 1, null, null, null, "//figure[contains(@class, 'img-container')]//img" },
                { 5, 8, 1, 1, "https://www.index.hr/rss/auto", true, "https://www.index.hr/mobile/clanak.aspx?id={0}", null, 1, null, null, null, "//figure[contains(@class, 'img-container')]//img" },
            });

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath"],
            values: new object[,]
            {
                { 6, 1, 2, 1, "https://www.24sata.hr/feeds/news.xml", false, null, null, null, null, null, "//img[contains(@class, 'article__figure_img')]" },
                { 7, 3, 2, 1, "https://www.24sata.hr/feeds/show.xml", false, null, null, null, null, null, "//img[contains(@class, 'article__figure_img')]" },
                { 8, 2, 2, 1, "https://www.24sata.hr/feeds/sport.xml", false, null, null, null, null, null, "//img[contains(@class, 'article__figure_img')]" },
                { 9, 4, 2, 1, "https://www.24sata.hr/feeds/lifestyle.xml", false, null, null, null, null, null, "//img[contains(@class, 'article__figure_img')]" },
                { 10, 5, 2, 1, "https://www.24sata.hr/feeds/tech.xml", false, null, null, null, null, null, "//img[contains(@class, 'article__figure_img')]" },
                { 11, 6, 2, 1, "https://www.24sata.hr/feeds/fun.xml", false, null, null, null, null, null, "//img[contains(@class, 'article__figure_img')]" },
            });

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath"],
            values: [12, 2, 3, 2, "http://sportske.jutarnji.hr/sn/feed", null, null, null, null, "//img[contains(@class, 'media-object adaptive lazy')]"]);

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips"],
            values: [13, 1, 4, 2, "https://www.jutarnji.hr/feed", 2, null, null, null, null, "//img[contains(@class, 'media-object adaptive lazy')]", 0, 5]);

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath"],
            values: [14, 1, 5, 1, "https://net.hr/feed", false, null, 2, null, null, null, null, "//div[contains(@class, 'featured-img')]//img"]);

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips"],
            values: new object[,]
            {
                { 15, 1, 6, 2, "https://www.slobodnadalmacija.hr/feed/category/119", null, null, null, null, "//img[contains(@class, 'card__image')]", 0, 5 },
                { 16, 2, 6, 2, "https://www.slobodnadalmacija.hr/feed/category/255", null, null, null, null, "//img[contains(@class, 'card__image')]", 0, 5 },
                { 17, 3, 6, 2, "https://www.slobodnadalmacija.hr/feed/category/262", null, null, null, null, "//img[contains(@class, 'card__image')]", 0, 5 },
                { 18, 3, 6, 2, "https://www.slobodnadalmacija.hr/feed/category/375", null, null, null, null, "//img[contains(@class, 'card__image')]", 0, 5 },
                { 19, 3, 6, 2, "https://www.slobodnadalmacija.hr/feed/category/263", null, null, null, null, "//img[contains(@class, 'card__image')]", 0, 5 },
                { 20, 4, 6, 2, "https://www.slobodnadalmacija.hr/feed/category/264", null, null, null, null, "//img[contains(@class, 'card__image')]", 0, 5 },
                { 21, 4, 6, 2, "https://www.slobodnadalmacija.hr/feed/category/265", null, null, null, null, "//img[contains(@class, 'card__image')]", 0, 5 },
                { 22, 4, 6, 2, "https://www.slobodnadalmacija.hr/feed/category/266", null, null, null, null, "//img[contains(@class, 'card__image')]", 0, 5 },
                { 23, 4, 6, 2, "https://www.slobodnadalmacija.hr/feed/category/267", null, null, null, null, "//img[contains(@class, 'card__image')]", 0, 5 },
                { 24, 4, 6, 2, "https://www.slobodnadalmacija.hr/feed/category/268", null, null, null, null, "//img[contains(@class, 'card__image')]", 0, 5 },
                { 25, 4, 6, 2, "https://www.slobodnadalmacija.hr/feed/category/270", null, null, null, null, "//img[contains(@class, 'card__image')]", 0, 5 },
                { 26, 4, 6, 2, "https://www.slobodnadalmacija.hr/feed/category/271", null, null, null, null, "//img[contains(@class, 'card__image')]", 0, 5 },
                { 27, 5, 6, 2, "https://www.slobodnadalmacija.hr/feed/category/269", null, null, null, null, "//img[contains(@class, 'card__image')]", 0, 5 },
                { 28, 6, 6, 2, "https://www.slobodnadalmacija.hr/feed/category/274", null, null, null, null, "//img[contains(@class, 'card__image')]", 0, 5 },
            });

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath"],
            values: new object[,]
            {
                { 29, 1, 7, 1, "https://www.tportal.hr/rss-vijesti.xml", null, null, null, null, "//img[contains(@class, 'lateImage lateImageLoaded')]" },
                { 30, 1, 7, 1, "https://www.tportal.hr/rss-biznis.xml", null, null, null, null, "//img[contains(@class, 'lateImage lateImageLoaded')]" },
                { 31, 2, 7, 1, "https://www.tportal.hr/rss-sport.xml", null, null, null, null, "//img[contains(@class, 'lateImage lateImageLoaded')]" },
                { 32, 5, 7, 1, "https://www.tportal.hr/rss-tehno.xml", null, null, null, null, "//img[contains(@class, 'lateImage lateImageLoaded')]" },
                { 33, 3, 7, 1, "https://www.tportal.hr/rss-showtime.xml", null, null, null, null, "//img[contains(@class, 'lateImage lateImageLoaded')]" },
                { 34, 4, 7, 1, "https://www.tportal.hr/rss-Lifestyle.xml", null, null, null, null, "//img[contains(@class, 'lateImage lateImageLoaded')]" },
                { 35, 6, 7, 1, "https://www.tportal.hr/rss-funbox.xml", null, null, null, null, "//img[contains(@class, 'lateImage lateImageLoaded')]" },
                { 36, 9, 7, 1, "https://www.tportal.hr/rss-kultura.xml", null, null, null, null, "//img[contains(@class, 'lateImage lateImageLoaded')]" },
            });

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath"],
            values: [37, 1, 8, 1, "https://www.vecernji.hr/feeds/latest", true, "https://m.vecernji.hr/amp/{1}{2}", 2, null, 2, null, "image,url", true, "//script[contains(@type, 'application/ld+json')]"]);

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath"],
            values: [39, 1, 9, 1, "https://www.telegram.hr/feed/", 2, null, null, null, null, "//div[contains(@class, 'thumb')]//img"]);

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath"],
            values: [40, 2, 9, 1, "https://telesport.telegram.hr/feed/", null, null, null, null, "//div[contains(@class, 'featured-img')]//img"]);

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath"],
            values: [42, 1, 10, 1, "https://dnevnik.hr/assets/feed/articles/", true, "https://dnevnik.hr/amp/{1}{2}{3}", 2, null, null, null, null, "//figure[contains(@class, 'article-main-img')]//img"]);

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath"],
            values: [43, 2, 11, 1, "https://gol.dnevnik.hr/assets/feed/articles", true, "https://gol.dnevnik.hr/amp/{1}{2}{3}{4}", null, null, null, null, "//figure[contains(@class, 'article-image main-image')]//img"]);

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath"],
            values: new object[,]
            {
                { 44, 2, 12, 2, "https://sportnet.rtl.hr/rss/", null, null, null, null, "//img[contains(@class, 'naslovna')]" },
                { 47, 2, 15, 1, "http://www.nogometplus.net/index.php/feed/", null, null, null, null, "//div[contains(@class, 'post-img')]//img" },
                { 48, 7, 16, 1, "http://lider.media/feed/", null, null, null, null, "//img[contains(@class, 'card__image')]" },
                { 49, 7, 16, 1, "http://lider.media/feed/", null, null, null, null, "//img[contains(@class, 'card__image')]" },
                { 50, 7, 16, 1, "http://lider.media/feed/", null, null, null, null, "//img[contains(@class, 'card__image')]" },
                { 54, 5, 18, 1, "http://www.bug.hr/rss/vijesti/", null, null, null, null, "//div[contains(@class, 'entry-content')]//img" },
                { 55, 5, 19, 1, "http://www.vidi.hr/rss/feed/vidi", null, null, null, null, "//div[contains(@class, 'attribute-image')]//img" },
            });

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath"],
            values: [56, 5, 20, 1, "https://zimo.dnevnik.hr/assets/feed/articles", true, "https://zimo.dnevnik.hr/amp/clanak/{2}", null, null, null, null, "//div[contains(@class, 'img-holder')]//img"]);

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath"],
            values: [57, 5, 21, 1, "http://www.netokracija.com/feed", true, "https://www.netokracija.com/{1}/amp", null, 2, null, null, null, "//div[contains(@class, 'post__hero')]//img"]);

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips"],
            values: [58, 7, 22, 2, "http://www.poslovnipuls.com/feed/", null, null, null, null, "//div[contains(@class, 'postFeaturedImg postFeaturedImg--single')]//img", 0, 10]);

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath"],
            values: new object[,]
            {
                { 59, 5, 23, 1, "http://pcchip.hr/feed/", null, null, null, null, "//div[contains(@class, 'td-post-featured-image')]//img" },
                { 61, 4, 25, 1, "http://www.cosmopolitan.hr/feed", null, null, null, null, "//div[contains(@class, 'first-image')]//img" },
            });

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath"],
            values: [62, 4, 26, 1, "http://wall.hr/cdn/feed.xml", null, 2, null, null, null, "//figure[contains(@class, 'dcms-image article-image')]//img"]);

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath"],
            values: new object[,]
            {
                { 63, 4, 27, 1, "http://www.ljepotaizdravlje.hr/feed", null, null, null, null, "//div[contains(@class, 'post-thumbnail')]//img" },
                { 64, 8, 28, 1, "https://www.autonet.hr/feed/", null, null, null, null, "//figure[contains(@class, 'figure')]//img" },
            });

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath"],
            values: [65, 1, 29, 2, "http://hr.n1info.com/rss/249/Naslovna", 2, 0, 3, false, null, null, "//figure[contains(@class, 'media')]//img"]);

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath"],
            values: [66, 1, 30, 2, "https://narod.hr/feed", 2, null, null, null, null, "//div[contains(@class, 'td-post-featured-image')]//img"]);

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips"],
            values: new object[,]
            {
                { 67, 1, 31, 1, "https://www.hrt.hr/rss/vijesti/", null, null, null, null, "//div[contains(@class, 'image-slider')]//img", 0, 5 },
                { 68, 2, 31, 1, "https://www.hrt.hr/rss/sport/", null, null, null, null, "//div[contains(@class, 'image-slider')]//img", 0, 5 },
                { 69, 3, 31, 1, "https://magazin.hrt.hr/feed.xml", null, null, null, null, "//div[contains(@class, 'image-slider')]//img", 0, 5 },
                { 70, 3, 31, 1, "https://www.hrt.hr/rss/glazba/", null, null, null, null, "//div[contains(@class, 'image-slider')]//img", 0, 5 },
            });

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips"],
            values: [71, 1, 32, 1, "https://100posto.jutarnji.hr/rss", 2, null, null, null, null, "//picture[contains(@class, 'pic')]//img", 0, 5]);

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath"],
            values: [72, 1, 33, 1, "https://www.dnevno.hr/feed/", 2, null, null, null, null, "//div[contains(@class, 'img-holder inner')]//img"]);

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath"],
            values: new object[,]
            {
                { 74, 1, 35, 1, "https://direktno.hr/rss/publish/latest/direkt-50/", null, null, null, null, "//div[contains(@class, 'pd-hero-image')]//img" },
                { 75, 1, 35, 1, "https://direktno.hr/rss/publish/latest/domovina-10/", null, null, null, null, "//div[contains(@class, 'pd-hero-image')]//img" },
                { 76, 1, 35, 1, "https://direktno.hr/rss/publish/latest/eu_svijet/", null, null, null, null, "//div[contains(@class, 'pd-hero-image')]//img" },
                { 77, 7, 35, 1, "https://direktno.hr/rss/publish/latest/razvoj-110/", null, null, null, null, "//div[contains(@class, 'pd-hero-image')]//img" },
                { 78, 2, 35, 1, "https://direktno.hr/rss/publish/latest/sport-60/", null, null, null, null, "//div[contains(@class, 'pd-hero-image')]//img" },
                { 79, 3, 35, 1, "https://direktno.hr/rss/publish/latest/zivot-70/", null, null, null, null, "//div[contains(@class, 'pd-hero-image')]//img" },
                { 80, 1, 35, 1, "https://direktno.hr/rss/publish/latest/kolumne-80/", null, null, null, null, "//div[contains(@class, 'pd-hero-image')]//img" },
                { 81, 1, 35, 1, "https://direktno.hr/rss/publish/latest/direktnotv-100/", null, null, null, null, "//div[contains(@class, 'pd-hero-image')]//img" },
            });

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips"],
            values: [82, 1, 36, 2, "https://www.scena.hr/feed/", 2, 1, 3, true, null, null, "//div[contains(@class, 'mycontent')]//img", 0, 6]);

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath"],
            values: new object[,]
            {
                { 83, 12, 37, 1, "https://www.dalmacijadanas.hr/feed/", null, null, null, null, "//div[contains(@class, 'td-full-screen-header-image-wrap')]//img" },
                { 84, 1, 38, 1, "https://www.nacional.hr/feed/", null, null, null, null, "//div[contains(@class, 'single-post-media')]//img" },
            });

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips"],
            values: [85, 1, 39, 1, "https://express.24sata.hr/feeds/placeholder-head/rss_feed", 2, null, null, null, null, "//img[contains(@class, 'article__figure_img')]", 0, 10]);

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url"],
            values: new object[,]
            {
                { 86, 12, 40, 1, "https://www.dalmacijanews.hr/rss" },
                { 87, 12, 41, 1, "https://dalmatinskiportal.hr/sadrzaj/rss/vijesti.xml" },
                { 90, 12, 44, 1, "https://www.novilist.hr/feed" },
                { 91, 12, 45, 1, "https://www.parentium.com/rssfeed.asp" },
                { 92, 12, 46, 1, "https://likaclub.eu/feed" },
                { 93, 12, 47, 2, "http://www.lika-express.hr/feed" },
                { 94, 12, 48, 1, "https://www.lika-online.com/feed" },
                { 95, 12, 49, 1, "http://www.likaplus.hr/rss" },
            });

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath"],
            values: [96, 12, 50, 1, "https://www.index.hr/rss/vijesti-zagreb", null, 2, null, null, null, "//figure[contains(@class, 'img-container')]//img"]);

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url"],
            values: new object[,]
            {
                { 97, 12, 51, 1, "https://www.zagreb.info/feed" },
                { 98, 12, 52, 1, "https://www.zagrebancija.com/feed" },
                { 99, 12, 53, 1, "https://sjever.hr/feed" },
                { 100, 12, 54, 1, "https://prigorski.hr/feed" },
                { 101, 12, 55, 1, "https://epodravina.hr/feed" },
                { 102, 12, 56, 1, "https://www.baranjainfo.hr/feed" },
                { 103, 12, 57, 1, "https://www.glas-slavonije.hr/rss" },
                { 104, 12, 58, 1, "https://slavonski.hr/feed" },
                { 105, 12, 59, 1, "https://ivijesti.hr/feed" },
                { 106, 12, 60, 1, "https://dubrovackidnevnik.net.hr/rss" },
                { 107, 12, 61, 1, "https://www.istra-istria.hr/en/articles/rss/novosti" },
                { 108, 12, 62, 1, "https://www.zagrebonline.hr/feed" },
                { 109, 12, 63, 1, "https://www.sisak.info/feed" },
                { 110, 12, 64, 1, "https://osijeknews.hr/feed" },
            });

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips"],
            values: new object[,]
            {
                { 111, 12, 65, 1, "https://slobodnadalmacija.hr/feed/category/246", 0, 5 },
                { 112, 12, 66, 1, "https://slobodnadalmacija.hr/feed/category/253", 0, 5 },
            });

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url"],
            values: new object[,]
            {
                { 113, 12, 67, 2, "https://www.dubrovniknet.hr/feed" },
                { 114, 12, 68, 1, "https://makarska-danas.com/feed" },
                { 115, 12, 69, 1, "https://makarska.hr/rss" },
                { 116, 12, 70, 1, "http://www.portaloko.hr/rss/-1" },
                { 117, 12, 71, 1, "https://www.antenazadar.hr/feed" },
                { 118, 12, 72, 1, "https://radioimotski.hr/feed" },
                { 119, 12, 73, 1, "https://imotskenovine.hr/feed" },
                { 120, 12, 74, 1, "http://www.kastela.org/?format=feed&type=rss" },
                { 121, 12, 75, 1, "https://huknet1.hr/?feed=rss2" },
                { 122, 12, 76, 1, "https://www.zadarskilist.hr/rss.xml" },
                { 123, 12, 77, 1, "https://www.istriaterramagica.eu/feed" },
                { 124, 12, 78, 1, "https://www.ipress.hr/index.php?format=feed&type=rss" },
                { 125, 12, 80, 1, "https://www.rijekadanas.com/feed" },
                { 126, 12, 81, 1, "https://www.fiuman.hr/feed" },
                { 127, 12, 82, 1, "https://riportal.net.hr/feed" },
                { 128, 12, 83, 1, "https://www.gspress.net/feed" },
                { 129, 12, 84, 1, "https://www.zagrebacki.hr/feed" },
                { 130, 12, 85, 1, "https://www.zgportal.com/feed" },
                { 131, 12, 86, 1, "https://www.zagreb.hr/RssFeeds.aspx?id=17" },
                { 132, 12, 87, 1, "https://regionalni.com/feed" },
                { 133, 12, 88, 1, "https://www.glaspodravine.hr/feed" },
                { 134, 12, 89, 1, "https://www.medjimurje.info/feed" },
                { 135, 12, 90, 1, "https://www.mnovine.hr/feed" },
                { 136, 12, 91, 1, "https://www.zagorje.com/rss" },
                { 137, 12, 92, 1, "https://www.icv.hr/feed/" },
                { 138, 12, 93, 1, "https://www.novska.in/feed" },
                { 139, 12, 94, 1, "https://novosti.hr/feed" },
                { 140, 12, 95, 1, "http://portal53.hr/feed" },
                { 141, 12, 96, 1, "https://sbplus.hr/rss" },
                { 142, 12, 97, 1, "https://www.pozeska-kronika.hr/component/fpss/module/292.feed?type=rss" },
                { 143, 12, 98, 1, "http://www.osijek031.com/news_rss.php" },
            });

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath"],
            values: new object[,]
            {
                { 144, 1, 99, 1, "https://otvoreno.hr/feed", 2, null, null, null, null, "//div[contains(@class, 'td-post-featured-image')]//img" },
                { 145, 1, 100, 1, "https://www.geopolitika.news/feed", 2, null, null, null, null, "//div[contains(@class, 'entry-image featured-image')]//img" },
            });

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath"],
            values: [146, 1, 101, 1, "https://povijest.hr/feed/", null, null, null, null, "//div[contains(@class, 'td-post-featured-image')]//img"]);

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath"],
            values: [147, 1, 102, 1, "https://7dnevno.hr/feed", 2, null, null, null, null, "//div[contains(@class, 'td-post-featured-image')]//img"]);

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips"],
            values: [148, 2, 103, 1, "https://basketball.hr/vijesti.xml", null, null, null, null, "//div[contains(@class, 'img')]//img", 0, 5]);

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips"],
            values: [149, 6, 104, 1, "https://joomboos.24sata.hr/feeds/axiom-feed/tes-partnerski", 0, 7]);

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips"],
            values: [150, 5, 105, 1, "https://www.ictbusiness.info/rss2.xml", null, 1, null, null, null, "//div[contains(@class, 'main-content')]//img", 0, 8]);

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips"],
            values: new object[,]
            {
                { 151, 5, 106, 1, "https://www.hcl.hr/feed", null, null, null, null, "//div[contains(@class, 'article')]//img", 0, 7 },
                { 152, 7, 107, 1, "https://profitiraj.hr/feed", null, null, null, null, "//div[contains(@class, 'site-content')]//img", 0, 5 },
            });

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips"],
            values: [153, 8, 108, 1, "https://www.motori.hr/feed", 1, 3, true, null, null, "//div[contains(@class, 'content')]//img", 0, 6]);

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips"],
            values: new object[,]
            {
                { 154, 8, 109, 1, "https://autoportal.hr/feed", null, null, null, null, "//div[contains(@class, 'td-post-content')]//img", 0, 3 },
                { 155, 8, 110, 1, "https://www.autopress.hr/feed", null, null, null, null, "//div[contains(@class, 'td-post-featured-image')]//img", 0, 9 },
                { 156, 8, 111, 1, "https://vozim.hr/feed", null, null, null, null, "//div[contains(@class, 'intro-image-over')]//img", 0, 8 },
                { 157, 8, 112, 1, "https://ams.hr/feed", null, null, null, null, "//main[contains(@class, 'main-content')]//img", 0, 8 },
                { 158, 2, 113, 1, "http://hoopster.hr/feed", null, null, null, null, "//div[contains(@class, 'post-img')]//img", 0, 7 },
                { 159, 2, 114, 2, "http://prvahnl.hr/rss", null, null, null, null, "//div[contains(@class, 'news')]//img", 0, 6 },
            });

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath"],
            values: [160, 1, 115, 1, "http://balkans.aljazeera.net/mobile/articles", null, null, null, null, "//div[contains(@class, 'field-items')]//img"]);

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips"],
            values: new object[,]
            {
                { 161, 5, 116, 2, "https://www.hifimedia.hr/feed", null, null, null, false, "//figure[contains(@class, 'post-gallery')]//img", 0, 7 },
                { 162, 5, 117, 1, "https://geek.hr/feed", null, null, null, null, "//div[contains(@class, 'zox-post-main')]//img", 0, 5 },
                { 163, 9, 118, 1, "https://vizkultura.hr/feed", null, null, null, null, "//div[contains(@class, 'content')]//img", 0, 7 },
                { 164, 9, 119, 1, "https://zivotumjetnosti.ipu.hr/feed", null, null, null, false, string.Empty, 0, 6 },
                { 165, 9, 120, 1, "https://svijetkulture.com/feed", null, null, null, null, "//div[contains(@class, 'td-post-featured-image')]//img", 0, 7 },
                { 166, 5, 121, 1, "http://www.gamer.hr/feed", null, null, null, null, "//div[contains(@class, 'site-featured-image')]//img", 0, 5 },
            });

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips"],
            values: [167, 12, 122, 1, "https://ogportal.com/feed", 0, 5]);

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips"],
            values: [168, 12, 123, 1, "https://zupanjac.net/feed", null, null, null, null, "//div[contains(@class, 'feature-img')]//img", 0, 4]);

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips"],
            values: [169, 12, 124, 1, "https://press032.com/feed", 0, 7]);

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_AttributeName", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "ImageUrlParseConfiguration_XPath", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips"],
            values: [170, 1, 125, 2, "https://www.bitno.net/feed", "data-lazy-src", null, null, null, null, "//section[contains(@class, 'article-content')]//picture[contains(@class, 'wp-caption')]//img[@data-lazy-src]", 0, 5]);

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips"],
            values: [171, 12, 126, 1, "https://ploce.com.hr/feed", 0, 3]);

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped"],
            values: [172, 12, 127, 1, "https://kaportal.net.hr/feed", null, 3, null, null, null]);

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url"],
            values: [173, 12, 128, 1, "https://radio-mreznica.hr/feed"]);

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips"],
            values: new object[,]
            {
                { 174, 1, 129, 1, "https://www.maxportal.hr/feed", 0, 3 },
                { 175, 7, 17, 1, "https://www.poslovni.hr/feed", 0, 5 },
                { 176, 8, 130, 1, "https://www.gp1.hr/feed", 0, 6 },
                { 177, 8, 131, 1, "https://f1.pulsmedia.hr/feed", 0, 7 },
                { 178, 5, 132, 1, "https://www.racunalo.com/feed", 0, 5 },
            });

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ElementExtensionIndex", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_IsSavedInHtmlElementWithSrcAttribute", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips"],
            values: [179, 5, 133, 1, "https://mob.hr/feed", 2, 3, true, null, null, 0, 9]);

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips"],
            values: new object[,]
            {
                { 180, 1, 134, 1, "https://www.startnews.hr/feeds/latest", 0, 7 },
                { 181, 6, 135, 1, "https://viral.hr/feed", 0, 3 },
            });

        _ = migrationBuilder.InsertData(
            table: "RssFeeds",
            columns: ["Id", "CategoryId", "NewsPortalId", "RequestType", "Url"],
            values: new object[,]
            {
                { 182, 2, 136, 1, "https://sportklub.hr/feed" },
                { 183, 4, 137, 1, "https://doktorehitno.hr/rss" },
            });

        _ = migrationBuilder.InsertData(
            table: "RssFeedCategory",
            columns: ["Id", "CategoryId", "RssFeedId", "UrlRegex", "UrlSegmentIndex"],
            values: new object[,]
            {
                { 1, 1, 13, "vijesti", 1 },
                { 2, 8, 13, "autoklub", 1 },
                { 3, 9, 13, "kultura", 1 },
                { 4, 1, 13, "globus", 1 },
                { 5, 4, 13, "domidizajn", 1 },
                { 6, 6, 13, "viral", 1 },
                { 7, 3, 13, "spektakli", 1 },
                { 8, 4, 13, "life", 1 },
                { 9, 3, 13, "scena", 1 },
                { 10, 3, 13, "spektakli", 1 },
                { 11, 1, 37, "vijesti", 1 },
                { 12, 2, 37, "sport", 1 },
                { 13, 3, 37, "showbiz", 1 },
                { 14, 4, 37, "lifestyle", 1 },
                { 15, 7, 37, "biznis", 1 },
                { 16, 5, 37, "techsci", 1 },
                { 17, 8, 37, "automoto", 1 },
                { 18, 9, 37, "kultura", 1 },
                { 19, 1, 42, "vijesti", 1 },
                { 20, 3, 42, "showbuzz", 1 },
                { 21, 1, 39, "politika-kriminal", 1 },
                { 22, 4, 39, "zivot", 1 },
                { 23, 9, 39, "kultura", 1 },
                { 24, 2, 39, "na-prvu", 1 },
                { 25, 1, 65, "Vijesti", 1 },
                { 26, 1, 65, "Svijet", 1 },
                { 27, 1, 65, "Znanost", 1 },
                { 28, 1, 65, "Regija", 1 },
                { 29, 1, 65, "Dnevnik", 1 },
                { 30, 1, 65, "Info", 1 },
                { 31, 7, 65, "Biznis", 1 },
                { 32, 4, 65, "Lifestyle", 1 },
                { 33, 4, 65, "Zdravlje", 1 },
                { 34, 2, 65, "Sport-Klub", 1 },
                { 35, 3, 65, "Showbiz", 1 },
                { 36, 5, 65, "Tehnologija", 1 },
                { 37, 9, 65, "Kultura", 1 },
                { 38, 1, 65, "Crna-Kronika", 1 },
                { 39, 1, 65, "Video", 1 },
                { 40, 1, 66, "Hrvatska", 1 },
                { 41, 2, 66, "Sport", 1 },
                { 42, 9, 66, "Kultura", 1 },
                { 43, 1, 66, "svijet", 1 },
                { 44, 1, 66, "koronovirus", 1 },
                { 45, 1, 66, "svijet", 1 },
                { 46, 4, 71, "zivot", 1 },
                { 47, 1, 71, "news", 1 },
                { 48, 3, 71, "scena", 1 },
                { 49, 4, 71, "bubble", 1 },
                { 50, 1, 72, "vijesti", 1 },
                { 51, 2, 72, "sport", 1 },
                { 52, 1, 72, "domovina", 1 },
                { 53, 3, 72, "magazin", 1 },
                { 54, 4, 72, "zdravlje", 1 },
                { 55, 1, 72, "korona-virus", 1 },
                { 56, 8, 72, "auto-moto", 1 },
                { 57, 1, 14, "hrvatska", 2 },
                { 58, 1, 14, "crna-kronika", 2 },
                { 59, 1, 14, "svijet", 2 },
                { 60, 9, 14, "kultura", 1 },
                { 61, 7, 14, "novac", 1 },
                { 62, 5, 14, "znanost", 1 },
                { 63, 2, 14, "sport", 1 },
                { 64, 6, 14, "vic-dana", 1 },
                { 65, 5, 14, "planet-x", 1 },
                { 66, 6, 14, "fora-dana", 1 },
                { 67, 3, 14, "hot", 1 },
                { 68, 4, 14, "magazin", 1 },
                { 69, 5, 14, "tehnoklik", 1 },
                { 70, 8, 14, "auto", 1 },
                { 71, 4, 82, "lifestyle", 1 },
                { 72, 3, 82, "vijesti", 1 },
                { 73, 9, 82, "kultura", 1 },
                { 74, 3, 82, "televizija", 1 },
                { 75, 4, 82, "dogadjanja", 1 },
                { 76, 1, 85, "top-news", 1 },
                { 77, 1, 85, "life", 1 },
                { 78, 7, 85, "ekonomix", 1 },
                { 79, 5, 85, "tehno", 1 },
                { 80, 1, 144, "vijesti", 1 },
                { 81, 7, 144, "gospodarstvo", 1 },
                { 82, 3, 144, "magazin", 1 },
                { 83, 9, 144, "kultura", 1 },
                { 84, 2, 144, "sport", 1 },
                { 85, 1, 144, "eu-i-svijet", 1 },
                { 86, 1, 147, "vijesti", 1 },
                { 87, 1, 147, "sport", 1 },
                { 88, 1, 147, "domovina", 1 },
                { 89, 1, 147, "kultura", 2 },
                { 90, 1, 147, "zdravlje", 1 },
            });

        _ = migrationBuilder.InsertData(
            table: "RssFeedContentModifier",
            columns: ["Id", "OrderIndex", "ReplacementValue", "RssFeedId", "SourceValue"],
            values: new object[,]
            {
                { 1, 1, "<notused>", 5, "<description>" },
                { 2, 2, "</notused>", 5, "</description>" },
                { 3, 3, "<description>", 5, "<content>" },
                { 4, 4, "</description>", 5, "</content>" },
                { 5, 1, "<notused>", 3, "<description>" },
                { 6, 2, "</notused>", 3, "</description>" },
                { 7, 3, "<description>", 3, "<content>" },
                { 8, 4, "</description>", 3, "</content>" },
                { 9, 1, "<notused>", 4, "<description>" },
                { 10, 2, "</notused>", 4, "</description>" },
                { 11, 3, "<description>", 4, "<content>" },
                { 12, 4, "</description>", 4, "</content>" },
                { 13, 1, "<notused>", 2, "<description>" },
                { 14, 2, "</notused>", 2, "</description>" },
                { 15, 3, "<description>", 2, "<content>" },
                { 16, 4, "</description>", 2, "</content>" },
                { 17, 1, "<description>", 1, "<content>" },
                { 18, 2, "</description>", 1, "</content>" },
                { 19, 3, "<description>", 1, "<content>" },
                { 20, 4, "</description>", 1, "</content>" },
                { 21, 1, "<description>", 96, "<content>" },
                { 22, 2, "</description>", 96, "</content>" },
                { 23, 3, "<description>", 96, "<content>" },
                { 24, 4, "</description>", 96, "</content>" },
                { 25, 1, "<link>", 70, "<thumb>" },
                { 26, 2, "</link>", 70, "</thumb>" },
                { 27, 1, "<link>", 69, "<thumb>" },
                { 28, 2, "</link>", 69, "</thumb>" },
                { 29, 1, "<link>", 68, "<thumb>" },
                { 30, 2, "</link>", 68, "</thumb>" },
                { 31, 1, "<link>", 67, "<thumb>" },
                { 32, 2, "</link>", 67, "</thumb>" },
                { 33, 1, "<notused>", 57, "<description>" },
                { 34, 2, "</notused>", 57, "</description>" },
                { 35, 3, "<description>", 57, "<content:encoded>" },
                { 36, 4, "</description>", 57, "</content:encoded>" },
                { 37, 1, "<notused>", 176, "<description>" },
                { 38, 2, "</notused>", 176, "</description>" },
                { 39, 3, "<description>", 176, "<content:encoded>" },
                { 40, 4, "</description>", 176, "</content:encoded>" },
                { 41, 1, string.Empty, 63, "\n" },
            });

        _ = migrationBuilder.CreateIndex(
            name: "IX_ArticleCategories_ArticleId",
            table: "ArticleCategories",
            column: "ArticleId");

        _ = migrationBuilder.CreateIndex(
            name: "IX_ArticleCategories_CategoryId",
            table: "ArticleCategories",
            column: "CategoryId");

        _ = migrationBuilder.CreateIndex(
            name: "IX_Articles_NewsPortalId",
            table: "Articles",
            column: "NewsPortalId");

        _ = migrationBuilder.CreateIndex(
            name: "IX_Articles_PublishDateTime",
            table: "Articles",
            column: "PublishDateTime");

        _ = migrationBuilder.CreateIndex(
            name: "IX_Articles_RssFeedId",
            table: "Articles",
            column: "RssFeedId");

        _ = migrationBuilder.CreateIndex(
            name: "IX_NewsPortals_CategoryId",
            table: "NewsPortals",
            column: "CategoryId");

        _ = migrationBuilder.CreateIndex(
            name: "IX_NewsPortals_RegionId",
            table: "NewsPortals",
            column: "RegionId");

        _ = migrationBuilder.CreateIndex(
            name: "IX_RssFeedCategory_CategoryId",
            table: "RssFeedCategory",
            column: "CategoryId");

        _ = migrationBuilder.CreateIndex(
            name: "IX_RssFeedCategory_RssFeedId",
            table: "RssFeedCategory",
            column: "RssFeedId");

        _ = migrationBuilder.CreateIndex(
            name: "IX_RssFeedContentModifier_RssFeedId",
            table: "RssFeedContentModifier",
            column: "RssFeedId");

        _ = migrationBuilder.CreateIndex(
            name: "IX_RssFeeds_CategoryId",
            table: "RssFeeds",
            column: "CategoryId");

        _ = migrationBuilder.CreateIndex(
            name: "IX_RssFeeds_NewsPortalId",
            table: "RssFeeds",
            column: "NewsPortalId");

        _ = migrationBuilder.CreateIndex(
            name: "IX_Settings_Created",
            table: "Settings",
            column: "Created");

        _ = migrationBuilder.CreateIndex(
            name: "IX_SimilarArticles_FirstArticleId",
            table: "SimilarArticles",
            column: "FirstArticleId");

        _ = migrationBuilder.CreateIndex(
            name: "IX_SimilarArticles_SecondArticleId",
            table: "SimilarArticles",
            column: "SecondArticleId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.DropTable(
            name: "ApplicationDownload");

        _ = migrationBuilder.DropTable(
            name: "ArticleCategories");

        _ = migrationBuilder.DropTable(
            name: "PushNotifications");

        _ = migrationBuilder.DropTable(
            name: "RssFeedCategory");

        _ = migrationBuilder.DropTable(
            name: "RssFeedContentModifier");

        _ = migrationBuilder.DropTable(
            name: "Settings");

        _ = migrationBuilder.DropTable(
            name: "SimilarArticles");

        _ = migrationBuilder.DropTable(
            name: "Articles");

        _ = migrationBuilder.DropTable(
            name: "RssFeeds");

        _ = migrationBuilder.DropTable(
            name: "NewsPortals");

        _ = migrationBuilder.DropTable(
            name: "Categories");

        _ = migrationBuilder.DropTable(
            name: "Regions");
    }
}
