// HideArticleCommand.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.Infrastructure.MediatorInfrastructure;
using MediatR;

namespace Espresso.WebApi.Application.Articles.Commands.HideArticle;

public record HideArticleCommand : Request<Unit>
{
    public Guid ArticleId { get; init; }

    public bool IsHidden { get; init; }
}
