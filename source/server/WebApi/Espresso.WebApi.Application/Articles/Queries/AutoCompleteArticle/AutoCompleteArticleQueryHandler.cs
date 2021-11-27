// AutoCompleteArticleQueryHandler.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Domain.Extensions;
using Espresso.Domain.Utilities;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.WebApi.Application.Articles.AutoCompleteArticle;

public class AutoCompleteArticleQueryHandler : IRequestHandler<AutoCompleteArticleQuery, AutoCompleteArticleQueryResponse>
{
    private readonly IMemoryCache _memoryCache;

    /// <summary>
    /// Initializes a new instance of the <see cref="AutoCompleteArticleQueryHandler"/> class.
    /// </summary>
    /// <param name="memoryCache"></param>
    public AutoCompleteArticleQueryHandler(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public Task<AutoCompleteArticleQueryResponse> Handle(AutoCompleteArticleQuery request, CancellationToken cancellationToken)
    {
        var articles = _memoryCache.Get<IEnumerable<Article>>(MemoryCacheConstants.ArticleKey);

        var keyWordsToFilterOut = request.KeyWordsToFilterOut is null ?
            Enumerable.Empty<string>() :
            request.KeyWordsToFilterOut.Split(',', StringSplitOptions.RemoveEmptyEntries);

        var matchedWords = GetMatchedWords(request, articles, keyWordsToFilterOut);

        var result = new AutoCompleteArticleQueryResponse
        {
            MatchedWords = matchedWords,
        };

        return Task.FromResult(result);
    }

    private static IEnumerable<string> GetMatchedWords(
        AutoCompleteArticleQuery request,
        IEnumerable<Article> articles,
        IEnumerable<string> keyWordsToFilterOut)
    {
        if (request.TitleSearchQuery is null)
        {
            return Array.Empty<string>();
        }

        var result = new List<string> { request.TitleSearchQuery };

        var matchedWords = new List<string>();
        var separatedWords = LanguageUtility.SeparateWords(request.TitleSearchQuery);

        var searchTerm = string.Join(" ", separatedWords);

        if (string.IsNullOrEmpty(searchTerm))
        {
            return result;
        }

        var matchedArticleTitleWords = articles
            .FilterArticlesContainingKeyWords(keyWordsToFilterOut)
            .SelectMany(article => LanguageUtility.MatchWordsThatBeginWithTerm(searchTerm, article.Title));

        matchedWords.AddRange(matchedArticleTitleWords);

        var filteredMatches = matchedWords
            .RemoveUnImpactfulCroatianWords()
            .Distinct(StringComparer.InvariantCultureIgnoreCase)
            .OrderBy(matchedWord => matchedWord)
            .Skip(request.Skip)
            .Take(request.Take);

        return result.Union(filteredMatches, StringComparer.InvariantCultureIgnoreCase);
    }
}
