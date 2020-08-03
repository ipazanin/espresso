using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.CQRS.ApplicationDownloads.Commands.CreateApplicationDownload;
using Espresso.Application.CQRS.ApplicationDownloads.Queries.GetApplicationDownloadStatistics;
using Espresso.Common.Constants;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.WebApi.Configuration;
using Espresso.WebApi.Infrastructure;
using Espresso.WebApi.Parameters.HeaderParameters;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Espresso.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationDownloadsController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="webApiConfiguration"></param>
        public ApplicationDownloadsController(
            IMediator mediator,
            IWebApiConfiguration webApiConfiguration
        ) : base(mediator, webApiConfiguration)
        {
        }


        /// <summary>
        /// Creates New Application Download
        /// </summary>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <response code="200">Response object containing articles from provided category</response>
        /// <response code="401">If API Key is invalid or missing</response>
        /// <response code="500">If unhandled exception occurred</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("api/application-downloads")]
        public async Task<IActionResult> CreateApplicationDownload(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            CancellationToken cancellationToken
        )
        {
            var command = new CreateApplicationDownloadCommand(
                currentEspressoWebApiVersion: WebApiConfiguration.Version,
                targetedEspressoWebApiVersion: basicInformationsHeaderParameters.EspressoWebApiVersion,
                consumerVersion: basicInformationsHeaderParameters.Version,
                deviceType: basicInformationsHeaderParameters.DeviceType
            );

            await Mediator.Send(
                request: command,
                cancellationToken: cancellationToken
            ).ConfigureAwait(false);

            return Ok();
        }

        /// <summary>
        /// Creates New Application Download
        /// </summary>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <response code="200">Response object containing articles from provided category</response>
        /// <response code="401">If API Key is invalid or missing</response>
        /// <response code="500">If unhandled exception occurred</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("api/applicationDownloads/create")]
        public async Task<IActionResult> CreateApplicationDownload_1_3(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            CancellationToken cancellationToken
        )
        {
            var command = new CreateApplicationDownloadCommand(
                currentEspressoWebApiVersion: WebApiConfiguration.Version,
                targetedEspressoWebApiVersion: basicInformationsHeaderParameters.EspressoWebApiVersion,
                consumerVersion: basicInformationsHeaderParameters.Version,
                deviceType: basicInformationsHeaderParameters.DeviceType
            );

            await Mediator.Send(
                request: command,
                cancellationToken: cancellationToken
            ).ConfigureAwait(false);

            return Ok();
        }

        /// <summary>
        /// Creates New Application Download
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="mobileAppVersion">Mobile App Version</param>
        /// <param name="mobileDeviceType">Mobile Device Type (1 = Android, 2 = Ios)</param>
        /// <returns></returns>
        /// <response code="200">Response object containing articles from provided category</response>
        /// <response code="400">If <paramref name="mobileAppVersion"/> is empty or longer than 20 characters or deviceType is not 1 or 2</response>
        /// <response code="401">If API Key is invalid or missing</response>
        /// <response code="500">If unhandled exception occurred</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("api/applicationDownloads/create")]
        public async Task<IActionResult> CreateApplicationDownload_1_2(
            [Required] string mobileAppVersion,
            [Required] DeviceType mobileDeviceType,
            CancellationToken cancellationToken
        )
        {
            var command = new CreateApplicationDownloadCommand(
                currentEspressoWebApiVersion: WebApiConfiguration.Version,
                targetedEspressoWebApiVersion: WebApiConfiguration.EspressoWebApiVersion_1_2.ToString(),
                consumerVersion: mobileAppVersion,
                deviceType: mobileDeviceType
            );
            await Mediator.Send(
                request: command,
                cancellationToken: cancellationToken
            ).ConfigureAwait(false);

            return Ok();
        }

        /// <summary>
        /// Gets App Downloads Statistics
        /// </summary>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Response object containing articles from provided category</returns>
        /// <response code="200">Response object containing articles from popular news portals</response>
        /// <response code="401">If API Key is invalid or missing</response>
        /// <response code="500">If unhandled exception occurred</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetApplicationDownloadStatisticsQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("api/application-downloads")]
        public async Task<IActionResult> GetApplicationDownloadsStatistics(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            CancellationToken cancellationToken
        )
        {
            var response = await Mediator.Send(
                request: new GetApplicationDownloadStatisticsQuery(
                    currentEspressoWebApiVersion: WebApiConfiguration.Version,
                    targetedEspressoWebApiVersion: basicInformationsHeaderParameters.EspressoWebApiVersion,
                    consumerVersion: basicInformationsHeaderParameters.Version,
                    deviceType: basicInformationsHeaderParameters.DeviceType
                ),
                cancellationToken: cancellationToken
            ).ConfigureAwait(false);

            return Ok(response);
        }


        /// <summary>
        /// Gets App Downloads Statistics
        /// </summary>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Response object containing articles from provided category</returns>
        /// <response code="200">Response object containing articles from popular news portals</response>
        /// <response code="401">If API Key is invalid or missing</response>
        /// <response code="500">If unhandled exception occurred</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetApplicationDownloadStatisticsQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("api/applicationDownloads/statistics")]
        public async Task<IActionResult> GetApplicationDownloadsStatistics_1_3(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            CancellationToken cancellationToken
        )
        {
            var response = await Mediator.Send(
                request: new GetApplicationDownloadStatisticsQuery(
                    currentEspressoWebApiVersion: WebApiConfiguration.Version,
                    targetedEspressoWebApiVersion: basicInformationsHeaderParameters.EspressoWebApiVersion,
                    consumerVersion: basicInformationsHeaderParameters.Version,
                    deviceType: basicInformationsHeaderParameters.DeviceType
                ),
                cancellationToken: cancellationToken
            ).ConfigureAwait(false);

            return Ok(response);
        }
    }
}
