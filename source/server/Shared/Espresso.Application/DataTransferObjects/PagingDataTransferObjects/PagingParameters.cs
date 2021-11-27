// PagingParameters.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.Application.DataTransferObjects.PagingDataTransferObjects;

/// <summary>
/// Paginationparameters Model
/// </summary>
public record PagingParameters
{
    /// <summary>
    /// Gets Current page.
    /// </summary>
    public int CurrentPage { get; init; }

    /// <summary>
    /// Gets page size.
    /// </summary>
    public int PageSize { get; init; }

    /// <summary>
    /// Returns number of items to take.
    /// </summary>
    /// <returns>Number of items to take.</returns>
    public int Take() => PageSize;

    /// <summary>
    /// Returns number of items to skip.
    /// </summary>
    /// <returns>Number of items to skip.</returns>
    public int Skip() => (CurrentPage - 1) * PageSize;
}
