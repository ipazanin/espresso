// NewArticlesNotificationDto.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Collections.Generic;

namespace Espresso.WebApi.Application.Notifications.Commands.SendArticlesNotifications
{
    public class NewArticlesNotificationDto
    {
        public IEnumerable<NewArticleDto> Articles { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewArticlesNotificationDto"/> class.
        /// </summary>
        /// <param name="articles"></param>
        public NewArticlesNotificationDto(IEnumerable<NewArticleDto> articles)
        {
            Articles = articles;
        }
    }
}
