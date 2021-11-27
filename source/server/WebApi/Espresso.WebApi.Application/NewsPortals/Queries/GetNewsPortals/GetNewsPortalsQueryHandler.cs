// GetNewsPortalsQueryHandler.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Enums.CategoryEnums;
using Espresso.Persistence.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Espresso.WebApi.Application.NewsPortals.Queries.GetNewsPortals;

public class GetNewsPortalsQueryHandler : IRequestHandler<GetNewsPortalsQuery, GetNewsPortalsQueryResponse>
{
    private readonly IEspressoDatabaseContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetNewsPortalsQueryHandler"/> class.
    /// </summary>
    /// <param name="context"></param>
    public GetNewsPortalsQueryHandler(
        IEspressoDatabaseContext context)
    {
        _context = context;
    }

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
}
