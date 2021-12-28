// EditorConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.Domain.ValueObjects.ArticleValueObjects;

public record EditorConfiguration
{
    public const bool IsHiddenDefaultValue = false;
    public static bool? IsFeaturedDefaultValue => null;
    public static int? FeaturedPositionDefaultValue => null;

    public bool IsHidden { get; init; } = IsHiddenDefaultValue;

    public bool? IsFeatured { get; init; } = IsFeaturedDefaultValue;

    public int? FeaturedPosition { get; init; } = FeaturedPositionDefaultValue;
}
