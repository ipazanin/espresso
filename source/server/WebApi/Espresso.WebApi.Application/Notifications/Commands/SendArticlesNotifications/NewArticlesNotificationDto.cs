// NewArticlesNotificationDto.cs
//
// © 2022 Espresso News. All rights reserved.

namespace Espresso.WebApi.Application.Notifications.Commands.SendArticlesNotifications;

public class NewArticlesNotificationDto
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NewArticlesNotificationDto"/> class.
    /// </summary>
    /// <param name="articles"></param>
    public NewArticlesNotificationDto(IEnumerable<NewArticleDto> articles)
    {
        Articles = articles;
    }

    public IEnumerable<NewArticleDto> Articles { get; }
}
