using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;
using MediatR;

namespace Espresso.Dashboard.Application.NewsPortals.GetNewsPortals
{
    public class GetNewsPortalsQuery : IRequest<GetNewsPortalsQueryResponse>
    {
        #region Properties
        public PagingParameters PagingParameters { get; }
        #endregion Properties

        #region Constructors
        public GetNewsPortalsQuery(PagingParameters pagingParameters)
        {
            PagingParameters = pagingParameters;
        }
        #endregion Constructors
    }
}
