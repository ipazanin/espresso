using Espresso.Common.Constants;
using System.Collections.Generic;
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
                new RssFeedCategory(1, "vijesti", (int)CategoryId.Vijesti, (int)RssFeedId.JutarnjiList),
                new RssFeedCategory(2, "autoklub", (int)CategoryId.AutoMoto, (int)RssFeedId.JutarnjiList),
                new RssFeedCategory(3, "kultura", (int)CategoryId.Kultura, (int)RssFeedId.JutarnjiList),

                new RssFeedCategory(13, "globus", (int)CategoryId.Vijesti, (int)RssFeedId.JutarnjiList),
                new RssFeedCategory(14, "domidizajn", (int)CategoryId.Lifestyle, (int)RssFeedId.JutarnjiList),
                new RssFeedCategory(15, "viral", (int)CategoryId.Viral, (int)RssFeedId.JutarnjiList),
                new RssFeedCategory(16, "spektakli", (int)CategoryId.Show, (int)RssFeedId.JutarnjiList),
                new RssFeedCategory(17, "life", (int)CategoryId.Lifestyle, (int)RssFeedId.JutarnjiList),
                #endregion

                #region  Vecernji List
                new RssFeedCategory(4, "vijesti", (int)CategoryId.Vijesti, (int)RssFeedId.VecernjiList),
                new RssFeedCategory(5, "sport", (int)CategoryId.Sport, (int)RssFeedId.VecernjiList),
                new RssFeedCategory(6, "showbiz", (int)CategoryId.Show, (int)RssFeedId.VecernjiList),
                new RssFeedCategory(7, "lifestyle", (int)CategoryId.Lifestyle, (int)RssFeedId.VecernjiList),
                new RssFeedCategory(8, "biznis", (int)CategoryId.Biznis, (int)RssFeedId.VecernjiList),
                new RssFeedCategory(9, "techsci", (int)CategoryId.Tech, (int)RssFeedId.VecernjiList),
                new RssFeedCategory(10, "automoto", (int)CategoryId.AutoMoto, (int)RssFeedId.VecernjiList),
                new RssFeedCategory(11, "kultura", (int)CategoryId.Kultura, (int)RssFeedId.VecernjiList),
                #endregion

                #region Dnevnik
                new RssFeedCategory(12, "vijest", (int)CategoryId.Vijesti, (int)RssFeedId.Dnevnik),
                #endregion

                #region  Telegram
                new RssFeedCategory(18, "politika-kriminal", (int)CategoryId.Vijesti, (int)RssFeedId.Telegram_Zivot),
                new RssFeedCategory(19, "život", (int)CategoryId.Lifestyle, (int)RssFeedId.Telegram_Zivot),
                #endregion 

                #region N1
                new RssFeedCategory(20, "Vijesti", (int)CategoryId.Vijesti, (int)RssFeedId.N1),
                new RssFeedCategory(21, "Svijet", (int)CategoryId.Vijesti, (int)RssFeedId.N1),
                new RssFeedCategory(22, "Znanost", (int)CategoryId.Vijesti, (int)RssFeedId.N1),
                new RssFeedCategory(23, "Regija", (int)CategoryId.Vijesti, (int)RssFeedId.N1),
                new RssFeedCategory(24, "Dnevnik", (int)CategoryId.Vijesti, (int)RssFeedId.N1),
                new RssFeedCategory(25, "Info", (int)CategoryId.Vijesti, (int)RssFeedId.N1),
                new RssFeedCategory(26, "Biznis", (int)CategoryId.Biznis, (int)RssFeedId.N1),
                new RssFeedCategory(27, "Lifestyle", (int)CategoryId.Lifestyle, (int)RssFeedId.N1),
                new RssFeedCategory(28, "Zdravlje", (int)CategoryId.Lifestyle, (int)RssFeedId.N1),
                new RssFeedCategory(29, "Sport-Klub", (int)CategoryId.Sport, (int)RssFeedId.N1),
                new RssFeedCategory(30, "Showbiz", (int)CategoryId.Show, (int)RssFeedId.N1),
                new RssFeedCategory(31, "Tehnologija", (int)CategoryId.Tech, (int)RssFeedId.N1),
                new RssFeedCategory(32, "Kultura", (int)CategoryId.Kultura, (int)RssFeedId.N1),
                #endregion

                #region NarodHr
                new RssFeedCategory(33, "Hrvatska", (int)CategoryId.Vijesti, (int)RssFeedId.NarodHr),
                new RssFeedCategory(34, "Sport", (int)CategoryId.Sport, (int)RssFeedId.NarodHr),
                new RssFeedCategory(35, "Kultura", (int)CategoryId.Kultura, (int)RssFeedId.NarodHr),
                #endregion

                #region 100posto
                new RssFeedCategory(36, "zivot", (int)CategoryId.Lifestyle, (int)RssFeedId.StoPosto),
                new RssFeedCategory(37, "news", (int)CategoryId.Vijesti, (int)RssFeedId.StoPosto),
                new RssFeedCategory(38, "scena", (int)CategoryId.Show, (int)RssFeedId.StoPosto),
                new RssFeedCategory(39, "bubble", (int)CategoryId.Lifestyle, (int)RssFeedId.StoPosto),
                #endregion

                #region Dnevno
                new RssFeedCategory(40, "vijesti", (int)CategoryId.Vijesti, (int)RssFeedId.Dnevno),
                new RssFeedCategory(41, "sport", (int)CategoryId.Sport, (int)RssFeedId.Dnevno),
                new RssFeedCategory(42, "domovina", (int)CategoryId.Vijesti, (int)RssFeedId.Dnevno),
                new RssFeedCategory(43, "magazin", (int)CategoryId.Show, (int)RssFeedId.Dnevno),
                new RssFeedCategory(44, "zdravlje", (int)CategoryId.Lifestyle, (int)RssFeedId.Dnevno),
                #endregion

                #region AutomobiliHr
                // new RssFeedCategory(45, "automobili.klik.hr", (int)CategoryId.AutoMoto, (int)RssFeedId.AutomobiliHr),
                #endregion
            };

            builder.HasData(rssFeedCategories);
        }
    }
}
