// NewArticleDto.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.WebApi.Application.Notifications.Commands.SendArticlesNotifications
{
    public class NewArticleDto
    {
        public int NewsPortal { get; }

        public IEnumerable<int> CategoryIds { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewArticleDto"/> class.
        /// </summary>
        /// <param name="newsPortal"></param>
        /// <param name="categoryIds"></param>
#pragma warning disable SA1201 // Elements should appear in the correct order
        public NewArticleDto(int newsPortal, IEnumerable<int> categoryIds)
#pragma warning restore SA1201 // Elements should appear in the correct order
        {
            NewsPortal = newsPortal;
            CategoryIds = categoryIds;
        }
    }
}
