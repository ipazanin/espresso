// NewArticleDto.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.WebApi.Application.Notifications.Commands.SendArticlesNotifications;

public class NewArticleDto
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NewArticleDto"/> class.
    /// </summary>
    /// <param name="newsPortal"></param>
    /// <param name="categoryIds"></param>
    public NewArticleDto(int newsPortal, IEnumerable<int> categoryIds)
    {
        NewsPortal = newsPortal;
        CategoryIds = categoryIds;
    }

    public int NewsPortal { get; }

    public IEnumerable<int> CategoryIds { get; }
}
