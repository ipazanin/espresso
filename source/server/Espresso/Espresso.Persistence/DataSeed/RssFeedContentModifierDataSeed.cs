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
                sourceValue: "<content>",
                replacementValue: "<description>",
                rssFeedId: (int)RssFeedId.Index_Auto
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "</content>",
                replacementValue: "</description>",
                rssFeedId: (int)RssFeedId.Index_Auto
            ));

            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "<content>",
                replacementValue: "<description>",
                rssFeedId: (int)RssFeedId.Index_Magazin
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "</content>",
                replacementValue: "</description>",
                rssFeedId: (int)RssFeedId.Index_Magazin
            ));

            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "<content>",
                replacementValue: "<description>",
                rssFeedId: (int)RssFeedId.Index_Rogue
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "</content>",
                replacementValue: "</description>",
                rssFeedId: (int)RssFeedId.Index_Rogue
            ));

            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "<content>",
                replacementValue: "<description>",
                rssFeedId: (int)RssFeedId.Index_Sport
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "</content>",
                replacementValue: "</description>",
                rssFeedId: (int)RssFeedId.Index_Sport
            ));

            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "<content>",
                replacementValue: "<description>",
                rssFeedId: (int)RssFeedId.Index_Vijesti
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "</content>",
                replacementValue: "</description>",
                rssFeedId: (int)RssFeedId.Index_Vijesti
            ));

            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "<content>",
                replacementValue: "<description>",
                rssFeedId: (int)RssFeedId.IndexHrZagreb
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "</content>",
                replacementValue: "</description>",
                rssFeedId: (int)RssFeedId.IndexHrZagreb
            ));

            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "<thumb>",
                replacementValue: "<link>",
                rssFeedId: (int)RssFeedId.Hrt_Glazba
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "</thumb>",
                replacementValue: "</link>",
                rssFeedId: (int)RssFeedId.Hrt_Glazba
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "<thumb>",
                replacementValue: "<link>",
                rssFeedId: (int)RssFeedId.Hrt_Magazin
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "</thumb>",
                replacementValue: "</link>",
                rssFeedId: (int)RssFeedId.Hrt_Magazin
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "<thumb>",
                replacementValue: "<link>",
                rssFeedId: (int)RssFeedId.Hrt_Sport
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "</thumb>",
                replacementValue: "</link>",
                rssFeedId: (int)RssFeedId.Hrt_Sport
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "<thumb>",
                replacementValue: "<link>",
                rssFeedId: (int)RssFeedId.Hrt_Vijesti
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "</thumb>",
                replacementValue: "</link>",
                rssFeedId: (int)RssFeedId.Hrt_Vijesti
            ));

            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "<description>",
                replacementValue: "<notused>",
                rssFeedId: (int)RssFeedId.Netokracija
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "</description>",
                replacementValue: "</notused>",
                rssFeedId: (int)RssFeedId.Netokracija
            ));

            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "<content:encoded>",
                replacementValue: "<description>",
                rssFeedId: (int)RssFeedId.Netokracija
            ));
            builder.HasData(new RssFeedContentModifier(
                id: id++,
                sourceValue: "</content:encoded>",
                replacementValue: "</description>",
                rssFeedId: (int)RssFeedId.Netokracija
            ));
        }
    }
}