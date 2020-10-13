using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Espresso.WebApi.Application.Articles.Commands.CalculateTrendingScore;
using Espresso.WebApi.Application.Articles.Commands.HideArticle;
using Espresso.WebApi.Application.Articles.Commands.IncrementTrendingArticleScore;
using Espresso.WebApi.Application.Articles.Commands.ToggleFeaturedArticle;
using Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles;
using Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles_1_3;
using Espresso.WebApi.Application.Articles.Queries.GetFeaturedArticles;
using Espresso.WebApi.Application.Articles.Queries.GetLatestArticles;
using Espresso.WebApi.Application.Articles.Queries.GetLatestArticles_1_3;
using Espresso.WebApi.Application.Articles.Queries.GetTrendingArticles;
using Espresso.Common.Constants;
using Espresso.WebApi.Authentication;
using Espresso.WebApi.Configuration;
using Espresso.WebApi.Infrastructure;
using Espresso.WebApi.Parameters.HeaderParameters;
using Espresso.WebApi.QueryParameters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Espresso.WebApi.DataTransferObjects;
using Espresso.WebApi.RequestObjects;
using Espresso.WebApi.Application.Articles.AutoCompleteArticle;

namespace Espresso.WebApi.Controllers
{
    /// <summary>
    /// Articles Controller
    /// </summary>
    public class ArticlesController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="webApiConfiguration"></param>
        public ArticlesController(
            IMediator mediator,
            IWebApiConfiguration webApiConfiguration
        ) : base(
            mediator, webApiConfiguration
        )
        {
        }

        /// <summary>
        /// Get articles from selected <paramref name="newsPortalIds"/> and <paramref name="categoryIds"/>
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Get /api/articles
        /// </remarks>
        /// <param name="cancellationToken"></param>
        /// <param name="newsPortalIds">Articles from given <paramref name="newsPortalIds"/> will be fetched or if <paramref name="newsPortalIds"/> is empty condition will be ignored</param>
        /// <param name="categoryIds">Articles from given <paramref name="categoryIds"/> will be fetched or if <paramref name="categoryIds"/> is empty condition will be ignored</param>
        /// <param name="titleSearchQuery">Article Title Search Query</param>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <param name="paginationParameters">Parameters used for pagination</param>
        /// <returns>Response object containing articles</returns>
        /// <response code="200">Response object containing articles</response>
        /// <response code="400">If validation fails</response>
        /// <response code="401">If API Key is invalid or missing</response>
        /// <response code="403">If API Key is forbiden from requested resource</response>
        /// <response code="500">If unhandled exception occurred</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetLatestArticlesQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
        [ApiVersion("2.0")]
        [ApiVersion("1.4")]
        [HttpGet]
        [Authorize(Roles = ApiKey.DevMobileAppRole + "," + ApiKey.MobileAppRole + "," + ApiKey.WebAppRole)]
        [Route("api/articles")]
        public async Task<IActionResult> GetLatestArticles(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            [FromQuery] PaginationParameters paginationParameters,
            CancellationToken cancellationToken,
            [FromQuery] string? newsPortalIds = null,
            [FromQuery] string? categoryIds = null,
            [FromQuery] string? titleSearchQuery = null
        )
        {
            var response = await Mediator.Send(
                request: new GetLatestArticlesQuery
                {
                    Take = paginationParameters.Take,
                    Skip = paginationParameters.Skip,
                    FirstArticleId = paginationParameters.FirstArticleId,
                    NewsPortalIds = newsPortalIds,
                    CategoryIds = categoryIds,
                    NewNewsPortalsPosition = WebApiConfiguration.AppConfiguration.NewNewsPortalsPosition,
                    TitleSearchQuery = titleSearchQuery,
                    MaxAgeOfNewNewsPortal = WebApiConfiguration.DateTimeConfiguration.MaxAgeOfNewNewsPortal,
                    CurrentApiVersion = WebApiConfiguration.AppConfiguration.Version,
                    TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                    ConsumerVersion = basicInformationsHeaderParameters.Version,
                    DeviceType = basicInformationsHeaderParameters.DeviceType,
                    AppEnvironment = WebApiConfiguration.AppConfiguration.AppEnvironment
                },
                cancellationToken: cancellationToken
            );

            return Ok(response);
        }

