using Espresso.Domain.Entities;
using Espresso.Domain.Enums.RssFeedEnums;
using Espresso.Domain.ValueObjects.RssFeedValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.DataSeed
{
    internal static class RssFeedSkipParseConfigurationSeed
    {
        public static void Seed(
            EntityTypeBuilder<RssFeed> builder
        )
        {
            var skipParseConfigurationBuilder = builder.OwnsOne(rssFeed => rssFeed.SkipParseConfiguration);

            SeedSkipParseConfiguration(skipParseConfigurationBuilder!);
        }

        private static void SeedSkipParseConfiguration(OwnedNavigationBuilder<RssFeed, SkipParseConfiguration> skipParseConfigurationBuilder)
        {
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.JutarnjiList,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Novosti,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Sport,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Showbiz,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Trend,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.SlobodnaDalmacija_DrustvenaMreza,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Zivot,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Zdravlje,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Moda,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Ljepota,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Putovanja,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Gastro,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Dom,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Tehnologija,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.SlobodnaDalmacija_Viral,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.PoslovniPuls,
                NumberOfSkips = 10,
                CurrentSkip = 0
            });

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.StoPosto,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Express,
                NumberOfSkips = 10,
                CurrentSkip = 0
            });

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Hrt_Glazba,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Hrt_Magazin,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Hrt_Sport,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Hrt_Vijesti,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.BasketballHr,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.JoomBoos,
                NumberOfSkips = 7,
                CurrentSkip = 0
            });

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.IctBusiness,
                NumberOfSkips = 8,
                CurrentSkip = 0
            });

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Scena,
                NumberOfSkips = 6,
                CurrentSkip = 0
            });

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Hcl,
                NumberOfSkips = 7,
                CurrentSkip = 0
            });

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.ProfitirajHr,
                NumberOfSkips = 11,
                CurrentSkip = 0
            });

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.MotoriHr,
                NumberOfSkips = 17,
                CurrentSkip = 0
            });

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.AutoportalHr,
                NumberOfSkips = 3,
                CurrentSkip = 0
            });

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.AutopressHr,
                NumberOfSkips = 9,
                CurrentSkip = 0
            });

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.VozimHr,
                NumberOfSkips = 8,
                CurrentSkip = 0
            });

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.AutoMotorSport,
                NumberOfSkips = 17,
                CurrentSkip = 0
            });

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Hoopster,
                NumberOfSkips = 19,
                CurrentSkip = 0
            });

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.PrvaHnl,
                NumberOfSkips = 6,
                CurrentSkip = 0
            });

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.HifiMedia,
                NumberOfSkips = 7,
                CurrentSkip = 0
            });

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.GeekHr,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.VizKultura,
                NumberOfSkips = 7,
                CurrentSkip = 0
            });

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.ZivotUmjetnosti,
                NumberOfSkips = 27,
                CurrentSkip = 0
            });

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.SvijetKulture,
                NumberOfSkips = 7,
                CurrentSkip = 0
            });

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.GamerHr,
                NumberOfSkips = 31,
                CurrentSkip = 0
            });

            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.BitnoNet,
                NumberOfSkips = 12,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.MaxPortal,
                NumberOfSkips = 11,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.PoslovniDnevnik,
                NumberOfSkips = 5,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Gp1,
                NumberOfSkips = 11,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.F1PulsMedia,
                NumberOfSkips = 7,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.Racunalo,
                NumberOfSkips = 11,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.MobHr,
                NumberOfSkips = 15,
                CurrentSkip = 0
            });
            skipParseConfigurationBuilder.HasData(new
            {
                RssFeedId = (int)RssFeedId.StartNews,
                NumberOfSkips = 21,
                CurrentSkip = 0
            });
        }
    }
}
