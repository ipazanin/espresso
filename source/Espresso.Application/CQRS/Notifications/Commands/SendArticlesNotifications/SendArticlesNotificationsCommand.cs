using System.Collections.Generic;
using System.Linq;
using Espresso.Application.DataTransferObjects;
using Espresso.Application.Infrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using MediatR;

namespace Espresso.Application.CQRS.Notifications.Commands.SendArticlesNotifications
{
    public class SendArticlesNotificationsCommand : Request<Unit>
    {
        public SendArticlesNotificationsCommand(
            IEnumerable<ArticleDto> createdArticles,
            IEnumerable<ArticleDto> updatedArticles,
            string currentEspressoWebApiVersion,
            string targetedEspressoWebApiVersion,
            string consumerVersion,
            DeviceType deviceType,
            AppEnvironment appEnvironment
        ) : base(
            currentEspressoWebApiVersion: currentEspressoWebApiVersion,
            targetedEspressoWebApiVersion: targetedEspressoWebApiVersion,
            consumerVersion: consumerVersion,
            deviceType: deviceType,
            appEnvironment: appEnvironment,
            Event.SendArticlesNotificationsQuery
        )
        {
            CreatedArticles = createdArticles;
            UpdatedArticles = updatedArticles;
        }

        public IEnumerable<ArticleDto> CreatedArticles { get; }
        public IEnumerable<ArticleDto> UpdatedArticles { get; }

        public override string ToString()
        {
            return $"{nameof(CreatedArticles)}:{CreatedArticles.Count()}, {nameof(UpdatedArticles)}:{UpdatedArticles.Count()}";
        }
    }
}
