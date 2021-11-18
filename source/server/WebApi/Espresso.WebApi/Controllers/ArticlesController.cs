// ArticlesController.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.WebApi.Application.Articles.AutoCompleteArticle;
using Espresso.WebApi.Application.Articles.Commands.HideArticle;
using Espresso.WebApi.Application.Articles.Commands.IncrementTrendingArticleScore;
using Espresso.WebApi.Application.Articles.Commands.SetFeaturedArticle;
using Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles;
using Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles_1_3;
using Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles_2_0;
using Espresso.WebApi.Application.Articles.Queries.GetFeaturedArticles;
using Espresso.WebApi.Application.Articles.Queries.GetLatestArticles;
using Espresso.WebApi.Application.Articles.Queries.GetLatestArticles_1_3;
using Espresso.WebApi.Application.Articles.Queries.GetLatestArticles_1_4;
using Espresso.WebApi.Application.Articles.Queries.GetLatestArticles_2_0;
using Espresso.WebApi.Application.Articles.Queries.GetTrendingArticles;
using Espresso.WebApi.Authentication;
using Espresso.WebApi.Configuration;
using Espresso.WebApi.DataTransferObjects;
using Espresso.WebApi.Infrastructure;
using Espresso.WebApi.RequestData.Body;
using Espresso.WebApi.RequestData.Header;
using Espresso.WebApi.RequestData.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Espresso.WebApi.Controllers
{
    /// <summary>
    /// Articles Controller.
    /// </summary>
    public class ArticlesController : ApiController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArticlesController"/> class.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="webApiConfiguration"></param>
        public ArticlesController(
            ISender sender,
            IWebApiConfiguration webApiConfiguration)
            : base(sender, webApiConfiguration)
        {
        }

        /// <summary>
        /// Get articles from selected <paramref name="newsPortalIds"/> and <paramref name="categoryIds"/>.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Get /api/articles.
        /// </remarks>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <param name="articlePaginationParameters">Parameters used for pagination.</param>
        /// <param name="cancellationToken"></param>
        /// <param name="newsPortalIds">Articles from given <paramref name="newsPortalIds"/> will be fetched or if <paramref name="newsPortalIds"/> is empty condition will be ignored.</param>
        /// <param name="categoryIds">Articles from given <paramref name="categoryIds"/> will be fetched or if <paramref name="categoryIds"/> is empty condition will be ignored.</param>
        /// <param name="titleSearchQuery">Article Title Search Query.</param>
        /// <param name="keyWordsToFilterOut"></param>
        /// <returns>Response object containing articles.</returns>
        /// <response code="200"><see cref="GetLatestArticlesQueryResponse"/> object containing articles.</response>
        /// <response code="400">If validation fails.</response>
        /// <response code="401">If API Key is invalid or missing.</response>
        /// <response code="403">If API Key is forbiden from requested resource.</response>
        /// <response code="500">If unhandled exception occurred.</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetLatestArticlesQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
        [ApiVersion("2.2")]
        [HttpGet]
        [Authorize(Roles = ApiKey.DevMobileAppRole + "," + ApiKey.MobileAppRole + "," + ApiKey.WebAppRole)]
        [Route("api/articles")]
        public async Task<IActionResult> GetLatestArticles(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            [FromQuery] ArticlePaginationParameters articlePaginationParameters,
            CancellationToken cancellationToken,
            [FromQuery] string? newsPortalIds = null,
            [FromQuery] string? categoryIds = null,
            [FromQuery] string? titleSearchQuery = null,
            [FromQuery] string? keyWordsToFilterOut = null)
        {
            var response = await Sender.Send(
                request: new GetLatestArticlesQuery
                {
                    Take = articlePaginationParameters.Take,
                    Skip = articlePaginationParameters.Skip,
                    FirstArticleId = articlePaginationParameters.FirstArticleId,
                    NewsPortalIds = newsPortalIds,
                    CategoryIds = categoryIds,
                    TitleSearchQuery = titleSearchQuery,
                    TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                    ConsumerVersion = basicInformationsHeaderParameters.Version,
                    DeviceType = basicInformationsHeaderParameters.DeviceType,
                    KeyWordsToFilterOut = keyWordsToFilterOut,
                },
                cancellationToken: cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Get articles from selected <paramref name="newsPortalIds"/> and <paramref name="categoryIds"/>.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Get /api/articles.
        /// </remarks>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <param name="articlePaginationParameters">Parameters used for pagination.</param>
        /// <param name="cancellationToken"></param>
        /// <param name="newsPortalIds">Articles from given <paramref name="newsPortalIds"/> will be fetched or if <paramref name="newsPortalIds"/> is empty condition will be ignored.</param>
        /// <param name="categoryIds">Articles from given <paramref name="categoryIds"/> will be fetched or if <paramref name="categoryIds"/> is empty condition will be ignored.</param>
        /// <param name="titleSearchQuery">Article Title Search Query.</param>
        /// <param name="keyWordsToFilterOut"></param>
        /// <returns>Response object containing articles.</returns>
        /// <response code="200">Response object containing articles.</response>
        /// <response code="400">If validation fails.</response>
        /// <response code="401">If API Key is invalid or missing.</response>
        /// <response code="403">If API Key is forbiden from requested resource.</response>
        /// <response code="500">If unhandled exception occurred.</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetLatestArticlesQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
        [ApiVersion("2.1")]
        [ApiVersion("2.0")]
        [HttpGet]
        [Authorize(Roles = ApiKey.DevMobileAppRole + "," + ApiKey.MobileAppRole + "," + ApiKey.WebAppRole)]
        [Route("api/articles")]
        public async Task<IActionResult> GetLatestArticles_2_0(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            [FromQuery] ArticlePaginationParameters articlePaginationParameters,
            CancellationToken cancellationToken,
            [FromQuery] string? newsPortalIds = null,
            [FromQuery] string? categoryIds = null,
            [FromQuery] string? titleSearchQuery = null,
            [FromQuery] string? keyWordsToFilterOut = null)
        {
            var response = await Sender.Send(
                request: new GetLatestArticlesQuery_2_0
                {
                    Take = articlePaginationParameters.Take,
                    Skip = articlePaginationParameters.Skip,
                    FirstArticleId = articlePaginationParameters.FirstArticleId,
                    NewsPortalIds = newsPortalIds,
                    CategoryIds = categoryIds,
                    TitleSearchQuery = titleSearchQuery,
                    TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                    ConsumerVersion = basicInformationsHeaderParameters.Version,
                    DeviceType = basicInformationsHeaderParameters.DeviceType,
                    KeyWordsToFilterOut = keyWordsToFilterOut,
                },
                cancellationToken: cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Get articles from selected <paramref name="newsPortalIds"/> and <paramref name="categoryIds"/>.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Get /api/articles.
        /// </remarks>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <param name="articlePaginationParameters">Parameters used for pagination.</param>
        /// <param name="cancellationToken"></param>
        /// <param name="newsPortalIds">Articles from given <paramref name="newsPortalIds"/> will be fetched or if <paramref name="newsPortalIds"/> is empty condition will be ignored.</param>
        /// <param name="categoryIds">Articles from given <paramref name="categoryIds"/> will be fetched or if <paramref name="categoryIds"/> is empty condition will be ignored.</param>
        /// <param name="titleSearchQuery">Article Title Search Query.</param>
        /// <returns>Response object containing articles.</returns>
        /// <response code="200">Response object containing articles.</response>
        /// <response code="400">If validation fails.</response>
        /// <response code="401">If API Key is invalid or missing.</response>
        /// <response code="403">If API Key is forbiden from requested resource.</response>
        /// <response code="500">If unhandled exception occurred.</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetLatestArticlesQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
        [ApiVersion("1.4")]
        [HttpGet]
        [Authorize(Roles = ApiKey.DevMobileAppRole + "," + ApiKey.MobileAppRole + "," + ApiKey.WebAppRole)]
        [Route("api/articles")]
        public async Task<IActionResult> GetLatestArticles_1_4(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            [FromQuery] ArticlePaginationParameters articlePaginationParameters,
            CancellationToken cancellationToken,
            [FromQuery] string? newsPortalIds = null,
            [FromQuery] string? categoryIds = null,
            [FromQuery] string? titleSearchQuery = null)
        {
            var response = await Sender.Send(
                request: new GetLatestArticlesQuery_1_4
                {
                    Take = articlePaginationParameters.Take,
                    Skip = articlePaginationParameters.Skip,
                    FirstArticleId = articlePaginationParameters.FirstArticleId,
                    NewsPortalIds = newsPortalIds,
                    CategoryIds = categoryIds,
                    TitleSearchQuery = titleSearchQuery,
                    TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                    ConsumerVersion = basicInformationsHeaderParameters.Version,
                    DeviceType = basicInformationsHeaderParameters.DeviceType,
                },
                cancellationToken: cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Get articles from selected <paramref name="newsPortalIds"/> and <paramref name="categoryIds"/>.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Get /api/articles/category?<paramref name="take"/>=1&amp;<paramref name="skip"/>=0.
        /// </remarks>
        /// <param name="basicInformationsHeaderParameters">App information header parameters.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <param name="take">Number of articles.</param>
        /// <param name="skip">Number of skipped articles.</param>
        /// <param name="newsPortalIds">Articles from given <paramref name="newsPortalIds"/> will be fetched or if <paramref name="newsPortalIds"/> is empty condition will be ignored.</param>
        /// <param name="categoryIds">Articles from given <paramref name="categoryIds"/> will be fetched or if <paramref name="categoryIds"/> is empty condition will be ignored.</param>
        /// <returns>Response object containing articles.</returns>
        /// <response code="200">Response object from version 1.3 containing articles.</response>
        /// <response code="400">If <paramref name="take"/> is not between 0 and 100 or <paramref name="skip"/> is lower than 0.</response>
        /// <response code="401">If API Key is invalid or missing.</response>
        /// <response code="403">If API Key is forbiden from requested resource.</response>
        /// <response code="500">If unhandled exception occurred.</response>
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
            [FromQuery] string? categoryIds = null)
        {
            var response = await Sender.Send(
                request: new GetLatestArticlesQuery_1_3
                {
                    Take = take,
                    Skip = skip,
                    NewsPortalIds = newsPortalIds,
                    CategoryIds = categoryIds,
                    TitleSearchQuery = null,
                    TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                    ConsumerVersion = basicInformationsHeaderParameters.Version,
                    DeviceType = basicInformationsHeaderParameters.DeviceType,
                },
                cancellationToken: cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Get articles from provided <paramref name="categoryId"/>.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Get /api/articles/category.
        /// </remarks>
        /// <param name="categoryId">Category Id.</param>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <param name="articlePaginationParameters">Parameters used for pagination.</param>
        /// <param name="cancellationToken"></param>
        /// <param name="newsPortalIds">Articles from given <paramref name="newsPortalIds"/> will be fetched or if <paramref name="newsPortalIds"/> is empty condition will be ignored.</param>
        /// <param name="regionId">Region ID.</param>
        /// <param name="titleSearchQuery">Article Title Search Query.</param>
        /// <param name="keyWordsToFilterOut"></param>
        /// <returns>Response object containing articles.</returns>
        /// <response code="200">Response object containing articles.</response>
        /// <response code="400">If validation fails.</response>
        /// <response code="401">If API Key is invalid or missing.</response>
        /// <response code="403">If API Key is forbiden from requested resource.</response>
        /// <response code="500">If unhandled exception occurred.</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCategoryArticlesQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
        [ApiVersion("2.2")]
        [HttpGet]
        [Authorize(Roles = ApiKey.DevMobileAppRole + "," + ApiKey.MobileAppRole + "," + ApiKey.WebAppRole)]
        [Route("api/categories/{categoryId}/articles")]
        public async Task<IActionResult> GetCategoryArticles(
            [FromRoute] int categoryId,
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            [FromQuery] ArticlePaginationParameters articlePaginationParameters,
            CancellationToken cancellationToken,
            [FromQuery] string? newsPortalIds = null,
            [FromQuery] int? regionId = null,
            [FromQuery] string? titleSearchQuery = null,
            [FromQuery] string? keyWordsToFilterOut = null)
        {
            var articles = await Sender.Send(
                request: new GetCategoryArticlesQuery
                {
                    Take = articlePaginationParameters.Take,
                    Skip = articlePaginationParameters.Skip,
                    FirstArticleId = articlePaginationParameters.FirstArticleId,
                    CategoryId = categoryId,
                    NewsPortalIds = newsPortalIds,
                    RegionId = regionId,
                    TitleSearchQuery = titleSearchQuery,
                    TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                    ConsumerVersion = basicInformationsHeaderParameters.Version,
                    DeviceType = basicInformationsHeaderParameters.DeviceType,
                    KeyWordsToFilterOut = keyWordsToFilterOut,
                },
                cancellationToken: cancellationToken);

            return Ok(articles);
        }

        /// <summary>
        /// Get articles from provided <paramref name="categoryId"/>.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Get /api/articles/category.
        /// </remarks>
        /// <param name="categoryId">Category Id.</param>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <param name="articlePaginationParameters">Parameters used for pagination.</param>
        /// <param name="cancellationToken"></param>
        /// <param name="newsPortalIds">Articles from given <paramref name="newsPortalIds"/> will be fetched or if <paramref name="newsPortalIds"/> is empty condition will be ignored.</param>
        /// <param name="regionId">Region ID.</param>
        /// <param name="titleSearchQuery">Article Title Search Query.</param>
        /// <param name="keyWordsToFilterOut"></param>
        /// <returns>Response object containing articles.</returns>
        /// <response code="200">Response object containing articles.</response>
        /// <response code="400">If validation fails.</response>
        /// <response code="401">If API Key is invalid or missing.</response>
        /// <response code="403">If API Key is forbiden from requested resource.</response>
        /// <response code="500">If unhandled exception occurred.</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCategoryArticlesQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
        [ApiVersion("2.1")]
        [ApiVersion("2.0")]
        [ApiVersion("1.4")]
        [HttpGet]
        [Authorize(Roles = ApiKey.DevMobileAppRole + "," + ApiKey.MobileAppRole + "," + ApiKey.WebAppRole)]
        [Route("api/categories/{categoryId}/articles")]
        public async Task<IActionResult> GetCategoryArticles_2_0(
            [FromRoute] int categoryId,
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            [FromQuery] ArticlePaginationParameters articlePaginationParameters,
            CancellationToken cancellationToken,
            [FromQuery] string? newsPortalIds = null,
            [FromQuery] int? regionId = null,
            [FromQuery] string? titleSearchQuery = null,
            [FromQuery] string? keyWordsToFilterOut = null)
        {
            var articles = await Sender.Send(
                request: new GetCategoryArticlesQuery_2_0
                {
                    Take = articlePaginationParameters.Take,
                    Skip = articlePaginationParameters.Skip,
                    FirstArticleId = articlePaginationParameters.FirstArticleId,
                    CategoryId = categoryId,
                    NewsPortalIds = newsPortalIds,
                    RegionId = regionId,
                    TitleSearchQuery = titleSearchQuery,
                    TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                    ConsumerVersion = basicInformationsHeaderParameters.Version,
                    DeviceType = basicInformationsHeaderParameters.DeviceType,
                    KeyWordsToFilterOut = keyWordsToFilterOut,
                },
                cancellationToken: cancellationToken);

            return Ok(articles);
        }

        /// <summary>
        /// Get articles from provided <paramref name="categoryId"/>.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Get /api/articles/category?<paramref name="take"/>=1&amp;<paramref name="skip"/>=0.
        /// </remarks>
        /// <param name="categoryId">Category Id.</param>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="take">Number of articles.</param>
        /// <param name="skip">Number of skipped articles.</param>
        /// <param name="newsPortalIds">Articles from given <paramref name="newsPortalIds"/> will be fetched or if <paramref name="newsPortalIds"/> is empty condition will be ignored.</param>
        /// <returns>Response object containing articles.</returns>
        /// <response code="200">Response object containing articles.</response>
        /// <response code="400">If <paramref name="take"/> is not between and 100 or <paramref name="skip"/> is lower than 0 or <paramref name="categoryId"/> is not valid category id.</response>
        /// <response code="401">If API Key is invalid or missing.</response>
        /// <response code="403">If API Key is forbiden from requested resource.</response>
        /// <response code="500">If unhandled exception occurred.</response>
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
            [FromQuery] string? newsPortalIds = null)
        {
            var articles = await Sender.Send(
                request: new GetCategoryArticlesQuery_1_3
                {
                    Take = take,
                    Skip = skip,
                    CategoryId = categoryId,
                    NewsPortalIds = newsPortalIds,
                    TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                    ConsumerVersion = basicInformationsHeaderParameters.Version,
                    DeviceType = basicInformationsHeaderParameters.DeviceType,
                },
                cancellationToken: cancellationToken);

            return Ok(articles);
        }

        /// <summary>
        /// Get trending articles.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Get /api/articles/trending.
        /// </remarks>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <param name="articlePaginationParameters">Parameters used for pagination.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Response object containing articles.</returns>
        /// <response code="200">Response object containing articles.</response>
        /// <response code="400">If validation fails.</response>
        /// <response code="401">If API Key is invalid or missing.</response>
        /// <response code="403">If API Key is forbiden from requested resource.</response>
        /// <response code="500">If unhandled exception occurred.</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTrendingArticlesQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
        [ApiVersion("1.4")]
        [ApiVersion("1.3")]
        [ApiVersion("1.2")]
        [HttpGet]
        [Authorize(Roles = ApiKey.DevMobileAppRole + "," + ApiKey.MobileAppRole + "," + ApiKey.WebAppRole)]
        [Route("api/articles/trending")]
        public async Task<IActionResult> GetTrendingArticles(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            [FromQuery] ArticlePaginationParameters articlePaginationParameters,
            CancellationToken cancellationToken)
        {
            var response = await Sender.Send(
                request: new GetTrendingArticlesQuery
                {
                    Take = articlePaginationParameters.Take,
                    Skip = articlePaginationParameters.Skip,
                    FirstArticleId = articlePaginationParameters.FirstArticleId,
                    TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                    ConsumerVersion = basicInformationsHeaderParameters.Version,
                    DeviceType = basicInformationsHeaderParameters.DeviceType,
                },
                cancellationToken: cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Get featured articles.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Get /api/articles/featured.
        /// </remarks>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <param name="articlePaginationParameters">Parameters used for pagination.</param>
        /// <param name="cancellationToken"></param>
        /// <param name="newsPortalIds">NewsPortal Ids comma delimited.</param>
        /// <param name="categoryIds">Category Ids comma delimited.</param>
        /// <param name="keyWordsToFilterOut"></param>
        /// <returns>Response object containing articles.</returns>
        /// <response code="200">Response object containing articles.</response>
        /// <response code="400">If validation fails.</response>
        /// <response code="401">If API Key is invalid or missing.</response>
        /// <response code="403">If API Key is forbiden from requested resource.</response>
        /// <response code="500">If unhandled exception occurred.</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTrendingArticlesQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
        [ApiVersion("2.2")]
        [ApiVersion("2.1")]
        [ApiVersion("2.0")]
        [ApiVersion("1.4")]
        [HttpGet]
        [Authorize(Roles = ApiKey.DevMobileAppRole + "," + ApiKey.MobileAppRole + "," + ApiKey.WebAppRole)]
        [Route("api/articles/featured")]
        public async Task<IActionResult> GetFeaturedArticles(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            [FromQuery] ArticlePaginationParameters articlePaginationParameters,
            CancellationToken cancellationToken,
            [FromQuery] string? newsPortalIds = null,
            [FromQuery] string? categoryIds = null,
            [FromQuery] string? keyWordsToFilterOut = null)
        {
            var response = await Sender.Send(
                request: new GetFeaturedArticlesQuery
                {
                    Take = articlePaginationParameters.Take,
                    Skip = articlePaginationParameters.Skip,
                    FirstArticleId = articlePaginationParameters.FirstArticleId,
                    CategoryIds = categoryIds,
                    NewsPortalIds = newsPortalIds,
                    TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                    ConsumerVersion = basicInformationsHeaderParameters.Version,
                    DeviceType = basicInformationsHeaderParameters.DeviceType,
                    KeyWordsToFilterOut = keyWordsToFilterOut,
                },
                cancellationToken: cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Increments Trending Score for article with <paramref name="articleId"/>.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Post /api/articles/score/<paramref name="articleId"/>.
        /// </remarks>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <param name="articleId">Article Id.</param>
        /// <param name="cancellationToken"></param>
        /// <response code="200">When operation is successfull.</response>
        /// <response code="400">If <paramref name="articleId"/> is not valid Guid.</response>
        /// <response code="401">If API Key is invalid or missing.</response>
        /// <response code="403">If API Key is forbiden from requested resource.</response>
        /// <response code="404">If resource is not found.</response>
        /// <response code="500">If unhandled exception occurred.</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
        [ApiVersion("2.2")]
        [ApiVersion("2.1")]
        [ApiVersion("2.0")]
        [ApiVersion("1.4")]
        [ApiVersion("1.3")]
        [ApiVersion("1.2")]
        [HttpPatch]
        [Authorize(Roles = ApiKey.DevMobileAppRole + "," + ApiKey.MobileAppRole + "," + ApiKey.WebAppRole)]
        [Route("api/articles/{articleId}")]
        public async Task<IActionResult> IncrementArticleScore(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            [Required] Guid articleId,
            CancellationToken cancellationToken)
        {
            await Sender.Send(
                request: new IncrementNumberOfClicksCommand
                {
                    Id = articleId,
                    TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                    ConsumerVersion = basicInformationsHeaderParameters.Version,
                    DeviceType = basicInformationsHeaderParameters.DeviceType,
                },
                cancellationToken: cancellationToken);

            return Ok();
        }

        /// <summary>
        /// Hide article with <paramref name="articleId"/>.
        /// </summary>
        /// <param name="basicInformationsHeaderParameters">Basic App Information.</param>
        /// <param name="articleId">Article Id.</param>
        /// <param name="isHidden"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="200">When operation is successfull.</response>
        /// <response code="400">If <paramref name="articleId"/> is not valid Guid.</response>
        /// <response code="401">If API Key is invalid or missing.</response>
        /// <response code="403">If API Key is forbiden from requested resource.</response>
        /// <response code="404">If resource is not found.</response>
        /// <response code="500">If unhandled exception occurred.</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
        [ApiVersion("2.2")]
        [ApiVersion("2.1")]
        [ApiVersion("2.0")]
        [HttpDelete]
        [Authorize(Roles = ApiKey.DevMobileAppRole)]
        [Route("api/articles/{articleId}")]
        public async Task<IActionResult> HideArticle(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            [FromRoute][Required] Guid articleId,
            [FromQuery][Required] bool isHidden,
            CancellationToken cancellationToken)
        {
            await Sender.Send(
                request: new HideArticleCommand
                {
                    ArticleId = articleId,
                    IsHidden = isHidden,
                    TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                    ConsumerVersion = basicInformationsHeaderParameters.Version,
                    DeviceType = basicInformationsHeaderParameters.DeviceType,
                },
                cancellationToken: cancellationToken);

            return Ok();
        }

        /// <summary>
        /// Set featured articles configuration.
        /// </summary>
        /// <param name="basicInformationsHeaderParameters">Basic App Information.</param>
        /// <param name="requestBody"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="200">When operation is successfull.</response>
        /// <response code="400">If validation fails.</response>
        /// <response code="401">If API Key is invalid or missing.</response>
        /// <response code="403">If API Key is forbiden from requested resource.</response>
        /// <response code="404">If resource is not found.</response>
        /// <response code="500">If unhandled exception occurred.</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
        [ApiVersion("2.2")]
        [ApiVersion("2.1")]
        [ApiVersion("2.0")]
        [HttpPatch]
        [Authorize(Roles = ApiKey.DevMobileAppRole)]
        [Route("api/articles/featured")]
        public async Task<IActionResult> SetArticleFeaturedConfiguration(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            [FromBody][Required] IEnumerable<SetArticleFeaturedConfigurationRequestBody> requestBody,
            CancellationToken cancellationToken)
        {
            await Sender.Send(
                request: new SetFeaturedArticleCommand
                {
                    FeaturedArticleConfigurations = requestBody
                        .Select(
                            featuredConfiguration =>
                            (
                                articleId: featuredConfiguration.Id,
                                isFeatured: featuredConfiguration.IsFeatured,
                                featuredPosition: featuredConfiguration.FeaturedPosition)),
                    TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                    ConsumerVersion = basicInformationsHeaderParameters.Version,
                    DeviceType = basicInformationsHeaderParameters.DeviceType,
                },
                cancellationToken: cancellationToken);

            return Ok();
        }

        /// <summary>
        /// Auto Complete Article Titles.
        /// </summary>
        /// <param name="basicInformationsHeaderParameters">Basic App Information.</param>
        /// <param name="titleSearchQuery"></param>
        /// <param name="paginationParameters"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="keyWordsToFilterOut"></param>
        /// <response code="200">When operation is successfull.</response>
        /// <response code="400">If validation fails.</response>
        /// <response code="401">If API Key is invalid or missing.</response>
        /// <response code="403">If API Key is forbiden from requested resource.</response>
        /// <response code="404">If resource is not found.</response>
        /// <response code="500">If unhandled exception occurred.</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
        [ApiVersion("2.2")]
        [ApiVersion("2.1")]
        [ApiVersion("2.0")]
        [HttpGet]
        [Authorize(Roles = ApiKey.DevMobileAppRole + "," + ApiKey.MobileAppRole)]
        [Route("api/articles/autocomplete")]
        public async Task<IActionResult> AutoCompleteArticleTitles(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            [FromQuery] string? titleSearchQuery,
            [FromQuery] PaginationParameters paginationParameters,
            CancellationToken cancellationToken,
            [FromQuery] string? keyWordsToFilterOut = null)
        {
            var request = new AutoCompleteArticleQuery
            {
                TitleSearchQuery = titleSearchQuery,
                Take = paginationParameters.Take,
                Skip = paginationParameters.Skip,
                TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                ConsumerVersion = basicInformationsHeaderParameters.Version,
                DeviceType = basicInformationsHeaderParameters.DeviceType,
                KeyWordsToFilterOut = keyWordsToFilterOut,
            };

            var response = await Sender.Send(request, cancellationToken);

            return Ok(response);
        }
    }
}
