using System;
using Espresso.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using MediatR;

namespace Espresso.WebApi.Application.Articles.Commands.IncrementTrendingArticleScore
{
    public record IncrementNumberOfClicksCommand : Request<Unit>
    {
        public Guid Id { get; init; }
    }
}
