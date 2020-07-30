using Espresso.Application.GraphQl.ApplicationTypes.ArticleTypes;
using Espresso.Application.ViewModels.NewsPortalViewModels;
using GraphQL.Types;

namespace Espresso.Application.GraphQl.ApplicationTypes.ConfigurationTypes
{
    public class CategoryViewModelWithNewsPortalsType :
        ObjectGraphType<CategoryViewModelWithNewsPortals>
    {
        public CategoryViewModelWithNewsPortalsType()
        {
            Name = nameof(CategoryViewModelWithNewsPortals);
            Field<NonNullGraphType<IdGraphType>>(
                name: nameof(CategoryViewModelWithNewsPortals.Id)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(CategoryViewModelWithNewsPortals.Name)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(CategoryViewModelWithNewsPortals.Color)
            );
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<NewsPortalViewModelType>>>>(
                name: nameof(CategoryViewModelWithNewsPortals.NewsPortals)
            );
        }
    }
}
