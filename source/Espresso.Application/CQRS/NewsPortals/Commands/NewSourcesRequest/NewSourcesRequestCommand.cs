using Espresso.Application.Infrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using MediatR;

namespace Espresso.Application.CQRS.NewsPortals.Commands.NewSourcesRequest
{
    public class NewsSourcesRequestCommand : Request<Unit>
    {
        public string NewsPortalName { get; }

        public string Email { get; }

        public string? Url { get; }

        public NewsSourcesRequestCommand(
            string currentEspressoWebApiVersion,
            string espressoWebApiVersion,
            string version,
            DeviceType deviceType,
            string newsPortalName,
            string email,
            string? url
        ) : base(
            currentEspressoWebApiVersion,
            espressoWebApiVersion,
            version,
            deviceType,
            Event.NewSourcesRequest
        )
        {
            NewsPortalName = newsPortalName;
            Email = email;
            Url = url;
        }
    }
}
