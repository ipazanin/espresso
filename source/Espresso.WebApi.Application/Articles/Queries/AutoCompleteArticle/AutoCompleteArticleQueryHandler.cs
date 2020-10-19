using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.Common.Extensions;
using Espresso.Domain.Entities;
using Espresso.WebApi.Application.Utilities;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.WebApi.Application.Articles.AutoCompleteArticle
{
    public class AutoCompleteArticleQueryHandler : IRequestHandler<AutoCompleteArticleQuery, AutoCompleteArticleQueryResponse>
    {
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

            var matchedWords = new List<string>();
            var matches = Regex
                .Matches(request.TitleSearchQuery, "([a-z]|[A-Z]|ž|Ž|đ|Đ|ć|Ć|č|Č|š|Š)+")
                .Select(match => match.Value);
            var searchTerm = string.Join(" ", matches)
                .ReplaceCroatianCharactersRegex();

            if (string.IsNullOrEmpty(searchTerm))
            {
                return Array.Empty<string>();
            }

            var searchRegexPattern = $"(^| |\n){searchTerm}([a-z])*( |\\.|;|:|,)";
            var replaceDelimiterCharactersRegexPatter = "( |\\.|;|:|,)";
            var matchedArticleTitleWords = articles
                .Select(article => Regex.Matches(article.Title, searchRegexPattern, RegexOptions.IgnoreCase))
                .SelectMany(matches => matches.Select(match => match.Value));

            var matchesWithReplacedDelimiterCharacter = matchedArticleTitleWords
                .Select(word => Regex.Replace(word, replaceDelimiterCharactersRegexPatter, ""));

            matchedWords.AddRange(matchesWithReplacedDelimiterCharacter);


            return matchedWords
                .Distinct(StringComparer.InvariantCultureIgnoreCase)
                .Skip(request.Skip)
                .Take(request.Take);
        }
        #endregion
    }
}