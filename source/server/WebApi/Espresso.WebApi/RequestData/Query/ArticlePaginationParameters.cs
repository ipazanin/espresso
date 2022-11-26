// ArticlePaginationParameters.cs
//
// © 2022 Espresso News. All rights reserved.

using Microsoft.AspNetCore.Mvc;

namespace Espresso.WebApi.RequestData.Query;

/// <summary>
///
/// </summary>
public class ArticlePaginationParameters : PaginationParameters
{
    /// <summary>
    ///
    /// </summary>
    [FromQuery(Name = "firstArticleId")]
    public Guid? FirstArticleId { get; set; } = null;
}
