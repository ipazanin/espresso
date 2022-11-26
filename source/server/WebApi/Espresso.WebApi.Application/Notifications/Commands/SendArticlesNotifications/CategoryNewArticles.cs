// CategoryNewArticles.cs
//
// © 2022 Espresso News. All rights reserved.

namespace Espresso.WebApi.Application.Notifications.Commands.SendArticlesNotifications;

public class CategoryNewArticles
{
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

    public int CategoryId { get; }

    public int NumberOfNewArticles { get; }
}
