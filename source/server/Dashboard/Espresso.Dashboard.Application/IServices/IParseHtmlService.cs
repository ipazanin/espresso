// IParseHtmlService.cs
//
// © 2022 Espresso News. All rights reserved.

using HtmlAgilityPack;

namespace Espresso.Dashboard.Application.IServices;

public interface IParseHtmlService
{
    public string? GetSrcAttributeFromFirstImgElement(string? html);

    public string? GetText(string? html);

    public string? GetImageUrlFromAttribute(
        HtmlNodeCollection elementTags,
        string attributeName);
}
