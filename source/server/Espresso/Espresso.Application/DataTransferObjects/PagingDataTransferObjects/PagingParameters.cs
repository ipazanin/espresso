namespace Espresso.Application.DataTransferObjects.PagingDataTransferObjects
{
    /// <summary>
    /// Paginationparameters Model
    /// </summary>
    public record PagingParameters
    {
        #region Properties
        public int CurrentPage { get; init; }

        public int PageSize { get; init; }
        #endregion

        #region Methods
        public int Take() => PageSize;
        public int Skip() => (CurrentPage - 1) * PageSize;
        #endregion
    }
}
