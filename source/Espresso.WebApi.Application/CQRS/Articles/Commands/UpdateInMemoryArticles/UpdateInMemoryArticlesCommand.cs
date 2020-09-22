using System;
using System.Collections.Generic;
using System.Linq;
using Espresso.WebApi.Application.DataTransferObjects;
using Espresso.WebApi.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.WebApi.Application.CQRS.Articles.Commands.UpdateInMemoryArticles
{
    public class UpdateInMemoryArticlesCommand : Request<UpdateInMemoryArticlesCommandResponse>
    {
        public IEnumerable<ArticleDto> CreatedArticles { get; }
        public IEnumerable<ArticleDto> UpdatedArticles { get; }

        public TimeSpan MaxAgeOfArticle { get; }

        public UpdateInMemoryArticlesCommand(
            IEnumerable<ArticleDto> createdArticles,
            IEnumerable<ArticleDto> updatedArticles,
            TimeSpan maxAgeOfArticle,
            string currentEspressoWebApiVersion,
            string targetedEspressoWebApiVersion,
            string consumerVersion,
            DeviceType deviceType,
            AppEnvironment appEnvironment
        ) : base(
            currentEspressoWebApiVersion: currentEspressoWebApiVersion,
            targetedEspressoWebApiVersion: targetedEspressoWebApiVersion,
            consumerVersion: consumerVersion,
            deviceType: deviceType,
            appEnvironment: appEnvironment,
            Event.UpdateInMemoryArticlesCommand
        )
        {
            CreatedArticles = createdArticles;
            UpdatedArticles = updatedArticles;
            MaxAgeOfArticle = maxAgeOfArticle;
        }

        public override string ToString()
        {
            return $"{nameof(CreatedArticles)}:{CreatedArticles.Count()}, {nameof(UpdatedArticles)}:{UpdatedArticles.Count()}";
        }
    }
}
