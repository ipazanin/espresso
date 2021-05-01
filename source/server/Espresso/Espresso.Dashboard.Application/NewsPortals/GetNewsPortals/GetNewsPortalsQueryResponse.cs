using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;

namespace Espresso.Dashboard.Application.NewsPortals.GetNewsPortals
{
    public class GetNewsPortalsQueryResponse
    {

        #region Properties
        public PagedList<GetNewsPortalsNewsPortal> NewsPortals { get; }
        #endregion Properties

        #region Constructors
        public GetNewsPortalsQueryResponse(PagedList<GetNewsPortalsNewsPortal> newsPortals)
        {
            NewsPortals = newsPortals;
        }
        #endregion Constructors
    }
}
