// ArticlesBodyDto.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Text.Json.Serialization;

namespace Espresso.Application.DataTransferObjects.ArticleDataTransferObjects
{
    public record ArticlesBodyDto
    {
        /// <summary>
        /// Gets created articles.
        /// </summary>
        public IEnumerable<ArticleDto> CreatedArticles { get; private set; } = new List<ArticleDto>();

        /// <summary>
        /// Gets updated articles.
        /// </summary>
        public IEnumerable<ArticleDto> UpdatedArticles { get; private set; } = new List<ArticleDto>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticlesBodyDto"/> class.
        /// </summary>
        /// <param name="createdArticles">Created articles.</param>
        /// <param name="updatedArticles">Updated articles.</param>
        [JsonConstructor]
        public ArticlesBodyDto(
            IEnumerable<ArticleDto> createdArticles,
            IEnumerable<ArticleDto> updatedArticles)
        {
            CreatedArticles = createdArticles;
            UpdatedArticles = updatedArticles;
        }
    }
}
