using Espresso.Application.Infrastructure;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using MediatR;
using Espresso.Common.Enums;

namespace Espresso.Application.CQRS.Articles.Commands.CalculateTrendingScore
{
    public class CalculateTrendingScoreCommand : Request<Unit>
    {
        public CalculateTrendingScoreCommand(
            string currentEspressoWebApiVersion,
            string espressoWebApiVersion,
            string version,
            DeviceType deviceType
        ) : base(
            currentEspressoWebApiVersion,
            espressoWebApiVersion,
            version,
            deviceType,
            Event.CalculateTrendingScoreCommand
        )
        {
        }

        public override string ToString()
        {
            return "";
        }
    }
}
