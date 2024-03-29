﻿// PagingMetadata.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Text.Json.Serialization;

namespace Espresso.Application.DataTransferObjects.PagingDataTransferObjects;

/// <summary>
/// PagingMetadata Model.
/// </summary>
public class PagingMetadata
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PagingMetadata"/> class.
    /// </summary>
    /// <param name="currentPage">Current page.</param>
    /// <param name="pageSize">Page size.</param>
    /// <param name="totalCount">Total items count.</param>
    [JsonConstructor]
    public PagingMetadata(
        int currentPage,
        int pageSize,
        int totalCount)
    {
        CurrentPage = currentPage;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    /// <summary>
    /// Gets current page.
    /// </summary>
    public int CurrentPage { get; }

    /// <summary>
    /// Gets current page size.
    /// </summary>
    public int PageSize { get; }

    /// <summary>
    /// Gets total items count.
    /// </summary>
    public int TotalCount { get; }

    /// <summary>
    /// Returns value indicating wheather previous page exists.
    /// </summary>
    /// <returns>Value indicating wheather previous page exists.</returns>
    public bool HasPrevious() => CurrentPage > 1;

    /// <summary>
    /// Returns value indicating wheather next page exists.
    /// </summary>
    /// <returns>Value indicating wheather next page exists.</returns>
    public bool HasNext() => CurrentPage < TotalPages();

    /// <summary>
    /// Returns number of total pages.
    /// </summary>
    /// <returns>Number of total pages.</returns>
    public int TotalPages() => (int)Math.Ceiling(TotalCount / (double)PageSize);
}
