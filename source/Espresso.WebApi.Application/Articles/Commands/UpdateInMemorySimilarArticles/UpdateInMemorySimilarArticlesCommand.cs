using System.Collections.Generic;
using Espresso.Application.DataTransferObjects;
using Espresso.Application.Infrastructure.MediatorInfrastructure;

namespace Espresso.WebApi.Application
{
    public record UpdateInMemorySimilarArticlesCommand : Request<UpdateInMemorySimilarArticlesCommandResponse>
    {
        public IEnumerable<SimilarArticleDto> SimilarArticles { get; init; } = new List<SimilarArticleDto>();
    }
}