// ArticleIQueryableExtensions.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;
using Espresso.Domain.Entities;

namespace Espresso.Dashboard.Application.Articles.Queries.GetArticles;

public static class ArticleIQueryableExtensions
{
    public static IQueryable<Article> OrderArticles(
        this IQueryable<Article> articles,
        PagingParameters pagingParameters)
    {
        return (pagingParameters.OrderType, pagingParameters.SortColumn) switch
        {
            (OrderType.Ascending, nameof(GetArticlesArticle.Title)) => articles.OrderBy(article => article.Title),
            (OrderType.Descending, nameof(GetArticlesArticle.Title)) => articles.OrderByDescending(article => article.Title),
            (OrderType.Ascending, nameof(GetArticlesArticle.NewsPortal)) => articles.OrderBy(article => article.NewsPortal!.Name),
            (OrderType.Descending, nameof(GetArticlesArticle.NewsPortal)) => articles.OrderByDescending(article => article.NewsPortal!.Name),
            (OrderType.Ascending, nameof(GetArticlesArticle.Categories)) => articles.OrderBy(article => article.ArticleCategories.First().Category!.Name),
            (OrderType.Descending, nameof(GetArticlesArticle.Categories)) => articles.OrderByDescending(article => article.ArticleCategories.First().Category!.Name),
            (OrderType.Ascending, nameof(GetArticlesArticle.Created)) => articles.OrderBy(article => article.CreateDateTime),
            (OrderType.Descending, nameof(GetArticlesArticle.Created)) => articles.OrderByDescending(article => article.CreateDateTime),
            _ => articles.OrderByDescending(article => article.CreateDateTime),
        };
    }

    public static IQueryable<Article> FilterArticles(
        this IQueryable<Article> articles,
        PagingParameters pagingParameters)
    {
        if (string.IsNullOrWhiteSpace(pagingParameters.SearchString))
        {
            return articles;
        }

        return articles.Where(article => article.Title.Contains(pagingParameters.SearchString) ||
            article.NewsPortal!.Name.Contains(pagingParameters.SearchString) ||
            article.ArticleCategories.Any(articleCategory => articleCategory.Category!.Name.Contains(pagingParameters.SearchString, StringComparison.Ordinal)));
    }
}
