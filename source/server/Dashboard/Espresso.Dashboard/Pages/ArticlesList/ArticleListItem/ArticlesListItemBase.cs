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
public class ArticlesListItemBase : ComponentBase
{
    [Parameter]
    [EditorRequired]
    public GetArticlesArticle Article { get; init; } = null!;
}
