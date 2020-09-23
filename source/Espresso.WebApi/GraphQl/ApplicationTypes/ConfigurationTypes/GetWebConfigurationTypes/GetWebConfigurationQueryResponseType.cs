using Espresso.WebApi.Application.Configuration.Queries.GetWebConfiguration;
using GraphQL.Types;

namespace Espresso.WebApi.GraphQl.ApplicationTypes.ConfigurationTypes.GetWebCategoryArticlesTypes
{
    /// <summary>
    /// 
    /// </summary>
    public class GetWebConfigurationQueryResponseType :
        ObjectGraphType<GetWebConfigurationQueryResponse>
    {
        /// <summary>
        /// 
        /// </summary>
        public GetWebConfigurationQueryResponseType()
        {
            Name = nameof(GetWebConfigurationQueryResponse);
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<GetWebConfigurationCategoryType>>>>(
                name: nameof(GetWebConfigurationQueryResponse.Categories)
            );
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<IntGraphType>>>>(
                name: nameof(GetWebConfigurationQueryResponse.NewsPortalIds)
            );
        }
    }
}
