using Espresso.Application.ViewModels.NewsPortalViewModels;
using GraphQL.Types;

namespace Espresso.Application.GraphQl.ApplicationTypes.ArticleTypes
{
    public class NewsPortalViewModelType : ObjectGraphType<NewsPortalViewModel>
    {
        public NewsPortalViewModelType()
        {
            Name = nameof(NewsPortalViewModelType);
            Field<NonNullGraphType<IdGraphType>>(
                name: nameof(NewsPortalViewModel.Id)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(NewsPortalViewModel.Name)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(NewsPortalViewModel.IconUrl)
            );
        }
    }
}
