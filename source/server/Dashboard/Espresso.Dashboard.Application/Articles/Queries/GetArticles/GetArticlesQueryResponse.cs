// GetArticlesQueryResponse.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;

namespace Espresso.Dashboard.Application.Articles.Queries.GetArticles;

public class GetArticlesQueryResponse
{
    public GetArticlesQueryResponse(PagedList<GetArticlesArticle> articles)
    {
        Articles = articles;
    }

    public PagedList<GetArticlesArticle> Articles { get; }
}
