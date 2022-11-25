// DeleteOldArticlesCommand.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Domain.Entities;

namespace Espresso.Dashboard.Application.Articles.Commands.DeleteOldArticles;

public record DeleteOldArticlesCommand : Request<DeleteOldArticlesCommandResponse>
{
    public IDictionary<Guid, Article> Articles { get; init; } = new Dictionary<Guid, Article>();
}
