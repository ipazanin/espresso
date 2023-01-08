// ImageUrlParseConfigurationDto.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Linq.Expressions;
using Espresso.Domain.Enums.RssFeedEnums;
using Espresso.Domain.ValueObjects.RssFeedValueObjects;

namespace Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects.RssFeedDataTransferObjects;

public class ImageUrlParseConfigurationDto
{
    public ImageUrlParseConfigurationDto(
        ImageUrlParseStrategy imageUrlParseStrategy,
        string xPath,
        string attributeName,
        bool shouldImageUrlBeWebScraped,
        ImageUrlWebScrapeType imageUrlWebScrapeType,
        string? jsonWebScrapePropertyNames,
        int? elementExtensionIndex,
        bool? isSavedInHtmlElementWithSrcAttribute)
    {
        ImageUrlParseStrategy = imageUrlParseStrategy;
        XPath = xPath;
        AttributeName = attributeName;
        ShouldImageUrlBeWebScraped = shouldImageUrlBeWebScraped;
        ImageUrlWebScrapeType = imageUrlWebScrapeType;
        JsonWebScrapePropertyNames = jsonWebScrapePropertyNames;
        ElementExtensionIndex = elementExtensionIndex;
        IsSavedInHtmlElementWithSrcAttribute = isSavedInHtmlElementWithSrcAttribute;
    }

    private ImageUrlParseConfigurationDto()
    {
    }

    public static Expression<Func<ImageUrlParseConfiguration, ImageUrlParseConfigurationDto>> Projection
    {
        get => imageUrlParseConfiguration => new ImageUrlParseConfigurationDto
        {
            ImageUrlParseStrategy = imageUrlParseConfiguration.ImageUrlParseStrategy,
            XPath = imageUrlParseConfiguration.XPath,
            AttributeName = imageUrlParseConfiguration.AttributeName,
            ShouldImageUrlBeWebScraped = imageUrlParseConfiguration.ShouldImageUrlBeWebScraped,
            ImageUrlWebScrapeType = imageUrlParseConfiguration.ImageUrlWebScrapeType,
            JsonWebScrapePropertyNames = imageUrlParseConfiguration.JsonWebScrapePropertyNames,
            ElementExtensionIndex = imageUrlParseConfiguration.ElementExtensionIndex,
            IsSavedInHtmlElementWithSrcAttribute = imageUrlParseConfiguration.IsSavedInHtmlElementWithSrcAttribute,
            WebScrapeRequestType = imageUrlParseConfiguration.WebScrapeRequestType,
        };
    }

    public ImageUrlParseStrategy ImageUrlParseStrategy { get; set; }

    public string XPath { get; set; } = string.Empty;

    public string AttributeName { get; set; } = string.Empty;

    public bool ShouldImageUrlBeWebScraped { get; set; }

    public ImageUrlWebScrapeType ImageUrlWebScrapeType { get; set; }

    public string? JsonWebScrapePropertyNames { get; set; }

    /// <summary>
    /// Gets index of element extension containing Image Url.
    /// </summary>
    public int? ElementExtensionIndex { get; set; }

    public bool? IsSavedInHtmlElementWithSrcAttribute { get; set; }

    public RequestType WebScrapeRequestType { get; set; }

    public ImageUrlParseConfiguration CreateImageUrlParseConfiguration()
    {
        return new ImageUrlParseConfiguration(
            imageUrlParseStrategy: ImageUrlParseStrategy,
            imgElementXPath: XPath,
            attributeName: AttributeName,
            shouldImageUrlBeWebScraped: ShouldImageUrlBeWebScraped,
            imageUrlWebScrapeType: ImageUrlWebScrapeType,
            jsonWebScrapePropertyNames: JsonWebScrapePropertyNames,
            elementExtensionIndex: ElementExtensionIndex,
            isSavedInHtmlElementWithSrcAttribute: IsSavedInHtmlElementWithSrcAttribute,
            webScrapeRequestType: WebScrapeRequestType);
    }
}
