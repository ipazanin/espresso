using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.CQRS.NewsPortals.Queries.GetNewNewsportals;
using Espresso.Application.CQRS.NewsPortals.Queries.GetNewsPortals;
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
    /// News Portals Controller
    /// </summary>
    public class NewsPortalsController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="webApiConfiguration"></param>
        public NewsPortalsController(
            IMediator mediator,
            IWebApiConfiguration webApiConfiguration
        ) : base(mediator, webApiConfiguration)
        {
        }

        /// <summary>
        /// Get all Espresso news portals
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Get /api/newsportals
        /// </remarks>
        /// <param name="cancellationToken"></param>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <returns>Response object containing Espresso newsportals</returns>
        /// <response code="200">Response object containing Espresso newsportals</response>
        /// <response code="401">If API Key is invalid or missing</response>
        /// <response code="500">If unhandled exception occurred</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetNewsPortalsQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("api/newsportals")]
        public async Task<IActionResult> GetNewsPortals(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            CancellationToken cancellationToken
        )
        {
            var getNewsPortalsQueryResponse = await Mediator.Send(
                request: new GetNewsPortalsQuery(
                    currentEspressoWebApiVersion: WebApiConfiguration.Version,
                    espressoWebApiVersion: basicInformationsHeaderParameters.EspressoWebApiVersion,
                    version: basicInformationsHeaderParameters.Version,
                    deviceType: basicInformationsHeaderParameters.DeviceType
                ),
                cancellationToken: cancellationToken
            ).ConfigureAwait(false);

            return Ok(getNewsPortalsQueryResponse);
        }

        /// <summary>
        /// Get new Espresso news portals
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Get /api/newsportals/new
        /// </remarks>
        /// <param name="cancellationToken"></param>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <param name="newsportalIds"></param>
        /// <param name="categoryIds"></param>
        /// <returns>Response object containing Espresso newsportals</returns>
        /// <response code="200">Response object containing Espresso newsportals</response>
        /// <response code="400">If request parameters are invalid</response>
        /// <response code="401">If API Key is invalid or missing</response>
        /// <response code="500">If unhandled exception occurred</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetNewNewsPortalsQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("api/newsportals/new")]
        public async Task<IActionResult> GetNewNewsPortals(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            [FromQuery] string? newsportalIds,
            [FromQuery] string? categoryIds,
            CancellationToken cancellationToken
        )
        {
            var response = await Mediator.Send(
                request: new GetNewNewsportalsQuery(
                    newsPortalIdsString: newsportalIds,
                    categoryIdsString: categoryIds,
                    currentEspressoWebApiVersion: WebApiConfiguration.Version,
                    espressoWebApiVersion: basicInformationsHeaderParameters.EspressoWebApiVersion,
                    version: basicInformationsHeaderParameters.Version,
                    deviceType: basicInformationsHeaderParameters.DeviceType
                ),
                cancellationToken: cancellationToken
            );

            return Ok(response);
        }
    }
}
