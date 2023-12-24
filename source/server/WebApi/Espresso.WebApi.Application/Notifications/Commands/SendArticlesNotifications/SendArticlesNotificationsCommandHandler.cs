// SendArticlesNotificationsCommandHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Persistence.Database;
using Espresso.WebApi.Application.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Espresso.WebApi.Application.Notifications.Commands.SendArticlesNotifications;

public class SendArticlesNotificationsCommandHandler : IRequestHandler<SendArticlesNotificationsCommand>
{
    public const string LatestArticlesClientMethodName = "GetNewArticles";
    private readonly IHubContext<ArticlesNotificationHub> _hubContext;
    private readonly IEspressoDatabaseContext _espressoDatabaseContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="SendArticlesNotificationsCommandHandler"/> class.
    /// </summary>
    /// <param name="hubContext"></param>
    /// <param name="espressoDatabaseContext"></param>
    public SendArticlesNotificationsCommandHandler(
        IHubContext<ArticlesNotificationHub> hubContext,
        IEspressoDatabaseContext espressoDatabaseContext)
    {
        _hubContext = hubContext;
        _espressoDatabaseContext = espressoDatabaseContext;
    }

    public async Task Handle(SendArticlesNotificationsCommand request, CancellationToken cancellationToken)
    {
        if (!request.CreatedArticleIds.Any())
        {
            return;
        }

        var createArticleIds = request
            .CreatedArticleIds
            .ToHashSet();

        var newArticles = await _espressoDatabaseContext
            .Articles
            .Where(article => createArticleIds.Contains(article.Id))
            .Select(NewArticleDto.Projection)
            .ToListAsync(cancellationToken);

        var newArticlesNotificationDto = new NewArticlesNotificationDto(newArticles);

        await _hubContext.Clients.All.SendAsync(
            method: LatestArticlesClientMethodName,
            arg1: newArticlesNotificationDto,
            cancellationToken: cancellationToken);
    }
}
