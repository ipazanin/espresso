// ImageUrlParseStrategy.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.Domain.Enums.RssFeedEnums;

public enum ImageUrlParseStrategy
{
    SecondLinkOrFromSummary = 1,
    FromContent = 2,
    FromElementExtension = 3,
}
