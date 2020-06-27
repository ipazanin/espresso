using System.Collections.Generic;
using System.Linq;
using Espresso.Application.DataTransferObjects;
using Espresso.Application.Infrastructure;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using MediatR;
using Espresso.Common.Enums;

namespace Espresso.Application.CQRS.Notifications.Queries.SendArticlesNotifications
{
    public class SendArticlesNotificationsQuery : Request<Unit>
    {
        public SendArticlesNotificationsQuery(
            IEnumerable<ArticleDto> createdArticles,
            IEnumerable<ArticleDto> updatedArticles,
            string currentEspressoWebApiVersion,
            string espressoWebApiVersion,
            string version,
            DeviceType deviceType
        ) : base(
            currentEspressoWebApiVersion,
            espressoWebApiVersion,
            version,
            deviceType,
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
