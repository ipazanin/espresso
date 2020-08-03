using Espresso.Application.CQRS.Configuration.Queries.GetConfiguration;
using GraphQL.Types;

namespace Espresso.Application.GraphQl.ApplicationTypes.ConfigurationTypes
{
    public class GetConfigurationQueryResponseType :
        ObjectGraphType<GetConfigurationQueryResponse>
    {
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
