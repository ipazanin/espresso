﻿using Espresso.Application.Infrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.Application.CQRS.Categories.Queries.GetCategories
{
    public class GetCategoriesQuery : Request<GetCategoriesQueryResponse>
    {
        public GetCategoriesQuery(
            string currentEspressoWebApiVersion,
            string espressoWebApiVersion,
            string version,
            DeviceType deviceType
        ) : base(
            currentEspressoWebApiVersion,
            espressoWebApiVersion,
            version,
            deviceType,
            Event.GetCategoriesQuery
        )
        {
        }

        public override string ToString()
        {
            return "";
        }
    }
}
