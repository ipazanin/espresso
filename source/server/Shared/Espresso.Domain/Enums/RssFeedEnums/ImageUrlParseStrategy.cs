// ImageUrlParseStrategy.cs
//
// © 2022 Espresso News. All rights reserved.

namespace Espresso.Domain.Enums.RssFeedEnums;

public enum ImageUrlParseStrategy
{
    None = 0,
    SecondLinkOrFromSummary = 1,
    FromContent = 2,
    FromElementExtension = 3,
}
