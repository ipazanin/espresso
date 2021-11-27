// IncrementNumberOfClicksCommand.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Application.Infrastructure.MediatorInfrastructure;
using MediatR;

namespace Espresso.WebApi.Application.Articles.Commands.IncrementTrendingArticleScore;

public record IncrementNumberOfClicksCommand : Request<Unit>
{
    public Guid Id { get; init; }
}
