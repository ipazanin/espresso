// SearchType.cs
//
// © 2022 Espresso News. All rights reserved.

namespace Espresso.Domain.Enums.RssFeedEnums;

public enum SearchType
{
    None = 0,
    Equals = 1,
    Contains = 2,
    DateMilliseconds = 3,
    Date = 4,
    Like = 5,
}
