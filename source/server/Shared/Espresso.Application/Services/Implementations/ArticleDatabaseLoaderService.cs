// ArticleDatabaseLoaderService.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.Services.Contracts;
using Espresso.Domain.Entities;
using Espresso.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Application.Services.Implementations;

public class ArticleDatabaseLoaderService : IArticleLoaderService
{
    private readonly IEspressoDatabaseContext _espressoDatabaseContext;

    public ArticleDatabaseLoaderService(IEspressoDatabaseContext espressoDatabaseContext)
    {
        _espressoDatabaseContext = espressoDatabaseContext;
    }

    public async Task<IEnumerable<Article>> LoadArticlesForWebApi(
        IEnumerable<NewsPortal> newsPortals,
        IEnumerable<Category> categories,
        CancellationToken cancellationToken)
    {
        var articles = await LoadArticles()
            .ToListAsync(cancellationToken);

        UpdateArticleRelations(
            articles: articles,
            newsPortals: newsPortals,
            categories: categories);

        return articles;
    }

    public async Task<IEnumerable<Article>> LoadArticlesForWebApi(
        ISet<Guid> articleIds,
        IEnumerable<NewsPortal> newsPortals,
        IEnumerable<Category> categories,
        CancellationToken cancellationToken)
    {
        var articles = await LoadArticles()
            .Where(article => articleIds.Contains(article.Id))
            .ToListAsync(cancellationToken);

        UpdateArticleRelations(
            articles: articles,
            newsPortals: newsPortals,
            categories: categories);

        return articles;
    }

    private static void UpdateArticleRelations(
        IEnumerable<Article> articles,
        IEnumerable<NewsPortal> newsPortals,
        IEnumerable<Category> categories)
    {
        var newsPortalsDictionary = newsPortals
            .ToDictionary(newsPortal => newsPortal.Id);

        var categoriesDictionary = categories.ToDictionary(category => category.Id);

        var articlesDictionary = articles.ToDictionary(article => article.Id);

        foreach (var article in articles)
        {
            var newsPortal = newsPortalsDictionary[article.NewsPortalId];
            article.SetNewsPortal(newsPortal);

            foreach (var articleCategory in article.ArticleCategories)
            {
                var category = categoriesDictionary[articleCategory.CategoryId];
                articleCategory.SetCategory(category);
            }

            foreach (var similarArticle in article.FirstSimilarArticles)
            {
                if (articlesDictionary.TryGetValue(similarArticle.SecondArticleId, out var secondArticle))
                {
                    similarArticle.SetSecondArticle(secondArticle);
                    secondArticle.SecondSimilarArticles.Add(similarArticle);
                }
            }

            foreach (var similarArticle in article.SecondSimilarArticles)
            {
                if (articlesDictionary.TryGetValue(similarArticle.FirstArticleId, out var firstArticle))
                {
                    similarArticle.SetFirstArticle(firstArticle);
                    firstArticle.FirstSimilarArticles.Add(similarArticle);
                }
            }
        }
    }

    private IQueryable<Article> LoadArticles()
    {
        var articles = _espressoDatabaseContext
                    .Articles
                    .Include(article => article.ArticleCategories)
                    .Include(article => article.FirstSimilarArticles)
                    .Include(article => article.SecondSimilarArticles)
                    .AsSplitQuery()
                    .AsNoTracking();

        return articles;
    }
}
