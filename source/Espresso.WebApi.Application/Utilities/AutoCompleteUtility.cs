using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Espresso.Common.Extensions;
using Espresso.Domain.Entities;

namespace Espresso.WebApi.Application.Utilities
{
    public static class AutoCompleteUtility
    {

        public static IEnumerable<string> GetSearchTerms(string? searchTerm)
        {
            if (searchTerm is null)
            {
                return Array.Empty<string>();
            }

            var searchTerms = searchTerm
                .RemoveExtraWhiteSpaceCharacters()
                .Split(" ")
                .Where(keyword => !string.IsNullOrEmpty(keyword))
                .Select(keyword => keyword.ReplaceCroatianCharacters());

            return searchTerms;
        }
    }
}