        /// <summary>
        /// Get articles from selected <paramref name="newsPortalIds"/> and <paramref name="categoryIds"/>
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Get /api/articles/category?<paramref name="take"/>=1&amp;<paramref name="skip"/>=0
        /// </remarks>
        /// <param name="cancellationToken"></param>
        /// <param name="take">Number of articles</param>
        /// <param name="skip">Number of skipped articles</param>
        /// <param name="newsPortalIds">Articles from given <paramref name="newsPortalIds"/> will be fetched or if <paramref name="newsPortalIds"/> is empty condition will be ignored</param>
        /// <param name="categoryIds">Articles from given <paramref name="categoryIds"/> will be fetched or if <paramref name="categoryIds"/> is empty condition will be ignored</param>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <returns>Response object containing articles</returns>
        /// <response code="200">Response object containing articles</response>
        /// <response code="400">If <paramref name="take"/> is not between 0 and 100 or <paramref name="skip"/> is lower than 0</response>
        /// <response code="401">If API Key is invalid or missing</response>
        /// <response code="403">If API Key is forbiden from requested resource</response>
        /// <response code="500">If unhandled exception occurred</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetLatestArticlesQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
        [ApiVersion("1.3")]
        [ApiVersion("1.2")]
        [HttpGet]
        [Authorize(Roles = ApiKey.DevMobileAppRole + "," + ApiKey.MobileAppRole)]
        [Route("api/articles/latest")]
        public async Task<IActionResult> GetLatestArticles_1_3(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            CancellationToken cancellationToken,
            [FromQuery] int take = DefaultValueConstants.DefaultTake,
            [FromQuery] int skip = DefaultValueConstants.DefaultSkip,
            [FromQuery] string? newsPortalIds = null,
            [FromQuery] string? categoryIds = null
        )
        {
            var response = await Mediator.Send(
                request: new GetLatestArticlesQuery_1_3
                {
                    Take = take,
                    Skip = skip,
                    NewsPortalIds = newsPortalIds,
                    CategoryIds = categoryIds,
                    TitleSearchQuery = null,
                    CurrentApiVersion = WebApiConfiguration.AppConfiguration.Version,
                    TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                    ConsumerVersion = basicInformationsHeaderParameters.Version,
                    DeviceType = basicInformationsHeaderParameters.DeviceType,
                    AppEnvironment = WebApiConfiguration.AppConfiguration.AppEnvironment
                },
                cancellationToken: cancellationToken
            );

            return Ok(response);
        }

        /// <summary>
        /// Get articles from provided <paramref name="categoryId"/>
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Get /api/articles/category
        /// </remarks>
        /// <param name="cancellationToken"></param>
        /// <param name="categoryId">Category Id</param>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <param name="paginationParameters">Parameters used for pagination</param>
        /// <param name="newsPortalIds">Articles from given <paramref name="newsPortalIds"/> will be fetched or if <paramref name="newsPortalIds"/> is empty condition will be ignored</param>
        /// <param name="regionId">Region ID</param>
        /// <param name="titleSearchQuery">Article Title Search Query</param>
        /// <returns>Response object containing articles</returns>
        /// <response code="200">Response object containing articles</response>
        /// <response code="400">If validation fails</response>
        /// <response code="401">If API Key is invalid or missing</response>
        /// <response code="403">If API Key is forbiden from requested resource</response>
        /// <response code="500">If unhandled exception occurred</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCategoryArticlesQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
        [ApiVersion("2.0")]
        [ApiVersion("1.4")]
        [HttpGet]
        [Authorize(Roles = ApiKey.DevMobileAppRole + "," + ApiKey.MobileAppRole + "," + ApiKey.WebAppRole)]
        [Route("api/categories/{categoryId}/articles")]
        public async Task<IActionResult> GetCategoryArticles(
            [FromRoute] int categoryId,
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            [FromQuery] PaginationParameters paginationParameters,
            CancellationToken cancellationToken,
            [FromQuery] string? newsPortalIds = null,
            [FromQuery] int? regionId = null,
            [FromQuery] string? titleSearchQuery = null
        )
        {
            var articles = await Mediator.Send(
                request: new GetCategoryArticlesQuery
                {
                    Take = paginationParameters.Take,
                    Skip = paginationParameters.Skip,
                    FirstArticleId = paginationParameters.FirstArticleId,
                    CategoryId = categoryId,
                    NewsPortalIds = newsPortalIds,
                    RegionId = regionId,
                    NewNewsPortalsPosition = WebApiConfiguration.AppConfiguration.NewNewsPortalsPosition,
                    TitleSearchQuery = titleSearchQuery,
                    MaxAgeOfNewNewsPortal = WebApiConfiguration.DateTimeConfiguration.MaxAgeOfNewNewsPortal,
                    CurrentApiVersion = WebApiConfiguration.AppConfiguration.Version,
                    TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                    ConsumerVersion = basicInformationsHeaderParameters.Version,
                    DeviceType = basicInformationsHeaderParameters.DeviceType,
                    AppEnvironment = WebApiConfiguration.AppConfiguration.AppEnvironment
                },
                cancellationToken: cancellationToken
            );

            return Ok(articles);
        }

