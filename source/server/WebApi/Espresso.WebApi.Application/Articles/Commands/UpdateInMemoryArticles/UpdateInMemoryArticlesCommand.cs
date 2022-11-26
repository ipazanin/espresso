// UpdateInMemoryArticlesCommand.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.Infrastructure.MediatorInfrastructure;

namespace Espresso.WebApi.Application.Articles.Commands.UpdateInMemoryArticles;

public record UpdateInMemoryArticlesCommand : Request<UpdateInMemoryArticlesCommandResponse>
{
    public IEnumerable<Guid> CreatedArticleIds { get; init; } = new List<Guid>();

    public IEnumerable<Guid> UpdatedArticleIds { get; init; } = new List<Guid>();
}
