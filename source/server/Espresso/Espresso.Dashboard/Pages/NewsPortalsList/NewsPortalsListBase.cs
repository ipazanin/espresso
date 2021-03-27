using System;
using System.Threading.Tasks;
using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;
using Espresso.Common.Constants;
using Espresso.Dashboard.Application.NewsPortals.GetNewsPortals;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace Espresso.Dashboard.Pages.NewsPortalsList
{
    /// <summary>
    /// </summary>
    [Authorize(Roles = RoleConstants.AdminRoleName)]
    public class NewsPortalsListBase : ComponentBase
    {
        #region Properties
        [Inject]
        protected ISender Sender { get; set; } = null!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = null!;

        protected GetNewsPortalsQueryResponse? Response { get; private set; }

        protected PagingParameters PagingParameters { get; set; } = new PagingParameters
        {
            CurrentPage = 1,
            PageSize = 10
        };
        #endregion Properties

        #region Methods
        protected override async Task OnInitializedAsync()
        {
            await FetchNewsPortals();
        }

        private async Task FetchNewsPortals()
        {
            Response = await Sender.Send(
                request: new GetNewsPortalsQuery(
                    pagingParameters: PagingParameters
                )
            );
        }

        protected async Task SelectedPage(int page)
        {
            PagingParameters = PagingParameters with { CurrentPage = page };
            await FetchNewsPortals();
        }

        protected Task DeleteNewsPortal(int newsPortalId)
        {
            // throw new NotImplementedException();
            return Task.CompletedTask;
        }

        protected void OpenNewsPortalDetails(int newsPortalId)
        {
            //NavigationManager.NavigateTo(uri: $"/news-portals/{newsPortalId}");
        }
        #endregion Methods
    }
}
