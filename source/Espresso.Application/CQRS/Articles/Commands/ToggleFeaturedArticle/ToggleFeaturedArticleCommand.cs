using System;
using Espresso.Application.Infrastructure;
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
            DeviceType deviceType
        ) : base(
            currentEspressoWebApiVersion,
            targetedEspressoWebApiVersion,
            consumerVersion,
            deviceType,
            Event.ToggleFeaturedArticle
        )
        {
            ArticleId = articleId;
        }
    }
}
