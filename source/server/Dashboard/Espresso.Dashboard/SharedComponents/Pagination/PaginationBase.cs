// PaginationBase.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Globalization;
using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;
using Microsoft.AspNetCore.Components;

namespace Espresso.Dashboard.SharedComponents.Pagination;

public class PaginationBase : ComponentBase
{
    [Parameter]
    public PagingMetadata? PagingMetadata { get; set; }

    [Parameter]
    public int Spread { get; set; }

    [Parameter]
    public EventCallback<int> SelectedPage { get; set; }

    protected IReadOnlyList<PaginationLink> PagingLinks { get; set; } = Array.Empty<PaginationLink>();

    protected override void OnInitialized()
    {
        CreatePaginationLinks();
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="link"></param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    protected async Task OnSelectedPage(PaginationLink link)
    {
        if (link.Page == PagingMetadata!.CurrentPage || !link.Enabled)
        {
            return;
        }

        PagingMetadata = new PagingMetadata(
            currentPage: link.Page,
            pageSize: PagingMetadata.PageSize,
            totalCount: PagingMetadata.TotalCount);

        await SelectedPage.InvokeAsync(link.Page);

        // It Seems that Blazor server does not re render component on PagingMetadata state change
        // So we manually update links to force UI change
        CreatePaginationLinks();
    }

    protected string GetClass(PaginationLink link)
    {
        var cssClass = "page-item " + (link.Enabled ? " " : "disabled ") + (link.Active ? "active" : string.Empty);
        return cssClass;
    }

    private void CreatePaginationLinks()
    {
        var pagingLinks = new List<PaginationLink>
            {
                new PaginationLink(
                    page: PagingMetadata!.CurrentPage - 1,
                    enabled: PagingMetadata.HasPrevious(),
                    text: "Previous",
                    active: false),
            };

        for (var i = 1; i <= PagingMetadata.TotalPages(); i++)
        {
            if (i >= PagingMetadata.CurrentPage - Spread && i <= PagingMetadata.CurrentPage + Spread)
            {
                pagingLinks
                    .Add(new PaginationLink(
                        page: i,
                        enabled: true,
                        text: i.ToString(CultureInfo.InvariantCulture),
                        active: PagingMetadata.CurrentPage == i));
            }
        }

        pagingLinks
            .Add(new PaginationLink(
                page: PagingMetadata.CurrentPage + 1,
                enabled: PagingMetadata.HasNext(),
                text: "Next",
                active: false));

        PagingLinks = pagingLinks;
    }
}
