using System;
using Espresso.WebApi.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using MediatR;

namespace Espresso.WebApi.Application.CQRS.Articles.Commands.HideArticle
{
    public class HideArticleCommand : Request<Unit>
    {
        public Guid ArticleId { get; }

        public HideArticleCommand(
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
            Event.HideArticle
        )
        {
            ArticleId = articleId;
        }
    }
}
