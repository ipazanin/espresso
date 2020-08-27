using System.Collections.Generic;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.CategoryEnums;
using Espresso.Domain.Enums.NewsPortalEnums;
using Espresso.Domain.Enums.RssFeedEnums;
using Espresso.Domain.ValueObjects.RssFeedValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.Configuration
{
    public class RssFeedConfiguration : IEntityTypeConfiguration<RssFeed>
    {
        public void Configure(EntityTypeBuilder<RssFeed> builder)
        {
            #region Property Mapping
            builder.Property(rssFeed => rssFeed.Url)
                .HasMaxLength(PropertyConstraintConstants.RssFeedUrlHasMaxLength);
            #endregion

            #region Value Object Mapping

            #region AmpConfiguration
            var ampConfigurationBuilder = builder.OwnsOne(rssFeed => rssFeed.AmpConfiguration);

            ampConfigurationBuilder
                .Property(ampConfiguration => ampConfiguration!.TemplateUrl)
                .HasMaxLength(PropertyConstraintConstants.RssFeedAmpConfigurationTemplateUrlHasMaxLength);

            ampConfigurationBuilder
                .Property(ampConfiguration => ampConfiguration!.HasAmpArticles);
            #endregion

            #region SkipParseConfiguration
            var skipParseConfigurationBuilder = builder.OwnsOne(rssFeed => rssFeed.SkipParseConfiguration);
            #endregion

            #region CategoryParseConfiguration
            var categoryParseConfigurationBuilder = builder
                .OwnsOne(rssFeed => rssFeed.CategoryParseConfiguration);

            categoryParseConfigurationBuilder
                .Property(categoryParseConfiguration => categoryParseConfiguration.CategoryParseStrategy);
            #endregion

            #region ImageUrlParseConfiguration
            var imageUrlParseConfiguration = builder.OwnsOne(rssFeed => rssFeed.ImageUrlParseConfiguration);

            imageUrlParseConfiguration.Property(imageUrlConfig => imageUrlConfig.ImgElementXPath)
                .HasMaxLength(PropertyConstraintConstants.RssFeedImgElementXPathHasMaxLength);

            imageUrlParseConfiguration
                .Property(imageUrlConfig => imageUrlConfig.ImageUrlParseStrategy);

            imageUrlParseConfiguration
                .Property(imageUrlConfig => imageUrlConfig.ShouldImageUrlBeWebScraped)
                .HasDefaultValue(false);
            #endregion

            #endregion

            #region Relations Mapping
            builder.HasOne(rssFeed => rssFeed.NewsPortal)
                .WithMany(newsPortal => newsPortal!.RssFeeds)
                .HasForeignKey(rssFeed => rssFeed.NewsPortalId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(rssFeed => rssFeed.Category)
                .WithMany(category => category!.RssFeeds)
                .HasForeignKey(rssFeed => rssFeed.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(rssFeed => rssFeed.RssFeedCategories)
                .WithOne(rssFeedCategory => rssFeedCategory.RssFeed!)
                .HasForeignKey(rssFeedCategory => rssFeedCategory.RssFeedId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(rssFeed => rssFeed.Articles)
                .WithOne(newArticle => newArticle.RssFeed!)
                .HasForeignKey(rssFeedCategory => rssFeedCategory.RssFeedId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Data Seed
            SeedRssFeeds(builder);
            SeedAmpConfigurations(ampConfigurationBuilder!);
            SeedSkipParseConfiguration(skipParseConfigurationBuilder!);
            SeedCategoryParseConfiguration(categoryParseConfigurationBuilder);
            SeedImageUrlParseConfiguration(imageUrlParseConfiguration);
            #endregion
        }

        private void SeedRssFeeds(EntityTypeBuilder<RssFeed> builder)
        {
            var rssFeeds = new List<RssFeed>
            {
                #region Index
                new RssFeed((int)RssFeedId.Index_Vijesti, "https://www.index.hr/rss/vijesti", (int)NewsPortalId.Index, (int)CategoryId.Vijesti),
                new RssFeed((int)RssFeedId.Index_Sport, "https://www.index.hr/rss/sport", (int)NewsPortalId.Index, (int)CategoryId.Sport),
                new RssFeed((int)RssFeedId.Index_Magazin, "https://www.index.hr/rss/magazin", (int)NewsPortalId.Index, (int)CategoryId.Show),
                new RssFeed((int)RssFeedId.Index_Rogue,  "https://www.index.hr/rss/rouge", (int)NewsPortalId.Index, (int)CategoryId.Lifestyle),
                new RssFeed((int)RssFeedId.Index_Auto, "https://www.index.hr/rss/auto", (int)NewsPortalId.Index, (int)CategoryId.AutoMoto),
                #endregion

                #region 24 sata
                new RssFeed((int)RssFeedId.DvadesetCetiriSata_Vijesti, "https://www.24sata.hr/feeds/news.xml", (int)NewsPortalId.DvadesetCetiriSata, (int)CategoryId.Vijesti),
                new RssFeed((int)RssFeedId.DvadesetCetiriSata_Show, "https://www.24sata.hr/feeds/show.xml", (int)NewsPortalId.DvadesetCetiriSata, (int)CategoryId.Show),
                new RssFeed((int)RssFeedId.DvadesetCetiriSata_Sport, "https://www.24sata.hr/feeds/sport.xml", (int)NewsPortalId.DvadesetCetiriSata, (int)CategoryId.Sport),
                new RssFeed((int)RssFeedId.DvadesetCetiriSata_Lifestyle, "https://www.24sata.hr/feeds/lifestyle.xml", (int)NewsPortalId.DvadesetCetiriSata, (int)CategoryId.Lifestyle),
                new RssFeed((int)RssFeedId.DvadesetCetiriSata_Tech, "https://www.24sata.hr/feeds/tech.xml", (int)NewsPortalId.DvadesetCetiriSata, (int)CategoryId.Tech),
                new RssFeed((int)RssFeedId.DvadesetCetiriSata_Viral, "https://www.24sata.hr/feeds/fun.xml", (int)NewsPortalId.DvadesetCetiriSata, (int)CategoryId.Viral),
                #endregion

                #region Sportske Novosti
                new RssFeed((int)RssFeedId.SportskeNovosti_Sport, "http://sportske.jutarnji.hr/sn/feed", (int)NewsPortalId.SportskeNovosti, (int)CategoryId.Sport),
                #endregion

                #region Jutarnji
                new RssFeed((int)RssFeedId.JutarnjiList, "https://www.jutarnji.hr/feed", (int)NewsPortalId.JutarnjiList, (int)CategoryId.Vijesti),
                #endregion

                #region Net.hr
                new RssFeed((int)RssFeedId.NetHr, "https://net.hr/feed/", (int)NewsPortalId.NetHr, (int)CategoryId.Vijesti),
                #endregion

                #region Slobodna Dalmacija
                new RssFeed((int)RssFeedId.SlobodnaDalmacija_Novosti, "https://www.slobodnadalmacija.hr/feed/category/119", (int)NewsPortalId.SlobodnaDalmacija, (int)CategoryId.Vijesti),
                new RssFeed((int)RssFeedId.SlobodnaDalmacija_Sport, "https://www.slobodnadalmacija.hr/feed/category/255", (int)NewsPortalId.SlobodnaDalmacija, (int)CategoryId.Sport),
                new RssFeed((int)RssFeedId.SlobodnaDalmacija_Showbiz, "https://www.slobodnadalmacija.hr/feed/category/262", (int)NewsPortalId.SlobodnaDalmacija, (int)CategoryId.Show),
                new RssFeed((int)RssFeedId.SlobodnaDalmacija_Trend, "https://www.slobodnadalmacija.hr/feed/category/375", (int)NewsPortalId.SlobodnaDalmacija, (int)CategoryId.Show),
                new RssFeed((int)RssFeedId.SlobodnaDalmacija_DrustvenaMreza, "https://www.slobodnadalmacija.hr/feed/category/263", (int)NewsPortalId.SlobodnaDalmacija, (int)CategoryId.Show),
                new RssFeed((int)RssFeedId.SlobodnaDalmacija_Zivot, "https://www.slobodnadalmacija.hr/feed/category/264", (int)NewsPortalId.SlobodnaDalmacija, (int)CategoryId.Lifestyle),
                new RssFeed((int)RssFeedId.SlobodnaDalmacija_Zdravlje, "https://www.slobodnadalmacija.hr/feed/category/265", (int)NewsPortalId.SlobodnaDalmacija, (int)CategoryId.Lifestyle),
                new RssFeed((int)RssFeedId.SlobodnaDalmacija_Moda, "https://www.slobodnadalmacija.hr/feed/category/266", (int)NewsPortalId.SlobodnaDalmacija, (int)CategoryId.Lifestyle),
                new RssFeed((int)RssFeedId.SlobodnaDalmacija_Ljepota, "https://www.slobodnadalmacija.hr/feed/category/267", (int)NewsPortalId.SlobodnaDalmacija, (int)CategoryId.Lifestyle),
                new RssFeed((int)RssFeedId.SlobodnaDalmacija_Putovanja, "https://www.slobodnadalmacija.hr/feed/category/268", (int)NewsPortalId.SlobodnaDalmacija, (int)CategoryId.Lifestyle),
                new RssFeed((int)RssFeedId.SlobodnaDalmacija_Gastro, "https://www.slobodnadalmacija.hr/feed/category/270", (int)NewsPortalId.SlobodnaDalmacija, (int)CategoryId.Lifestyle),
                new RssFeed((int)RssFeedId.SlobodnaDalmacija_Dom, "https://www.slobodnadalmacija.hr/feed/category/271", (int)NewsPortalId.SlobodnaDalmacija, (int)CategoryId.Lifestyle),
                new RssFeed((int)RssFeedId.SlobodnaDalmacija_Tehnologija, "https://www.slobodnadalmacija.hr/feed/category/269", (int)NewsPortalId.SlobodnaDalmacija, (int)CategoryId.Tech),
                new RssFeed((int)RssFeedId.SlobodnaDalmacija_Viral, "https://www.slobodnadalmacija.hr/feed/category/274", (int)NewsPortalId.SlobodnaDalmacija, (int)CategoryId.Viral),
                #endregion 

                #region TPortal
                new RssFeed((int)RssFeedId.TPortal_Vijesti, "https://www.tportal.hr/rss-vijesti.xml", (int)NewsPortalId.TPortal, (int)CategoryId.Vijesti),
                new RssFeed((int)RssFeedId.TPortal_Biznis, "https://www.tportal.hr/rss-biznis.xml", (int)NewsPortalId.TPortal, (int)CategoryId.Vijesti),
                new RssFeed((int)RssFeedId.TPortal_Sport, "https://www.tportal.hr/rss-sport.xml", (int)NewsPortalId.TPortal, (int)CategoryId.Sport),
                new RssFeed((int)RssFeedId.TPortal_Tehno, "https://www.tportal.hr/rss-tehno.xml", (int)NewsPortalId.TPortal, (int)CategoryId.Tech),
                new RssFeed((int)RssFeedId.TPortal_Showtime, "https://www.tportal.hr/rss-showtime.xml", (int)NewsPortalId.TPortal, (int)CategoryId.Show),
                new RssFeed((int)RssFeedId.TPortal_Lifestyle, "https://www.tportal.hr/rss-lifestyle.xml", (int)NewsPortalId.TPortal, (int)CategoryId.Lifestyle),
                new RssFeed((int)RssFeedId.TPortal_FunBox, "https://www.tportal.hr/rss-funbox.xml", (int)NewsPortalId.TPortal, (int)CategoryId.Viral),
                new RssFeed((int)RssFeedId.TPortal_Kultura, "https://www.tportal.hr/rss-kultura.xml", (int)NewsPortalId.TPortal, (int)CategoryId.Kultura),
                #endregion

                #region Večernji List
                new RssFeed((int)RssFeedId.VecernjiList, "https://www.vecernji.hr/feeds/latest", (int)NewsPortalId.VecernjiList, (int)CategoryId.Vijesti),
                #endregion

                #region Telegram
                new RssFeed((int)RssFeedId.Telegram,"https://www.telegram.hr/feed/", (int)NewsPortalId.Telegram, (int)CategoryId.Vijesti),
                new RssFeed((int)RssFeedId.Telegram_Telesport, "https://telesport.telegram.hr/feed/", (int)NewsPortalId.Telegram, (int)CategoryId.Sport),
                #endregion

                // Dnevnik
                new RssFeed((int)RssFeedId.Dnevnik, "https://dnevnik.hr/assets/feed/articles/", (int)NewsPortalId.Dnevnik, (int)CategoryId.Vijesti),

                // Gol
                new RssFeed((int)RssFeedId.Gol_Sport, "https://gol.dnevnik.hr/assets/feed/articles", (int)NewsPortalId.Gol, (int)CategoryId.Sport),

                // Rtl Vijesti
                new RssFeed((int)RssFeedId.RtlVijesti_Sport, "https://sportnet.rtl.hr/rss/", (int)NewsPortalId.RtlVijesti, (int)CategoryId.Sport),
                
                // Sprdex
                //new RssFeed((int)RssFeedId.Sprdex_ZabavnaSatira, "http://sprdex.net.hr/feed/", (int)NewsPortalId.Sprdex, (int)CategoryId.Viral),
                
                // Nogometni Plus
                new RssFeed((int)RssFeedId.NogometPlus_Nogomet, "http://www.nogometplus.net/index.php/feed/", (int)NewsPortalId.NogometPlus, (int)CategoryId.Sport),
                
                // Lider
                new RssFeed((int)RssFeedId.Lider_BiznisIPolitikaHrvatska, "http://lider.media/feed/", (int)NewsPortalId.Lider, (int)CategoryId.Biznis),
                new RssFeed((int)RssFeedId.Lider_BiznisIPolitikaSvijet, "http://lider.media/feed/", (int)NewsPortalId.Lider, (int)CategoryId.Biznis),
                new RssFeed((int)RssFeedId.Lider_Trziste, "http://lider.media/feed/", (int)NewsPortalId.Lider, (int)CategoryId.Biznis),
                
                // Bug
                new RssFeed((int)RssFeedId.Bug_TechVijesti, "http://www.bug.hr/rss/vijesti/", (int)NewsPortalId.Bug, (int)CategoryId.Tech),

                // Vidi.Hr
                new RssFeed((int)RssFeedId.VidiHr_TechVijesti, "http://www.vidi.hr/rss/feed/vidi", (int)NewsPortalId.VidiHr, (int)CategoryId.Tech),

                // Zimo
                new RssFeed((int)RssFeedId.Zimo_TechVijesti, "https://zimo.dnevnik.hr/assets/feed/articles", (int)NewsPortalId.Zimo, (int)CategoryId.Tech),

                // Netokracija
                new RssFeed((int)RssFeedId.Netokracija, "http://www.netokracija.com/feed", (int)NewsPortalId.Netokracija, (int)CategoryId.Tech),

                // Poslovni Plus
                new RssFeed((int)RssFeedId.PoslovniPuls, "http://www.poslovnipuls.com/feed/", (int)NewsPortalId.PoslovniPuls, (int)CategoryId.Biznis),
                
                // PCChip
                new RssFeed((int)RssFeedId.PcChip, "http://pcchip.hr/feed/", (int)NewsPortalId.PcChip, (int)CategoryId.Tech),
                
                // Cosmopolitan
                new RssFeed((int)RssFeedId.Cosmopolitan, "http://www.cosmopolitan.hr/feed", (int)NewsPortalId.Cosmopolitan, (int)CategoryId.Lifestyle),
                
                #region Wall.hr
                new RssFeed((int)RssFeedId.WallHr, "http://wall.hr/cdn/feed.xml", (int)NewsPortalId.WallHr, (int)CategoryId.Lifestyle),
                #endregion
                
                #region Ljepota i zdravlje
                new RssFeed((int)RssFeedId.LjepotaIZdravlje, "http://www.ljepotaizdravlje.hr/feed", (int)NewsPortalId.LjepotaIZdravlje, (int)CategoryId.Lifestyle),
                #endregion

                #region Autonet
                new RssFeed((int)RssFeedId.AutoNet, "https://www.autonet.hr/feed/", (int)NewsPortalId.Autonet, (int)CategoryId.AutoMoto),
                #endregion

                #region N1
                new RssFeed((int)RssFeedId.N1, "http://hr.n1info.com/rss/249/Naslovna", (int)NewsPortalId.N1, (int)CategoryId.Vijesti),
                #endregion

                #region NarodHr
                new RssFeed((int)RssFeedId.NarodHr, "https://narod.hr/feed", (int)NewsPortalId.NarodHr, (int)CategoryId.Vijesti),
                #endregion

                #region Hrt
                new RssFeed(
                    id: (int)RssFeedId.Hrt_Vijesti,
                    url: "https://www.hrt.hr/rss/vijesti/",
                    newsPortalId: (int)NewsPortalId.Hrt,
                    categoryId: (int)CategoryId.Vijesti
                ),
                new RssFeed(
                    id: (int)RssFeedId.Hrt_Sport,
                    url: "https://www.hrt.hr/rss/sport/",
                    newsPortalId: (int)NewsPortalId.Hrt,
                    categoryId: (int)CategoryId.Sport
                ),
                new RssFeed(
                    id: (int)RssFeedId.Hrt_Magazin,
                    url: "https://magazin.hrt.hr/feed.xml",
                    newsPortalId: (int)NewsPortalId.Hrt,
                    categoryId: (int)CategoryId.Show
                ),
                new RssFeed(
                    id: (int)RssFeedId.Hrt_Glazba,
                    url: "https://www.hrt.hr/rss/glazba/",
                    newsPortalId: (int)NewsPortalId.Hrt,
                    categoryId: (int)CategoryId.Show
                ),
                #endregion

                #region  100posto
                new RssFeed(
                    id: (int)RssFeedId.StoPosto,
                    url: "https://100posto.jutarnji.hr/rss",
                    newsPortalId: (int)NewsPortalId.StoPosto,
                    categoryId: (int)CategoryId.Vijesti
                ),
                #endregion

                #region  Dnevno
                new RssFeed(
                    id: (int)RssFeedId.Dnevno,
                    url: "https://www.dnevno.hr/feed/",
                    newsPortalId: (int)NewsPortalId.Dnevno,
                    categoryId: (int)CategoryId.Vijesti
                ),
                #endregion

                #region AutomobiliHr
                // new RssFeed(
                //     id: (int)RssFeedId.AutomobiliHr,
                //     url: "https://klik.hr/rss",
                //     newsPortalId: (int)NewsPortalId.AutomobiliHr,
                //     categoryId: (int)CategoryId.AutoMoto
                // ),
                #endregion

                #region DirektnoHr
                new RssFeed(
                    id: (int)RssFeedId.DirektnoHr_Direkt,
                    url: "https://direktno.hr/rss/publish/latest/direkt-50/",
                    newsPortalId: (int)NewsPortalId.DirektnoHr,
                    categoryId: (int)CategoryId.Vijesti
                ),
                new RssFeed(
                    id: (int)RssFeedId.DirektnoHr_Domovina,
                    url: "https://direktno.hr/rss/publish/latest/domovina-10/",
                    newsPortalId: (int)NewsPortalId.DirektnoHr,
                    categoryId: (int)CategoryId.Vijesti
                ),
                new RssFeed(
                    id: (int)RssFeedId.DirektnoHr_EuSvijet,
                    url: "https://direktno.hr/rss/publish/latest/eu_svijet/",
                    newsPortalId: (int)NewsPortalId.DirektnoHr,
                    categoryId: (int)CategoryId.Vijesti
                ),
                new RssFeed(
                    id: (int)RssFeedId.DirektnoHr_Razvoj,
                    url: "https://direktno.hr/rss/publish/latest/razvoj-110/",
                    newsPortalId: (int)NewsPortalId.DirektnoHr,
                    categoryId: (int)CategoryId.Biznis
                ),
                new RssFeed(
                    id: (int)RssFeedId.DirektnoHr_Sport,
                    url: "https://direktno.hr/rss/publish/latest/sport-60/",
                    newsPortalId: (int)NewsPortalId.DirektnoHr,
                    categoryId: (int)CategoryId.Sport
                ),
                new RssFeed(
                    id: (int)RssFeedId.DirektnoHr_Zivot,
                    url: "https://direktno.hr/rss/publish/latest/zivot-70/",
                    newsPortalId: (int)NewsPortalId.DirektnoHr,
                    categoryId: (int)CategoryId.Show
                ),
                new RssFeed(
                    id: (int)RssFeedId.DirektnoHr_Kolumne,
                    url: "https://direktno.hr/rss/publish/latest/kolumne-80/",
                    newsPortalId: (int)NewsPortalId.DirektnoHr,
                    categoryId: (int)CategoryId.Vijesti
                ),
                new RssFeed(
                    id: (int)RssFeedId.DirektnoHr_Direktno,
                    url: "https://direktno.hr/rss/publish/latest/direktnotv-100/",
                    newsPortalId: (int)NewsPortalId.DirektnoHr,
                    categoryId: (int)CategoryId.Vijesti
                ),                                                                                
                #endregion

                #region Scena
                new RssFeed(
                    id: (int)RssFeedId.Scena,
                    url: "https://www.scena.hr/feed/",
                    newsPortalId: (int)NewsPortalId.Scena,
                    categoryId: (int)CategoryId.Vijesti
                ),
                #endregion

                #region Nacional
                new RssFeed(
                    id: (int)RssFeedId.Nacional,
                    url: "https://www.nacional.hr/feed/",
                    newsPortalId: (int)NewsPortalId.Nacional,
                    categoryId: (int)CategoryId.Vijesti
                ),
                #endregion
            };

            builder.HasData(rssFeeds);

            var localRssFeeds = new List<RssFeed>
            {
                #region Dalmacija Danas
                new RssFeed(
                    id: (int)RssFeedId.DalmacijaDanas,
                    url: "https://www.dalmacijadanas.hr/feed/",
                    newsPortalId: (int)NewsPortalId.DalmacijaDanas,
                    categoryId: (int)CategoryId.Local
                ),
                #endregion
            };

            builder.HasData(localRssFeeds);

        }

        private void SeedAmpConfigurations(OwnedNavigationBuilder<RssFeed, AmpConfiguration> ampConfigurationBuilder)
        {
            #region Index
            ampConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Index_Vijesti, HasAmpArticles = true, TemplateUrl = "https://amp.index.hr/article/{0}{3}" });
            ampConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Index_Sport, HasAmpArticles = true, TemplateUrl = "https://amp.index.hr/article/{0}{3}" });
            ampConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Index_Rogue, HasAmpArticles = true, TemplateUrl = "https://amp.index.hr/article/{0}{3}" });
            ampConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Index_Magazin, HasAmpArticles = true, TemplateUrl = "https://amp.index.hr/article/{0}{3}" });
            ampConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Index_Auto, HasAmpArticles = true, TemplateUrl = "https://amp.index.hr/article/{0}{3}" });
            #endregion

            #region 24 Sata
            //ampConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Lifestyle, HasAmpArticles = true, TemplateUrl = "https://m.24sata.hr/amp/{1}{2}" });
            //ampConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Show, HasAmpArticles = true, TemplateUrl = "https://m.24sata.hr/amp/{1}{2}" });
            //ampConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Sport, HasAmpArticles = true, TemplateUrl = "https://m.24sata.hr/amp/{1}{2}" });
            //ampConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Tech, HasAmpArticles = true, TemplateUrl = "https://m.24sata.hr/amp/{1}{2}" });
            //ampConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Vijesti, HasAmpArticles = true, TemplateUrl = "https://m.24sata.hr/amp/{1}{2}" });
            //ampConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Viral, HasAmpArticles = true, TemplateUrl = "https://m.24sata.hr/amp/{1}{2}" });
            ampConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Lifestyle, HasAmpArticles = false, TemplateUrl = (string?)null });
            ampConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Show, HasAmpArticles = false, TemplateUrl = (string?)null });
            ampConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Sport, HasAmpArticles = false, TemplateUrl = (string?)null });
            ampConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Tech, HasAmpArticles = false, TemplateUrl = (string?)null });
            ampConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Vijesti, HasAmpArticles = false, TemplateUrl = (string?)null });
            ampConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Viral, HasAmpArticles = false, TemplateUrl = (string?)null });
            #endregion

            #region NetHr
            ampConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.NetHr, HasAmpArticles = true, TemplateUrl = "https://net.hr/{1}{2}{3}amp" });
            #endregion

            #region Vecernji List
            ampConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.VecernjiList, HasAmpArticles = true, TemplateUrl = "https://m.vecernji.hr/amp/{1}{2}" });
            #endregion

            #region Dnevnik
            ampConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Dnevnik, HasAmpArticles = true, TemplateUrl = "https://dnevnik.hr/amp/{1}{2}{3}" });
            #endregion

            #region  Gol
            ampConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Gol_Sport, HasAmpArticles = true, TemplateUrl = "https://gol.dnevnik.hr/amp/{1}{2}{3}{4}" });
            #endregion

            #region  Zimo
            ampConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Zimo_TechVijesti, HasAmpArticles = true, TemplateUrl = "https://zimo.dnevnik.hr/amp/clanak/{2}" });
            #endregion

            #region Netokracija
            ampConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Netokracija, HasAmpArticles = true, TemplateUrl = "https://www.netokracija.com/{1}/amp" });
            #endregion
        }

        private void SeedSkipParseConfiguration(OwnedNavigationBuilder<RssFeed, SkipParseConfiguration> skipParseConfigurationBuilder)
        {
            #region Jutarnji List
            skipParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.JutarnjiList, NumberOfSkips = 5, CurrentSkip = 0 });
            #endregion

            #region Slobodna Dalmacija
            skipParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Novosti, NumberOfSkips = 5, CurrentSkip = 0 });
            skipParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Sport, NumberOfSkips = 5, CurrentSkip = 0 });
            skipParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Showbiz, NumberOfSkips = 5, CurrentSkip = 0 });
            skipParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Trend, NumberOfSkips = 5, CurrentSkip = 0 });
            skipParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_DrustvenaMreza, NumberOfSkips = 5, CurrentSkip = 0 });
            skipParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Zivot, NumberOfSkips = 5, CurrentSkip = 0 });
            skipParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Zdravlje, NumberOfSkips = 5, CurrentSkip = 0 });
            skipParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Moda, NumberOfSkips = 5, CurrentSkip = 0 });
            skipParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Ljepota, NumberOfSkips = 5, CurrentSkip = 0 });
            skipParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Putovanja, NumberOfSkips = 5, CurrentSkip = 0 });
            skipParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Gastro, NumberOfSkips = 5, CurrentSkip = 0 });
            skipParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Dom, NumberOfSkips = 5, CurrentSkip = 0 });
            skipParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Tehnologija, NumberOfSkips = 5, CurrentSkip = 0 });
            skipParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Viral, NumberOfSkips = 5, CurrentSkip = 0 });
            #endregion

            #region Poslovni Puls
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.PoslovniPuls,
                NumberOfSkips = 10,
                CurrentSkip = 0
            });
            #endregion

            #region 100posto
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.StoPosto,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            #endregion
        }

        private void SeedCategoryParseConfiguration(OwnedNavigationBuilder<RssFeed, CategoryParseConfiguration> categoryParseConfigurationBuilder)
        {
            #region Index
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Index_Vijesti, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Index_Sport, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Index_Magazin, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Index_Rogue, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Index_Auto, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });
            #endregion

            #region 24 sata
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Vijesti, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Show, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Sport, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Lifestyle, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Tech, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Viral, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });
            #endregion

            #region Sportske Novosti
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SportskeNovosti_Sport, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });
            #endregion

            #region  Jutarnji
            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.JutarnjiList,
                CategoryParseStrategy = CategoryParseStrategy.FromUrl

            });
            #endregion

            #region  Net.hr
            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.NetHr,
                CategoryParseStrategy = CategoryParseStrategy.FromUrl

            });
            #endregion

            #region Slobodna Dalmacija
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Novosti, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Sport, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Showbiz, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Trend, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_DrustvenaMreza, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Zivot, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Zdravlje, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Moda, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Ljepota, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Putovanja, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Gastro, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Dom, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Tehnologija, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Viral, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });
            #endregion

            #region TPortal
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_Vijesti, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_Biznis, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_Sport, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_Tehno, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_Showtime, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_Lifestyle, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_FunBox, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_Kultura, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });
            #endregion

            #region Večernji List
            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.VecernjiList,
                CategoryParseStrategy = CategoryParseStrategy.FromUrl

            });
            #endregion

            #region Telegram
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Telegram, CategoryParseStrategy = CategoryParseStrategy.FromUrl });
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Telegram_Telesport, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });
            #endregion

            // Dnevnik
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Dnevnik, CategoryParseStrategy = CategoryParseStrategy.FromUrl });

            // Gol
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Gol_Sport, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });

            // Rtl Vijesti
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.RtlVijesti_Sport, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });

            // Nogometni Plus
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.NogometPlus_Nogomet, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });

            // Lider
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Lider_BiznisIPolitikaHrvatska, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Lider_BiznisIPolitikaSvijet, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Lider_Trziste, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });

            // Bug
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Bug_TechVijesti, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });

            // Vidi.Hr
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.VidiHr_TechVijesti, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });

            // Zimo
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Zimo_TechVijesti, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });

            // Netokracija
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Netokracija, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });

            // Poslovni Plus
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.PoslovniPuls, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });

            // PCChip
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.PcChip, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });

            // Cosmopolitan
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Cosmopolitan, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });

            // Wall.hr
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.WallHr, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });

            // Ljepota i zdravlje
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.LjepotaIZdravlje, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });

            #region Autonet
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.AutoNet, CategoryParseStrategy = CategoryParseStrategy.FromRssFeed });
            #endregion

            #region N1
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.N1, CategoryParseStrategy = CategoryParseStrategy.FromUrl });
            #endregion

            #region NarodHr
            categoryParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.NarodHr, CategoryParseStrategy = CategoryParseStrategy.FromUrl });
            #endregion

            #region Hrt
            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Hrt_Vijesti,
                CategoryParseStrategy = CategoryParseStrategy.FromRssFeed,
                UrlSegmentIndex = (int?)null,
            });
            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Hrt_Sport,
                CategoryParseStrategy = CategoryParseStrategy.FromRssFeed,
                UrlSegmentIndex = (int?)null,
            });
            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Hrt_Magazin,
                CategoryParseStrategy = CategoryParseStrategy.FromRssFeed,
                UrlSegmentIndex = (int?)null,
            });
            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Hrt_Glazba,
                CategoryParseStrategy = CategoryParseStrategy.FromRssFeed,
                UrlSegmentIndex = (int?)null,
            });
            #endregion

            #region 100posto
            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.StoPosto,
                CategoryParseStrategy = CategoryParseStrategy.FromUrl

            });
            #endregion

            #region Dnevno
            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Dnevno,
                CategoryParseStrategy = CategoryParseStrategy.FromUrl

            });
            #endregion

            #region AutomobiliHr
            // categoryParseConfigurationBuilder.HasData(new
            // {
            //     RssFeedId = (int)RssFeedId.Dnevno,
            //     CategoryParseStrategy = CategoryParseStrategy.FromUrl
            //     
            // });
            #endregion

            #region DirektHr
            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DirektnoHr_Direkt,
                CategoryParseStrategy = CategoryParseStrategy.FromRssFeed,
                UrlSegmentIndex = (int?)null,
            });
            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DirektnoHr_Direktno,
                CategoryParseStrategy = CategoryParseStrategy.FromRssFeed,
                UrlSegmentIndex = (int?)null,
            });
            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DirektnoHr_Domovina,
                CategoryParseStrategy = CategoryParseStrategy.FromRssFeed,
                UrlSegmentIndex = (int?)null,
            });
            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DirektnoHr_EuSvijet,
                CategoryParseStrategy = CategoryParseStrategy.FromRssFeed,
                UrlSegmentIndex = (int?)null,
            });
            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DirektnoHr_Kolumne,
                CategoryParseStrategy = CategoryParseStrategy.FromRssFeed,
                UrlSegmentIndex = (int?)null,
            });
            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DirektnoHr_Razvoj,
                CategoryParseStrategy = CategoryParseStrategy.FromRssFeed,
                UrlSegmentIndex = (int?)null,
            });
            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DirektnoHr_Sport,
                CategoryParseStrategy = CategoryParseStrategy.FromRssFeed,
                UrlSegmentIndex = (int?)null,
            });
            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DirektnoHr_Zivot,
                CategoryParseStrategy = CategoryParseStrategy.FromRssFeed,
                UrlSegmentIndex = (int?)null,
            });
            #endregion

            #region Scena
            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Scena,
                CategoryParseStrategy = CategoryParseStrategy.FromUrl,
                UrlSegmentIndex = 1
            });
            #endregion

            #region Nacional
            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Nacional,
                CategoryParseStrategy = CategoryParseStrategy.FromRssFeed,
                UrlSegmentIndex = (int?)null
            });
            #endregion

            #region Local NewsPortals

            #region Dalmacija Danas
            categoryParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DalmacijaDanas,
                CategoryParseStrategy = CategoryParseStrategy.FromRssFeed,
                UrlSegmentIndex = (int?)null
            });
            #endregion

            #endregion
        }

        private void SeedImageUrlParseConfiguration(OwnedNavigationBuilder<RssFeed, ImageUrlParseConfiguration> imageUrlParseConfigurationBuilder)
        {
            #region Index
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Index_Vijesti, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'img-large loaded')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Index_Sport, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'img-large loaded')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Index_Magazin, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'img-large loaded')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Index_Rogue, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'img-large loaded')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Index_Auto, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'img-large loaded')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            #endregion

            #region 24 sata
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Vijesti, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'article__figure_img')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Show, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'article__figure_img')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Sport, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'article__figure_img')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Lifestyle, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'article__figure_img')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Tech, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'article__figure_img')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.DvadesetCetiriSata_Viral, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'article__figure_img')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            #endregion

            #region Sportske Novosti
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SportskeNovosti_Sport, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'media-object adaptive lazy')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            #endregion

            #region Jutarnji
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.JutarnjiList, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'media-object adaptive lazy')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            #endregion

            #region Net.hr
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.NetHr, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//div[contains(@class, 'featured-img')]//img", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            #endregion

            #region Slobodna Dalmacija
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Novosti, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'card__image')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Sport, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'card__image')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Showbiz, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'card__image')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Trend, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'card__image')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_DrustvenaMreza, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'card__image')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Zivot, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'card__image')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Zdravlje, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'card__image')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Moda, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'card__image')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Ljepota, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'card__image')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Putovanja, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'card__image')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Gastro, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'card__image')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Dom, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'card__image')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Tehnologija, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'card__image')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Viral, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'card__image')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            #endregion

            #region TPortal
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_Vijesti, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'lateImage lateImageLoaded')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_Biznis, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'lateImage lateImageLoaded')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_Sport, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'lateImage lateImageLoaded')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_Tehno, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'lateImage lateImageLoaded')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_Showtime, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'lateImage lateImageLoaded')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_Lifestyle, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'lateImage lateImageLoaded')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_FunBox, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'lateImage lateImageLoaded')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.TPortal_Kultura, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'lateImage lateImageLoaded')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            #endregion

            #region Večernji List
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.VecernjiList, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//script[contains(@type, 'application/ld+json')]", ShouldImageUrlBeWebScraped = true, ImageUrlWebScrapeType = ImageUrlWebScrapeType.JsonObjectInScriptElement, JsonWebScrapePropertyNames = "image,url" });
            #endregion

            #region  Telegram
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Telegram, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//div[contains(@class, 'thumb')]//img", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Telegram_Telesport, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//div[contains(@class, 'featured-img')]//img", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            #endregion

            #region Dnevnik
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Dnevnik, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//figure[contains(@class, 'article-main-img')]//img", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            #endregion

            #region  Gol
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Gol_Sport, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//figure[contains(@class, 'article-image main-image')]//img", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            #endregion

            #region Rtl Vijesti
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.RtlVijesti_Sport, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'naslovna')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            #endregion

            #region Nogometni Plus
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.NogometPlus_Nogomet, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//div[contains(@class, 'post-img')]//img", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            #endregion

            #region  Lider
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Lider_BiznisIPolitikaHrvatska, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'card__image')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Lider_BiznisIPolitikaSvijet, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'card__image')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Lider_Trziste, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//img[contains(@class, 'card__image')]", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            #endregion

            #region  Bug
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Bug_TechVijesti, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//div[contains(@class, 'entry-content')]//img", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            #endregion

            #region Vidi.Hr
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.VidiHr_TechVijesti, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//div[contains(@class, 'attribute-image')]//img", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            #endregion

            #region Zimo
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Zimo_TechVijesti, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//div[contains(@class, 'img-holder')]//img", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            #endregion

            #region Netokracija
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Netokracija, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//div[contains(@class, 'post__hero')]//img", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            #endregion

            #region Poslovni Plus
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.PoslovniPuls, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//div[contains(@class, 'postFeaturedImg postFeaturedImg--single')]//img", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            #endregion

            #region PCChip
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.PcChip, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//div[contains(@class, 'td-post-featured-image')]//img", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            #endregion

            #region Cosmopolitan
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.Cosmopolitan, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//div[contains(@class, 'first-image')]//img", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            #endregion

            #region Wall.hr
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.WallHr, ImageUrlParseStrategy = ImageUrlParseStrategy.FromContent, ImgElementXPath = "//figure[contains(@class, 'dcms-image article-image')]//img", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            #endregion

            #region  Ljepota i zdravlje
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.LjepotaIZdravlje, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//div[contains(@class, 'post-thumbnail')]//img", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            #endregion

            #region Autonet
            imageUrlParseConfigurationBuilder.HasData(new { RssFeedId = (int)RssFeedId.AutoNet, ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary, ImgElementXPath = "//figure[contains(@class, 'figure')]//img", ShouldImageUrlBeWebScraped = false, ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute, JsonWebScrapePropertyNames = (string?)null });
            #endregion

            #region N1        
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.N1,
                ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary,
                ImgElementXPath = "//figure[contains(@class, 'media')]//img",
                ShouldImageUrlBeWebScraped = false,
                ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute,
                JsonWebScrapePropertyNames = (string?)null
            });
            #endregion

            #region NarodHr        
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.NarodHr,
                ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary,
                ImgElementXPath = "//div[contains(@class, 'td-post-featured-image')]//img",
                ShouldImageUrlBeWebScraped = false,
                ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute,
                JsonWebScrapePropertyNames = (string?)null
            });
            #endregion

            #region Hrt        
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Hrt_Vijesti,
                ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary,
                ImgElementXPath = "//div[contains(@class, 'image-slider')]//img",
                ShouldImageUrlBeWebScraped = false,
                ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute,
                JsonWebScrapePropertyNames = (string?)null
            });
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Hrt_Sport,
                ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary,
                ImgElementXPath = "//div[contains(@class, 'image-slider')]//img",
                ShouldImageUrlBeWebScraped = false,
                ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute,
                JsonWebScrapePropertyNames = (string?)null
            });
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Hrt_Magazin,
                ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary,
                ImgElementXPath = "//div[contains(@class, 'image-slider')]//img",
                ShouldImageUrlBeWebScraped = false,
                ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute,
                JsonWebScrapePropertyNames = (string?)null
            });
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Hrt_Glazba,
                ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary,
                ImgElementXPath = "//div[contains(@class, 'image-slider')]//img",
                ShouldImageUrlBeWebScraped = false,
                ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute,
                JsonWebScrapePropertyNames = (string?)null
            });
            #endregion

            #region 100posto
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.StoPosto,
                ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary,
                ImgElementXPath = "//picture[contains(@class, 'pic')]//img",
                ShouldImageUrlBeWebScraped = false,
                ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute,
                JsonWebScrapePropertyNames = (string?)null
            });
            #endregion

            #region Dnevno
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Dnevno,
                ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary,
                ImgElementXPath = "//div[contains(@class, 'img-holder inner')]//img",
                ShouldImageUrlBeWebScraped = false,
                ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute,
                JsonWebScrapePropertyNames = (string?)null
            });
            #endregion

            #region DirektHr
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DirektnoHr_Direkt,
                ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary,
                ImgElementXPath = "//div[contains(@class, 'pd-hero-image')]//img",
                ShouldImageUrlBeWebScraped = false,
                ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute,
                JsonWebScrapePropertyNames = (string?)null
            });
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DirektnoHr_Direktno,
                ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary,
                ImgElementXPath = "//div[contains(@class, 'pd-hero-image')]//img",
                ShouldImageUrlBeWebScraped = false,
                ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute,
                JsonWebScrapePropertyNames = (string?)null
            });
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DirektnoHr_Domovina,
                ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary,
                ImgElementXPath = "//div[contains(@class, 'pd-hero-image')]//img",
                ShouldImageUrlBeWebScraped = false,
                ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute,
                JsonWebScrapePropertyNames = (string?)null
            });
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DirektnoHr_EuSvijet,
                ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary,
                ImgElementXPath = "//div[contains(@class, 'pd-hero-image')]//img",
                ShouldImageUrlBeWebScraped = false,
                ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute,
                JsonWebScrapePropertyNames = (string?)null
            });
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DirektnoHr_Kolumne,
                ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary,
                ImgElementXPath = "//div[contains(@class, 'pd-hero-image')]//img",
                ShouldImageUrlBeWebScraped = false,
                ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute,
                JsonWebScrapePropertyNames = (string?)null
            });
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DirektnoHr_Razvoj,
                ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary,
                ImgElementXPath = "//div[contains(@class, 'pd-hero-image')]//img",
                ShouldImageUrlBeWebScraped = false,
                ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute,
                JsonWebScrapePropertyNames = (string?)null
            });
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DirektnoHr_Sport,
                ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary,
                ImgElementXPath = "//div[contains(@class, 'pd-hero-image')]//img",
                ShouldImageUrlBeWebScraped = false,
                ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute,
                JsonWebScrapePropertyNames = (string?)null
            });
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DirektnoHr_Zivot,
                ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary,
                ImgElementXPath = "//div[contains(@class, 'pd-hero-image')]//img",
                ShouldImageUrlBeWebScraped = false,
                ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute,
                JsonWebScrapePropertyNames = (string?)null
            });
            #endregion

            #region Scena
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Scena,
                ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary,
                ImgElementXPath = "//div[contains(@class, 'mycontent')]//img",
                ShouldImageUrlBeWebScraped = false,
                ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute,
                JsonWebScrapePropertyNames = (string?)null
            });
            #endregion

            #region Nacional
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Nacional,
                ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary,
                ImgElementXPath = "//div[contains(@class, 'single-post-media')]//img",
                ShouldImageUrlBeWebScraped = false,
                ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute,
                JsonWebScrapePropertyNames = (string?)null
            });
            #endregion

            #region Local NewsPortals

            #region Dalmacija Danas
            imageUrlParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.DalmacijaDanas,
                ImageUrlParseStrategy = ImageUrlParseStrategy.SecondLinkOrFromSummary,
                ImgElementXPath = "//div[contains(@class, 'td-full-screen-header-image-wrap')]//img",
                ShouldImageUrlBeWebScraped = false,
                ImageUrlWebScrapeType = ImageUrlWebScrapeType.SrcAttribute,
                JsonWebScrapePropertyNames = (string?)null
            });
            #endregion

            #endregion
        }
    }
}
