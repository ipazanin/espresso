using System.Collections.Generic;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Espresso.WebApi.Application.IService
{
    public interface IParseHtmlService
    {
        public string? GetSrcAttributeFromFirstImgElement(string? html);

        public string? GetText(string? html);

        public Task<string?> GetImageUrlFromJsonObjectFromScriptTag(
            HtmlNodeCollection elementTags,
            IEnumerable<string> propertyNames
        );

        public string? GetImageUrlFromSrcAttribute(HtmlNodeCollection elementTags);
    }
}