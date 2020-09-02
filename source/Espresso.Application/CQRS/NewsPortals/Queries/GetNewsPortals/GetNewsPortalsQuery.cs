﻿using Espresso.Application.Infrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.Application.CQRS.NewsPortals.Queries.GetNewsPortals
{
    public class GetNewsPortalsQuery : Request<GetNewsPortalsQueryResponse>
    {
        public GetNewsPortalsQuery(
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
            Event.GetNewsPortalsQuery
        )
        {
        }

        public override string ToString()
        {
            return "";
        }
    }
}
