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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WebApiVersion = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MobileAppVersion = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DownloadedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MobileDeviceType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationDownload", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    KeyWordsRegexPattern = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    SortIndex = table.Column<int>(type: "int", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: true),
                    CategoryType = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PushNotifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InternalName = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Topic = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ArticleUrl = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    IsSoundEnabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PushNotifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Subtitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsPortals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BaseUrl = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IconUrl = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsNewOverride = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    RequestType = table.Column<int>(type: "int", nullable: false),
                    AmpConfiguration_HasAmpArticles = table.Column<bool>(type: "bit", nullable: true),
                    AmpConfiguration_TemplateUrl = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CategoryParseConfiguration_CategoryParseStrategy = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    ImageUrlParseConfiguration_ImageUrlParseStrategy = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    ImageUrlParseConfiguration_ImgElementXPath = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false, defaultValue: ""),
                    ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped = table.Column<bool>(type: "bit", nullable: true),
                    ImageUrlParseConfiguration_ImageUrlWebScrapeType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    ImageUrlParseConfiguration_JsonWebScrapePropertyNames = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    SkipParseConfiguration_NumberOfSkips = table.Column<int>(type: "int", nullable: true),
                    SkipParseConfiguration_CurrentSkip = table.Column<int>(type: "int", nullable: true),
                    NewsPortalId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    WebUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfClicks = table.Column<int>(type: "int", nullable: false),
                    TrendingScore = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    NewsPortalId = table.Column<int>(type: "int", nullable: false),
                    RssFeedId = table.Column<int>(type: "int", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrlRegex = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UrlSegmentIndex = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    RssFeedId = table.Column<int>(type: "int", nullable: false)
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
                name: "RssFeedContentModifier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceValue = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ReplacementValue = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    RssFeedId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "ArticleCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArticleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
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
                columns: new[] { "Id", "CategoryType", "Color", "KeyWordsRegexPattern", "Name", "Position", "SortIndex", "Url" },
                values: new object?[,]
                {
                    { 1, 1, "#E84855", null, "Vijesti", null, 2, "/vijesti" },
                    { 12, 2, "#AC80A0", null, "Lokalno", 3, null, "/local" },
                    { 11, 3, "#AC80A0", null, "Generalno", null, 1, "/general" },
                    { 8, 1, "#FC814A", null, "Auto/Moto", null, 9, "/auto-moto" },
                    { 7, 1, "#3185FC", null, "Biznis", null, 8, "/biznis" },
                    { 9, 1, "#AC80A0", null, "Kultura", null, 10, "/kultura" },
                    { 5, 1, "#2E86AB", null, "Tech", null, 6, "/tech" },
                    { 4, 1, "#32936F", null, "Lifestyle", null, 5, "/lifestyle" },
                    { 3, 1, "#F4B100", null, "Show", null, 4, "/show" },
                    { 2, 1, "#4CB944", null, "Sport", null, 3, "/sport" },
                    { 6, 1, "#9055A2", null, "Viral", null, 7, "/viral" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Name", "Subtitle" },
                values: new object?[,]
                {
                    { 7, "Slavonija & Baranja", "Osijek, Vinkovci, Slavonski Brod, Vukovar, Požega..." },
                    { 1, "Global", "Global" },
                    { 2, "Dalmacija", "Split, Zadar, Dubrovnik, Šibenik, Kaštela, Imotski..." },
                    { 3, "Istra & Kvarner", "Rijeka, Pula, Opatija, Pazin, Umag, Poreč, Rovinj..." },
                    { 4, "Lika", "Lokalne vijesti iz Ličko-Senjske županije" },
                    { 6, "Sjeverna Hrvatska", "Međimurje, Podravina, Sisak, Zagorje..." },
                    { 5, "Zagreb", "Lokalne vijesti iz grada Zagreba i okolice" }
                });

            migrationBuilder.InsertData(
                table: "NewsPortals",
                columns: new[] { "Id", "BaseUrl", "CategoryId", "CreatedAt", "IconUrl", "IsNewOverride", "Name", "RegionId" },
                values: new object?[,]
                {
                    { 1, "https://www.index.hr", 11, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/Index.png", null, "Index.hr", 1 },
                    { 47, "http://www.lika-express.hr", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/LikaExpress.png", null, "Lika express", 4 },
                    { 46, "https://likaclub.eu", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/LikaKlub.png", null, "Lika Klub", 4 },
                    { 82, "https://riportal.net.hr", 12, new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/Riportal.png", null, "Riportal", 3 },
                    { 81, "https://www.fiuman.hr", 12, new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/Fiuman.png", null, "Fiuman", 3 },
                    { 80, "https://www.rijekadanas.com", 12, new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/RijekaDanas.png", null, "Rijeka Danas", 3 },
                    { 78, "https://www.ipress.hr", 12, new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/IPress.png", null, "iPress", 3 },
                    { 77, "https://www.istriaterramagica.eu", 12, new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/IstraTerraMagica.png", null, "Istra Terra Magica", 3 },
                    { 61, "http://www.istra-istria.hr", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/IstarskaZupanija.png", null, "Istarska Županija", 3 },
                    { 59, "https://ivijesti.hr", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/IVijesti.png", null, "iVijesti", 3 },
                    { 45, "https://www.parentium.com", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/Parentium.png", null, "Parentium", 3 },
                    { 44, "https://www.novilist.hr", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/NoviList.png", null, "NL", 3 },
                    { 48, "https://www.lika-online.com", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/LikaOnline.png", null, "Lika Online", 4 },
                    { 76, "https://www.zadarskilist.hr", 12, new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/ZadarskiList.png", null, "Zadarski List", 2 },
                    { 74, "http://www.kastela.org", 12, new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/PortalKastela.png", null, "Portal Grada Kaštela", 2 },
                    { 73, "https://imotskenovine.hr", 12, new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/ImotskeNovine.png", null, "Imotske Novine", 2 },
                    { 72, "https://radioimotski.hr", 12, new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/RadioImotski.png", null, "Radio Imotski", 2 },
                    { 71, "https://www.antenazadar.hr", 12, new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/AntenaZadar.png", null, "Antena portal Zadar", 2 },
                    { 70, "http://www.portaloko.hr", 12, new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/PortalOko.png", null, "Portaloko.hr", 2 },
                    { 69, "https://makarska.hr", 12, new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/MakarskaHr.png", null, "Makarska", 2 },
                    { 68, "https://makarska-danas.com", 12, new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/MakarskaDanas.png", null, "MakarskaDanas", 2 },
                    { 67, "https://www.dubrovniknet.hr", 12, new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/DubrovnikNet.png", null, "Dubrovniknet.hr", 2 },
                    { 66, "https://slobodnadalmacija.hr", 12, new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/SlobodnaDalmacija_Split.png", null, "Slobodna Dalmacija - Split", 2 },
                    { 65, "https://slobodnadalmacija.hr", 12, new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/SlobodnaDalmacija_Dalmacija.png", null, "Slobodna Dalmacija - Dalmacija", 2 },
                    { 60, "https://dubrovackidnevnik.net.hr", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/DubrovackiDnevnik.png", null, "Dubrovački Dnevnik.hr", 2 },
                    { 75, "https://huknet1.hr", 12, new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/HukNet.png", null, "Huknet", 2 },
                    { 41, "https://dalmatinskiportal.hr", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/DalmatinskiPortal.png", null, "Dalmatinski Portal", 2 },
                    { 49, "http://www.likaplus.hr", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/LikaPlus.png", null, "Lika plus", 4 },
                    { 53, "https://sjever.hr", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/SjeverHr.png", null, "Sjever.hr", 6 },
                    { 84, "https://www.zagrebacki.hr", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/ZagrebackiList.png", null, "Zagrebački List", 5 },
                    { 62, "https://www.zagrebonline.hr", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/ZagrebOnline.png", null, "Zagreb Online", 5 },
                    { 52, "https://www.zagrebancija.com", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/Zagrebancija.png", null, "Zagrebancija", 5 },
                    { 51, "https://www.zagreb.info", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/ZagrebInfo.png", null, "Zagreb Info", 5 },
                    { 50, "https://www.index.hr", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/IndexHrZagreb.png", null, "Index.Hr - Zagreb", 5 },
                    { 98, "http://www.osijek031.com", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/Osijek031.png", null, "Osijek031", 7 },
                    { 97, "https://www.pozeska-kronika.hr", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/PozeskaKronika.png", null, "Požeška Kronika", 7 },
                    { 96, "https://sbplus.hr", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/SbPlusHr.png", null, "SBplus.hr", 7 },
                    { 95, "http://portal53.hr", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/Portal53.png", null, "Portal53", 7 },
                    { 94, "https://novosti.hr", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/NovostiHr.png", null, "Novosti.hr", 7 },
                    { 93, "https://www.novska.in", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/NovskaIn.png", null, "Novska.IN", 7 },
                    { 83, "https://www.gspress.net", 12, new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/GsPress.png", null, "GS Press", 4 },
                    { 92, "https://www.icv.hr", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/InformativniCentarVirovitica.png", null, "Informativni Centar Virovitica", 7 }
                });

            migrationBuilder.InsertData(
                table: "NewsPortals",
                columns: new[] { "Id", "BaseUrl", "CategoryId", "CreatedAt", "IconUrl", "IsNewOverride", "Name", "RegionId" },
                values: new object?[,]
                {
                    { 58, "https://slavonski.hr", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/SlavonskiHr.png", null, "Slavonski Hr", 7 },
                    { 57, "https://www.glas-slavonije.hr", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/GlasSlavonije.png", null, "Glas Slavonije", 7 },
                    { 56, "https://www.baranjainfo.hr", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/BaranjaInfo.png", null, "Baranja info", 7 },
                    { 91, "https://www.zagorje.com", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/ZagorjeCom.png", null, "Zagorje.com", 6 },
                    { 90, "https://www.mnovine.hr", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/MedimurskeNovine.png", null, "Međimurske Novine", 6 },
                    { 89, "https://www.medjimurje.info", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/MedimurjeInfo.png", null, "Međimurje Info", 6 },
                    { 88, "https://www.glaspodravine.hr", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/GlasPodravineIPrigorja.png", null, "Glas Podravine i Prigorja", 6 },
                    { 87, "https://regionalni.com", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/PlusRegionalniTjednik.png", null, "7Plus Regionalni Tjednik", 6 },
                    { 63, "https://www.sisak.info", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/SisakInfo.png", null, "Sisak.Info", 6 },
                    { 55, "https://epodravina.hr", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/PodravinaHr.png", null, "ePodravina.hr", 6 },
                    { 54, "https://prigorski.hr", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/PrigorskiHr.png", null, "Sjever.hr", 6 },
                    { 64, "https://osijeknews.hr", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/OsijekNews.png", null, "Osijek NEWS", 7 },
                    { 40, "https://www.dalmacijanews.hr", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/DalmacijaNews.png", null, "Dalmacija News", 2 },
                    { 37, "https://www.dalmacijadanas.hr", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/DalmacijaDanas.png", null, "Dalmacija Danas", 2 },
                    { 120, "https://svijetkulture.com", 9, new DateTime(2020, 10, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/SvijetKulture.png", null, "SVIJET KULTURE", 1 },
                    { 29, "https://hr.n1info.com", 1, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/N1.png", null, "N1", 1 },
                    { 28, "https://www.autonet.hr", 8, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/Autonet.png", null, "Autonet", 1 },
                    { 27, "http://www.ljepotaizdravlje.hr", 4, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/LjepotaIZdravlje.png", null, "Ljepota i zdravlje", 1 },
                    { 26, "https://wall.hr", 4, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/WallHr.png", null, "Wall.hr", 1 },
                    { 25, "http://www.cosmopolitan.hr", 4, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/Cosmopolitan.png", null, "Cosmopolitan", 1 },
                    { 23, "https://pcchip.hr", 5, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/PcChip.png", null, "PCchip", 1 },
                    { 22, "https://poslovnipuls.com", 7, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/PoslovniPuls.png", null, "Poslovni Puls", 1 },
                    { 21, "https://www.netokracija.com", 5, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/Netokracija.png", null, "Netokracija", 1 },
                    { 20, "https://zimo.dnevnik.hr", 5, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/Zimo.png", null, "Zimo", 1 },
                    { 19, "https://www.vidi.hr", 5, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/VidiHr.png", null, "Vidi.hr", 1 },
                    { 18, "https://www.bug.hr", 5, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/Bug.png", null, "Bug", 1 },
                    { 30, "https://narod.hr", 1, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/NarodHr.png", null, "Narod HR", 1 },
                    { 16, "https://lider.media", 7, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/Lider.png", null, "Lider", 1 },
                    { 12, "https://sportnet.rtl.hr", 11, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/RtlVijesti.png", null, "RTL Vijesti", 1 },
                    { 11, "https://gol.dnevnik.hr", 2, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/Gol.png", null, "Gol", 1 },
                    { 10, "https://dnevnik.hr", 11, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/Dnevnik.png", null, "Dnevnik", 1 },
                    { 9, "https://www.telegram.hr", 11, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/Telegram.png", null, "Telegram", 1 },
                    { 8, "https://www.vecernji.hr", 11, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/VecernjiList.png", null, "Večernji List", 1 },
                    { 7, "https://www.tportal.hr", 11, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/TPortal.png", null, "tportal", 1 },
                    { 6, "https://slobodnadalmacija.hr", 11, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/SlobodnaDalmacija.png", null, "Slobodna Dalmacija", 1 },
                    { 5, "https://net.hr", 11, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/NetHr.png", null, "Net.hr", 1 },
                    { 4, "https://sportske.jutarnji.hr", 11, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/JutarnjiList.png", null, "Jutarnji List", 1 },
                    { 3, "https://sportske.jutarnji.hr", 2, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/SportskeNovosti.png", null, "Sportske Novosti", 1 },
                    { 2, "https://www.24sata.hr", 11, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/DvadesetCetiriSata.png", null, "24 sata", 1 },
                    { 15, "http://www.nogometplus.net", 2, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/NogometPlus.png", null, "Nogomet Plus", 1 },
                    { 31, "https://www.hrt.hr", 1, new DateTime(2020, 6, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/Hrt.png", null, "HRT", 1 },
                    { 32, "https://100posto.jutarnji.hr", 1, new DateTime(2020, 6, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/StoPosto.png", null, "100posto", 1 }
                });

            migrationBuilder.InsertData(
                table: "NewsPortals",
                columns: new[] { "Id", "BaseUrl", "CategoryId", "CreatedAt", "IconUrl", "IsNewOverride", "Name", "RegionId" },
                values: new object?[,]
                {
                    { 33, "https://www.dnevno.hr", 11, new DateTime(2020, 6, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/Dnevno.png", null, "Dnevno.Hr", 1 },
                    { 119, "https://zivotumjetnosti.ipu.hr", 9, new DateTime(2020, 10, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/ZivotUmjetnosti.png", null, "Život umjetnosti", 1 },
                    { 118, "https://vizkultura.hr", 9, new DateTime(2020, 10, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/VizKultura.png", null, "vizkultura.hr", 1 },
                    { 117, "https://geek.hr", 5, new DateTime(2020, 10, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/GeekHr.png", null, "Geek.hr", 1 },
                    { 116, "https://www.hifimedia.hr", 5, new DateTime(2020, 10, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/HifiMedia.png", null, "hifimedia", 1 },
                    { 115, "http://balkans.aljazeera.net", 1, new DateTime(2020, 10, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/AlJazeera.png", null, "Al Jazeera Balkans", 1 },
                    { 114, "http://prvahnl.hr", 2, new DateTime(2020, 10, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/PrvaHnl.png", null, "Hrvatski Telekom Prva liga", 1 },
                    { 113, "http://hoopster.hr", 2, new DateTime(2020, 9, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/Hoopster.png", null, "Hoopster.hr", 1 },
                    { 112, "https://ams.hr", 8, new DateTime(2020, 9, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/AutoMotorSport.png", null, "AUTO MOTOR I SPORT", 1 },
                    { 111, "https://vozim.hr", 8, new DateTime(2020, 9, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/VozimHr.png", null, "Vozim.HR", 1 },
                    { 110, "https://www.autopress.hr", 8, new DateTime(2020, 9, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/AutopressHr.png", null, "AutopressHR", 1 },
                    { 109, "https://autoportal.hr", 8, new DateTime(2020, 9, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/AutoportalHr.png", null, "Autoportal.hr", 1 },
                    { 108, "https://www.motori.hr/", 8, new DateTime(2020, 9, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/MotoriHr.png", null, "motori.hr", 1 },
                    { 107, "https://profitiraj.hr", 7, new DateTime(2020, 9, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/ProfitirajHr.png", null, "Profitiraj.hr", 1 },
                    { 106, "https://www.hcl.hr", 5, new DateTime(2020, 9, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/Hcl.png", null, "HCL Gaming Portal", 1 },
                    { 105, "https://www.ictbusiness.info", 5, new DateTime(2020, 9, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/IctBusiness.png", null, "ICT Business", 1 },
                    { 104, "https://joomboos.24sata.hr", 6, new DateTime(2020, 9, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/JoomBoos.png", null, "JoomBoos", 1 },
                    { 103, "https://basketball.hr", 2, new DateTime(2020, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/BasketballHr.png", null, "Basketball.hr", 1 },
                    { 102, "https://7dnevno.hr", 1, new DateTime(2020, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/Dnevno7.png", null, "7dnevno", 1 },
                    { 101, "https://povijest.hr", 1, new DateTime(2020, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/PovijestHr.png", null, "Povijest.hr", 1 },
                    { 100, "https://www.geopolitika.news", 1, new DateTime(2020, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/GeoPolitika.png", null, "Geopolitika News", 1 },
                    { 99, "https://otvoreno.hr", 1, new DateTime(2020, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/OtvorenoHr.png", null, "Otvoreno.hr", 1 },
                    { 39, "https://express.24sata.hr", 1, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/Express.png", null, "Express", 1 },
                    { 38, "https://www.nacional.hr", 1, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/Nacional.png", null, "Nacional", 1 },
                    { 36, "https://www.scena.hr", 3, new DateTime(2020, 7, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/Scena.png", null, "Scena", 1 },
                    { 35, "https://direktno.hr", 1, new DateTime(2020, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/DirektnoHr.png", null, "Direktno", 1 },
                    { 85, "https://www.zgportal.com", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/ZgPortal.png", null, "ZG portal", 5 },
                    { 86, "https://www.zagreb.hr/", 12, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Icons/GradZagreb.png", null, "Grad Zagreb", 5 }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped" },
                values: new object?[] { 1, 1, 1, 1, "https://www.index.hr/rss/vijesti", true, "https://amp.index.hr/article/{0}{3}", 1, "//figure[contains(@class, 'img-container')]//img", null, null });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url" },
                values: new object?[,]
                {
                    { 86, 12, 40, 1, "https://www.dalmacijanews.hr/rss" },
                    { 87, 12, 41, 1, "https://dalmatinskiportal.hr/sadrzaj/rss/vijesti.xml" },
                    { 106, 12, 60, 1, "https://dubrovackidnevnik.net.hr/rss" }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object?[,]
                {
                    { 111, 12, 65, 1, "https://slobodnadalmacija.hr/feed/category/246", 0, 5 },
                    { 112, 12, 66, 1, "https://slobodnadalmacija.hr/feed/category/253", 0, 5 }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url" },
                values: new object?[,]
                {
                    { 113, 12, 67, 1, "https://www.dubrovniknet.hr/feed" },
                    { 114, 12, 68, 1, "https://makarska-danas.com/feed" },
                    { 115, 12, 69, 1, "https://makarska.hr/rss" },
                    { 116, 12, 70, 1, "http://www.portaloko.hr/rss/-1" },
                    { 117, 12, 71, 1, "https://www.antenazadar.hr/feed" },
                    { 118, 12, 72, 1, "https://radioimotski.hr/feed" },
                    { 119, 12, 73, 1, "https://imotskenovine.hr/feed" },
                    { 120, 12, 74, 1, "http://www.kastela.org/?format=feed&type=rss" },
                    { 121, 12, 75, 1, "https://huknet1.hr/?feed=rss2" },
                    { 122, 12, 76, 1, "https://www.zadarskilist.hr/rss.xml" }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped" },
                values: new object?[] { 83, 12, 37, 1, "https://www.dalmacijadanas.hr/feed/", "//div[contains(@class, 'td-full-screen-header-image-wrap')]//img", null, null });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url" },
                values: new object?[] { 90, 12, 44, 1, "https://www.novilist.hr/feed" });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object?[,]
                {
                    { 165, 9, 120, 1, "https://svijetkulture.com/feed", "//div[contains(@class, 'td-post-featured-image')]//img", null, null, 0, 7 },
                    { 163, 9, 118, 1, "https://vizkultura.hr/feed", "//div[contains(@class, 'content')]//img", null, null, 0, 7 },
                    { 148, 2, 103, 1, "https://basketball.hr/vijesti.xml", "//div[contains(@class, 'img')]//img", null, null, 0, 5 }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object?[] { 149, 6, 104, 1, "https://joomboos.24sata.hr/feeds/axiom-feed/tes-partnerski", 0, 7 });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object?[] { 150, 5, 105, 1, "https://www.ictbusiness.info/rss2.xml", 1, "//div[contains(@class, 'main-content')]//img", null, null, 0, 8 });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object?[,]
                {
                    { 151, 5, 106, 1, "https://www.hcl.hr/feed", "//div[contains(@class, 'article')]//img", null, null, 0, 7 },
                    { 152, 7, 107, 1, "https://profitiraj.hr/feed", "//div[contains(@class, 'site-content')]//img", null, null, 0, 11 }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object?[] { 153, 8, 108, 1, "https://www.motori.hr/feed", 4, "//div[contains(@class, 'content')]//img", null, null, 0, 17 });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object?[,]
                {
                    { 154, 8, 109, 1, "https://autoportal.hr/feed", "//div[contains(@class, 'td-post-content')]//img", null, null, 0, 3 },
                    { 155, 8, 110, 1, "https://www.autopress.hr/feed", "//div[contains(@class, 'td-post-featured-image')]//img", null, null, 0, 9 },
                    { 156, 8, 111, 1, "https://vozim.hr/feed", "//div[contains(@class, 'intro-image-over')]//img", null, null, 0, 8 },
                    { 157, 8, 112, 1, "https://ams.hr/feed", "//main[contains(@class, 'main-content')]//img", null, null, 0, 17 },
                    { 158, 2, 113, 1, "http://hoopster.hr/feed", "//div[contains(@class, 'post-img')]//img", null, null, 0, 19 },
                    { 159, 2, 114, 1, "http://prvahnl.hr/rss", "//div[contains(@class, 'news')]//img", null, null, 0, 6 }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped" },
                values: new object?[] { 160, 1, 115, 1, "http://balkans.aljazeera.net/mobile/articles", "//div[contains(@class, 'field-items')]//img", null, null });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object?[,]
                {
                    { 161, 5, 116, 2, "https://www.hifimedia.hr/feed", "//figure[contains(@class, 'post-gallery')]//img", null, false, 0, 7 },
                    { 162, 5, 117, 1, "https://geek.hr/feed", "//div[contains(@class, 'zox-post-main')]//img", null, null, 0, 5 },
                    { 164, 9, 119, 1, "https://zivotumjetnosti.ipu.hr/feed", "", null, false, 0, 27 }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url" },
                values: new object?[,]
                {
                    { 91, 12, 45, 1, "https://www.parentium.com/rssfeed.asp" },
                    { 105, 12, 59, 1, "https://ivijesti.hr/feed" },
                    { 107, 12, 61, 1, "http://www.istra-istria.hr/index.php?id=2415&type=100" },
                    { 103, 12, 57, 1, "https://www.glas-slavonije.hr/rss" },
                    { 104, 12, 58, 1, "https://slavonski.hr/feed" },
                    { 110, 12, 64, 1, "https://osijeknews.hr/feed" }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url" },
                values: new object?[,]
                {
                    { 137, 12, 92, 1, "https://www.icv.hr/feed/" },
                    { 138, 12, 93, 1, "https://www.novska.in/feed" },
                    { 139, 12, 94, 1, "https://novosti.hr/feed" },
                    { 140, 12, 95, 1, "http://portal53.hr/feed" },
                    { 141, 12, 96, 1, "https://sbplus.hr/rss" },
                    { 142, 12, 97, 1, "https://www.pozeska-kronika.hr/component/fpss/module/292.feed?type=rss" },
                    { 143, 12, 98, 1, "http://www.osijek031.com/news_rss.php" }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped" },
                values: new object?[] { 96, 12, 50, 1, "https://www.index.hr/rss/vijesti-zagreb", 2, "//figure[contains(@class, 'img-container')]//img", null, null });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url" },
                values: new object?[,]
                {
                    { 97, 12, 51, 1, "https://www.zagreb.info/feed" },
                    { 98, 12, 52, 1, "https://www.zagrebancija.com/feed" },
                    { 108, 12, 62, 1, "https://www.zagrebonline.hr/feed" },
                    { 129, 12, 84, 1, "https://www.zagrebacki.hr/feed" },
                    { 102, 12, 56, 1, "https://www.baranjainfo.hr/feed" },
                    { 136, 12, 91, 1, "https://www.zagorje.com/rss" },
                    { 135, 12, 90, 1, "https://www.mnovine.hr/feed" },
                    { 134, 12, 89, 1, "https://www.medjimurje.info/feed" },
                    { 123, 12, 77, 1, "https://www.istriaterramagica.eu/feed" },
                    { 124, 12, 78, 1, "https://www.ipress.hr/index.php?format=feed&type=rss" },
                    { 125, 12, 80, 1, "https://www.rijekadanas.com/feed" },
                    { 126, 12, 81, 1, "https://www.fiuman.hr/feed" },
                    { 127, 12, 82, 1, "https://riportal.net.hr/feed" },
                    { 92, 12, 46, 1, "https://likaclub.eu/feed" },
                    { 93, 12, 47, 2, "http://www.lika-express.hr/feed" }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped" },
                values: new object?[] { 147, 1, 102, 1, "https://7dnevno.hr/feed", 2, "//div[contains(@class, 'td-post-featured-image')]//img", null, null });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url" },
                values: new object?[,]
                {
                    { 94, 12, 48, 1, "https://www.lika-online.com/feed" },
                    { 128, 12, 83, 1, "https://www.gspress.net/feed" },
                    { 99, 12, 53, 1, "https://sjever.hr/feed" },
                    { 100, 12, 54, 1, "https://prigorski.hr/feed" },
                    { 101, 12, 55, 1, "https://epodravina.hr/feed" },
                    { 109, 12, 63, 1, "https://www.sisak.info/feed" },
                    { 132, 12, 87, 1, "https://regionalni.com/feed" },
                    { 133, 12, 88, 1, "https://www.glaspodravine.hr/feed" },
                    { 95, 12, 49, 1, "http://www.likaplus.hr/rss" }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped" },
                values: new object?[] { 146, 1, 101, 1, "https://povijest.hr/feed/", "//div[contains(@class, 'td-post-featured-image')]//img", null, null });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped" },
                values: new object?[,]
                {
                    { 145, 1, 100, 1, "https://www.geopolitika.news/feed", 2, "//div[contains(@class, 'entry-image featured-image')]//img", null, null },
                    { 144, 1, 99, 1, "https://otvoreno.hr/feed", 2, "//div[contains(@class, 'td-post-featured-image')]//img", null, null }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object?[,]
                {
                    { 22, 4, 6, 2, "https://www.slobodnadalmacija.hr/feed/category/266", "//img[contains(@class, 'card__image')]", null, null, 0, 5 },
                    { 23, 4, 6, 2, "https://www.slobodnadalmacija.hr/feed/category/267", "//img[contains(@class, 'card__image')]", null, null, 0, 5 },
                    { 24, 4, 6, 2, "https://www.slobodnadalmacija.hr/feed/category/268", "//img[contains(@class, 'card__image')]", null, null, 0, 5 },
                    { 25, 4, 6, 2, "https://www.slobodnadalmacija.hr/feed/category/270", "//img[contains(@class, 'card__image')]", null, null, 0, 5 },
                    { 26, 4, 6, 2, "https://www.slobodnadalmacija.hr/feed/category/271", "//img[contains(@class, 'card__image')]", null, null, 0, 5 },
                    { 27, 5, 6, 2, "https://www.slobodnadalmacija.hr/feed/category/269", "//img[contains(@class, 'card__image')]", null, null, 0, 5 }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object?[] { 28, 6, 6, 2, "https://www.slobodnadalmacija.hr/feed/category/274", "//img[contains(@class, 'card__image')]", null, null, 0, 5 });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped" },
                values: new object?[,]
                {
                    { 29, 1, 7, 1, "https://www.tportal.hr/rss-vijesti.xml", "//img[contains(@class, 'lateImage lateImageLoaded')]", null, null },
                    { 30, 1, 7, 1, "https://www.tportal.hr/rss-biznis.xml", "//img[contains(@class, 'lateImage lateImageLoaded')]", null, null },
                    { 31, 2, 7, 1, "https://www.tportal.hr/rss-sport.xml", "//img[contains(@class, 'lateImage lateImageLoaded')]", null, null },
                    { 32, 5, 7, 1, "https://www.tportal.hr/rss-tehno.xml", "//img[contains(@class, 'lateImage lateImageLoaded')]", null, null },
                    { 33, 3, 7, 1, "https://www.tportal.hr/rss-showtime.xml", "//img[contains(@class, 'lateImage lateImageLoaded')]", null, null },
                    { 34, 4, 7, 1, "https://www.tportal.hr/rss-Lifestyle.xml", "//img[contains(@class, 'lateImage lateImageLoaded')]", null, null },
                    { 35, 6, 7, 1, "https://www.tportal.hr/rss-funbox.xml", "//img[contains(@class, 'lateImage lateImageLoaded')]", null, null },
                    { 36, 9, 7, 1, "https://www.tportal.hr/rss-kultura.xml", "//img[contains(@class, 'lateImage lateImageLoaded')]", null, null }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object?[,]
                {
                    { 21, 4, 6, 2, "https://www.slobodnadalmacija.hr/feed/category/265", "//img[contains(@class, 'card__image')]", null, null, 0, 5 },
                    { 20, 4, 6, 2, "https://www.slobodnadalmacija.hr/feed/category/264", "//img[contains(@class, 'card__image')]", null, null, 0, 5 },
                    { 19, 3, 6, 2, "https://www.slobodnadalmacija.hr/feed/category/263", "//img[contains(@class, 'card__image')]", null, null, 0, 5 },
                    { 18, 3, 6, 2, "https://www.slobodnadalmacija.hr/feed/category/375", "//img[contains(@class, 'card__image')]", null, null, 0, 5 }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped" },
                values: new object?[,]
                {
                    { 2, 2, 1, 1, "https://www.index.hr/rss/sport", true, "https://amp.index.hr/article/{0}{3}", 1, "//figure[contains(@class, 'img-container')]//img", null, null },
                    { 3, 3, 1, 1, "https://www.index.hr/rss/magazin", true, "https://amp.index.hr/article/{0}{3}", 1, "//figure[contains(@class, 'img-container')]//img", null, null },
                    { 4, 4, 1, 1, "https://www.index.hr/rss/rouge", true, "https://amp.index.hr/article/{0}{3}", 1, "//figure[contains(@class, 'img-container')]//img", null, null },
                    { 5, 8, 1, 1, "https://www.index.hr/rss/auto", true, "https://amp.index.hr/article/{0}{3}", 1, "//figure[contains(@class, 'img-container')]//img", null, null }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped" },
                values: new object?[,]
                {
                    { 6, 1, 2, 1, "https://www.24sata.hr/feeds/news.xml", false, null, "//img[contains(@class, 'article__figure_img')]", null, null },
                    { 7, 3, 2, 1, "https://www.24sata.hr/feeds/show.xml", false, null, "//img[contains(@class, 'article__figure_img')]", null, null },
                    { 8, 2, 2, 1, "https://www.24sata.hr/feeds/sport.xml", false, null, "//img[contains(@class, 'article__figure_img')]", null, null }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlWebScrapeType", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped" },
                values: new object?[] { 37, 1, 8, 1, "https://www.vecernji.hr/feeds/latest", true, "https://m.vecernji.hr/amp/{1}{2}", 2, 2, "//script[contains(@type, 'application/ld+json')]", "image,url", true });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped" },
                values: new object?[,]
                {
                    { 9, 4, 2, 1, "https://www.24sata.hr/feeds/lifestyle.xml", false, null, "//img[contains(@class, 'article__figure_img')]", null, null },
                    { 11, 6, 2, 1, "https://www.24sata.hr/feeds/fun.xml", false, null, "//img[contains(@class, 'article__figure_img')]", null, null }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped" },
                values: new object?[] { 12, 2, 3, 2, "http://sportske.jutarnji.hr/sn/feed", "//img[contains(@class, 'media-object adaptive lazy')]", null, null });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object?[] { 13, 1, 4, 2, "https://www.jutarnji.hr/feed", 2, "//img[contains(@class, 'media-object adaptive lazy')]", null, null, 0, 5 });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped" },
                values: new object?[] { 14, 1, 5, 1, "https://net.hr/feed/", true, "https://net.hr/{1}{2}{3}amp", 2, "//div[contains(@class, 'featured-img')]//img", null, null });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object?[,]
                {
                    { 15, 1, 6, 2, "https://www.slobodnadalmacija.hr/feed/category/119", "//img[contains(@class, 'card__image')]", null, null, 0, 5 },
                    { 16, 2, 6, 2, "https://www.slobodnadalmacija.hr/feed/category/255", "//img[contains(@class, 'card__image')]", null, null, 0, 5 },
                    { 17, 3, 6, 2, "https://www.slobodnadalmacija.hr/feed/category/262", "//img[contains(@class, 'card__image')]", null, null, 0, 5 }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped" },
                values: new object?[] { 10, 5, 2, 1, "https://www.24sata.hr/feeds/tech.xml", false, null, "//img[contains(@class, 'article__figure_img')]", null, null });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url" },
                values: new object?[] { 130, 12, 85, 1, "https://www.zgportal.com/feed" });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped" },
                values: new object?[] { 39, 1, 9, 1, "https://www.telegram.hr/feed/", 2, "//div[contains(@class, 'thumb')]//img", null, null });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped" },
                values: new object?[] { 42, 1, 10, 1, "https://dnevnik.hr/assets/feed/articles/", true, "https://dnevnik.hr/amp/{1}{2}{3}", 2, "//figure[contains(@class, 'article-main-img')]//img", null, null });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object?[,]
                {
                    { 69, 3, 31, 1, "https://magazin.hrt.hr/feed.xml", "//div[contains(@class, 'image-slider')]//img", null, null, 0, 5 },
                    { 70, 3, 31, 1, "https://www.hrt.hr/rss/glazba/", "//div[contains(@class, 'image-slider')]//img", null, null, 0, 5 }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object?[] { 71, 1, 32, 1, "https://100posto.jutarnji.hr/rss", 2, "//picture[contains(@class, 'pic')]//img", null, null, 0, 5 });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped" },
                values: new object?[] { 72, 1, 33, 1, "https://www.dnevno.hr/feed/", 2, "//div[contains(@class, 'img-holder inner')]//img", null, null });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped" },
                values: new object?[,]
                {
                    { 74, 1, 35, 1, "https://direktno.hr/rss/publish/latest/direkt-50/", "//div[contains(@class, 'pd-hero-image')]//img", null, null },
                    { 75, 1, 35, 1, "https://direktno.hr/rss/publish/latest/domovina-10/", "//div[contains(@class, 'pd-hero-image')]//img", null, null },
                    { 76, 1, 35, 1, "https://direktno.hr/rss/publish/latest/eu_svijet/", "//div[contains(@class, 'pd-hero-image')]//img", null, null },
                    { 77, 7, 35, 1, "https://direktno.hr/rss/publish/latest/razvoj-110/", "//div[contains(@class, 'pd-hero-image')]//img", null, null },
                    { 78, 2, 35, 1, "https://direktno.hr/rss/publish/latest/sport-60/", "//div[contains(@class, 'pd-hero-image')]//img", null, null }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped" },
                values: new object?[,]
                {
                    { 79, 3, 35, 1, "https://direktno.hr/rss/publish/latest/zivot-70/", "//div[contains(@class, 'pd-hero-image')]//img", null, null },
                    { 80, 1, 35, 1, "https://direktno.hr/rss/publish/latest/kolumne-80/", "//div[contains(@class, 'pd-hero-image')]//img", null, null },
                    { 81, 1, 35, 1, "https://direktno.hr/rss/publish/latest/direktnotv-100/", "//div[contains(@class, 'pd-hero-image')]//img", null, null }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object?[] { 82, 1, 36, 2, "https://www.scena.hr/feed/", 2, 4, "//div[contains(@class, 'mycontent')]//img", null, null, 0, 6 });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped" },
                values: new object?[] { 84, 1, 38, 1, "https://www.nacional.hr/feed/", "//div[contains(@class, 'single-post-media')]//img", null, null });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object?[] { 85, 1, 39, 1, "https://express.24sata.hr/feeds/placeholder-head/rss_feed", 2, "//img[contains(@class, 'article__figure_img')]", null, null, 0, 10 });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object?[,]
                {
                    { 68, 2, 31, 1, "https://www.hrt.hr/rss/sport/", "//div[contains(@class, 'image-slider')]//img", null, null, 0, 5 },
                    { 67, 1, 31, 1, "https://www.hrt.hr/rss/vijesti/", "//div[contains(@class, 'image-slider')]//img", null, null, 0, 5 }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped" },
                values: new object?[] { 66, 1, 30, 2, "https://narod.hr/feed", 2, "//div[contains(@class, 'td-post-featured-image')]//img", null, null });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "CategoryParseConfiguration_CategoryParseStrategy", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped" },
                values: new object?[] { 65, 1, 29, 2, "http://hr.n1info.com/rss/249/Naslovna", 2, 3, "//figure[contains(@class, 'media')]//img", null, null });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped" },
                values: new object?[] { 43, 2, 11, 1, "https://gol.dnevnik.hr/assets/feed/articles", true, "https://gol.dnevnik.hr/amp/{1}{2}{3}{4}", "//figure[contains(@class, 'article-image main-image')]//img", null, null });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped" },
                values: new object?[,]
                {
                    { 44, 2, 12, 1, "https://sportnet.rtl.hr/rss/", "//img[contains(@class, 'naslovna')]", null, null },
                    { 47, 2, 15, 1, "http://www.nogometplus.net/index.php/feed/", "//div[contains(@class, 'post-img')]//img", null, null },
                    { 48, 7, 16, 1, "http://lider.media/feed/", "//img[contains(@class, 'card__image')]", null, null },
                    { 49, 7, 16, 1, "http://lider.media/feed/", "//img[contains(@class, 'card__image')]", null, null },
                    { 50, 7, 16, 1, "http://lider.media/feed/", "//img[contains(@class, 'card__image')]", null, null },
                    { 54, 5, 18, 1, "http://www.bug.hr/rss/vijesti/", "//div[contains(@class, 'entry-content')]//img", null, null },
                    { 40, 2, 9, 1, "https://telesport.telegram.hr/feed/", "//div[contains(@class, 'featured-img')]//img", null, null },
                    { 55, 5, 19, 1, "http://www.vidi.hr/rss/feed/vidi", "//div[contains(@class, 'attribute-image')]//img", null, null }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped" },
                values: new object?[] { 57, 5, 21, 1, "http://www.netokracija.com/feed", true, "https://www.netokracija.com/{1}/amp", "//div[contains(@class, 'post__hero')]//img", null, null });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped", "SkipParseConfiguration_CurrentSkip", "SkipParseConfiguration_NumberOfSkips" },
                values: new object?[] { 58, 7, 22, 2, "http://www.poslovnipuls.com/feed/", "//div[contains(@class, 'postFeaturedImg postFeaturedImg--single')]//img", null, null, 0, 10 });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped" },
                values: new object?[,]
                {
                    { 59, 5, 23, 1, "http://pcchip.hr/feed/", "//div[contains(@class, 'td-post-featured-image')]//img", null, null },
                    { 61, 4, 25, 1, "http://www.cosmopolitan.hr/feed", "//div[contains(@class, 'first-image')]//img", null, null }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ImageUrlParseStrategy", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped" },
                values: new object?[] { 62, 4, 26, 1, "http://wall.hr/cdn/feed.xml", 2, "//figure[contains(@class, 'dcms-image article-image')]//img", null, null });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped" },
                values: new object?[,]
                {
                    { 63, 4, 27, 1, "http://www.ljepotaizdravlje.hr/feed", "//div[contains(@class, 'post-thumbnail')]//img", null, null },
                    { 64, 8, 28, 1, "https://www.autonet.hr/feed/", "//figure[contains(@class, 'figure')]//img", null, null }
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url", "AmpConfiguration_HasAmpArticles", "AmpConfiguration_TemplateUrl", "ImageUrlParseConfiguration_ImgElementXPath", "ImageUrlParseConfiguration_JsonWebScrapePropertyNames", "ImageUrlParseConfiguration_ShouldImageUrlBeWebScraped" },
                values: new object?[] { 56, 5, 20, 1, "https://zimo.dnevnik.hr/assets/feed/articles", true, "https://zimo.dnevnik.hr/amp/clanak/{2}", "//div[contains(@class, 'img-holder')]//img", null, null });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "CategoryId", "NewsPortalId", "RequestType", "Url" },
                values: new object?[] { 131, 12, 86, 1, "https://www.zagreb.hr/RssFeeds.aspx?id=17" });

            migrationBuilder.InsertData(
                table: "RssFeedCategory",
                columns: new[] { "Id", "CategoryId", "RssFeedId", "UrlRegex", "UrlSegmentIndex" },
                values: new object?[,]
                {
                    { 31, 7, 65, "Biznis", 1 },
                    { 41, 2, 66, "Sport", 1 },
                    { 40, 1, 66, "Hrvatska", 1 },
                    { 39, 1, 65, "Video", 1 },
                    { 38, 1, 65, "Crna-Kronika", 1 },
                    { 37, 9, 65, "Kultura", 1 },
                    { 36, 5, 65, "Tehnologija", 1 },
                    { 35, 3, 65, "Showbiz", 1 },
                    { 34, 2, 65, "Sport-Klub", 1 },
                    { 42, 9, 66, "Kultura", 1 },
                    { 33, 4, 65, "Zdravlje", 1 },
                    { 81, 7, 144, "gospodarstvo", 1 },
                    { 30, 1, 65, "Info", 1 },
                    { 29, 1, 65, "Dnevnik", 1 },
                    { 28, 1, 65, "Regija", 1 },
                    { 27, 1, 65, "Znanost", 1 },
                    { 26, 1, 65, "Svijet", 1 },
                    { 25, 1, 65, "Vijesti", 1 },
                    { 20, 3, 42, "showbuzz", 1 },
                    { 32, 4, 65, "Lifestyle", 1 },
                    { 43, 1, 66, "svijet", 1 },
                    { 44, 1, 66, "koronovirus", 1 },
                    { 45, 1, 66, "svijet", 1 },
                    { 56, 8, 72, "auto-moto", 1 },
                    { 55, 1, 72, "korona-virus", 1 },
                    { 54, 4, 72, "zdravlje", 1 },
                    { 53, 3, 72, "magazin", 1 },
                    { 52, 1, 72, "domovina", 1 },
                    { 51, 2, 72, "sport", 1 },
                    { 50, 1, 72, "vijesti", 1 },
                    { 49, 4, 71, "bubble", 1 },
                    { 48, 3, 71, "scena", 1 },
                    { 47, 1, 71, "news", 1 },
                    { 46, 4, 71, "zivot", 1 },
                    { 73, 9, 82, "kultura", 1 },
                    { 74, 3, 82, "televizija", 1 },
                    { 75, 4, 82, "dogadjanja", 1 },
                    { 76, 1, 85, "top-news", 1 },
                    { 77, 1, 85, "life", 1 },
                    { 78, 7, 85, "ekonomix", 1 },
                    { 79, 5, 85, "tehno", 1 },
                    { 80, 1, 144, "vijesti", 1 }
                });

            migrationBuilder.InsertData(
                table: "RssFeedCategory",
                columns: new[] { "Id", "CategoryId", "RssFeedId", "UrlRegex", "UrlSegmentIndex" },
                values: new object?[,]
                {
                    { 19, 1, 42, "vijesti", 1 },
                    { 24, 2, 39, "na-prvu", 1 },
                    { 23, 9, 39, "kultura", 1 },
                    { 22, 4, 39, "zivot", 1 },
                    { 10, 3, 13, "spektakli", 1 },
                    { 9, 3, 13, "scena", 1 },
                    { 8, 4, 13, "life", 1 },
                    { 7, 3, 13, "spektakli", 1 },
                    { 6, 6, 13, "viral", 1 },
                    { 5, 4, 13, "domidizajn", 1 },
                    { 4, 1, 13, "globus", 1 },
                    { 3, 9, 13, "kultura", 1 },
                    { 2, 8, 13, "autoklub", 1 },
                    { 1, 1, 13, "vijesti", 1 },
                    { 82, 3, 144, "magazin", 1 },
                    { 83, 9, 144, "kultura", 1 },
                    { 84, 2, 144, "sport", 1 },
                    { 85, 1, 144, "eu-i-svijet", 1 },
                    { 86, 1, 147, "vijesti", 1 },
                    { 87, 1, 147, "sport", 1 },
                    { 88, 1, 147, "domovina", 1 },
                    { 89, 1, 147, "kultura", 2 },
                    { 90, 1, 147, "zdravlje", 1 },
                    { 57, 1, 14, "hrvatska", 2 },
                    { 71, 4, 82, "lifestyle", 1 },
                    { 58, 1, 14, "crna-kronika", 2 },
                    { 60, 9, 14, "kultura", 1 },
                    { 21, 1, 39, "politika-kriminal", 1 },
                    { 18, 9, 37, "kultura", 1 },
                    { 17, 8, 37, "automoto", 1 },
                    { 16, 5, 37, "techsci", 1 },
                    { 15, 7, 37, "biznis", 1 },
                    { 14, 4, 37, "lifestyle", 1 },
                    { 13, 3, 37, "showbiz", 1 },
                    { 12, 2, 37, "sport", 1 },
                    { 11, 1, 37, "vijesti", 1 },
                    { 70, 8, 14, "auto", 1 },
                    { 69, 5, 14, "tehnoklik", 1 },
                    { 68, 4, 14, "magazin", 1 },
                    { 67, 3, 14, "hot", 1 },
                    { 66, 6, 14, "fora-dana", 1 },
                    { 65, 5, 14, "planet-x", 1 }
                });

            migrationBuilder.InsertData(
                table: "RssFeedCategory",
                columns: new[] { "Id", "CategoryId", "RssFeedId", "UrlRegex", "UrlSegmentIndex" },
                values: new object?[,]
                {
                    { 64, 6, 14, "vic-dana", 1 },
                    { 63, 2, 14, "sport", 1 },
                    { 62, 5, 14, "znanost", 1 },
                    { 61, 7, 14, "novac", 1 },
                    { 59, 1, 14, "svijet", 2 },
                    { 72, 3, 82, "vijesti", 1 }
                });

            migrationBuilder.InsertData(
                table: "RssFeedContentModifier",
                columns: new[] { "Id", "ReplacementValue", "RssFeedId", "SourceValue" },
                values: new object?[,]
                {
                    { 9, "<description>", 1, "<content>" },
                    { 13, "<link>", 70, "<thumb>" },
                    { 10, "</description>", 1, "</content>" },
                    { 7, "<description>", 2, "<content>" },
                    { 8, "</description>", 2, "</content>" },
                    { 3, "<description>", 3, "<content>" },
                    { 4, "</description>", 3, "</content>" },
                    { 5, "<description>", 4, "<content>" },
                    { 6, "</description>", 4, "</content>" },
                    { 14, "</link>", 70, "</thumb>" },
                    { 1, "<description>", 5, "<content>" },
                    { 11, "<description>", 96, "<content>" },
                    { 19, "<link>", 67, "<thumb>" },
                    { 20, "</link>", 67, "</thumb>" },
                    { 17, "<link>", 68, "<thumb>" },
                    { 18, "</link>", 68, "</thumb>" },
                    { 15, "<link>", 69, "<thumb>" },
                    { 16, "</link>", 69, "</thumb>" },
                    { 2, "</description>", 5, "</content>" },
                    { 12, "</description>", 96, "</content>" }
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
                name: "IX_RssFeedContentModifier_RssFeedId",
                table: "RssFeedContentModifier",
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
                name: "RssFeedContentModifier");

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
