// ArticlesListBase.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;
using Espresso.Dashboard.Application.Articles.Queries.GetArticles;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace Espresso.Dashboard.Pages.ArticlesList;

/// <summary>
/// NewsPortalDetailsBase.
/// </summary>
public class ArticlesListBase : ComponentBase
{
    protected GetArticlesQueryResponse? GetArticlesResponse { get; private set; }

    protected PagingParameters PagingParameters { get; set; } = new PagingParameters
    {
        CurrentPage = 1,
        PageSize = 10,
    };

    [Inject]
    private ISender Sender { get; init; } = null!;

    /// <inheritdoc/>
    protected override Task OnInitializedAsync()
    {
        return FetchArticlesList();
    }

    private async Task FetchArticlesList()
    {
        var request = new GetArticlesQuery(pagingParameters: PagingParameters);
        GetArticlesResponse = await Sender.Send(request);
    }

    protected Task OnPageSelected(int page)
    {
        PagingParameters = PagingParameters with { CurrentPage = page };
        return FetchArticlesList();
    }    
}
