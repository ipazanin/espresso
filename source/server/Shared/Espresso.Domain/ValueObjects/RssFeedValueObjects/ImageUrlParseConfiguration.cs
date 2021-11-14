// ImageUrlParseConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Enums.RssFeedEnums;
using Espresso.Domain.Infrastructure;
using System;
using System.Collections.Generic;

namespace Espresso.Domain.ValueObjects.RssFeedValueObjects
{
    public class ImageUrlParseConfiguration : ValueObject
    {
        public const ImageUrlParseStrategy ImageUrlParseStrategyDefaultValue = ImageUrlParseStrategy.SecondLinkOrFromSummary;

        public const string ImgElementXPathDefaultValue = "";

        public const int ImgElementXPathHasMaxLength = 300;

        public const string AttributeNameDefaultValue = "src";

        public const int AttributeNameMaxLength = 100;

        public static bool? ShouldImageUrlBeWebScrapedDefaultValue => null;

#pragma warning disable SA1201 // Elements should appear in the correct order
        public const ImageUrlWebScrapeType ImageUrlWebScrapeTypeDefaultValue = ImageUrlWebScrapeType.Attribute;
#pragma warning restore SA1201 // Elements should appear in the correct order

        public const string? JsonWebScrapePropertyNamesDefaultValue = null;

        public const int JsonWebScrapePropertyNamesHasMaxLength = 300;

        public static int? ElementExtensionIndexDefaultValue => null;

        public static bool? IsSavedInHtmlElementWithSrcAttributeDefaultValue => null;

        public ImageUrlParseStrategy ImageUrlParseStrategy { get; private set; }

        public string XPath { get; private set; }

        public string AttributeName { get; private set; }

        public bool? ShouldImageUrlBeWebScraped { get; private set; }

        public ImageUrlWebScrapeType ImageUrlWebScrapeType { get; private set; }

        public string? JsonWebScrapePropertyNames { get; private set; }

        /// <summary>
        /// Gets index of element extension containing Image Url.
        /// </summary>
        public int? ElementExtensionIndex { get; private set; }

        public bool? IsSavedInHtmlElementWithSrcAttribute { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageUrlParseConfiguration"/> class.
        /// ORM COnstructor.
        /// </summary>
#pragma warning disable SA1201 // Elements should appear in the correct order
        private ImageUrlParseConfiguration()
#pragma warning restore SA1201 // Elements should appear in the correct order
        {
            XPath = null!;
            AttributeName = null!;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageUrlParseConfiguration"/> class.
        /// </summary>
        /// <param name="imageUrlParseStrategy"></param>
        /// <param name="imgElementXPath"></param>
        /// <param name="attributeName"></param>
        /// <param name="shouldImageUrlBeWebScraped"></param>
        /// <param name="imageUrlWebScrapeType"></param>
        /// <param name="jsonWebScrapePropertyNames"></param>
        /// <param name="elementExtensionIndex"></param>
        /// <param name="isSavedInHtmlElementWithSrcAttribute"></param>
        public ImageUrlParseConfiguration(
            ImageUrlParseStrategy imageUrlParseStrategy,
            string imgElementXPath,
            string attributeName,
            bool shouldImageUrlBeWebScraped,
            ImageUrlWebScrapeType imageUrlWebScrapeType,
            string jsonWebScrapePropertyNames,
            int? elementExtensionIndex,
            bool? isSavedInHtmlElementWithSrcAttribute)
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
    }
}
