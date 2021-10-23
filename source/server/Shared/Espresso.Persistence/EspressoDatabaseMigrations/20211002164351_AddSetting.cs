// 20211002164351_AddSetting.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System;

namespace Espresso.Persistence.EspressoDatabaseMigrations
{
    public partial class AddSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
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
                    SimilarArticleSetting_ArticlePublishDateTimeDifferenceThreshol = table.Column<long>(name: "SimilarArticleSetting_ArticlePublishDateTimeDifferenceThreshol~", type: "bigint", nullable: false),
                    SimilarArticleSetting_MaxAgeOfSimilarArticleCheckingInMiliseco = table.Column<long>(name: "SimilarArticleSetting_MaxAgeOfSimilarArticleCheckingInMiliseco~", type: "bigint", nullable: false),
                    SimilarArticleSetting_MinimalNumberOfWordsForArticleToBeCompar = table.Column<int>(name: "SimilarArticleSetting_MinimalNumberOfWordsForArticleToBeCompar~", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "Created", "ArticleSetting_FeaturedArticlesTake", "ArticleSetting_MaxAgeOfArticleInMiliseconds", "ArticleSetting_MaxAgeOfFeaturedArticleInMiliseconds", "ArticleSetting_MaxAgeOfTrendingArticleInMiliseconds", "JobsSetting_AnalyticsCronExpression", "JobsSetting_ParseArticlesCronExpression", "JobsSetting_WebApiReportCronExpression", "NewsPortalSetting_MaxAgeOfNewNewsPortalInMiliseconds", "NewsPortalSetting_NewNewsPortalsPosition", "SimilarArticleSetting_ArticlePublishDateTimeDifferenceThreshol~", "SimilarArticleSetting_MaxAgeOfSimilarArticleCheckingInMiliseco~", "SimilarArticleSetting_MinimalNumberOfWordsForArticleToBeCompar~", "SimilarArticleSetting_SimilarityScoreThreshold" },
                values: new object[] { 1, new DateTimeOffset(new DateTime(2021, 10, 2, 16, 43, 47, 372, DateTimeKind.Unspecified).AddTicks(324), new TimeSpan(0, 0, 0, 0, 0)), 10, 432000000L, 21600000L, 21600000L, "0 10 * * *", "*/1 * * * *", "0 9 * * *", 5184000000L, 3, 86400000L, 93600000L, 4, 0.65000000000000002 });

            migrationBuilder.CreateIndex(
                name: "IX_Settings_Created",
                table: "Settings",
                column: "Created");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}
