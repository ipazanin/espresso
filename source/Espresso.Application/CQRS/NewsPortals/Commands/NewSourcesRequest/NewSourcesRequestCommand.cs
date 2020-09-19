using Espresso.Application.Infrastructure.MediatorInfrastructure;
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
            string targetedEspressoWebApiVersion,
            string consumerVersion,
            DeviceType deviceType,
            AppEnvironment appEnvironment,
            string newsPortalName,
            string email,
            string? url
        ) : base(
            currentEspressoWebApiVersion: currentEspressoWebApiVersion,
            targetedEspressoWebApiVersion: targetedEspressoWebApiVersion,
            consumerVersion: consumerVersion,
            deviceType: deviceType,
            appEnvironment: appEnvironment,
            Event.NewSourcesRequest
        )
        {
            NewsPortalName = newsPortalName;
            Email = email;
            Url = url;
        }

        public override string ToString()
        {
            return $"{nameof(NewsPortalName)}:{NewsPortalName}, {nameof(Email)}:{Email}, {nameof(Url)}:{Url}";
        }
    }
}
