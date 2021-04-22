using Espresso.Domain.Entities;
using Espresso.Domain.Enums.RssFeedEnums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.DataSeed
{
    public static class RssFeedContentModifierDataSeed
    {
        public static void Seed(EntityTypeBuilder<RssFeedContentModifier> builder)
        {
            var id = 1;

            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "<description>",
                replacementValue: "<notused>",
                orderIndex: 1,
                rssFeedId: (int)RssFeedId.Index_Auto
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "</description>",
                replacementValue: "</notused>",
                orderIndex: 2,
                rssFeedId: (int)RssFeedId.Index_Auto
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "<content>",
                replacementValue: "<description>",
                orderIndex: 3,
                rssFeedId: (int)RssFeedId.Index_Auto
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "</content>",
                replacementValue: "</description>",
                orderIndex: 4,
                rssFeedId: (int)RssFeedId.Index_Auto
            ));

            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "<description>",
                replacementValue: "<notused>",
                orderIndex: 1,
                rssFeedId: (int)RssFeedId.Index_Magazin
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "</description>",
                replacementValue: "</notused>",
                orderIndex: 2,
                rssFeedId: (int)RssFeedId.Index_Magazin
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "<content>",
                replacementValue: "<description>",
                orderIndex: 3,
                rssFeedId: (int)RssFeedId.Index_Magazin
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "</content>",
                replacementValue: "</description>",
                orderIndex: 4,
                rssFeedId: (int)RssFeedId.Index_Magazin
            ));

            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "<description>",
                replacementValue: "<notused>",
                orderIndex: 1,
                rssFeedId: (int)RssFeedId.Index_Rogue
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "</description>",
                replacementValue: "</notused>",
                orderIndex: 2,
                rssFeedId: (int)RssFeedId.Index_Rogue
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "<content>",
                replacementValue: "<description>",
                orderIndex: 3,
                rssFeedId: (int)RssFeedId.Index_Rogue
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "</content>",
                replacementValue: "</description>",
                orderIndex: 4,
                rssFeedId: (int)RssFeedId.Index_Rogue
            ));

            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "<description>",
                replacementValue: "<notused>",
                orderIndex: 1,
                rssFeedId: (int)RssFeedId.Index_Sport
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "</description>",
                replacementValue: "</notused>",
                orderIndex: 2,
                rssFeedId: (int)RssFeedId.Index_Sport
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "<content>",
                replacementValue: "<description>",
                orderIndex: 3,
                rssFeedId: (int)RssFeedId.Index_Sport
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "</content>",
                replacementValue: "</description>",
                orderIndex: 4,
                rssFeedId: (int)RssFeedId.Index_Sport
            ));

            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "<content>",
                replacementValue: "<description>",
                orderIndex: 1,
                rssFeedId: (int)RssFeedId.Index_Vijesti
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "</content>",
                replacementValue: "</description>",
                orderIndex: 2,
                rssFeedId: (int)RssFeedId.Index_Vijesti
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "<content>",
                replacementValue: "<description>",
                orderIndex: 3,
                rssFeedId: (int)RssFeedId.Index_Vijesti
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "</content>",
                replacementValue: "</description>",
                orderIndex: 4,
                rssFeedId: (int)RssFeedId.Index_Vijesti
            ));

            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "<content>",
                replacementValue: "<description>",
                orderIndex: 1,
                rssFeedId: (int)RssFeedId.IndexHrZagreb
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "</content>",
                replacementValue: "</description>",
                orderIndex: 2,
                rssFeedId: (int)RssFeedId.IndexHrZagreb
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "<content>",
                replacementValue: "<description>",
                orderIndex: 3,
                rssFeedId: (int)RssFeedId.IndexHrZagreb
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "</content>",
                replacementValue: "</description>",
                orderIndex: 4,
                rssFeedId: (int)RssFeedId.IndexHrZagreb
            ));

            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "<thumb>",
                replacementValue: "<link>",
                orderIndex: 1,
                rssFeedId: (int)RssFeedId.Hrt_Glazba
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "</thumb>",
                replacementValue: "</link>",
                orderIndex: 2,
                rssFeedId: (int)RssFeedId.Hrt_Glazba
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "<thumb>",
                replacementValue: "<link>",
                orderIndex: 1,
                rssFeedId: (int)RssFeedId.Hrt_Magazin
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "</thumb>",
                replacementValue: "</link>",
                orderIndex: 2,
                rssFeedId: (int)RssFeedId.Hrt_Magazin
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "<thumb>",
                replacementValue: "<link>",
                orderIndex: 1,
                rssFeedId: (int)RssFeedId.Hrt_Sport
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "</thumb>",
                replacementValue: "</link>",
                orderIndex: 2,
                rssFeedId: (int)RssFeedId.Hrt_Sport
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "<thumb>",
                replacementValue: "<link>",
                orderIndex: 1,
                rssFeedId: (int)RssFeedId.Hrt_Vijesti
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "</thumb>",
                replacementValue: "</link>",
                orderIndex: 2,
                rssFeedId: (int)RssFeedId.Hrt_Vijesti
            ));

            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "<description>",
                replacementValue: "<notused>",
                orderIndex: 1,
                rssFeedId: (int)RssFeedId.Netokracija
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "</description>",
                replacementValue: "</notused>",
                orderIndex: 2,
                rssFeedId: (int)RssFeedId.Netokracija
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "<content:encoded>",
                replacementValue: "<description>",
                orderIndex: 3,
                rssFeedId: (int)RssFeedId.Netokracija
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "</content:encoded>",
                replacementValue: "</description>",
                orderIndex: 4,
                rssFeedId: (int)RssFeedId.Netokracija
            ));

            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "<description>",
                replacementValue: "<notused>",
                orderIndex: 1,
                rssFeedId: (int)RssFeedId.Gp1
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "</description>",
                replacementValue: "</notused>",
                orderIndex: 2,
                rssFeedId: (int)RssFeedId.Gp1
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "<content:encoded>",
                replacementValue: "<description>",
                orderIndex: 3,
                rssFeedId: (int)RssFeedId.Gp1
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "</content:encoded>",
                replacementValue: "</description>",
                orderIndex: 4,
                rssFeedId: (int)RssFeedId.Gp1
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "\n",
                replacementValue: "",
                orderIndex: 1,
                rssFeedId: (int)RssFeedId.LjepotaIZdravlje
            ));
        }
    }
}
