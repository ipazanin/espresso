using Espresso.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.WebApi.Application.NewsPortals.Queries.GetNewsPortals
{
    public record GetNewsPortalsQuery : Request<GetNewsPortalsQueryResponse>
    {
    }
}
