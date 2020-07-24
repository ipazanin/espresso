using System.Collections.Generic;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.CategoryEnums;
using Espresso.Domain.Enums.RssFeedEnums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.Configuration
{
    public class RssFeedCategoryConfiguration : IEntityTypeConfiguration<RssFeedCategory>
    {
        public void Configure(EntityTypeBuilder<RssFeedCategory> builder)
        {
            #region Property Mapping
            builder.Property(rssFeedcategory => rssFeedcategory.UrlRegex)
                .HasMaxLength(PropertyConstraintConstants.RssFeedCategoryUrlRegexHasMaxLength)
                .IsRequired(PropertyConstraintConstants.RssFeedCategoryUrlRegexIsRequired);
            #endregion

            #region Relationship Mapping
            builder.HasOne(rssFeedCategory => rssFeedCategory.RssFeed)
                .WithMany(rssFeed => rssFeed!.RssFeedCategories)
                .HasForeignKey(rssFeedCategory => rssFeedCategory.RssFeedId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Data Seed
            Seed(builder);
            #endregion
        }

        private void Seed(EntityTypeBuilder<RssFeedCategory> builder)
        {
            var rssFeedCategories = new List<RssFeedCategory>
            {
                #region Jutarnji List
                new RssFeedCategory(1, "vijesti", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.JutarnjiList),
                new RssFeedCategory(2, "autoklub", urlSegmentIndex: 1, (int)CategoryId.AutoMoto, (int)RssFeedId.JutarnjiList),
                new RssFeedCategory(3, "kultura", urlSegmentIndex: 1, (int)CategoryId.Kultura, (int)RssFeedId.JutarnjiList),

                new RssFeedCategory(13, "globus", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.JutarnjiList),
                new RssFeedCategory(14, "domidizajn", urlSegmentIndex: 1, (int)CategoryId.Lifestyle, (int)RssFeedId.JutarnjiList),
                new RssFeedCategory(15, "viral", urlSegmentIndex: 1, (int)CategoryId.Viral, (int)RssFeedId.JutarnjiList),
                new RssFeedCategory(16, "spektakli", urlSegmentIndex: 1, (int)CategoryId.Show, (int)RssFeedId.JutarnjiList),
                new RssFeedCategory(17, "life", urlSegmentIndex: 1, (int)CategoryId.Lifestyle, (int)RssFeedId.JutarnjiList),
                #endregion

                #region  Vecernji List
                new RssFeedCategory(4, "vijesti", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.VecernjiList),
                new RssFeedCategory(5, "sport", urlSegmentIndex: 1, (int)CategoryId.Sport, (int)RssFeedId.VecernjiList),
                new RssFeedCategory(6, "showbiz", urlSegmentIndex: 1, (int)CategoryId.Show, (int)RssFeedId.VecernjiList),
                new RssFeedCategory(7, "lifestyle", urlSegmentIndex: 1, (int)CategoryId.Lifestyle, (int)RssFeedId.VecernjiList),
                new RssFeedCategory(8, "biznis", urlSegmentIndex: 1, (int)CategoryId.Biznis, (int)RssFeedId.VecernjiList),
                new RssFeedCategory(9, "techsci", urlSegmentIndex: 1, (int)CategoryId.Tech, (int)RssFeedId.VecernjiList),
                new RssFeedCategory(10, "automoto", urlSegmentIndex: 1, (int)CategoryId.AutoMoto, (int)RssFeedId.VecernjiList),
                new RssFeedCategory(11, "kultura", urlSegmentIndex: 1, (int)CategoryId.Kultura, (int)RssFeedId.VecernjiList),
                #endregion

                #region Dnevnik
                new RssFeedCategory(12, "vijesti", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.Dnevnik),
                #endregion

                 #region  Telegram
                new RssFeedCategory(18, "politika-kriminal", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.Telegram),
                new RssFeedCategory(19, "zivot", urlSegmentIndex: 1, (int)CategoryId.Lifestyle, (int)RssFeedId.Telegram),
                #endregion 

                #region N1
                new RssFeedCategory(20, "Vijesti", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.N1),
                new RssFeedCategory(21, "Svijet", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.N1),
                new RssFeedCategory(22, "Znanost", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.N1),
                new RssFeedCategory(23, "Regija", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.N1),
                new RssFeedCategory(24, "Dnevnik", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.N1),
                new RssFeedCategory(25, "Info", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.N1),
                new RssFeedCategory(26, "Biznis", urlSegmentIndex: 1, (int)CategoryId.Biznis, (int)RssFeedId.N1),
                new RssFeedCategory(27, "Lifestyle", urlSegmentIndex: 1, (int)CategoryId.Lifestyle, (int)RssFeedId.N1),
                new RssFeedCategory(28, "Zdravlje", urlSegmentIndex: 1, (int)CategoryId.Lifestyle, (int)RssFeedId.N1),
                new RssFeedCategory(29, "Sport-Klub", urlSegmentIndex: 1, (int)CategoryId.Sport, (int)RssFeedId.N1),
                new RssFeedCategory(30, "Showbiz", urlSegmentIndex: 1, (int)CategoryId.Show, (int)RssFeedId.N1),
                new RssFeedCategory(31, "Tehnologija", urlSegmentIndex: 1, (int)CategoryId.Tech, (int)RssFeedId.N1),
                new RssFeedCategory(32, "Kultura", urlSegmentIndex: 1, (int)CategoryId.Kultura, (int)RssFeedId.N1),
                #endregion

                #region NarodHr
                new RssFeedCategory(33, "Hrvatska", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.NarodHr),
                new RssFeedCategory(34, "Sport", urlSegmentIndex: 1, (int)CategoryId.Sport, (int)RssFeedId.NarodHr),
                new RssFeedCategory(35, "Kultura", urlSegmentIndex: 1, (int)CategoryId.Kultura, (int)RssFeedId.NarodHr),
                #endregion

                #region 100posto
                new RssFeedCategory(36, "zivot", urlSegmentIndex: 1, (int)CategoryId.Lifestyle, (int)RssFeedId.StoPosto),
                new RssFeedCategory(37, "news", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.StoPosto),
                new RssFeedCategory(38, "scena", urlSegmentIndex: 1, (int)CategoryId.Show, (int)RssFeedId.StoPosto),
                new RssFeedCategory(39, "bubble", urlSegmentIndex: 1, (int)CategoryId.Lifestyle, (int)RssFeedId.StoPosto),
                #endregion

                #region Dnevno
                new RssFeedCategory(40, "vijesti", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.Dnevno),
                new RssFeedCategory(41, "sport", urlSegmentIndex: 1, (int)CategoryId.Sport, (int)RssFeedId.Dnevno),
                new RssFeedCategory(42, "domovina", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.Dnevno),
                new RssFeedCategory(43, "magazin", urlSegmentIndex: 1, (int)CategoryId.Show, (int)RssFeedId.Dnevno),
                new RssFeedCategory(44, "zdravlje", urlSegmentIndex: 1, (int)CategoryId.Lifestyle, (int)RssFeedId.Dnevno),
                #endregion

                #region AutomobiliHr
                // new RssFeedCategory(45, "automobili.klik.hr", urlSegmentIndex: 1, (int)CategoryId.AutoMoto, (int)RssFeedId.AutomobiliHr),
                #endregion

                #region Nethr
                new RssFeedCategory(46, "hrvatska", urlSegmentIndex: 2, (int)CategoryId.Vijesti, (int)RssFeedId.NetHr),
                new RssFeedCategory(47, "crna-kronika", urlSegmentIndex: 2, (int)CategoryId.Vijesti, (int)RssFeedId.NetHr),
                new RssFeedCategory(48, "svijet", urlSegmentIndex: 2, (int)CategoryId.Vijesti, (int)RssFeedId.NetHr),
                new RssFeedCategory(49, "kultura", urlSegmentIndex: 1, (int)CategoryId.Kultura, (int)RssFeedId.NetHr),
                new RssFeedCategory(50, "novac", urlSegmentIndex: 1, (int)CategoryId.Biznis, (int)RssFeedId.NetHr),
                new RssFeedCategory(51, "znanost", urlSegmentIndex: 1, (int)CategoryId.Tech, (int)RssFeedId.NetHr),
                new RssFeedCategory(52, "sport", urlSegmentIndex: 1, (int)CategoryId.Sport, (int)RssFeedId.NetHr),
                new RssFeedCategory(53, "vic-dana", urlSegmentIndex: 1, (int)CategoryId.Viral, (int)RssFeedId.NetHr),
                new RssFeedCategory(54, "planet-x", urlSegmentIndex: 1, (int)CategoryId.Tech, (int)RssFeedId.NetHr),
                new RssFeedCategory(55, "fora-dana", urlSegmentIndex: 1, (int)CategoryId.Viral, (int)RssFeedId.NetHr),
                new RssFeedCategory(56, "hot", urlSegmentIndex: 1, (int)CategoryId.Show, (int)RssFeedId.NetHr),
                new RssFeedCategory(57, "magazin", urlSegmentIndex: 1, (int)CategoryId.Lifestyle, (int)RssFeedId.NetHr),
                new RssFeedCategory(58, "tehnoklik", urlSegmentIndex: 1, (int)CategoryId.Tech, (int)RssFeedId.NetHr),
                new RssFeedCategory(59, "auto", urlSegmentIndex: 1, (int)CategoryId.AutoMoto, (int)RssFeedId.NetHr),
                #endregion

                #region N1
                new RssFeedCategory(60, "Crna-Kronika", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.N1),
                new RssFeedCategory(61, "Video", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.N1),
                #endregion

                #region Dnevnik
                new RssFeedCategory(62, "showbuzz", urlSegmentIndex: 1, (int)CategoryId.Show, (int)RssFeedId.Dnevnik),
                #endregion

                #region NarodHr
                new RssFeedCategory(63, "koronovirus", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.NarodHr),
                new RssFeedCategory(64, "svijet", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.NarodHr),
                #endregion

                #region Dnevno
                new RssFeedCategory(65, "korona-virus", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.Dnevno),
                new RssFeedCategory(66, "auto-moto", urlSegmentIndex: 1, (int)CategoryId.AutoMoto, (int)RssFeedId.Dnevno),
                #endregion

                #region Jutarnji List
                new RssFeedCategory(67, "scena", urlSegmentIndex: 1, (int)CategoryId.Show, (int)RssFeedId.JutarnjiList),
                new RssFeedCategory(68, "spektakli", urlSegmentIndex: 1, (int)CategoryId.Show, (int)RssFeedId.JutarnjiList),
                #endregion

                #region  Telegram
                new RssFeedCategory(69, "kultura", urlSegmentIndex: 1, (int)CategoryId.Kultura, (int)RssFeedId.Telegram),
                #endregion 

                #region NarodHr
                new RssFeedCategory(70, "svijet", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.NarodHr),
                #endregion

                #region Telegram
                new RssFeedCategory(71, "na-prvu", urlSegmentIndex: 1, (int)CategoryId.Sport, (int)RssFeedId.Telegram),
                #endregion 

                #region Scena
                new RssFeedCategory(72, "lifestyle", urlSegmentIndex: 1, (int)CategoryId.Lifestyle, (int)RssFeedId.Scena),
                new RssFeedCategory(73, "vijesti", urlSegmentIndex: 1, (int)CategoryId.Show, (int)RssFeedId.Scena),
                new RssFeedCategory(74, "kultura", urlSegmentIndex: 1, (int)CategoryId.Kultura, (int)RssFeedId.Scena),
                new RssFeedCategory(75, "televizija", urlSegmentIndex: 1, (int)CategoryId.Show, (int)RssFeedId.Scena),
                new RssFeedCategory(76, "dogadjanja", urlSegmentIndex: 1, (int)CategoryId.Lifestyle, (int)RssFeedId.Scena),
                #endregion 
            };

            builder.HasData(rssFeedCategories);
        }
    }
}
