using System.Collections.Generic;
using Espresso.Application.Infrastructure.MediatorInfrastructure;
using MediatR;
using Espresso.Application.DataTransferObjects;
using System;

namespace Espresso.WebApi.Application.Notifications.Commands.SendArticlesNotifications
{
    public record SendArticlesNotificationsCommand : Request<Unit>
    {
        public IEnumerable<Guid> CreatedArticleIds { get; init; } = new List<Guid>();
        public IEnumerable<Guid> UpdatedArticleIds { get; init; } = new List<Guid>();
    }
}
