// GetNewsPortalDetailsQuery.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using MediatR;

namespace Espresso.Dashboard.Application.NewsPortals.GetNewsPortalDetails
{
    /// <summary>
    /// Get news portal details query.
    /// </summary>
    public class GetNewsPortalDetailsQuery : IRequest<GetNewsPortalDetailsQueryResponse>
    {
        /// <summary>
        /// Gets <see cref="NewsPortal"/> id.
        /// </summary>
        public int NewsPortalId { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetNewsPortalDetailsQuery"/> class.
        /// </summary>
        /// <param name="newsPortalId"><see cref="NewsPortal"/> id.</param>
#pragma warning disable SA1201 // Elements should appear in the correct order
        public GetNewsPortalDetailsQuery(int newsPortalId)
#pragma warning restore SA1201 // Elements should appear in the correct order
        {
            NewsPortalId = newsPortalId;
        }
    }
}
