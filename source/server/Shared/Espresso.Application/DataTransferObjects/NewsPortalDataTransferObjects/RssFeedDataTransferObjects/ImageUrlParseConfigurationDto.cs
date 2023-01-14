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
        string elementExtensionName,
        string elementExtensionAttributeName,
        RequestType webScrapeRequestType,
        ValueParseType elementExtensionValueParseType,
        XmlValueType elementExtensionValueType)
    {
        ImageUrlParseStrategy = imageUrlParseStrategy;
        XPath = xPath;
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
            ElementExtensionName = imageUrlParseConfiguration.ElementExtensionName,
            ElementExtensionAttributeName = imageUrlParseConfiguration.ElementExtensionAttributeName,
            WebScrapeRequestType = imageUrlParseConfiguration.WebScrapeRequestType,
            ElementExtensionValueParseType = imageUrlParseConfiguration.ElementExtensionValueParseType,
            ElementExtensionValueType = imageUrlParseConfiguration.ElementExtensionValueType,
        };
    }

    public ImageUrlParseStrategy ImageUrlParseStrategy { get; set; }

    public string XPath { get; set; } = string.Empty;

    public string AttributeName { get; set; } = string.Empty;

    public bool ShouldImageUrlBeWebScraped { get; set; }

    public ImageUrlWebScrapeType ImageUrlWebScrapeType { get; set; }

    public string? JsonWebScrapePropertyNames { get; set; }

    public XmlValueType ElementExtensionValueType { get; set; }

    public string ElementExtensionName { get; set; } = string.Empty;

    public string ElementExtensionAttributeName { get; set; } = string.Empty;

    public ValueParseType ElementExtensionValueParseType { get; set; }

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
            elementExtensionName: ElementExtensionName,
            elementExtensionAttributeName: ElementExtensionAttributeName,
            webScrapeRequestType: WebScrapeRequestType,
            elementExtensionValueParseType: ElementExtensionValueParseType,
            elementExtensionValueType: ElementExtensionValueType);
    }
}
