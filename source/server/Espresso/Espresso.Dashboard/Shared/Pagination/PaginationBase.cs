using System.Collections.Generic;
using System.Threading.Tasks;
using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;
using Microsoft.AspNetCore.Components;

namespace Espresso.Dashboard.Shared.Pagination
{
    public class PaginationBase : ComponentBase
    {
        [Parameter]
        public PagingMetadata? PagingMetadata { get; set; }

        [Parameter]
        public int Spread { get; set; }

        [Parameter]
        public EventCallback<int> SelectedPage { get; set; }

        protected List<PagingLink> PagingLinks { get; set; } = new List<PagingLink>();

        protected override void OnInitialized()
        {
            CreatePaginationLinks();
        }

        private void CreatePaginationLinks()
        {
            PagingLinks = new List<PagingLink>
            {
                new PagingLink(
                    page: PagingMetadata!.CurrentPage - 1,
                    enabled: PagingMetadata.HasPrevious(),
                    text: "Previous",
                    active: false
                )
            };

            for (var i = 1; i <= PagingMetadata.TotalPages(); i++)
            {
                if (i >= PagingMetadata.CurrentPage - Spread && i <= PagingMetadata.CurrentPage + Spread)
                {
                    PagingLinks
                        .Add(new PagingLink(
                            page: i,
                            enabled: true,
                            text: i.ToString(),
                            active: PagingMetadata.CurrentPage == i
                        ));
                }
            }
            PagingLinks
                .Add(new PagingLink(
                    page: PagingMetadata.CurrentPage + 1,
                    enabled: PagingMetadata.HasNext(),
                    text: "Next",
                    active: false
                ));

            System.Console.WriteLine("-------------------------------------------------------------");
            System.Console.WriteLine("PaginationBase CreatePaginationLinks");
            System.Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(PagingMetadata));
            System.Console.WriteLine("-------------------------------------------------------------");
        }

        protected async Task OnSelectedPage(PagingLink link)
        {
            if (link.Page == PagingMetadata!.CurrentPage || !link.Enabled)
            {
                return;
            }

            PagingMetadata = new PagingMetadata(
                currentPage: link.Page,
                pageSize: PagingMetadata.PageSize,
                totalCount: PagingMetadata.TotalCount
            );

            await SelectedPage.InvokeAsync(link.Page);

            // It Seems that Blazor server does not re render component on PagingMetadata state change
            // So we manually update links to force UI change 
            CreatePaginationLinks();
        }

        protected string GetClass(PagingLink link)
        {
            var cssClass = "page-item " + (link.Enabled ? " " : "disabled ") + (link.Active ? "active" : "");
            return cssClass;
        }
    }
}
