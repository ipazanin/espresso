using System.Collections.Generic;

namespace Espresso.Common.Constants
{
    public static class DefaultValueConstants
    {
        public const int DefaultTake = 20;
        public const int DefaultSkip = 0;

        public static IEnumerable<string> BannedKeywords => new List<string>
            {
                "virus",
                "pandemij",
                "koron",
                "coron",
                "capak",
                "božinović",
                "bozinovic",
                "markotić",
                "markotic",
                "mask",
                "karanten",
                "umrlih",
                "zaraz",
                "zaraž",
                "staracki dom",
                "starački dom",
                "bolnica",
                "prva pomoc",
                "prva pomoć",
                "hitna",
                "zabran",
                "pozitiv",
                "testir",
                "slučaj",
                "slucaj",
                "covid",
                "stozer",
                "stožer",
                "respirator",
                "bolest",
                "ambulant"
            };
    }
}
