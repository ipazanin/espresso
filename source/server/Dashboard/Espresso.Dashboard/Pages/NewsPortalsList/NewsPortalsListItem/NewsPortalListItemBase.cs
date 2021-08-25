// NewsPortalListItemBase.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Dashboard.Application.NewsPortals.GetNewsPortals;
using Espresso.Dashboard.Configuration;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;

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
        /// <param name="_"></param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public async Task DeleteButtonClickHandler(MouseEventArgs _)
        {
            var task = DeleteNewsPortalHandler?.Invoke(NewsPortal.Id);
            if (task is not null)
            {
                await task;
            }
        }

        public void DetailsButtonClickHandler(MouseEventArgs _)
        {
            OpenNewsPortalDetailsHandler?.Invoke(NewsPortal.Id);
        }
    }
}
