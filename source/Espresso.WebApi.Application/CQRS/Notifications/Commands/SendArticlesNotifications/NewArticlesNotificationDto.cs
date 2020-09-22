using System.Collections.Generic;

namespace Espresso.WebApi.Application.CQRS.Notifications.Commands.SendArticlesNotifications
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
