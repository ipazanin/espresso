using System.Collections.Generic;
using System.Linq;
using Espresso.Application.DataTransferObjects;
using Espresso.Application.Infrastructure;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.Common.Enums;

namespace Espresso.Application.CQRS.Articles.Commands.UpdateInMemoryArticles
{
    public class UpdateInMemoryArticlesCommand : Request<UpdateInMemoryArticlesCommandResponse>
    {
        public IEnumerable<ArticleDto> CreatedArticles { get; }
        public IEnumerable<ArticleDto> UpdatedArticles { get; }

        public UpdateInMemoryArticlesCommand(
            IEnumerable<ArticleDto> createdArticles,
            IEnumerable<ArticleDto> updatedArticles,
            string currentEspressoWebApiVersion,
            string espressoWebApiVersion,
            string version,
            DeviceType deviceType
        ) : base(
            currentEspressoWebApiVersion,
            espressoWebApiVersion,
            version,
            deviceType,
            Event.UpdateInMemoryArticlesCommand
        )
        {
            CreatedArticles = createdArticles;
            UpdatedArticles = updatedArticles;
        }

        public override string ToString()
        {
            return $"{nameof(CreatedArticles)}:{CreatedArticles.Count()}, {nameof(UpdatedArticles)}:{UpdatedArticles.Count()}";
        }
    }
}
