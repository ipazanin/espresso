using System;
using Espresso.Application.Infrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using MediatR;

namespace Espresso.Application.CQRS.Articles.Commands.HideArticle
{
    public class HideArticleCommand : Request<Unit>
    {
        public Guid ArticleId { get; }

        public HideArticleCommand(
            Guid articleId,
            string currentEspressoWebApiVersion,
            string espressoWebApiVersion,
            string version,
            DeviceType deviceType
        ) : base(
            currentEspressoWebApiVersion,
            espressoWebApiVersion,
            version,
            deviceType,
            Event.HideArticle
        )
        {
            ArticleId = articleId;
        }
    }
}
