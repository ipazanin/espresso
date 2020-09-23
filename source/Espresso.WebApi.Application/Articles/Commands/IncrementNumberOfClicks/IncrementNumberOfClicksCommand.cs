using System;
using Espresso.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using MediatR;

namespace Espresso.WebApi.Application.Articles.Commands.IncrementTrendingArticleScore
{
    public class IncrementNumberOfClicksCommand : Request<Unit>
    {
        public Guid Id { get; }

        public IncrementNumberOfClicksCommand(
            Guid id,
            string currentEspressoWebApiVersion,
            string targetedEspressoWebApiVersion,
            string consumerVersion,
            DeviceType deviceType,
            AppEnvironment appEnvironment
        ) : base(
            currentEspressoWebApiVersion: currentEspressoWebApiVersion,
            targetedEspressoWebApiVersion: targetedEspressoWebApiVersion,
            consumerVersion: consumerVersion,
            deviceType: deviceType,
            appEnvironment: appEnvironment,
            Event.IncrementNumberOfClicksCommand
        )
        {
            Id = id;
        }

        public override string ToString()
        {
            return $"{nameof(Id)}:{Id}";
        }
    }
}
