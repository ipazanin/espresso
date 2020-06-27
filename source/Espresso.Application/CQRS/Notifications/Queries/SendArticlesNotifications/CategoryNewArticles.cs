namespace Espresso.Application.CQRS.Notifications.Queries.SendArticlesNotifications
{
    public class CategoryNewArticles
    {

        public int CategoryId { get; }

        public int NumberOfNewArticles { get; }

        public CategoryNewArticles(int categoryId, int numberOfNewArticles)
        {
            CategoryId = categoryId;
            NumberOfNewArticles = numberOfNewArticles;
        }
    }
}
