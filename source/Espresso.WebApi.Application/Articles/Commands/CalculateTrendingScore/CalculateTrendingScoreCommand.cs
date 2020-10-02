using Espresso.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using MediatR;

namespace Espresso.WebApi.Application.Articles.Commands.CalculateTrendingScore
{
    public record CalculateTrendingScoreCommand : Request<Unit>
    {
    }
}
