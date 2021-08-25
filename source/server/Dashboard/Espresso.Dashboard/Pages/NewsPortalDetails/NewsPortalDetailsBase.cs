// NewsPortalDetailsBase.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Dashboard.Application.NewsPortals.GetNewsPortalDetails;
using Espresso.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Espresso.Dashboard.Pages.NewsPortalDetails
{
    /// <summary>
    /// NewsPortalDetailsBase.
    /// </summary>
    public class NewsPortalDetailsBase : ComponentBase
    {
        /// <summary>
        /// Gets <see cref="NewsPortal"/> id.
        /// </summary>
        [Parameter]
        public int Id { get; init; }

        /// <summary>
        /// Gets <see cref="Mediator"/> request sender.
        /// </summary>
        [Inject]
        private ISender Sender { get; init; } = null!;

        /// <summary>
        /// Gets or sets <see cref="NewsPortal"/> details.
        /// </summary>
        protected Application.NewsPortals.GetNewsPortalDetails.NewsPortalDetails? NewsPortalDetails { get; set; }

        /// <inheritdoc/>
        protected override async Task OnInitializedAsync()
        {
            var getNewsPortalDetailsQueryResponse = await Sender.Send(new GetNewsPortalDetailsQuery(Id));

            NewsPortalDetails = getNewsPortalDetailsQueryResponse.NewsPortalDetails;
        }
    }
}
