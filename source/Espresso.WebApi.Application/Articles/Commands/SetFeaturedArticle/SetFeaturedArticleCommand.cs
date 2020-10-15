using System;
using Espresso.Application.Infrastructure.MediatorInfrastructure;
using MediatR;

namespace Espresso.WebApi.Application.Articles.Commands.SetFeaturedArticle
{
    public record SetFeaturedArticleCommand : Request<Unit>
    {
        public Guid ArticleId { get; init; }

        public bool? IsFeatured { get; init; }

        public int? FeraturedPosition { get; init; }
    }
}
