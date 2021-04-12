﻿using System;
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
            IEnumerable<ArticleCategory> createArticleCategories,
            IEnumerable<ArticleCategory> deleteArticleCategories
        ) SortArticles(
            IEnumerable<Article> articles,
            IDictionary<Guid, Article> savedArticles
        )
        {
            var createArticles = new List<Article>();
            var updateArticles = new List<Article>();
            var createArticleCategories = new List<ArticleCategory>();
            var deleteArticleCategories = new List<ArticleCategory>();

            var savedArticlesArticleIdDictionary = new Dictionary<(int newsPortalId, string articleUrl), Guid>();
            var savedArticlesTitleDictionary = new Dictionary<(int newsPortalId, string title), Guid>();
            var savedArticlesSummaryDictionary = new Dictionary<(int newsPortalId, string summary), Guid>();

            foreach (var (id, article) in savedArticles)
            {
                savedArticlesArticleIdDictionary.TryAdd((article.NewsPortalId, article.Url), id);
                savedArticlesTitleDictionary.TryAdd((article.NewsPortalId, article.Title), id);
                savedArticlesSummaryDictionary.TryAdd((article.NewsPortalId, article.Summary), id);
            }

            foreach (var article in articles)
            {
                if (
                    savedArticlesArticleIdDictionary.TryGetValue((article.NewsPortalId, article.Url), out var savedArticleId) ||
                    savedArticlesTitleDictionary.TryGetValue((article.NewsPortalId, article.Title), out savedArticleId) ||
                    savedArticlesSummaryDictionary.TryGetValue((article.NewsPortalId, article.Summary), out savedArticleId)
                )
                {
                    var savedArticle = savedArticles[savedArticleId];
                    var (shouldUpdate, articleCategoriesToCreate, articleCategoriesToDelete) = savedArticle.Update(article);
                    if (shouldUpdate)
                    {
                        createArticleCategories.AddRange(articleCategoriesToCreate);
                        deleteArticleCategories.AddRange(articleCategoriesToDelete);
                        updateArticles.Add(savedArticle);
                    }
                }
                else
                {
                    createArticles.Add(article);
                }
            }

            return (createArticles, updateArticles, createArticleCategories, deleteArticleCategories);
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
