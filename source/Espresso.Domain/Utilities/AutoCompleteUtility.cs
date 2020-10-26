using System;
using System.Collections.Generic;
using System.Linq;
using Espresso.Common.Extensions;

namespace Espresso.Domain.Utilities
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