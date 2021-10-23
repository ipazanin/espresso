// GetConfigurationRegionType.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.WebApi.Application.Configuration.Queries.GetConfiguration;
using GraphQL.Types;

namespace Espresso.WebApi.GraphQl.ApplicationTypes.ConfigurationTypes.GetConfigurationTypes
{
    /// <summary>
    /// 
    /// </summary>
    public class GetConfigurationRegionType : ObjectGraphType<GetConfigurationRegion>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetConfigurationRegionType"/> class.
        /// </summary>
        public GetConfigurationRegionType()
        {
            Name = nameof(GetConfigurationRegion);
            Field<NonNullGraphType<IdGraphType>>(
                name: nameof(GetConfigurationRegion.Id));
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetConfigurationRegion.Name));
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<GetConfigurationNewsPortalType>>>>(
                name: nameof(GetConfigurationRegion.NewsPortals));
        }
    }
}