        /// <summary>
        /// Get articles from provided <paramref name="categoryId"/>
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Get /api/articles/category?<paramref name="take"/>=1&amp;<paramref name="skip"/>=0
        /// </remarks>
        /// <param name="cancellationToken"></param>
        /// <param name="take">Number of articles</param>
        /// <param name="skip">Number of skipped articles</param>
        /// <param name="categoryId">Category Id</param>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <param name="newsPortalIds">Articles from given <paramref name="newsPortalIds"/> will be fetched or if <paramref name="newsPortalIds"/> is empty condition will be ignored</param>
        /// <returns>Response object containing articles</returns>
        /// <response code="200">Response object containing articles</response>
        /// <response code="400">If <paramref name="take"/> is not between and 100 or <paramref name="skip"/> is lower than 0 or <paramref name="categoryId"/> is not valid category id</response>
        /// <response code="401">If API Key is invalid or missing</response>
        /// <response code="403">If API Key is forbiden from requested resource</response>
        /// <response code="500">If unhandled exception occurred</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCategoryArticlesQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
        [ApiVersion("1.3")]
        [ApiVersion("1.2")]
        [HttpGet]
        [Authorize(Roles = ApiKey.DevMobileAppRole + "," + ApiKey.MobileAppRole)]
        [Route("api/articles/category")]
        public async Task<IActionResult> GetCategoryArticles_1_3(
            [Required] int categoryId,
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            CancellationToken cancellationToken,
            [FromQuery] int take = DefaultValueConstants.DefaultTake,
            [FromQuery] int skip = DefaultValueConstants.DefaultSkip,
            [FromQuery] string? newsPortalIds = null
        )
        {
            var articles = await Mediator.Send(
                request: new GetCategoryArticlesQuery_1_3
                {
                    Take = take,
                    Skip = skip,
                    CategoryId = categoryId,
                    NewsPortalIds = newsPortalIds,
                    CurrentApiVersion = WebApiConfiguration.AppConfiguration.Version,
                    TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                    ConsumerVersion = basicInformationsHeaderParameters.Version,
                    DeviceType = basicInformationsHeaderParameters.DeviceType,
                    AppEnvironment = WebApiConfiguration.AppConfiguration.AppEnvironment
                },
                cancellationToken: cancellationToken
            );

            return Ok(articles);
        }

