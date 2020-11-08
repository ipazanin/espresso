﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Espresso.ParserDeleter.Application.IServices
{
    public interface IParseHtmlService
    {
        public string? GetSrcAttributeFromFirstImgElement(string? html);

        public string? GetText(string? html);

        public string? GetImageUrlFromSrcAttribute(HtmlNodeCollection elementTags);
    }
}