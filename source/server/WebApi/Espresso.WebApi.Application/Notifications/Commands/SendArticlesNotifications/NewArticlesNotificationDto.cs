// NewArticlesNotificationDto.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.WebApi.Application.Notifications.Commands.SendArticlesNotifications;

public class NewArticlesNotificationDto
{
    public IEnumerable<NewArticleDto> Articles { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="NewArticlesNotificationDto"/> class.
    /// </summary>
    /// <param name="articles"></param>
#pragma warning disable SA1201 // Elements should appear in the correct order
    public NewArticlesNotificationDto(IEnumerable<NewArticleDto> articles)
#pragma warning restore SA1201 // Elements should appear in the correct order
    {
        Articles = articles;
    }
}
