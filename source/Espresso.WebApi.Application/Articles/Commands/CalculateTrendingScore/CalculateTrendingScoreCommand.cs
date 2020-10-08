using Espresso.Application.Infrastructure.MediatorInfrastructure;
using MediatR;

namespace Espresso.WebApi.Application.Articles.Commands.CalculateTrendingScore
{
    public record CalculateTrendingScoreCommand : Request<Unit>
    {
    }
}
