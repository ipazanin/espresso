// GetArticlesQuery.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;
using Espresso.Application.Infrastructure.MediatorInfrastructure;

namespace Espresso.Dashboard.Application.Articles.Queries.GetArticles;

public class GetArticlesQueryResponse
{
    public GetArticlesQueryResponse(PagedList<GetArticlesArticle> articles)
    {
        Articles = articles;
    }

    public PagedList<GetArticlesArticle> Articles { get; }
}
