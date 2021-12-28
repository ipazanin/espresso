// ArticleCategoryDto.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Linq.Expressions;
using System.Text.Json.Serialization;
using Espresso.Domain.Entities;

namespace Espresso.Application.DataTransferObjects.ArticleDataTransferObjects;

public record ArticleCategoryDto
{
    /// <summary>
    /// Gets id.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets category id.
    /// </summary>
    public int CategoryId { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ArticleCategoryDto"/> class.
    /// </summary>
    /// <param name="id">Article id.</param>
    /// <param name="categoryId">Category id.</param>
    [JsonConstructor]
    public ArticleCategoryDto(
        Guid id,
        int categoryId)
    {
        Id = id;
        CategoryId = categoryId;
    }

    private ArticleCategoryDto()
    {
    }

    /// <summary>
    /// Creates <see cref="ArticleCategory"/> to <see cref="ArticleCategoryDto"/> projection.
    /// </summary>
    /// <returns><see cref="ArticleCategory"/> to <see cref="ArticleCategoryDto"/> projection.</returns>
    public static Expression<Func<ArticleCategory, ArticleCategoryDto>> GetProjection()
    {
        return articleCategory => new ArticleCategoryDto
        {
            Id = articleCategory.Id,
            CategoryId = articleCategory.CategoryId,
        };
    }
}
