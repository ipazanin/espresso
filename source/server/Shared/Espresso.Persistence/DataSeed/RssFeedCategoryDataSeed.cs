// RssFeedCategoryDataSeed.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Espresso.Domain.Enums.CategoryEnums;
using Espresso.Domain.Enums.RssFeedEnums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Espresso.Persistence.DataSeed
{
    /// <summary>
    /// <see cref="RssFeedCategory"/> data seed.
    /// </summary>
    internal static class RssFeedCategoryDataSeed
    {
        /// <summary>
        /// Seeds entity data.
        /// </summary>
        /// <param name="builder">Entity builder.</param>
        public static void Seed(EntityTypeBuilder<RssFeedCategory> builder)
        {
            var currentId = 1;
            var rssFeedCategories = new List<RssFeedCategory>
            {
                new RssFeedCategory(currentId++, "vijesti", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.JutarnjiList),
                new RssFeedCategory(currentId++, "autoklub", urlSegmentIndex: 1, (int)CategoryId.AutoMoto, (int)RssFeedId.JutarnjiList),
                new RssFeedCategory(currentId++, "kultura", urlSegmentIndex: 1, (int)CategoryId.Kultura, (int)RssFeedId.JutarnjiList),
                new RssFeedCategory(currentId++, "globus", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.JutarnjiList),
                new RssFeedCategory(currentId++, "domidizajn", urlSegmentIndex: 1, (int)CategoryId.Lifestyle, (int)RssFeedId.JutarnjiList),
                new RssFeedCategory(currentId++, "viral", urlSegmentIndex: 1, (int)CategoryId.Viral, (int)RssFeedId.JutarnjiList),
                new RssFeedCategory(currentId++, "spektakli", urlSegmentIndex: 1, (int)CategoryId.Show, (int)RssFeedId.JutarnjiList),
                new RssFeedCategory(currentId++, "life", urlSegmentIndex: 1, (int)CategoryId.Lifestyle, (int)RssFeedId.JutarnjiList),
                new RssFeedCategory(currentId++, "scena", urlSegmentIndex: 1, (int)CategoryId.Show, (int)RssFeedId.JutarnjiList),
                new RssFeedCategory(currentId++, "spektakli", urlSegmentIndex: 1, (int)CategoryId.Show, (int)RssFeedId.JutarnjiList),
                new RssFeedCategory(currentId++, "vijesti", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.VecernjiList),
                new RssFeedCategory(currentId++, "sport", urlSegmentIndex: 1, (int)CategoryId.Sport, (int)RssFeedId.VecernjiList),
                new RssFeedCategory(currentId++, "showbiz", urlSegmentIndex: 1, (int)CategoryId.Show, (int)RssFeedId.VecernjiList),
                new RssFeedCategory(currentId++, "lifestyle", urlSegmentIndex: 1, (int)CategoryId.Lifestyle, (int)RssFeedId.VecernjiList),
                new RssFeedCategory(currentId++, "biznis", urlSegmentIndex: 1, (int)CategoryId.Biznis, (int)RssFeedId.VecernjiList),
                new RssFeedCategory(currentId++, "techsci", urlSegmentIndex: 1, (int)CategoryId.Tech, (int)RssFeedId.VecernjiList),
                new RssFeedCategory(currentId++, "automoto", urlSegmentIndex: 1, (int)CategoryId.AutoMoto, (int)RssFeedId.VecernjiList),
                new RssFeedCategory(currentId++, "kultura", urlSegmentIndex: 1, (int)CategoryId.Kultura, (int)RssFeedId.VecernjiList),
                new RssFeedCategory(currentId++, "vijesti", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.Dnevnik),
                new RssFeedCategory(currentId++, "showbuzz", urlSegmentIndex: 1, (int)CategoryId.Show, (int)RssFeedId.Dnevnik),
                new RssFeedCategory(currentId++, "politika-kriminal", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.Telegram),
                new RssFeedCategory(currentId++, "zivot", urlSegmentIndex: 1, (int)CategoryId.Lifestyle, (int)RssFeedId.Telegram),
                new RssFeedCategory(currentId++, "kultura", urlSegmentIndex: 1, (int)CategoryId.Kultura, (int)RssFeedId.Telegram),
                new RssFeedCategory(currentId++, "na-prvu", urlSegmentIndex: 1, (int)CategoryId.Sport, (int)RssFeedId.Telegram),
                new RssFeedCategory(currentId++, "Vijesti", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.N1),
                new RssFeedCategory(currentId++, "Svijet", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.N1),
                new RssFeedCategory(currentId++, "Znanost", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.N1),
                new RssFeedCategory(currentId++, "Regija", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.N1),
                new RssFeedCategory(currentId++, "Dnevnik", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.N1),
                new RssFeedCategory(currentId++, "Info", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.N1),
                new RssFeedCategory(currentId++, "Biznis", urlSegmentIndex: 1, (int)CategoryId.Biznis, (int)RssFeedId.N1),
                new RssFeedCategory(currentId++, "Lifestyle", urlSegmentIndex: 1, (int)CategoryId.Lifestyle, (int)RssFeedId.N1),
                new RssFeedCategory(currentId++, "Zdravlje", urlSegmentIndex: 1, (int)CategoryId.Lifestyle, (int)RssFeedId.N1),
                new RssFeedCategory(currentId++, "Sport-Klub", urlSegmentIndex: 1, (int)CategoryId.Sport, (int)RssFeedId.N1),
                new RssFeedCategory(currentId++, "Showbiz", urlSegmentIndex: 1, (int)CategoryId.Show, (int)RssFeedId.N1),
                new RssFeedCategory(currentId++, "Tehnologija", urlSegmentIndex: 1, (int)CategoryId.Tech, (int)RssFeedId.N1),
                new RssFeedCategory(currentId++, "Kultura", urlSegmentIndex: 1, (int)CategoryId.Kultura, (int)RssFeedId.N1),
                new RssFeedCategory(currentId++, "Crna-Kronika", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.N1),
                new RssFeedCategory(currentId++, "Video", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.N1),
                new RssFeedCategory(currentId++, "Hrvatska", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.NarodHr),
                new RssFeedCategory(currentId++, "Sport", urlSegmentIndex: 1, (int)CategoryId.Sport, (int)RssFeedId.NarodHr),
                new RssFeedCategory(currentId++, "Kultura", urlSegmentIndex: 1, (int)CategoryId.Kultura, (int)RssFeedId.NarodHr),
                new RssFeedCategory(currentId++, "svijet", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.NarodHr),
                new RssFeedCategory(currentId++, "koronovirus", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.NarodHr),
                new RssFeedCategory(currentId++, "svijet", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.NarodHr),
                new RssFeedCategory(currentId++, "zivot", urlSegmentIndex: 1, (int)CategoryId.Lifestyle, (int)RssFeedId.StoPosto),
                new RssFeedCategory(currentId++, "news", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.StoPosto),
                new RssFeedCategory(currentId++, "scena", urlSegmentIndex: 1, (int)CategoryId.Show, (int)RssFeedId.StoPosto),
                new RssFeedCategory(currentId++, "bubble", urlSegmentIndex: 1, (int)CategoryId.Lifestyle, (int)RssFeedId.StoPosto),
                new RssFeedCategory(currentId++, "vijesti", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.Dnevno),
                new RssFeedCategory(currentId++, "sport", urlSegmentIndex: 1, (int)CategoryId.Sport, (int)RssFeedId.Dnevno),
                new RssFeedCategory(currentId++, "domovina", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.Dnevno),
                new RssFeedCategory(currentId++, "magazin", urlSegmentIndex: 1, (int)CategoryId.Show, (int)RssFeedId.Dnevno),
                new RssFeedCategory(currentId++, "zdravlje", urlSegmentIndex: 1, (int)CategoryId.Lifestyle, (int)RssFeedId.Dnevno),
                new RssFeedCategory(currentId++, "korona-virus", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.Dnevno),
                new RssFeedCategory(currentId++, "auto-moto", urlSegmentIndex: 1, (int)CategoryId.AutoMoto, (int)RssFeedId.Dnevno),
                new RssFeedCategory(currentId++, "hrvatska", urlSegmentIndex: 2, (int)CategoryId.Vijesti, (int)RssFeedId.NetHr),
                new RssFeedCategory(currentId++, "crna-kronika", urlSegmentIndex: 2, (int)CategoryId.Vijesti, (int)RssFeedId.NetHr),
                new RssFeedCategory(currentId++, "svijet", urlSegmentIndex: 2, (int)CategoryId.Vijesti, (int)RssFeedId.NetHr),
                new RssFeedCategory(currentId++, "kultura", urlSegmentIndex: 1, (int)CategoryId.Kultura, (int)RssFeedId.NetHr),
                new RssFeedCategory(currentId++, "novac", urlSegmentIndex: 1, (int)CategoryId.Biznis, (int)RssFeedId.NetHr),
                new RssFeedCategory(currentId++, "znanost", urlSegmentIndex: 1, (int)CategoryId.Tech, (int)RssFeedId.NetHr),
                new RssFeedCategory(currentId++, "sport", urlSegmentIndex: 1, (int)CategoryId.Sport, (int)RssFeedId.NetHr),
                new RssFeedCategory(currentId++, "vic-dana", urlSegmentIndex: 1, (int)CategoryId.Viral, (int)RssFeedId.NetHr),
                new RssFeedCategory(currentId++, "planet-x", urlSegmentIndex: 1, (int)CategoryId.Tech, (int)RssFeedId.NetHr),
                new RssFeedCategory(currentId++, "fora-dana", urlSegmentIndex: 1, (int)CategoryId.Viral, (int)RssFeedId.NetHr),
                new RssFeedCategory(currentId++, "hot", urlSegmentIndex: 1, (int)CategoryId.Show, (int)RssFeedId.NetHr),
                new RssFeedCategory(currentId++, "magazin", urlSegmentIndex: 1, (int)CategoryId.Lifestyle, (int)RssFeedId.NetHr),
                new RssFeedCategory(currentId++, "tehnoklik", urlSegmentIndex: 1, (int)CategoryId.Tech, (int)RssFeedId.NetHr),
                new RssFeedCategory(currentId++, "auto", urlSegmentIndex: 1, (int)CategoryId.AutoMoto, (int)RssFeedId.NetHr),
                new RssFeedCategory(currentId++, "lifestyle", urlSegmentIndex: 1, (int)CategoryId.Lifestyle, (int)RssFeedId.Scena),
                new RssFeedCategory(currentId++, "vijesti", urlSegmentIndex: 1, (int)CategoryId.Show, (int)RssFeedId.Scena),
                new RssFeedCategory(currentId++, "kultura", urlSegmentIndex: 1, (int)CategoryId.Kultura, (int)RssFeedId.Scena),
                new RssFeedCategory(currentId++, "televizija", urlSegmentIndex: 1, (int)CategoryId.Show, (int)RssFeedId.Scena),
                new RssFeedCategory(currentId++, "dogadjanja", urlSegmentIndex: 1, (int)CategoryId.Lifestyle, (int)RssFeedId.Scena),
                new RssFeedCategory(currentId++, "top-news", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.Express),
                new RssFeedCategory(currentId++, "life", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.Express),
                new RssFeedCategory(currentId++, "ekonomix", urlSegmentIndex: 1, (int)CategoryId.Biznis, (int)RssFeedId.Express),
                new RssFeedCategory(currentId++, "tehno", urlSegmentIndex: 1, (int)CategoryId.Tech, (int)RssFeedId.Express),
                new RssFeedCategory(currentId++, "vijesti", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.OtvorenoHr),
                new RssFeedCategory(currentId++, "gospodarstvo", urlSegmentIndex: 1, (int)CategoryId.Biznis, (int)RssFeedId.OtvorenoHr),
                new RssFeedCategory(currentId++, "magazin", urlSegmentIndex: 1, (int)CategoryId.Show, (int)RssFeedId.OtvorenoHr),
                new RssFeedCategory(currentId++, "kultura", urlSegmentIndex: 1, (int)CategoryId.Kultura, (int)RssFeedId.OtvorenoHr),
                new RssFeedCategory(currentId++, "sport", urlSegmentIndex: 1, (int)CategoryId.Sport, (int)RssFeedId.OtvorenoHr),
                new RssFeedCategory(currentId++, "eu-i-svijet", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.OtvorenoHr),
                new RssFeedCategory(currentId++, "vijesti", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.Dnevno7),
                new RssFeedCategory(currentId++, "sport", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.Dnevno7),
                new RssFeedCategory(currentId++, "domovina", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.Dnevno7),
                new RssFeedCategory(currentId++, "kultura", urlSegmentIndex: 2, (int)CategoryId.Vijesti, (int)RssFeedId.Dnevno7),
                new RssFeedCategory(currentId++, "zdravlje", urlSegmentIndex: 1, (int)CategoryId.Vijesti, (int)RssFeedId.Dnevno7),
            };

            builder.HasData(rssFeedCategories);
        }
    }
}
