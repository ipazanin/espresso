using System;
using System.Collections.Generic;
using System.Linq;
using Espresso.Domain.Entities;
using Espresso.Domain.IServices;

namespace Espresso.Domain.Services
{
    public class SortArticlesService : ISortArticlesService
    {
        public (
            IEnumerable<Article> createdArticles,
            IEnumerable<Article> updatedArticles,
            IEnumerable<ArticleCategory> articleCategoriesToCreate,
            IEnumerable<ArticleCategory> articleCategoriesToDelete
        ) SortArticles(
            IEnumerable<Article> articles,
            IEnumerable<Article> savedArticles
        )
        {
            var createArticles = new List<Article>();
            var updateArticles = new List<Article>();
            var articleCategoriesToCreate = new List<ArticleCategory>();
            var articleCategoriesToDelete = new List<ArticleCategory>();

            var savedArticlesIdDictionary = savedArticles.ToDictionary(
                keySelector: article => article.Id
            );

            var savedArticlesArticleIdDictionary = new Dictionary<(int newsPortalId, string articleUrl), Guid>();
            var savedArticlesTitleDictionary = new Dictionary<(int newsPortalId, string title), Guid>();
            var savedArticlesSummaryDictionary = new Dictionary<(int newsPortalId, string summary), Guid>();

            foreach (var article in savedArticles)
            {
                savedArticlesArticleIdDictionary.TryAdd((article.NewsPortalId, article.Url), article.Id);
                savedArticlesTitleDictionary.TryAdd((article.NewsPortalId, article.Title), article.Id);
                savedArticlesSummaryDictionary.TryAdd((article.NewsPortalId, article.Summary), article.Id);
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
                    var (shouldUpdate, createArticleCategories, deleteArticleCategories) = savedArticle.Update(article);
                    if (shouldUpdate)
                    {
                        articleCategoriesToCreate.AddRange(createArticleCategories);
                        articleCategoriesToDelete.AddRange(deleteArticleCategories);
                        updateArticles.Add(savedArticle);
                    }
                }
                else
                {
                    createArticles.Add(article);
                    articleCategoriesToCreate.AddRange(article.ArticleCategories);
                }
            }

            return (createArticles, updateArticles, articleCategoriesToCreate, articleCategoriesToDelete);
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