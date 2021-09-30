// NewsPortalsController.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.WebApi.Application.NewsPortals.Commands.NewSourcesRequest;
using Espresso.WebApi.Application.NewsPortals.Queries.GetNewsPortals;
using Espresso.WebApi.Application.NewsPortals.Queries.GetNewsPortals_1_3;
using Espresso.WebApi.Authentication;
using Espresso.WebApi.Configuration;
using Espresso.WebApi.DataTransferObjects;
using Espresso.WebApi.Infrastructure;
using Espresso.WebApi.RequestData.Body;
using Espresso.WebApi.RequestData.Header;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Espresso.WebApi.Controllers
{
    /// <summary>
    /// News Portals Controller.
    /// </summary>
    public class NewsPortalsController : ApiController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewsPortalsController"/> class.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="webApiConfiguration"></param>
        public NewsPortalsController(
            ISender sender,
            IWebApiConfiguration webApiConfiguration
        )
            : base(sender, webApiConfiguration)
        {
        }

        /// <summary>
        /// Get all Espresso news portals.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Get /api/newsportals.
        /// </remarks>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Response object containing Espresso newsportals.</returns>
        /// <response code="200">Response object containing Espresso newsportals.</response>
        /// <response code="400">If validation fails.</response>
        /// <response code="401">If API Key is invalid or missing.</response>
        /// <response code="403">If API Key is forbiden from requested resource.</response>
        /// <response code="500">If unhandled exception occurred.</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetNewsPortalsQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
        [ApiVersion("2.2")]
        [ApiVersion("2.1")]
        [ApiVersion("2.0")]
        [ApiVersion("1.4")]
        [HttpGet]
        [Authorize(Roles = ApiKey.DevMobileAppRole)]
        [Route("api/newsportals")]
        public async Task<IActionResult> GetNewsPortals(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            CancellationToken cancellationToken
        )
        {
            var getNewsPortalsQueryResponse = await Sender.Send(
                request: new GetNewsPortalsQuery
                {
                    TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                    ConsumerVersion = basicInformationsHeaderParameters.Version,
                    DeviceType = basicInformationsHeaderParameters.DeviceType,
                },
                cancellationToken: cancellationToken
            );

            return Ok(getNewsPortalsQueryResponse);
        }

        /// <summary>
        /// Get all Espresso news portals.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Get /api/newsportals.
        /// </remarks>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Response object containing Espresso newsportals.</returns>
        /// <response code="200">Response object containing Espresso newsportals.</response>
        /// <response code="400">If validation fails.</response>
        /// <response code="401">If API Key is invalid or missing.</response>
        /// <response code="403">If API Key is forbiden from requested resource.</response>
        /// <response code="500">If unhandled exception occurred.</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetNewsPortalsQueryResponse_1_3))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
        [ApiVersion("1.3")]
        [ApiVersion("1.2")]
        [HttpGet]
        [Authorize(Roles = ApiKey.DevMobileAppRole + "," + ApiKey.MobileAppRole)]
        [Route("api/newsportals")]
        public async Task<IActionResult> GetNewsPortals_1_3(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            CancellationToken cancellationToken
        )
        {
            var getNewsPortalsQueryResponse = await Sender.Send(
                request: new GetNewsPortalsQuery_1_3
                {
                    TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                    ConsumerVersion = basicInformationsHeaderParameters.Version,
                    DeviceType = basicInformationsHeaderParameters.DeviceType,
                },
                cancellationToken: cancellationToken
            );

            return Ok(getNewsPortalsQueryResponse);
        }

        /// <summary>
        /// Request new NewsPortal.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     POST /api/newsportals.
        /// </remarks>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <param name="requestNewsPortalRequestObject"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Response object containing Espresso newsportals.</returns>
        /// <response code="200">Response object containing Espresso newsportals.</response>
        /// <response code="400">If validation fails.</response>
        /// <response code="401">If API Key is invalid or missing.</response>
        /// <response code="403">If API Key is forbiden from requested resource.</response>
        /// <response code="500">If unhandled exception occurred.</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetNewsPortalsQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
        [ApiVersion("2.2")]
        [ApiVersion("2.1")]
        [ApiVersion("2.0")]
        [ApiVersion("1.4")]
        [HttpPost]
        [Authorize(Roles = ApiKey.DevMobileAppRole + "," + ApiKey.MobileAppRole)]
        [Route("api/newsportals")]
        public async Task<IActionResult> RequestNewsPortal(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            [FromBody] RequestNewsPortalRequestBody requestNewsPortalRequestObject,
            CancellationToken cancellationToken
        )
        {
            var getNewsPortalsQueryResponse = await Sender.Send(
                request: new NewsSourcesRequestCommand
                {
                    NewsPortalName = requestNewsPortalRequestObject.NewsPortalName ?? string.Empty,
                    Email = requestNewsPortalRequestObject.Email ?? string.Empty,
                    Url = requestNewsPortalRequestObject.Url,
                    TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                    ConsumerVersion = basicInformationsHeaderParameters.Version,
                    DeviceType = basicInformationsHeaderParameters.DeviceType,
                },
                cancellationToken: cancellationToken
            );

            return Ok(getNewsPortalsQueryResponse);
        }
    }
}
