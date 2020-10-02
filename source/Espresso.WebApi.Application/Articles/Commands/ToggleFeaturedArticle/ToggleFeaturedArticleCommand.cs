using System;
using Espresso.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using MediatR;

namespace Espresso.WebApi.Application.Articles.Commands.ToggleFeaturedArticle
{
    public record ToggleFeaturedArticleCommand : Request<Unit>
    {
        public Guid ArticleId { get; init; }
    }
}