        /// <summary>
        /// Get trending articles
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Get /api/articles/trending
        /// </remarks>
        /// <param name="cancellationToken"></param>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <param name="paginationParameters">Parameters used for pagination</param>
        /// <returns>Response object containing articles</returns>
        /// <response code="200">Response object containing articles</response>
        /// <response code="400">If validation fails</response>
        /// <response code="401">If API Key is invalid or missing</response>
        /// <response code="403">If API Key is forbiden from requested resource</response>
        /// <response code="500">If unhandled exception occurred</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTrendingArticlesQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
        [ApiVersion("2.0")]
        [ApiVersion("1.4")]
        [ApiVersion("1.3")]
        [ApiVersion("1.2")]
        [HttpGet]
        [Authorize(Roles = ApiKey.DevMobileAppRole + "," + ApiKey.MobileAppRole + "," + ApiKey.WebAppRole)]
        [Route("api/articles/trending")]
        public async Task<IActionResult> GetTrendingArticles(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            [FromQuery] PaginationParameters paginationParameters,
            CancellationToken cancellationToken
        )
        {
            var response = await Mediator.Send(
                request: new GetTrendingArticlesQuery
                {
                    Take = paginationParameters.Take,
                    Skip = paginationParameters.Skip,
                    FirstArticleId = paginationParameters.FirstArticleId,
                    MaxAgeOfTrendingArticle = WebApiConfiguration.DateTimeConfiguration.MaxAgeOfTrendingArticle,
                    CurrentApiVersion = WebApiConfiguration.AppConfiguration.Version,
                    TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                    ConsumerVersion = basicInformationsHeaderParameters.Version,
                    DeviceType = basicInformationsHeaderParameters.DeviceType,
                    AppEnvironment = WebApiConfiguration.AppConfiguration.AppEnvironment
                },
                cancellationToken: cancellationToken
            );

            return Ok(response);
        }

        /// <summary>
        /// Get featured articles
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Get /api/articles/featured
        /// </remarks>
        /// <param name="cancellationToken"></param>
        /// <param name="newsPortalIds">NewsPortal Ids comma delimited</param>
        /// <param name="categoryIds">Category Ids comma delimited</param>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <param name="paginationParameters">Parameters used for pagination</param>
        /// <returns>Response object containing articles</returns>
        /// <response code="200">Response object containing articles</response>
        /// <response code="400">If validation fails</response>
        /// <response code="401">If API Key is invalid or missing</response>
        /// <response code="403">If API Key is forbiden from requested resource</response>
        /// <response code="500">If unhandled exception occurred</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTrendingArticlesQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
        [ApiVersion("2.0")]
        [ApiVersion("1.4")]
        [HttpGet]
        [Authorize(Roles = ApiKey.DevMobileAppRole + "," + ApiKey.MobileAppRole + "," + ApiKey.WebAppRole)]
        [Route("api/articles/featured")]
        public async Task<IActionResult> GetFeaturedArticles(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            [FromQuery] PaginationParameters paginationParameters,
            CancellationToken cancellationToken,
            [FromQuery] string? newsPortalIds = null,
            [FromQuery] string? categoryIds = null
        )
        {
            var response = await Mediator.Send(
                request: new GetFeaturedArticlesQuery
                {
                    Take = paginationParameters.Take,
                    Skip = paginationParameters.Skip,
                    FirstArticleId = paginationParameters.FirstArticleId,
                    CategoryIds = categoryIds,
                    NewsPortalIds = newsPortalIds,
                    MaxAgeOfFeaturedArticle = WebApiConfiguration.DateTimeConfiguration.MaxAgeOfFeaturedArticle,
                    MaxAgeOfTrendingArticle = WebApiConfiguration.DateTimeConfiguration.MaxAgeOfTrendingArticle,
                    CurrentApiVersion = WebApiConfiguration.AppConfiguration.Version,
                    TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                    ConsumerVersion = basicInformationsHeaderParameters.Version,
                    DeviceType = basicInformationsHeaderParameters.DeviceType,
                    AppEnvironment = WebApiConfiguration.AppConfiguration.AppEnvironment
                },
                cancellationToken: cancellationToken
            );

            return Ok(response);
        }

