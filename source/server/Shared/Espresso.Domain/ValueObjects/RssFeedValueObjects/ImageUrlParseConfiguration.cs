// ImageUrlParseConfiguration.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Domain.Enums.RssFeedEnums;
using Espresso.Domain.Infrastructure;

#pragma warning disable RCS1170 // Use read-only auto-implemented property.

namespace Espresso.Domain.ValueObjects.RssFeedValueObjects;

public class ImageUrlParseConfiguration : ValueObject
{
    public const ImageUrlParseStrategy ImageUrlParseStrategyDefaultValue = ImageUrlParseStrategy.SecondLinkOrFromSummary;

    public const string ImgElementXPathDefaultValue = "";

    public const int ImgElementXPathHasMaxLength = 300;

    public const string AttributeNameDefaultValue = "src";

    public const int AttributeNameMaxLength = 100;

    public const ImageUrlWebScrapeType ImageUrlWebScrapeTypeDefaultValue = ImageUrlWebScrapeType.Attribute;

    public const string? JsonWebScrapePropertyNamesDefaultValue = null;

    public const int JsonWebScrapePropertyNamesHasMaxLength = 300;

    public const RequestType WebScrapeRequestTypeDefaultValue = RequestType.Browser;

    public const int ElementExtensionNameMaxLength = 100;

    public const int ElementExtensionAttributeNameMaxLength = 100;

    /// <summary>
    /// Initializes a new instance of the <see cref="ImageUrlParseConfiguration"/> class.
    /// </summary>
    /// <param name="imageUrlParseStrategy"></param>
    /// <param name="imgElementXPath"></param>
    /// <param name="attributeName"></param>
    /// <param name="shouldImageUrlBeWebScraped"></param>
    /// <param name="imageUrlWebScrapeType"></param>
    /// <param name="jsonWebScrapePropertyNames"></param>
    /// <param name="elementExtensionName"></param>
    /// <param name="elementExtensionAttributeName"></param>
    /// <param name="webScrapeRequestType"></param>
    /// <param name="elementExtensionValueParseType"></param>
    /// <param name="elementExtensionValueType"></param>
    public ImageUrlParseConfiguration(
        ImageUrlParseStrategy imageUrlParseStrategy,
        string imgElementXPath,
        string attributeName,
        bool shouldImageUrlBeWebScraped,
        ImageUrlWebScrapeType imageUrlWebScrapeType,
        string? jsonWebScrapePropertyNames,
        string elementExtensionName,
        string elementExtensionAttributeName,
        RequestType webScrapeRequestType,
        ValueParseType elementExtensionValueParseType,
        XmlValueType elementExtensionValueType)
    {
        ImageUrlParseStrategy = imageUrlParseStrategy;
        XPath = imgElementXPath;
        AttributeName = attributeName;
        ShouldImageUrlBeWebScraped = shouldImageUrlBeWebScraped;
        ImageUrlWebScrapeType = imageUrlWebScrapeType;
        JsonWebScrapePropertyNames = jsonWebScrapePropertyNames;
        ElementExtensionName = elementExtensionName;
        ElementExtensionAttributeName = elementExtensionAttributeName;
        WebScrapeRequestType = webScrapeRequestType;
        ElementExtensionValueParseType = elementExtensionValueParseType;
        ElementExtensionValueType = elementExtensionValueType;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ImageUrlParseConfiguration"/> class.
    /// ORM COnstructor.
    /// </summary>
    private ImageUrlParseConfiguration()
    {
        XPath = null!;
        AttributeName = null!;
        ElementExtensionName = null!;
        ElementExtensionAttributeName = null!;
    }

    public static bool? ShouldImageUrlBeWebScrapedDefaultValue => null;

    public ImageUrlParseStrategy ImageUrlParseStrategy { get; private set; }

    public string XPath { get; private set; }

    public string AttributeName { get; private set; }

    public bool ShouldImageUrlBeWebScraped { get; private set; }

    public ImageUrlWebScrapeType ImageUrlWebScrapeType { get; private set; }

    public string? JsonWebScrapePropertyNames { get; private set; }

    public XmlValueType ElementExtensionValueType { get; private set; }

    public string ElementExtensionName { get; private set; }

    public string ElementExtensionAttributeName { get; private set; }

    public ValueParseType ElementExtensionValueParseType { get; private set; }

    public RequestType WebScrapeRequestType { get; set; }

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
        yield return ElementExtensionName;
        yield return ElementExtensionAttributeName;
        yield return WebScrapeRequestType;
        yield return ElementExtensionValueType;
        yield return ElementExtensionValueParseType;
    }
}
