// NewsPortalListItemBase.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Dashboard.Application.NewsPortals.GetNewsPortals;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Espresso.Dashboard.Pages.NewsPortalsList.NewsPortalsListItem
{
    public class NewsPortalListItemBase : ComponentBase
    {
        [Parameter]
        public GetNewsPortalsNewsPortal NewsPortal { get; set; } = null!;

        [Parameter]
        public Func<int, Task>? DeleteNewsPortalHandler { get; set; }

        [Parameter]
        public Action<int>? OpenNewsPortalDetailsHandler { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public async Task DeleteButtonClickHandler()
        {
            var task = DeleteNewsPortalHandler?.Invoke(NewsPortal.Id);
            if (task is not null)
            {
                await task;
            }
        }

        public void DetailsButtonClickHandler()
        {
            OpenNewsPortalDetailsHandler?.Invoke(NewsPortal.Id);
        }
    }
}