        /// <summary>
        /// Increments Trending Score for article with <paramref name="articleId"/>
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Post /api/articles/score/<paramref name="articleId"/>
        /// </remarks>
        /// <param name="cancellationToken"></param>
        /// <param name="articleId">Article Id</param>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <returns></returns>
        /// <response code="200">When operation is sucessfull</response>
        /// <response code="400">If <paramref name="articleId"/> is not valid Guid</response>
        /// <response code="401">If API Key is invalid or missing</response>
        /// <response code="403">If API Key is forbiden from requested resource</response>
        /// <response code="404">If resource is nout found</response>
        /// <response code="500">If unhandled exception occurred</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
        [ApiVersion("2.0")]
        [ApiVersion("1.4")]
        [HttpPatch]
        [Authorize(Roles = ApiKey.DevMobileAppRole + "," + ApiKey.MobileAppRole + "," + ApiKey.WebAppRole)]
        [Route("api/articles/{articleId}")]
        public async Task<IActionResult> IncrementArticleScore(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            [Required] Guid articleId,
            CancellationToken cancellationToken
        )
        {
            await Mediator.Send(
                request: new IncrementNumberOfClicksCommand
                {
                    Id = articleId,
                    CurrentApiVersion = WebApiConfiguration.AppConfiguration.Version,
                    TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                    ConsumerVersion = basicInformationsHeaderParameters.Version,
                    DeviceType = basicInformationsHeaderParameters.DeviceType,
                    AppEnvironment = WebApiConfiguration.AppConfiguration.AppEnvironment
                },
                cancellationToken: cancellationToken
            );

            await Mediator.Send(
                request: new CalculateTrendingScoreCommand
                {
                    CurrentApiVersion = WebApiConfiguration.AppConfiguration.Version,
                    TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                    ConsumerVersion = basicInformationsHeaderParameters.Version,
                    DeviceType = basicInformationsHeaderParameters.DeviceType,
                    AppEnvironment = WebApiConfiguration.AppConfiguration.AppEnvironment
                },
                cancellationToken: cancellationToken
            );

            return Ok();
        }

        /// <summary>
        /// Increments Trending Score for article with <paramref name="articleId"/>
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Post /api/articles/score/<paramref name="articleId"/>
        /// </remarks>
        /// <param name="cancellationToken"></param>
        /// <param name="articleId">Article Id</param>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <returns></returns>
        /// <response code="200">When operation is sucessfull</response>
        /// <response code="400">If <paramref name="articleId"/> is not valid Guid</response>
        /// <response code="401">If API Key is invalid or missing</response>
        /// <response code="403">If API Key is forbiden from requested resource</response>
        /// <response code="404">If resource is nout found</response>
        /// <response code="500">If unhandled exception occurred</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
        [ApiVersion("1.3")]
        [ApiVersion("1.2")]
        [HttpPatch]
        [Authorize(Roles = ApiKey.DevMobileAppRole + "," + ApiKey.MobileAppRole)]
        [Route("api/articles/score/{articleId}")]
        public async Task<IActionResult> IncrementArticleScore_1_3(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            [Required] Guid articleId,
            CancellationToken cancellationToken
        )
        {
            await Mediator.Send(
                request: new IncrementNumberOfClicksCommand
                {
                    Id = articleId,
                    CurrentApiVersion = WebApiConfiguration.AppConfiguration.Version,
                    TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                    ConsumerVersion = basicInformationsHeaderParameters.Version,
                    DeviceType = basicInformationsHeaderParameters.DeviceType,
                    AppEnvironment = WebApiConfiguration.AppConfiguration.AppEnvironment
                },
                cancellationToken: cancellationToken
            );

            await Mediator.Send(
                request: new CalculateTrendingScoreCommand
                {
                    CurrentApiVersion = WebApiConfiguration.AppConfiguration.Version,
                    TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                    ConsumerVersion = basicInformationsHeaderParameters.Version,
                    DeviceType = basicInformationsHeaderParameters.DeviceType,
                    AppEnvironment = WebApiConfiguration.AppConfiguration.AppEnvironment
                },
                cancellationToken: cancellationToken
            );

            return Ok();
        }

