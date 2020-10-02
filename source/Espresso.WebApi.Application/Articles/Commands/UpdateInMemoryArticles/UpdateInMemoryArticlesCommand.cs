using System;
using System.Collections.Generic;
using System.Linq;
using Espresso.Application.DataTransferObjects;
using Espresso.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.WebApi.Application.Articles.Commands.UpdateInMemoryArticles
{
    public record UpdateInMemoryArticlesCommand : Request<UpdateInMemoryArticlesCommandResponse>
    {
        public IEnumerable<ArticleDto> CreatedArticles { get; init; } = new List<ArticleDto>();
        public IEnumerable<ArticleDto> UpdatedArticles { get; init; } = new List<ArticleDto>();
        public TimeSpan MaxAgeOfArticle { get; init; }
    }
}
