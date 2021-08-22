﻿// LivenessHealthCheck.cs
//
// © 2021 Espresso News. All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.DataTransferObjects.SlackDataTransferObjects;
using Espresso.Application.Services.Contracts;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Espresso.Dashboard.Application.HealthChecks
{
    /// <summary>
    /// Health check to know when to restart a container.
    /// </summary>
    public class LivenessHealthCheck : IHealthCheck
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ISlackService _slackService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LivenessHealthCheck"/> class.
        /// </summary>
        /// <param name="memoryCache">Memory cache.</param>
        public LivenessHealthCheck(
            IMemoryCache memoryCache,
            ISlackService slackService)
        {
            _memoryCache = memoryCache;
            _slackService = slackService;
        }

        /// <inheritdoc/>
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var healthyResult = HealthCheckResult.Healthy("Application is live");
            var unhealthyResult = HealthCheckResult.Unhealthy("Application is unhealthy");

            var hourInDay = DateTime.UtcNow.Hour;

            if (hourInDay is (>= 21 and <= 24) or (>= 0 and <= 5))
            {
                return healthyResult;
            }

            var articles = _memoryCache.Get<IDictionary<Guid, Article>?>(MemoryCacheConstants.ArticleKey);

            if (articles is null)
            {
                return unhealthyResult;
            }

            var greatesCreatedArticleDateTime = articles.Values.Max(article => article.CreateDateTime);

            var timespan = DateTime.UtcNow - greatesCreatedArticleDateTime;
            if (timespan.TotalMinutes > 20)
            {
                var deadLockMessage = _memoryCache.Get<string>(MemoryCacheConstants.DeadLockLogKey);
                await _slackService.SendToSlack(
                    data: new SlackWebHookRequestBodyDto(
                        userName: "Liveness health check failed",
                        iconEmoji: ":warning:",
                        text: $"Liveness health check failed with message: {deadLockMessage}.\nCheck if new articles are being parsed!",
                        channel: "#general",
                        Enumerable.Empty<object>()),
                    cancellationToken: cancellationToken);

                return unhealthyResult;
            }

            return healthyResult;
        }
    }
}