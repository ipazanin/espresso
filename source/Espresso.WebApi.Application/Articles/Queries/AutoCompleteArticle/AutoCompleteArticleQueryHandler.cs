using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.Common.Extensions;
using Espresso.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.WebApi.Application.Articles.AutoCompleteArticle
{
    public class AutoCompleteArticleQueryHandler : IRequestHandler<AutoCompleteArticleQuery, AutoCompleteArticleQueryResponse>
    {
        #region Constants
        private const string AllowedCharactersRegex = "([a-z]|[A-Z]|[0-9]|ž|Ž|đ|Đ|ć|Ć|č|Č|š|Š)";
        private const string DelimiterCharacters = "( |\\.|;|:|,)";
        private const string StartOfWordCharacters = "(^| |\n)";
        private const string EscapedAutoCompleteWords = "(ako|ili|da|ne)";
        #endregion

        #region Fields
        private readonly IMemoryCache _memoryCache;
        #endregion

        #region Constructors
        public AutoCompleteArticleQueryHandler(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        #endregion

        #region Methods
        public Task<AutoCompleteArticleQueryResponse> Handle(AutoCompleteArticleQuery request, CancellationToken cancellationToken)
        {
            var articles = _memoryCache.Get<IEnumerable<Article>>(MemoryCacheConstants.ArticleKey);

            var matchedWords = GetMatchedWords(request, articles);

            var result = new AutoCompleteArticleQueryResponse
            {
                MatchedWords = matchedWords
            };

            return Task.FromResult(result);
        }

        public static IEnumerable<string> GetMatchedWords(
            AutoCompleteArticleQuery request,
            IEnumerable<Article> articles
        )
        {
            if (request.TitleSearchQuery is null)
            {
                return Array.Empty<string>();
            }

            var result = new List<string> { request.TitleSearchQuery };

            var matchedWords = new List<string>();
            var matches = Regex
                .Matches(request.TitleSearchQuery, $"{AllowedCharactersRegex}+")
                .Select(match => match.Value);

            var searchTerm = string.Join(" ", matches)
                .ReplaceCroatianCharactersRegex();

            if (string.IsNullOrEmpty(searchTerm))
            {
                return result;
            }

            var searchRegexPattern = $"{StartOfWordCharacters}{searchTerm}{AllowedCharactersRegex}*{DelimiterCharacters}";
            var replaceDelimiterCharactersRegexPatter = DelimiterCharacters;
            var matchedArticleTitleWords = articles
                .Select(article => Regex.Matches(article.Title, searchRegexPattern, RegexOptions.IgnoreCase))
                .SelectMany(matches => matches.Select(match => match.Value));

            var matchesWithReplacedDelimiterCharacter = matchedArticleTitleWords
                .Select(word => Regex.Replace(word, replaceDelimiterCharactersRegexPatter, ""));

            matchedWords.AddRange(matchesWithReplacedDelimiterCharacter);

            var filteredMatches = matchedWords
                .Where(matchedWord => !Regex.IsMatch(matchedWord, EscapedAutoCompleteWords, RegexOptions.IgnoreCase))
                .Distinct(StringComparer.InvariantCultureIgnoreCase)
                .OrderBy(matchedWord => matchedWord)
                .Skip(request.Skip)
                .Take(request.Take);

            result.AddRange(filteredMatches);

            return result;
        }
        #endregion
    }
}