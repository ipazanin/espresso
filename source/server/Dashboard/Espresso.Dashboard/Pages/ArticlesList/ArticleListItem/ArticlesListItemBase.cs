// ArticlesListItemBase.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Dashboard.Application.Articles.Queries.GetArticles;
using Microsoft.AspNetCore.Components;

namespace Espresso.Dashboard.Pages.ArticlesList.ArticleListItem;

/// <summary>
/// NewsPortalDetailsBase.
/// </summary>
public class ArticlesListItemBase : ComponentBase
{
    [Parameter]
    [EditorRequired]
    public GetArticlesArticle Article { get; init; } = null!;
}
