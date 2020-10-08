using System;
using Espresso.Application.Infrastructure.MediatorInfrastructure;

namespace Espresso.ParserDeleter.Application.DeleteOldArticles
{
    public record DeleteOldArticlesCommand : Request<DeleteOldArticlesCommandResponse>
    {
        public TimeSpan MaxAgeOfOldArticles { get; init; }
    }
}