        /// <summary>
        /// Hide article with <paramref name="articleId"/>
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="articleId">Article Id</param>
        /// <param name="isHidden"></param>
        /// <param name="basicInformationsHeaderParameters">Basic App Informations</param>
        /// <returns></returns>
        /// <response code="200">When operation is sucessfull</response>
        /// <response code="400">If <paramref name="articleId"/> is not valid Guid</response>
        /// <response code="401">If API Key is invalid or missing</response>
        /// <response code="403">If API Key is forbiden from requested resource</response>
        /// <response code="404">If resource is nout found</response>
        /// <response code="500">If unhandled exception occurred</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
        [ApiVersion("2.0")]
        [HttpDelete]
        [Authorize(Roles = ApiKey.DevMobileAppRole)]
        [Route("api/articles/{articleId}")]
        public async Task<IActionResult> HideArticle(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            [FromRoute][Required] Guid articleId,
            [FromQuery][Required] bool isHidden,
            CancellationToken cancellationToken
        )
        {
            await Mediator.Send(
                request: new HideArticleCommand
                {
                    ArticleId = articleId,
                    IsHidden = isHidden,
                    CurrentApiVersion = WebApiConfiguration.AppConfiguration.Version,
                    TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                    ConsumerVersion = basicInformationsHeaderParameters.Version,
                    DeviceType = basicInformationsHeaderParameters.DeviceType,
                    AppEnvironment = WebApiConfiguration.AppConfiguration.AppEnvironment
                },
                cancellationToken: cancellationToken
            );

            return Ok();
        }

        /// <summary>
        /// Feature Article with <paramref name="articleId"/>
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="articleId">Article Id</param>
        /// <param name="requestBody"></param>
        /// <param name="basicInformationsHeaderParameters">Basic App Informations</param>
        /// <returns></returns>
        /// <response code="200">When operation is sucessfull</response>
        /// <response code="400">If <paramref name="articleId"/> is not valid Guid</response>
        /// <response code="401">If API Key is invalid or missing</response>
        /// <response code="403">If API Key is forbiden from requested resource</response>
        /// <response code="404">If resource is nout found</response>
        /// <response code="500">If unhandled exception occurred</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
        [ApiVersion("2.0")]
        [HttpPatch]
        [Authorize(Roles = ApiKey.DevMobileAppRole)]
        [Route("api/articles/{articleId}/featured")]
        public async Task<IActionResult> SetArticleFeaturedConfiguration(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            [FromRoute][Required] Guid articleId,
            [FromBody][Required] SetArticleFeaturedConfigurationRequestBody requestBody,
            CancellationToken cancellationToken
        )
        {
            await Mediator.Send(
                request: new ToggleFeaturedArticleCommand
                {
                    ArticleId = articleId,
                    IsFeatured = requestBody.IsFeatured,
                    FeraturedPosition = requestBody.FeaturedPosition,
                    CurrentApiVersion = WebApiConfiguration.AppConfiguration.Version,
                    TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                    ConsumerVersion = basicInformationsHeaderParameters.Version,
                    DeviceType = basicInformationsHeaderParameters.DeviceType,
                    AppEnvironment = WebApiConfiguration.AppConfiguration.AppEnvironment
                },
                cancellationToken: cancellationToken
            );

            return Ok();
        }

        /// <summary>
        /// Auto Complete Article Titles
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="basicInformationsHeaderParameters">Basic App Informations</param>
        /// <param name="titleSearchQuery"></param>
        /// <returns></returns>
        /// <response code="200">When operation is sucessfull</response>
        /// <response code="400">If validation fails</response>
        /// <response code="401">If API Key is invalid or missing</response>
        /// <response code="403">If API Key is forbiden from requested resource</response>
        /// <response code="404">If resource is nout found</response>
        /// <response code="500">If unhandled exception occurred</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
        [ApiVersion("2.0")]
        [HttpGet]
        [Authorize(Roles = ApiKey.DevMobileAppRole + "," + ApiKey.MobileAppRole)]
        [Route("api/articles/autocomplete")]
        public async Task<IActionResult> AutoCompleteArticleTitles(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            [FromQuery] string? titleSearchQuery,
            CancellationToken cancellationToken
        )
        {
            var request = new AutoCompleteArticleQuery
            {
                TitleSearchQuery = titleSearchQuery,
                Take = WebApiConfiguration.AppConfiguration.AutoCompleteResultsTake,
                TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                ConsumerVersion = basicInformationsHeaderParameters.Version,
                DeviceType = basicInformationsHeaderParameters.DeviceType,
                AppEnvironment = WebApiConfiguration.AppConfiguration.AppEnvironment
            };

            var response = await Mediator.Send(request, cancellationToken);

            return Ok(response);
        }
    }
}
