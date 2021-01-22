using System;
using Espresso.Application.Infrastructure.MediatorInfrastructure;

namespace Espresso.Dashboard.Application.DeleteOldArticles
{
    public record DeleteOldArticlesCommand : Request<DeleteOldArticlesCommandResponse>
    {
        public TimeSpan MaxAgeOfOldArticles { get; init; }
    }
}
