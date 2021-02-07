using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Espresso.Application.DataTransferObjects.PagingDataTransferObjects
{
    /// <summary>
    /// /// PagedList Model
    /// </summary>
    public class PagedList<T>
    {
        #region Properties
        public IEnumerable<T> Items { get; init; }
        public PagingMetadata PagingMetadata { get; init; }
        #endregion Properties

        #region Constructors
        [JsonConstructor]
        public PagedList(
            IEnumerable<T> items,
            PagingMetadata pagingMetadata
        )
        {
            Items = items;
            PagingMetadata = pagingMetadata;
        }
        #endregion Constructors
    }
}
