// CategoryNewArticles.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.WebApi.Application.Notifications.Commands.SendArticlesNotifications
{
    public class CategoryNewArticles
    {
        public int CategoryId { get; }

        public int NumberOfNewArticles { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryNewArticles"/> class.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="numberOfNewArticles"></param>
        public CategoryNewArticles(int categoryId, int numberOfNewArticles)
        {
            CategoryId = categoryId;
            NumberOfNewArticles = numberOfNewArticles;
        }
    }
}
