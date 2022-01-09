// NewArticleDto.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.WebApi.Application.Notifications.Commands.SendArticlesNotifications;

public class NewArticleDto
{
    private NewArticleDto()
    {
    }

    public static Expression<Func<Article, NewArticleDto>> Projection
    {
        get
        {
            return article => new NewArticleDto
            {
                NewsPortal = article.NewsPortalId,
                CategoryIds = article
                    .ArticleCategories
                    .Select(articleCategory => articleCategory.CategoryId)
                    .ToArray(),
            };
        }
    }

    public int NewsPortal { get; private set; }

    public IEnumerable<int> CategoryIds { get; private set; } = Enumerable.Empty<int>();
}
