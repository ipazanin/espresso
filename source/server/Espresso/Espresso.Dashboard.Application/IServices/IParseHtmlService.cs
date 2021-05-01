using HtmlAgilityPack;

namespace Espresso.Dashboard.Application.IServices
{
    public interface IParseHtmlService
    {
        public string? GetSrcAttributeFromFirstImgElement(string? html);

        public string? GetText(string? html);

        public string? GetImageUrlFromSrcAttribute(
            HtmlNodeCollection elementTags,
            string attributeName
        );
    }
}
