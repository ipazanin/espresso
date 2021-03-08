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

        public const string AttributeNameDefaultValue = "src";

        public const int AttributeNameMaxLength = 100;

        public static bool? ShouldImageUrlBeWebScrapedDefaultValue => null;

        public const ImageUrlWebScrapeType ImageUrlWebScrapeTypeDefaultValue = ImageUrlWebScrapeType.Attribute;

        public const string? JsonWebScrapePropertyNamesDefaultValue = null;

        public const int JsonWebScrapePropertyNamesHasMaxLength = 300;

        public static int? ElementExtensionIndexDefaultValue => null;

        public static bool? IsSavedInHtmlElementWithSrcAttributeDefaultValue => null;
        #endregion

        #region Properties
        public ImageUrlParseStrategy ImageUrlParseStrategy { get; private set; }

        public string XPath { get; private set; }

        public string AttributeName { get; private set; }

        public bool? ShouldImageUrlBeWebScraped { get; private set; }

        public ImageUrlWebScrapeType ImageUrlWebScrapeType { get; private set; }

        public string? JsonWebScrapePropertyNames { get; private set; }

        /// <summary>
        /// Index of element extension containing Image Url 
        /// </summary>
        public int? ElementExtensionIndex { get; private set; }

        public bool? IsSavedInHtmlElementWithSrcAttribute { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// ORM COnstructor
        /// </summary>
        private ImageUrlParseConfiguration()
        {
            XPath = null!;
            AttributeName = null!;
        }

        public ImageUrlParseConfiguration(
            ImageUrlParseStrategy imageUrlParseStrategy,
            string imgElementXPath,
            string attributeName,
            bool shouldImageUrlBeWebScraped,
            ImageUrlWebScrapeType imageUrlWebScrapeType,
            string jsonWebScrapePropertyNames,
            int? elementExtensionIndex,
            bool? isSavedInHtmlElementWithSrcAttribute
        )
        {
            ImageUrlParseStrategy = imageUrlParseStrategy;
            XPath = imgElementXPath;
            AttributeName = attributeName;
            ShouldImageUrlBeWebScraped = shouldImageUrlBeWebScraped;
            ImageUrlWebScrapeType = imageUrlWebScrapeType;
            JsonWebScrapePropertyNames = jsonWebScrapePropertyNames;
            ElementExtensionIndex = elementExtensionIndex;
            IsSavedInHtmlElementWithSrcAttribute = isSavedInHtmlElementWithSrcAttribute;
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
            yield return XPath;
            yield return AttributeName;
            yield return ShouldImageUrlBeWebScraped;
            yield return ImageUrlWebScrapeType;
            yield return JsonWebScrapePropertyNames;
        }
        #endregion
    }
}
