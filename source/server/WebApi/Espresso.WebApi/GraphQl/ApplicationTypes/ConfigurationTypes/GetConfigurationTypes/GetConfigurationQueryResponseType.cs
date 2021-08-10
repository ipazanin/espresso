// GetConfigurationQueryResponseType.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.WebApi.Application.Configuration.Queries.GetConfiguration;
using GraphQL.Types;

namespace Espresso.WebApi.GraphQl.ApplicationTypes.ConfigurationTypes.GetConfigurationTypes
{
    /// <summary>
    /// 
    /// </summary>
    public class GetConfigurationQueryResponseType :
        ObjectGraphType<GetConfigurationQueryResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetConfigurationQueryResponseType"/> class.
        /// </summary>
        public GetConfigurationQueryResponseType()
        {
            Name = nameof(GetConfigurationQueryResponse);
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<GetConfigurationCategoryType>>>>(
                name: nameof(GetConfigurationQueryResponse.Categories)
            );
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<GetConfigurationCategoryWithNewsPortalsType>>>>(
                name: nameof(GetConfigurationQueryResponse.CategoriesWithNewsPortals)
            );
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<GetConfigurationRegionType>>>>(
                name: nameof(GetConfigurationQueryResponse.Regions)
            );
        }
    }
}
