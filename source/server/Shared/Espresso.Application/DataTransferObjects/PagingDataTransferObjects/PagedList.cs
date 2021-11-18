// PagedList.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Text.Json.Serialization;

namespace Espresso.Application.DataTransferObjects.PagingDataTransferObjects
{
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
        [JsonConstructor]
        public PagedList(
            IEnumerable<T> items,
            PagingMetadata pagingMetadata)
        {
            Items = items;
            PagingMetadata = pagingMetadata;
        }

        /// <summary>
        /// Gets paging items.
        /// </summary>
        public IEnumerable<T> Items { get; init; }

        /// <summary>
        /// Gets paging metadata.
        /// </summary>
        public PagingMetadata PagingMetadata { get; init; }
    }
}
