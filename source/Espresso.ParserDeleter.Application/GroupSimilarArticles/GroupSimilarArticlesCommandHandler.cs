using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.DataTransferObjects;
using Espresso.Application.Extensions;
using Espresso.Application.IServices;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.Domain.IServices;
using Espresso.Persistence.IRepositories;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Espresso.ParserDeleter.Application.GroupSimilarArticles
{
    public class GroupSimilarArticlesCommandHandler : IRequestHandler<GroupSimilarArticlesCommand, GroupSimilarArticlesCommandResponse>
    {
        #region Fields
        private readonly IMemoryCache _memoryCache;
        private readonly ISimilarArticleRepository _similarArticleRepository;
        private readonly IGroupSimilarArticlesService _groupSimilarArticlesService;
        private readonly HttpClient _httpClient;
        private readonly ISlackService _slackService;
        private readonly ILoggerService<GroupSimilarArticlesCommandHandler> _loggerService;
        #endregion

        #region Constructors
        public GroupSimilarArticlesCommandHandler(
            IMemoryCache memoryCache,
            ISimilarArticleRepository similarArticleRepository,
            IGroupSimilarArticlesService groupSimilarArticlesService,
            IHttpClientFactory httpClientFactory,
            ISlackService slackService,
            ILoggerService<GroupSimilarArticlesCommandHandler> loggerService
        )
        {
            _memoryCache = memoryCache;
            _similarArticleRepository = similarArticleRepository;
            _groupSimilarArticlesService = groupSimilarArticlesService;
            _httpClient = httpClientFactory.CreateClient();
            _slackService = slackService;
            _loggerService = loggerService;
        }
        #endregion

        #region Methods
        public async Task<GroupSimilarArticlesCommandResponse> Handle(
            GroupSimilarArticlesCommand request,
            CancellationToken cancellationToken
        )
        {
            var articles = _memoryCache.Get<IEnumerable<Article>>(key: MemoryCacheConstants.ArticleKey);
            var lastSimilarityGroupingTime = _memoryCache.Get<DateTime>(key: MemoryCacheConstants.LastSimilarityGroupingTime);

            var similarArticles = _groupSimilarArticlesService.GroupSimilarArticles(
                articles,
                lastSimilarityGroupingTime
            );

            _similarArticleRepository.InsertSimilarArticles(similarArticles);
            UpdateInMemory(similarArticles);

            await CallWebServer(request, similarArticles, cancellationToken);

            _memoryCache.Set(key: MemoryCacheConstants.LastSimilarityGroupingTime, value: DateTime.UtcNow);

            var response = new GroupSimilarArticlesCommandResponse
            {
                SimilarArticlesCount = similarArticles.Count()
            };

            return response;
        }

        private void UpdateInMemory(
            IEnumerable<SimilarArticle> similarArticles
        )
        {
            // New Articles are fetched since this oepration takes some time and data is probably updated
            var articlesDictionary = _memoryCache
                .Get<IEnumerable<Article>>(key: MemoryCacheConstants.ArticleKey)
                .ToDictionary(article => article.Id);

            foreach (var similarArticle in similarArticles)
            {
                if (
                    articlesDictionary.TryGetValue(similarArticle.MainArticleId, out var mainArticle) &&
                    articlesDictionary.TryGetValue(similarArticle.SubordinateArticleId, out var subordinateArticle)
                )
                {
                    mainArticle.SubordinateArticles.Add(similarArticle);
                    subordinateArticle.SetMainArticle(similarArticle);
                }
            }

            _memoryCache.Set(key: MemoryCacheConstants.ArticleKey, value: articlesDictionary.Values.ToList());
        }

        private async Task CallWebServer(
            GroupSimilarArticlesCommand request,
            IEnumerable<SimilarArticle> similarArticles,
            CancellationToken cancellationToken
        )
        {
            if (!similarArticles.Any())
            {
                return;
            }

            var similarArticleDtos = similarArticles.Select(SimilarArticleDto.GetProjection().Compile());

            var httpHeaders = new List<(string headerKey, string headerValue)>
            {
                (headerKey: HttpHeaderConstants.ApiKeyHeaderName, headerValue: request.ParserApiKey),
                (headerKey: HttpHeaderConstants.EspressoApiHeaderName, headerValue: request.TargetedApiVersion),
                (headerKey: HttpHeaderConstants.VersionHeaderName, headerValue: request.CurrentApiVersion),
                (headerKey: HttpHeaderConstants.DeviceTypeHeaderName, headerValue: ((int)DeviceType.RssFeedParser).ToString()),
            };

            try
            {
                _httpClient.AddHeadersToHttpClient(httpHeaders);
                var response = await _httpClient.PutAsJsonAsync(
                    requestUri: $"{request.ServerUrl}/api/similar-articles",
                    value: new SimilarArticlesBodyDto
                    {
                        SimilarArticles = similarArticleDtos
                    },
                    cancellationToken: cancellationToken
                );

                response.EnsureSuccessStatusCode();

                return;
            }
            catch (Exception exception)
            {
                var eventName = "Send similar articles";
                var version = request.CurrentApiVersion;
                var arguments = new (string parameterName, object parameterValue)[]
                {
                    (nameof(version), version)
                };

                _loggerService.Log(eventName, exception, LogLevel.Error, arguments);

                await _slackService.LogError(
                        eventName: eventName,
                        version: request.TargetedApiVersion,
                        message: exception.Message,
                        exception: exception,
                        appEnvironment: request.AppEnvironment,
                        cancellationToken: default
                );
            }
        }
        #endregion
    }
}