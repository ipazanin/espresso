using System.Collections.Generic;
using System.Linq;
using Espresso.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using MediatR;
using Espresso.Application.DataTransferObjects;

namespace Espresso.WebApi.Application.Notifications.Commands.SendArticlesNotifications
{
    public record SendArticlesNotificationsCommand : Request<Unit>
    {
        public IEnumerable<ArticleDto> CreatedArticles { get; init; } = new List<ArticleDto>();
        public IEnumerable<ArticleDto> UpdatedArticles { get; init; } = new List<ArticleDto>();
    }
}
