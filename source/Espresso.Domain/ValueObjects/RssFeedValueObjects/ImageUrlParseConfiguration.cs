using System;
using System.Collections.Generic;

using Espresso.Domain.Enums.RssFeedEnums;
using Espresso.Domain.Infrastructure;

namespace Espresso.Domain.ValueObjects.RssFeedValueObjects
{
    public class ImageUrlParseConfiguration : ValueObject
    {
        #region Constants

        public const ImageUrlParseStrategy ImageUrlParseStrategyDefaultValue = ImageUrlParseStrategy.SecondLinkOrFromSummary;

        public const string ImgElementXPathDefaultValue = "";
        public const int ImgElementXPathHasMaxLength = 300;

        public static bool? ShouldImageUrlBeWebScrapedDefaultValue => null;

        public const ImageUrlWebScrapeType ImageUrlWebScrapeTypeDefaultValue = ImageUrlWebScrapeType.SrcAttribute;

        public const string? JsonWebScrapePropertyNamesDefaultValue = null;
        public const int JsonWebScrapePropertyNamesHasMaxLength = 300;
        #endregion

        #region Properties
        public ImageUrlParseStrategy ImageUrlParseStrategy { get; private set; }

        public string ImgElementXPath { get; private set; }

        public bool? ShouldImageUrlBeWebScraped { get; private set; }

        public ImageUrlWebScrapeType ImageUrlWebScrapeType { get; private set; }

        public string? JsonWebScrapePropertyNames { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// ORM COnstructor
        /// </summary>
        private ImageUrlParseConfiguration()
        {
            ImgElementXPath = null!;
        }

        public ImageUrlParseConfiguration(
            ImageUrlParseStrategy imageUrlParseStrategy,
            string imgElementXPath,
            bool shouldImageUrlBeWebScraped,
            ImageUrlWebScrapeType imageUrlWebScrapeType,
            string jsonWebScrapePropertyNames
        )
        {
            ImageUrlParseStrategy = imageUrlParseStrategy;
            ImgElementXPath = imgElementXPath;
            ShouldImageUrlBeWebScraped = shouldImageUrlBeWebScraped;
            ImageUrlWebScrapeType = imageUrlWebScrapeType;
            JsonWebScrapePropertyNames = jsonWebScrapePropertyNames;
        }
        #endregion

        #region Methods
        public IEnumerable<string> GetPropertyNames()
        {
            var propertyNames = JsonWebScrapePropertyNames?.Split(",", StringSplitOptions.RemoveEmptyEntries) ??
                Array.Empty<string>();

            return propertyNames;
        }

        protected override IEnumerable<object?> GetAtomicValues()
        {
            yield return ImageUrlParseStrategy;
            yield return ImgElementXPath;
            yield return ShouldImageUrlBeWebScraped;
            yield return ImageUrlWebScrapeType;
            yield return JsonWebScrapePropertyNames;
        }
        #endregion
    }
}
