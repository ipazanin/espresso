using System;
using Espresso.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.ParserDeleter.Application.DeleteOldArticles
{
    public record DeleteOldArticlesCommand : Request<DeleteOldArticlesCommandResponse>
    {
        public TimeSpan MaxAgeOfOldArticles { get; init; }
    }
}
