using System.Collections.Generic;
using Espresso.Application.Infrastructure.MediatorInfrastructure;
using MediatR;
using Espresso.Application.DataTransferObjects.ArticleDataTransferObjects;

namespace Espresso.WebApi.Application.Notifications.Commands.SendArticlesNotifications
{
    public record SendArticlesNotificationsCommand : Request<Unit>
    {
        public IEnumerable<ArticleDto> CreatedArticles { get; init; } = new List<ArticleDto>();
        public IEnumerable<ArticleDto> UpdatedArticles { get; init; } = new List<ArticleDto>();
    }
}
