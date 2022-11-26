// SetFeaturedArticleCommand.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.Infrastructure.MediatorInfrastructure;
using MediatR;

namespace Espresso.WebApi.Application.Articles.Commands.SetFeaturedArticle;

public record SetFeaturedArticleCommand : Request<Unit>
{
    public IEnumerable<(Guid articleId, bool? isFeatured, int? featuredPosition)> FeaturedArticleConfigurations { get; init; } = new List<(Guid articleId, bool? isFeatured, int? featuredPosition)>();
}
