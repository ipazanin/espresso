using System;
using System.Collections.Generic;
using System.Linq;
using Espresso.Domain.Entities;
using Espresso.ParserDeleter.Application.IServices;

namespace Espresso.ParserDeleter.Application.Services
{
    public class SortArticlesService : ISortArticlesService
    {
        public (IEnumerable<Article> createdArticles, IEnumerable<Article> updatedArticles) SortArticles(
            IEnumerable<Article> articles,
            IEnumerable<Article> savedArticles
        )
        {
            var createArticles = new List<Article>();
            var updateArticles = new List<Article>();


            var savedArticlesIdDictionary = savedArticles.ToDictionary(
                keySelector: article => article.Id
            );

            var savedArticlesArticleIdDictionary = new Dictionary<(int newsPortalId, string articleId), Guid>();
            var savedArticlesTitleDictionary = new Dictionary<(int newsPortalId, string title), Guid>();
            var savedArticlesSummaryDictionary = new Dictionary<(int newsPortalId, string summary), Guid>();

            foreach (var article in savedArticles)
            {
                _ = savedArticlesArticleIdDictionary.TryAdd((article.NewsPortalId, article.Url), article.Id);
                _ = savedArticlesTitleDictionary.TryAdd((article.NewsPortalId, article.Title), article.Id);
                _ = savedArticlesSummaryDictionary.TryAdd((article.NewsPortalId, article.Summary), article.Id);
            }

            foreach (var article in articles)
            {
                if (
                    savedArticlesArticleIdDictionary.TryGetValue((article.NewsPortalId, article.Url), out var savedArticleId) ||
                    savedArticlesTitleDictionary.TryGetValue((article.NewsPortalId, article.Title), out savedArticleId) ||
                    savedArticlesSummaryDictionary.TryGetValue((article.NewsPortalId, article.Summary), out savedArticleId)
                )
                {
                    var savedArticle = savedArticlesIdDictionary[savedArticleId];

                    if (savedArticle.Update(article))
                    {
                        updateArticles.Add(savedArticle);
                    }
                }
                else
                {
                    createArticles.Add(article);
                }
            }

            return (createArticles, updateArticles);
        }

        public IEnumerable<Article> RemoveDuplicateArticles(IEnumerable<Article> articles)
        {
            var capacity = articles.Count();
            var articleIdArticleDictionary = new Dictionary<(int newsPortalId, string articleId), Guid>(capacity);
            var titleArticleDictionary = new Dictionary<(int newsPortalId, string title), Guid>(capacity);
            var summaryArticleDictionary = new Dictionary<(int newsPortalId, string summary), Guid>(capacity);

            var uniqueArticles = new Dictionary<Guid, Article>(capacity);

            foreach (var article in articles)
            {
                if (
                    articleIdArticleDictionary.TryGetValue((article.NewsPortalId, article.Url), out var alreadyParsedArticleId) ||
                    titleArticleDictionary.TryGetValue((article.NewsPortalId, article.Title), out alreadyParsedArticleId) ||
                    summaryArticleDictionary.TryGetValue((article.NewsPortalId, article.Summary), out alreadyParsedArticleId)
                )
                {
                    if (uniqueArticles.TryGetValue(alreadyParsedArticleId, out var parsedArticle))
                    {
                        parsedArticle.UpdateArticleCategories(article.ArticleCategories);
                    }
                }
                else
                {
                    uniqueArticles.TryAdd(article.Id, article);
                    articleIdArticleDictionary.TryAdd((article.NewsPortalId, article.Url), article.Id);
                    titleArticleDictionary.TryAdd((article.NewsPortalId, article.Title), article.Id);
                    summaryArticleDictionary.TryAdd((article.NewsPortalId, article.Summary), article.Id);
                }
            }

            return uniqueArticles.Values;
        }

    }
}