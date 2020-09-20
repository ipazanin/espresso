namespace Espresso.Domain.Entities
{
    public class RssFeedContentModifier
    {
        #region Constants
        public const int SourceValueMaxLength = 1000;
        public const int ReplacementValueMaxLength = 1000;
        #endregion

        #region Properties
        public int Id { get; private set; }
        public string SourceValue { get; private set; }

        public string ReplacementValue { get; private set; }

        public int RssFeedId { get; private set; }

        public RssFeed? RssFeed { get; private set; }
        #endregion

        #region Constructors
        public RssFeedContentModifier(
            int id,
            string sourceValue,
            string replacementValue,
            int rssFeedId
        )
        {
            Id = id;
            SourceValue = sourceValue;
            ReplacementValue = replacementValue;
            RssFeedId = rssFeedId;
        }
        #endregion
    }
}