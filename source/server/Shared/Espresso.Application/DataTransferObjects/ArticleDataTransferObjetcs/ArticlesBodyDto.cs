// ArticlesBodyDto.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Text.Json.Serialization;

namespace Espresso.Application.DataTransferObjects.ArticleDataTransferObjects;

public record ArticlesBodyDto
{
    /// <summary>
    /// Gets created articles.
    /// </summary>
    public IEnumerable<Guid> CreatedArticleIds { get; private set; }

    /// <summary>
    /// Gets updated articles.
    /// </summary>
    public IEnumerable<Guid> UpdatedArticleIds { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ArticlesBodyDto"/> class.
    /// </summary>
    /// <param name="createdArticleIds">Created articles.</param>
    /// <param name="updatedArticleIds">Updated articles.</param>
    [JsonConstructor]
    public ArticlesBodyDto(
        IEnumerable<Guid> createdArticleIds,
        IEnumerable<Guid> updatedArticleIds)
    {
        CreatedArticleIds = createdArticleIds;
        UpdatedArticleIds = updatedArticleIds;
    }
}
