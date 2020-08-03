using Espresso.Application.CQRS.Articles.Queries.GetCategoryArticles;
using Espresso.Application.CQRS.Configuration.Queries.GetConfiguration;
using GraphQL.Types;

namespace Espresso.Application.GraphQl.ApplicationTypes.ConfigurationTypes
{
    public class GetConfigurationCategoryWithNewsPortalsType : ObjectGraphType<GetConfigurationCategoryWithNewsPortals>
    {
        public GetConfigurationCategoryWithNewsPortalsType()
        {
            Name = nameof(GetConfigurationCategoryWithNewsPortals);
            Field<NonNullGraphType<IdGraphType>>(
                name: nameof(GetConfigurationCategoryWithNewsPortals.Id)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetConfigurationCategoryWithNewsPortals.Name)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetConfigurationCategoryWithNewsPortals.Color)
            );
            Field<NonNullGraphType<IntGraphType>>(
                name: nameof(GetConfigurationCategoryWithNewsPortals.CategoryType)
            );
            Field<IntGraphType>(
                name: nameof(GetConfigurationCategoryWithNewsPortals.Position)
            );
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<GetConfigurationNewsPortalType>>>>(
                name: nameof(GetConfigurationCategoryWithNewsPortals.NewsPortals)
            );
        }
    }
}
