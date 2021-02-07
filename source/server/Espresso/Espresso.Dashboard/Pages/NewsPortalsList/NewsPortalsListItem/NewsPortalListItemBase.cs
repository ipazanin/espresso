using System;
using System.Threading.Tasks;
using Espresso.Dashboard.Application.NewsPortals.GetNewsPortals;
using Microsoft.AspNetCore.Components;

namespace Espresso.Dashboard.Pages.NewsPortalsList.NewsPortalsListItem
{
    public class NewsPortalListItemBase : ComponentBase
    {
        #region Properties
        [Parameter]
        public GetNewsPortalsNewsPortal NewsPortal { get; set; } = null!;

        [Parameter]
        public Func<int, Task>? DeleteNewsPortalHandler { get; set; }

        [Parameter]
        public Action<int>? OpenNewsPortalDetailsHandler { get; set; }
        #endregion Properties

        #region Methods
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
        #endregion Methods
    }
}
