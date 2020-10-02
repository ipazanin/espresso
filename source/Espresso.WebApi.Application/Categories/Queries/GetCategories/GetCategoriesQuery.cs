using Espresso.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.WebApi.Application.Categories.Queries.GetCategories
{
    public record GetCategoriesQuery : Request<GetCategoriesQueryResponse>
    {
    }
}
