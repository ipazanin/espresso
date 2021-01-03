using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Domain.Enums.CategoryEnums;
using Espresso.Persistence.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Espresso.WebApi.Application.NewsPortals.Queries.GetNewsPortals
{
    public class GetNewsPortalsQueryHandler : IRequestHandler<GetNewsPortalsQuery, GetNewsPortalsQueryResponse>
    {
        #region Fields
        private readonly IApplicationDatabaseContext _context;
        #endregion

        #region Constructors
        public GetNewsPortalsQueryHandler(
            IApplicationDatabaseContext context
        )
        {
            _context = context;
        }
        #endregion

        #region Methods
        public async Task<GetNewsPortalsQueryResponse> Handle(GetNewsPortalsQuery request, CancellationToken cancellationToken)
        {
            var query = GetNewsPortalsNewsPortal.Include(_context.NewsPortals);
            var newsPortalDtos = await query
                .OrderBy(newsPortal => newsPortal.Name)
                .Where(newsPortal => !newsPortal.CategoryId.Equals((int)CategoryId.Local))
                .Select(GetNewsPortalsNewsPortal.GetProjection())
                .ToListAsync(cancellationToken);

            var response = new GetNewsPortalsQueryResponse(newsPortalDtos);

            return response;
        }
        #endregion
    }
}
