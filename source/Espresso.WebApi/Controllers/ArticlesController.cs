using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.CQRS.Articles.Commands.CalculateTrendingScore;
using Espresso.Application.CQRS.Articles.Commands.IncrementTrendingArticleScore;
using Espresso.Application.CQRS.Articles.Queries.GetCategoryArticles;
using Espresso.Application.CQRS.Articles.Queries.GetLatestArticles;
using Espresso.Application.CQRS.Articles.Queries.GetTrendingArticles;
using Espresso.Common.Constants;
using Espresso.WebApi.Configuration;
using Espresso.WebApi.HeaderParameters;
using Espresso.WebApi.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public ArticlesController(IMediator mediator, IWebApiConfiguration webApiConfiguration) : base(mediator, webApiConfiguration)
        {
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
        /// <returns>Response object containing articles from provided category</returns>
        /// <response code="200">Response object containing articles from popular news portals</response>
        /// <response code="400">If <paramref name="take"/> is not between 0 and 100 or <paramref name="skip"/> is lower than 0</response>
        /// <response code="401">If API Key is invalid or missing</response>
        /// <response code="500">If unhandled exception occurred</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetLatestArticlesQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("api/articles/latest")]
        public async Task<IActionResult> GetLatestArticles(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            CancellationToken cancellationToken,
            int take = DefaultValueConstants.DefaultTake,
            int skip = DefaultValueConstants.DefaultSkip,
            string? newsPortalIds = null,
            string? categoryIds = null
        )
        {
            var response = await Mediator.Send(
                request: new GetLatestArticlesQuery(
                    take: take,
                    skip: skip,
                    newsPortalIdsString: newsPortalIds,
                    categoryIdsString: categoryIds,
                    currentEspressoWebApiVersion: WebApiConfiguration.EspressoWebApiVersion,
                    espressoWebApiVersion: basicInformationsHeaderParameters.EspressoWebApiVersion,
                    version: basicInformationsHeaderParameters.Version,
                    deviceType: basicInformationsHeaderParameters.DeviceType
                ),
                cancellationToken: cancellationToken
            ).ConfigureAwait(false);

            return Ok(response);
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
        /// <returns>Response object containing articles from provided category</returns>
        /// <response code="200">Response object containing articles from provided category</response>
        /// <response code="400">If <paramref name="take"/> is not between and 100 or <paramref name="skip"/> is lower than 0 or <paramref name="categoryId"/> is not valid category id</response>
        /// <response code="401">If API Key is invalid or missing</response>
        /// <response code="500">If unhandled exception occurred</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCategoryArticlesQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("api/articles/category")]
        public async Task<IActionResult> GetCategoryArticles(
            [Required] int categoryId,
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            CancellationToken cancellationToken,
            int take = DefaultValueConstants.DefaultTake,
            int skip = DefaultValueConstants.DefaultSkip,
            string? newsPortalIds = null
        )
        {
            var articles = await Mediator.Send(
                request: new GetCategoryArticlesQuery(
                    take: take,
                    skip: skip,
                    categoryId: categoryId,
                    newsPortalIdsString: newsPortalIds,
                    currentEspressoWebApiVersion: WebApiConfiguration.EspressoWebApiVersion,
                    espressoWebApiVersion: basicInformationsHeaderParameters.EspressoWebApiVersion,
                    version: basicInformationsHeaderParameters.Version,
                    deviceType: basicInformationsHeaderParameters.DeviceType
                ),
                cancellationToken: cancellationToken
            ).ConfigureAwait(false);

            return Ok(articles);
        }

        /// <summary>
        /// Get trending articles
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Get /api/articles/trending?<paramref name="take"/>=1&amp;<paramref name="skip"/>=0
        /// </remarks>
        /// <param name="cancellationToken"></param>
        /// <param name="take">Number of articles</param>
        /// <param name="skip">Number of skipped articles</param>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <returns>Response object containing articles from provided category</returns>
        /// <response code="200">Response object containing articles from popular news portals</response>
        /// <response code="400">If <paramref name="take"/> is not between 0 and 100 or <paramref name="skip"/> is lower than 0</response>
        /// <response code="401">If API Key is invalid or missing</response>
        /// <response code="500">If unhandled exception occurred</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTrendingArticlesQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("api/articles/trending")]
        public async Task<IActionResult> GetTrendingArticles(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            CancellationToken cancellationToken,
            int take = DefaultValueConstants.DefaultTake,
            int skip = DefaultValueConstants.DefaultSkip
        )
        {
            var response = await Mediator.Send(
                request: new GetTrendingArticlesQuery(
                    take: take,
                    skip: skip,
                    currentEspressoWebApiVersion: WebApiConfiguration.EspressoWebApiVersion,
                    espressoWebApiVersion: basicInformationsHeaderParameters.EspressoWebApiVersion,
                    version: basicInformationsHeaderParameters.Version,
                    deviceType: basicInformationsHeaderParameters.DeviceType
                ),
                cancellationToken: cancellationToken
            ).ConfigureAwait(false);

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
        /// <response code="200">Response object containing articles from provided category</response>
        /// <response code="400">If <paramref name="articleId"/> is not valid Guid</response>
        /// <response code="401">If API Key is invalid or missing</response>
        /// <response code="500">If unhandled exception occurred</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPatch]
        [Route("api/articles/score/{articleId}")]
        public async Task<IActionResult> IncrementArticleScore(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            [Required] Guid articleId,
            CancellationToken cancellationToken
        )
        {
            await Mediator.Send(
                request: new IncrementNumberOfClicksCommand(
                    articleId,
                    currentEspressoWebApiVersion: WebApiConfiguration.EspressoWebApiVersion,
                    espressoWebApiVersion: basicInformationsHeaderParameters.EspressoWebApiVersion,
                    version: basicInformationsHeaderParameters.Version,
                    deviceType: basicInformationsHeaderParameters.DeviceType
                ),
                cancellationToken: cancellationToken
            ).ConfigureAwait(false);

            await Mediator.Send(
                request: new CalculateTrendingScoreCommand(
                    currentEspressoWebApiVersion: WebApiConfiguration.EspressoWebApiVersion,
                    espressoWebApiVersion: basicInformationsHeaderParameters.EspressoWebApiVersion,
                    version: basicInformationsHeaderParameters.Version,
                    deviceType: basicInformationsHeaderParameters.DeviceType
                ),
                cancellationToken: cancellationToken).ConfigureAwait(false);

            return Ok();
        }
    }
}
