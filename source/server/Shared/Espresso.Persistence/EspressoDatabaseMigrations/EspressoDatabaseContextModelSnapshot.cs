﻿// <auto-generated />
using System;
using Espresso.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Espresso.Persistence.EspressoDatabaseMigrations
{
    [DbContext(typeof(EspressoDatabaseContext))]
    partial class EspressoDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Espresso.Domain.Entities.ApplicationDownload", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("DownloadedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("MobileAppVersion")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<int>("MobileDeviceType")
                        .HasColumnType("integer");

                    b.Property<string>("WebApiVersion")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.HasKey("Id");

                    b.ToTable("ApplicationDownload");
                });

            modelBuilder.Entity("Espresso.Domain.Entities.Article", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreateDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<int>("NewsPortalId")
                        .HasColumnType("integer");

                    b.Property<int>("NumberOfClicks")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset>("PublishDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("RssFeedId")
                        .HasColumnType("integer");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<decimal>("TrendingScore")
                        .HasColumnType("numeric");

                    b.Property<DateTimeOffset>("UpdateDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("WebUrl")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.HasKey("Id");

                    b.HasIndex("NewsPortalId");

                    b.HasIndex("PublishDateTime");

                    b.HasIndex("RssFeedId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("Espresso.Domain.Entities.ArticleCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ArticleId")
                        .HasColumnType("uuid");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("CategoryId");

                    b.ToTable("ArticleCategories");
                });

            modelBuilder.Entity("Espresso.Domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryType")
                        .HasColumnType("integer");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("KeyWordsRegexPattern")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<int?>("Position")
                        .HasColumnType("integer");

                    b.Property<int?>("SortIndex")
                        .HasColumnType("integer");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryType = 1,
                            Color = "#E84855",
                            Name = "Vijesti",
                            SortIndex = 2,
                            Url = "/vijesti"
                        },
                        new
                        {
                            Id = 2,
                            CategoryType = 1,
                            Color = "#4CB944",
                            Name = "Sport",
                            SortIndex = 3,
                            Url = "/sport"
                        },
                        new
                        {
                            Id = 3,
                            CategoryType = 1,
                            Color = "#F4B100",
                            Name = "Show",
                            SortIndex = 4,
                            Url = "/show"
                        },
                        new
                        {
                            Id = 4,
                            CategoryType = 1,
                            Color = "#32936F",
                            Name = "Lifestyle",
                            SortIndex = 5,
                            Url = "/lifestyle"
                        },
                        new
                        {
                            Id = 5,
                            CategoryType = 1,
                            Color = "#2E86AB",
                            Name = "Tech",
                            SortIndex = 6,
                            Url = "/tech"
                        },
                        new
                        {
                            Id = 6,
                            CategoryType = 1,
                            Color = "#9055A2",
                            Name = "Viral",
                            SortIndex = 7,
                            Url = "/viral"
                        },
                        new
                        {
                            Id = 7,
                            CategoryType = 1,
                            Color = "#3185FC",
                            Name = "Biznis",
                            SortIndex = 8,
                            Url = "/biznis"
                        },
                        new
                        {
                            Id = 8,
                            CategoryType = 1,
                            Color = "#FC814A",
                            Name = "Auto/Moto",
                            SortIndex = 9,
                            Url = "/auto-moto"
                        },
                        new
                        {
                            Id = 9,
                            CategoryType = 1,
                            Color = "#AC80A0",
                            Name = "Kultura",
                            SortIndex = 10,
                            Url = "/kultura"
                        },
                        new
                        {
                            Id = 11,
                            CategoryType = 3,
                            Color = "#AC80A0",
                            Name = "Generalno",
                            SortIndex = 1,
                            Url = "/general"
                        },
                        new
                        {
                            Id = 12,
                            CategoryType = 2,
                            Color = "#AC80A0",
                            Name = "Lokalno",
                            Position = 3,
                            Url = "/local"
                        });
                });

            modelBuilder.Entity("Espresso.Domain.Entities.NewsPortal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("BaseUrl")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("IconUrl")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool>("IsEnabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<bool?>("IsNewOverride")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("RegionId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("RegionId");

                    b.ToTable("NewsPortals");
                });

            modelBuilder.Entity("Espresso.Domain.Entities.NewsPortalImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("ImageBytes")
                        .HasColumnType("bytea");

                    b.Property<int>("NewsPortalId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("NewsPortalId")
                        .IsUnique();

                    b.ToTable("NewsPortalImages");
                });

            modelBuilder.Entity("Espresso.Domain.Entities.PushNotification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ArticleUrl")
                        .IsRequired()
                        .HasMaxLength(5000)
                        .HasColumnType("character varying(5000)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("InternalName")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<bool>("IsSoundEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.HasKey("Id");

                    b.ToTable("PushNotifications");
                });

            modelBuilder.Entity("Espresso.Domain.Entities.Region", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Subtitle")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Regions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Global",
                            Subtitle = "Global"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Dalmacija",
                            Subtitle = "Split, Zadar, Dubrovnik, Šibenik, Kaštela, Imotski..."
                        },
                        new
                        {
                            Id = 3,
                            Name = "Istra & Kvarner",
                            Subtitle = "Rijeka, Pula, Opatija, Pazin, Umag, Poreč, Rovinj..."
                        },
                        new
                        {
                            Id = 4,
                            Name = "Lika",
                            Subtitle = "Lokalne vijesti iz Ličko-Senjske županije"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Sjeverna Hrvatska",
                            Subtitle = "Međimurje, Podravina, Sisak, Zagorje..."
                        },
                        new
                        {
                            Id = 7,
                            Name = "Slavonija & Baranja",
                            Subtitle = "Osijek, Vinkovci, Slavonski Brod, Vukovar, Požega..."
                        },
                        new
                        {
                            Id = 5,
                            Name = "Zagreb i okolica",
                            Subtitle = "Lokalne vijesti iz grada Zagreba i okolice"
                        });
                });

            modelBuilder.Entity("Espresso.Domain.Entities.RssFeed", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<int>("NewsPortalId")
                        .HasColumnType("integer");

                    b.Property<int>("RequestType")
                        .HasColumnType("integer");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("NewsPortalId");

                    b.ToTable("RssFeeds");
                });

            modelBuilder.Entity("Espresso.Domain.Entities.RssFeedCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<int>("RssFeedId")
                        .HasColumnType("integer");

                    b.Property<string>("UrlRegex")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("UrlSegmentIndex")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("RssFeedId");

                    b.ToTable("RssFeedCategory");
                });

            modelBuilder.Entity("Espresso.Domain.Entities.RssFeedContentModifier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("OrderIndex")
                        .HasColumnType("integer");

                    b.Property<string>("ReplacementValue")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<int>("RssFeedId")
                        .HasColumnType("integer");

                    b.Property<string>("SourceValue")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.HasKey("Id");

                    b.HasIndex("RssFeedId");

                    b.ToTable("RssFeedContentModifier", (string)null);
                });

            modelBuilder.Entity("Espresso.Domain.Entities.Setting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("SettingsRevision")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SettingsRevision"));

                    b.HasKey("Id");

                    b.HasIndex("Created");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("Espresso.Domain.Entities.SimilarArticle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("FirstArticleId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SecondArticleId")
                        .HasColumnType("uuid");

                    b.Property<double>("SimilarityScore")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("FirstArticleId");

                    b.HasIndex("SecondArticleId");

                    b.ToTable("SimilarArticles");
                });

            modelBuilder.Entity("Espresso.Domain.Entities.Article", b =>
                {
                    b.HasOne("Espresso.Domain.Entities.NewsPortal", "NewsPortal")
                        .WithMany("Articles")
                        .HasForeignKey("NewsPortalId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Espresso.Domain.Entities.RssFeed", "RssFeed")
                        .WithMany("Articles")
                        .HasForeignKey("RssFeedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Espresso.Domain.ValueObjects.ArticleValueObjects.EditorConfiguration", "EditorConfiguration", b1 =>
                        {
                            b1.Property<Guid>("ArticleId")
                                .HasColumnType("uuid");

                            b1.Property<int?>("FeaturedPosition")
                                .HasColumnType("integer");

                            b1.Property<bool?>("IsFeatured")
                                .HasColumnType("boolean");

                            b1.Property<bool>("IsHidden")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("boolean")
                                .HasDefaultValue(false);

                            b1.HasKey("ArticleId");

                            b1.ToTable("Articles");

                            b1.WithOwner()
                                .HasForeignKey("ArticleId");
                        });

                    b.Navigation("EditorConfiguration")
                        .IsRequired();

                    b.Navigation("NewsPortal");

                    b.Navigation("RssFeed");
                });

            modelBuilder.Entity("Espresso.Domain.Entities.ArticleCategory", b =>
                {
                    b.HasOne("Espresso.Domain.Entities.Article", "Article")
                        .WithMany("ArticleCategories")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Espresso.Domain.Entities.Category", "Category")
                        .WithMany("ArticleCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Article");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Espresso.Domain.Entities.NewsPortal", b =>
                {
                    b.HasOne("Espresso.Domain.Entities.Category", "Category")
                        .WithMany("NewsPortals")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Espresso.Domain.Entities.Region", "Region")
                        .WithMany("NewsPortals")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Region");
                });

            modelBuilder.Entity("Espresso.Domain.Entities.NewsPortalImage", b =>
                {
                    b.HasOne("Espresso.Domain.Entities.NewsPortal", "NewsPortal")
                        .WithOne("NewsPortalImage")
                        .HasForeignKey("Espresso.Domain.Entities.NewsPortalImage", "NewsPortalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NewsPortal");
                });

            modelBuilder.Entity("Espresso.Domain.Entities.RssFeed", b =>
                {
                    b.HasOne("Espresso.Domain.Entities.Category", "Category")
                        .WithMany("RssFeeds")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Espresso.Domain.Entities.NewsPortal", "NewsPortal")
                        .WithMany("RssFeeds")
                        .HasForeignKey("NewsPortalId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.OwnsOne("Espresso.Domain.ValueObjects.RssFeedValueObjects.AmpConfiguration", "AmpConfiguration", b1 =>
                        {
                            b1.Property<int>("RssFeedId")
                                .HasColumnType("integer");

                            b1.Property<bool?>("HasAmpArticles")
                                .HasColumnType("boolean");

                            b1.Property<string>("TemplateUrl")
                                .HasMaxLength(300)
                                .HasColumnType("character varying(300)");

                            b1.HasKey("RssFeedId");

                            b1.ToTable("RssFeeds");

                            b1.WithOwner()
                                .HasForeignKey("RssFeedId");
                        });

                    b.OwnsOne("Espresso.Domain.ValueObjects.RssFeedValueObjects.CategoryParseConfiguration", "CategoryParseConfiguration", b1 =>
                        {
                            b1.Property<int>("RssFeedId")
                                .HasColumnType("integer");

                            b1.Property<int>("CategoryParseStrategy")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .HasDefaultValue(1);

                            b1.HasKey("RssFeedId");

                            b1.ToTable("RssFeeds");

                            b1.WithOwner()
                                .HasForeignKey("RssFeedId");
                        });

                    b.OwnsOne("Espresso.Domain.ValueObjects.RssFeedValueObjects.ImageUrlParseConfiguration", "ImageUrlParseConfiguration", b1 =>
                        {
                            b1.Property<int>("RssFeedId")
                                .HasColumnType("integer");

                            b1.Property<string>("AttributeName")
                                .IsRequired()
                                .ValueGeneratedOnAdd()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasDefaultValue("src");

                            b1.Property<string>("ElementExtensionAttributeName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)");

                            b1.Property<string>("ElementExtensionName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)");

                            b1.Property<int>("ImageUrlParseStrategy")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .HasDefaultValue(1);

                            b1.Property<int>("ImageUrlWebScrapeType")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .HasDefaultValue(1);

                            b1.Property<string>("JsonWebScrapePropertyNames")
                                .HasMaxLength(300)
                                .HasColumnType("character varying(300)");

                            b1.Property<bool>("ShouldImageUrlBeWebScraped")
                                .HasColumnType("boolean");

                            b1.Property<int>("WebScrapeRequestType")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .HasDefaultValue(2);

                            b1.Property<string>("XPath")
                                .IsRequired()
                                .ValueGeneratedOnAdd()
                                .HasMaxLength(300)
                                .HasColumnType("character varying(300)")
                                .HasDefaultValue("");

                            b1.HasKey("RssFeedId");

                            b1.ToTable("RssFeeds");

                            b1.WithOwner()
                                .HasForeignKey("RssFeedId");
                        });

                    b.OwnsOne("Espresso.Domain.ValueObjects.RssFeedValueObjects.SkipParseConfiguration", "SkipParseConfiguration", b1 =>
                        {
                            b1.Property<int>("RssFeedId")
                                .HasColumnType("integer");

                            b1.Property<int?>("CurrentSkip")
                                .HasColumnType("integer");

                            b1.Property<int?>("NumberOfSkips")
                                .HasColumnType("integer");

                            b1.HasKey("RssFeedId");

                            b1.ToTable("RssFeeds");

                            b1.WithOwner()
                                .HasForeignKey("RssFeedId");
                        });

                    b.Navigation("AmpConfiguration")
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("CategoryParseConfiguration")
                        .IsRequired();

                    b.Navigation("ImageUrlParseConfiguration")
                        .IsRequired();

                    b.Navigation("NewsPortal");

                    b.Navigation("SkipParseConfiguration")
                        .IsRequired();
                });

            modelBuilder.Entity("Espresso.Domain.Entities.RssFeedCategory", b =>
                {
                    b.HasOne("Espresso.Domain.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Espresso.Domain.Entities.RssFeed", "RssFeed")
                        .WithMany("RssFeedCategories")
                        .HasForeignKey("RssFeedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("RssFeed");
                });

            modelBuilder.Entity("Espresso.Domain.Entities.RssFeedContentModifier", b =>
                {
                    b.HasOne("Espresso.Domain.Entities.RssFeed", "RssFeed")
                        .WithMany("RssFeedContentModifiers")
                        .HasForeignKey("RssFeedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RssFeed");
                });

            modelBuilder.Entity("Espresso.Domain.Entities.Setting", b =>
                {
                    b.OwnsOne("Espresso.Domain.ValueObjects.SettingsValueObjects.ArticleSetting", "ArticleSetting", b1 =>
                        {
                            b1.Property<int>("SettingId")
                                .HasColumnType("integer");

                            b1.Property<int>("FeaturedArticlesTake")
                                .HasColumnType("integer");

                            b1.Property<long>("MaxAgeOfArticleInMiliseconds")
                                .HasColumnType("bigint");

                            b1.Property<long>("MaxAgeOfFeaturedArticleInMiliseconds")
                                .HasColumnType("bigint");

                            b1.Property<long>("MaxAgeOfTrendingArticleInMiliseconds")
                                .HasColumnType("bigint");

                            b1.HasKey("SettingId");

                            b1.ToTable("Settings");

                            b1.WithOwner()
                                .HasForeignKey("SettingId");
                        });

                    b.OwnsOne("Espresso.Domain.ValueObjects.SettingsValueObjects.JobsSetting", "JobsSetting", b1 =>
                        {
                            b1.Property<int>("SettingId")
                                .HasColumnType("integer");

                            b1.Property<string>("AnalyticsCronExpression")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("ParseArticlesCronExpression")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("WebApiReportCronExpression")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("SettingId");

                            b1.ToTable("Settings");

                            b1.WithOwner()
                                .HasForeignKey("SettingId");
                        });

                    b.OwnsOne("Espresso.Domain.ValueObjects.SettingsValueObjects.NewsPortalSetting", "NewsPortalSetting", b1 =>
                        {
                            b1.Property<int>("SettingId")
                                .HasColumnType("integer");

                            b1.Property<long>("MaxAgeOfNewNewsPortalInMiliseconds")
                                .HasColumnType("bigint");

                            b1.Property<int>("NewNewsPortalsPosition")
                                .HasColumnType("integer");

                            b1.HasKey("SettingId");

                            b1.ToTable("Settings");

                            b1.WithOwner()
                                .HasForeignKey("SettingId");
                        });

                    b.OwnsOne("Espresso.Domain.ValueObjects.SettingsValueObjects.SimilarArticleSetting", "SimilarArticleSetting", b1 =>
                        {
                            b1.Property<int>("SettingId")
                                .HasColumnType("integer");

                            b1.Property<long>("ArticlePublishDateTimeDifferenceThresholdInMiliseconds")
                                .HasColumnType("bigint");

                            b1.Property<long>("MaxAgeOfSimilarArticleCheckingInMiliseconds")
                                .HasColumnType("bigint");

                            b1.Property<int>("MinimalNumberOfWordsForArticleToBeComparable")
                                .HasColumnType("integer");

                            b1.Property<double>("SimilarityScoreThreshold")
                                .HasColumnType("double precision");

                            b1.HasKey("SettingId");

                            b1.ToTable("Settings");

                            b1.WithOwner()
                                .HasForeignKey("SettingId");
                        });

                    b.Navigation("ArticleSetting")
                        .IsRequired();

                    b.Navigation("JobsSetting")
                        .IsRequired();

                    b.Navigation("NewsPortalSetting")
                        .IsRequired();

                    b.Navigation("SimilarArticleSetting")
                        .IsRequired();
                });

            modelBuilder.Entity("Espresso.Domain.Entities.SimilarArticle", b =>
                {
                    b.HasOne("Espresso.Domain.Entities.Article", "FirstArticle")
                        .WithMany("FirstSimilarArticles")
                        .HasForeignKey("FirstArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Espresso.Domain.Entities.Article", "SecondArticle")
                        .WithMany("SecondSimilarArticles")
                        .HasForeignKey("SecondArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FirstArticle");

                    b.Navigation("SecondArticle");
                });

            modelBuilder.Entity("Espresso.Domain.Entities.Article", b =>
                {
                    b.Navigation("ArticleCategories");

                    b.Navigation("FirstSimilarArticles");

                    b.Navigation("SecondSimilarArticles");
                });

            modelBuilder.Entity("Espresso.Domain.Entities.Category", b =>
                {
                    b.Navigation("ArticleCategories");

                    b.Navigation("NewsPortals");

                    b.Navigation("RssFeeds");
                });

            modelBuilder.Entity("Espresso.Domain.Entities.NewsPortal", b =>
                {
                    b.Navigation("Articles");

                    b.Navigation("NewsPortalImage");

                    b.Navigation("RssFeeds");
                });

            modelBuilder.Entity("Espresso.Domain.Entities.Region", b =>
                {
                    b.Navigation("NewsPortals");
                });

            modelBuilder.Entity("Espresso.Domain.Entities.RssFeed", b =>
                {
                    b.Navigation("Articles");

                    b.Navigation("RssFeedCategories");

                    b.Navigation("RssFeedContentModifiers");
                });
#pragma warning restore 612, 618
        }
    }
}
