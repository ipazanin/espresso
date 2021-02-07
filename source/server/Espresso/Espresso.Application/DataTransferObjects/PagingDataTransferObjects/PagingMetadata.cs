using System;
using System.Text.Json.Serialization;

namespace Espresso.Application.DataTransferObjects.PagingDataTransferObjects
{
    /// <summary>
    /// PagingMetadata Model
    /// </summary>
    public class PagingMetadata
    {

        #region Properties
        public int CurrentPage { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        #endregion Properties

        #region Constructors
        [JsonConstructor]
        public PagingMetadata(
            int currentPage,
            int pageSize,
            int totalCount
        )
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
        #endregion Constructors

        #region Methods
        public bool HasPrevious() => CurrentPage > 1;
        public bool HasNext() => CurrentPage < TotalPages();
        public int TotalPages() => (int)Math.Ceiling(TotalCount / (double)PageSize);
        #endregion Methods     
    }
}
