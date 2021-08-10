// NewSourcesRequestCommand.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Application.Infrastructure.MediatorInfrastructure;
using MediatR;

namespace Espresso.WebApi.Application.NewsPortals.Commands.NewSourcesRequest
{
    public record NewsSourcesRequestCommand : Request<Unit>
    {
        public string NewsPortalName { get; init; } = string.Empty;

        public string Email { get; init; } = string.Empty;

        public string? Url { get; init; }
    }
}
