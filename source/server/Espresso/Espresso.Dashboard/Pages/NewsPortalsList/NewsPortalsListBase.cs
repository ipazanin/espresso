// NewsPortalsListBase.cs
//
// © 2021 Espresso News. All rights reserved.

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
    /// Base class for News Portal List Component.
    /// </summary>
    [Authorize(Roles = RoleConstants.AdminRoleName)]
    public class NewsPortalsListBase : ComponentBase
    {
        [Inject]
        protected ISender Sender { get; set; } = null!;

        protected GetNewsPortalsQueryResponse? Response { get; private set; }

        protected PagingParameters PagingParameters { get; set; } = new PagingParameters
        {
            CurrentPage = 1,
            PageSize = 10,
        };

        protected override Task OnInitializedAsync()
        {
            return FetchNewsPortals();
        }

        private async Task FetchNewsPortals()
        {
            Response = await Sender.Send(
                request: new GetNewsPortalsQuery(
                    pagingParameters: PagingParameters
                )
            );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="page"></param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        protected Task SelectedPage(int page)
        {
            PagingParameters = PagingParameters with { CurrentPage = page };
            return FetchNewsPortals();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="newsPortalId"></param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        protected Task DeleteNewsPortal(int newsPortalId)
        {
            // throw new NotImplementedException();
            return Task.CompletedTask;
        }

        protected void OpenNewsPortalDetails(int newsPortalId)
        {
            //NavigationManager.NavigateTo(uri: $"/news-portals/{newsPortalId}");
        }
    }
}
