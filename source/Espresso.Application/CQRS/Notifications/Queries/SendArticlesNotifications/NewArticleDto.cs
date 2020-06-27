using System.Collections.Generic;

namespace Espresso.Application.CQRS.Notifications.Queries.SendArticlesNotifications
{
    public class NewArticleDto
    {
        public int NewsPortal { get; }

        public IEnumerable<int> CategoryIds { get; }

        public NewArticleDto(int newsPortal, IEnumerable<int> categoryIds)
        {
            NewsPortal = newsPortal;
            CategoryIds = categoryIds;
        }
    }
}
