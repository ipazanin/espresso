// CategoryNewArticles.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.WebApi.Application.Notifications.Commands.SendArticlesNotifications;

public class CategoryNewArticles
{
    public int CategoryId { get; }

    public int NumberOfNewArticles { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryNewArticles"/> class.
    /// </summary>
    /// <param name="categoryId"></param>
    /// <param name="numberOfNewArticles"></param>
#pragma warning disable SA1201 // Elements should appear in the correct order
    public CategoryNewArticles(int categoryId, int numberOfNewArticles)
#pragma warning restore SA1201 // Elements should appear in the correct order
    {
        CategoryId = categoryId;
        NumberOfNewArticles = numberOfNewArticles;
    }
}
