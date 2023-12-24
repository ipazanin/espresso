// HideArticleCommand.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Common.Enums;
using MediatR;

namespace Espresso.WebApi.Application.Articles.Commands.HideArticle;

public record HideArticleCommand : IRequest
{
    /// <summary>
    /// Gets targeted api version.
    /// </summary>
    public string TargetedApiVersion { get; init; } = string.Empty;

    /// <summary>
    /// Gets consumer version.
    /// </summary>
    public string ConsumerVersion { get; init; } = string.Empty;

    /// <summary>
    /// Gets device type.
    /// </summary>
    public DeviceType DeviceType { get; init; }

    public Guid ArticleId { get; init; }

    public bool IsHidden { get; init; }
}
