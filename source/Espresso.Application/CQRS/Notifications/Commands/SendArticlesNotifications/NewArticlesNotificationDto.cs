using System.Collections.Generic;

namespace Espresso.Application.CQRS.Notifications.Commands.SendArticlesNotifications
{
    public class NewArticlesNotificationDto
    {
        public IEnumerable<NewArticleDto> Articles { get; }

        public NewArticlesNotificationDto(IEnumerable<NewArticleDto> articles)
        {
            Articles = articles;
        }
    }
}
