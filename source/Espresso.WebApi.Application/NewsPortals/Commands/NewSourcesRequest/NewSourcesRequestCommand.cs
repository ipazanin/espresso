using Espresso.Application.Infrastructure.MediatorInfrastructure;
using MediatR;

namespace Espresso.WebApi.Application.NewsPortals.Commands.NewSourcesRequest
{
    public record NewsSourcesRequestCommand : Request<Unit>
    {
        public string NewsPortalName { get; init; } = "";

        public string Email { get; init; } = "";

        public string? Url { get; init; }
    }
}
