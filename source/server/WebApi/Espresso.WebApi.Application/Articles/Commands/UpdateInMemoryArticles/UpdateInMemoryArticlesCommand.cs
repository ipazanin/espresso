// UpdateInMemoryArticlesCommand.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.ArticleDataTransferObjects;
using Espresso.Application.Infrastructure.MediatorInfrastructure;
using System;
using System.Collections.Generic;

namespace Espresso.WebApi.Application.Articles.Commands.UpdateInMemoryArticles
{
    public record UpdateInMemoryArticlesCommand : Request<UpdateInMemoryArticlesCommandResponse>
    {
        public IEnumerable<ArticleDto> CreatedArticles { get; init; } = new List<ArticleDto>();
        public IEnumerable<ArticleDto> UpdatedArticles { get; init; } = new List<ArticleDto>();
    }
}
