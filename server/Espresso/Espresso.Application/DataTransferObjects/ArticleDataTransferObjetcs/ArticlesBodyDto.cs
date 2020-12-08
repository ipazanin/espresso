using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Espresso.Application.DataTransferObjects.ArticleDataTransferObjects
{
    public record ArticlesBodyDto
    {
        #region Properties
        public IEnumerable<ArticleDto> CreatedArticles { get; private set; } = new List<ArticleDto>();

        public IEnumerable<ArticleDto> UpdatedArticles { get; private set; } = new List<ArticleDto>();
        #endregion

        #region Constructors
        [JsonConstructor]
        public ArticlesBodyDto(
            IEnumerable<ArticleDto> createdArticles,
            IEnumerable<ArticleDto> updatedArticles
        )
        {
            CreatedArticles = createdArticles;
            UpdatedArticles = updatedArticles;
        }
        #endregion
    }
}