using System;
using Espresso.Application.Infrastructure;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using MediatR;
using Espresso.Common.Enums;

namespace Espresso.Application.CQRS.Articles.Commands.IncrementTrendingArticleScore
{
    public class IncrementNumberOfClicksCommand : Request<Unit>
    {
        public Guid Id { get; }

        public IncrementNumberOfClicksCommand(
            Guid id,
            string currentEspressoWebApiVersion,
            string espressoWebApiVersion,
            string version,
            DeviceType deviceType
        ) : base(
            currentEspressoWebApiVersion,
            espressoWebApiVersion,
            version,
            deviceType,
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
