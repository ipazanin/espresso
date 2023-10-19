// PagedList.cs
//
// © 2022 Espresso News. All rights reserved.

namespace Espresso.Application.DataTransferObjects.PagingDataTransferObjects;

/// <summary>
/// /// PagedList Model.
/// </summary>
/// <typeparam name="T">Paged type.</typeparam>
public class PagedList<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PagedList{T}"/> class.
    /// </summary>
    /// <param name="items">Paging items.</param>
    /// <param name="pagingMetadata">Paging metadata.</param>
    public PagedList(
        IReadOnlyList<T> items,
        PagingMetadata pagingMetadata)
    {
        Items = items;
        PagingMetadata = pagingMetadata;
    }

    /// <summary>
    /// Gets paging items.
    /// </summary>
    public IReadOnlyList<T> Items { get; }

    /// <summary>
    /// Gets paging metadata.
    /// </summary>
    public PagingMetadata PagingMetadata { get; }
}
