using Espresso.Application.CQRS.Configuration.Queries.GetConfiguration;
using GraphQL.Types;

namespace Espresso.WebApi.GraphQl.ApplicationTypes.ConfigurationTypes
{
    /// <summary>
    /// 
    /// </summary>
    public class GetConfigurationQueryResponseType :
        ObjectGraphType<GetConfigurationQueryResponse>
    {
        /// <summary>
        /// 
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
