namespace Espresso.WebApi.Application.Notifications.Commands.SendArticlesNotifications
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
