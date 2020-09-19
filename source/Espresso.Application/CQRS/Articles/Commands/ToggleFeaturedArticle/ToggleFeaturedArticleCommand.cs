using System;
using Espresso.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using MediatR;

namespace Espresso.Application.CQRS.Articles.Commands.ToggleFeaturedArticle
{
    public class ToggleFeaturedArticleCommand : Request<Unit>
    {
        public Guid ArticleId { get; }

        public ToggleFeaturedArticleCommand(
            Guid articleId,
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
            eventIdEnum: Event.ToggleFeaturedArticle
        )
        {
            ArticleId = articleId;
        }
    }
}
