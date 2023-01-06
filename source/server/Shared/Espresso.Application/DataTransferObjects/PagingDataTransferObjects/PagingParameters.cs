// PagingParameters.cs
//
// © 2022 Espresso News. All rights reserved.

namespace Espresso.Application.DataTransferObjects.PagingDataTransferObjects;

/// <summary>
/// Pagination Parameters Model.
/// </summary>
public class PagingParameters
{
    /// <summary>
    /// Gets Current page.
    /// </summary>
    public int CurrentPage { get; init; }

    /// <summary>
    /// Gets page size.
    /// </summary>
    public int PageSize { get; init; }

    public string SearchString { get; init; } = string.Empty;

    public string SortColumn { get; init; } = string.Empty;

    public OrderType OrderType { get; init; }

    /// <summary>
    /// Returns number of items to take.
    /// </summary>
    /// <returns>Number of items to take.</returns>
    public int GetTake() => PageSize;

    /// <summary>
    /// Returns number of items to skip.
    /// </summary>
    /// <returns>Number of items to skip.</returns>
    public int GetSkip() => (CurrentPage - 1) * PageSize;
}
