// GetNewsPortalImageQueryHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects;
using Espresso.Persistence.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Dashboard.Application.NewsPortalImage.Queries.GetNewsPortalImage;

public class GetNewsPortalImageQueryHandler : IRequestHandler<GetNewsPortalImageQuery, GetNewsPortalImageQueryResponse>
{
    private readonly IEspressoDatabaseContext _espressoDatabaseContext;

    public GetNewsPortalImageQueryHandler(IEspressoDatabaseContext espressoDatabaseContext)
    {
        _espressoDatabaseContext = espressoDatabaseContext;
    }

    public async Task<GetNewsPortalImageQueryResponse> Handle(GetNewsPortalImageQuery request, CancellationToken cancellationToken)
    {
        var newsPortalImage = await _espressoDatabaseContext
            .NewsPortalImages
            .AsNoTracking()
            .AsSplitQuery()
            .Select(NewsPortalImageDto.GetProjection())
            .FirstOrDefaultAsync(newsPortalImage => newsPortalImage.NewsPortalId == request.NewsPortalId, cancellationToken);

        return new GetNewsPortalImageQueryResponse(newsPortalImage);
    }
}
