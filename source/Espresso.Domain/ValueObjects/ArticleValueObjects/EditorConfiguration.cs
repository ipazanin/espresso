namespace Espresso.Domain.ValueObjects.ArticleValueObjects
{
    public record EditorConfiguration
    {
        #region Constants
        public const bool IsHiddenDefaultValue = false;
        public static bool? IsFeaturedDefaultValue => null;
        public static int? FeaturedPositionDefaultValue => null;
        #endregion

        #region Properties
        public bool IsHidden { get; init; } = IsHiddenDefaultValue;

        public bool? IsFeatured { get; init; } = IsFeaturedDefaultValue;

        public int? FeaturedPosition { get; init; } = FeaturedPositionDefaultValue;
        #endregion
    }
}