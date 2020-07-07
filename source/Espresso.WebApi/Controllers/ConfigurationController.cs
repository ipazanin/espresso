using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.CQRS.Configuration.Query.GetConfiguration;
using Espresso.Common.Constants;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.WebApi.Configuration;
using Espresso.WebApi.HeaderParameters;
using Espresso.WebApi.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Espresso.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigurationController : ApiController
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="webApiConfiguration"></param>
        public ConfigurationController(IMediator mediator, IWebApiConfiguration webApiConfiguration)
            : base(mediator, webApiConfiguration)
        {
        }

        /// <summary>
        /// Get App Configuration
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Get /api/configuration
        /// </remarks>
        /// <param name="cancellationToken"></param>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <returns>Response object containing app configuration</returns>
        /// <response code="200">Response object containing app configuration</response>
        /// <response code="401">If API Key is invalid or missing</response>
        /// <response code="500">If unhandled exception occurred</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetConfigurationQueryResponse))]
        [HttpGet]
        [Route("api/configuration")]
        public async Task<IActionResult> GetConfiguration(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            CancellationToken cancellationToken
        )
        {
            var request = new GetConfigurationQuery(
                currentEspressoWebApiVersion: WebApiConfiguration.Version,
                espressoWebApiVersion: basicInformationsHeaderParameters.EspressoWebApiVersion,
                version: basicInformationsHeaderParameters.Version,
                deviceType: basicInformationsHeaderParameters.DeviceType
            );
            var getNewsPortalsQueryResponse = await Mediator.Send(
                request: request,
                cancellationToken: cancellationToken
            ).ConfigureAwait(false);

            return Ok(getNewsPortalsQueryResponse);
        }

        /// <summary>
        /// Get App Configuration
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Get /api/configuration
        /// </remarks>
        /// <param name="mobileAppVersion">Mobile app version</param>
        /// <param name="mobileDeviceType">Mobile device type</param>
        /// <param name="cancellationToken">Mobile Device Type (1 = Android, 2 = Ios)</param>
        /// <returns>Response object containing app configuration</returns>
        /// <response code="200">Response object containing app configuration</response>
        /// <response code="400">If <paramref name="mobileAppVersion"/> is empty or longer than 20 characters or deviceType is not 1 or 2</response>
        /// <response code="401">If API Key is invalid or missing</response>
        /// <response code="500">If unhandled exception occurred</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetConfigurationQueryResponse))]
        [HttpGet]
        [Route("api/configuration")]
        public async Task<IActionResult> GetConfiguration_1_2(
            [Required] string mobileAppVersion,
            [Required] DeviceType mobileDeviceType,
            CancellationToken cancellationToken
        )
        {
            var request = new GetConfigurationQuery(
                currentEspressoWebApiVersion: WebApiConfiguration.Version,
                espressoWebApiVersion: WebApiConfiguration.EspressoWebApiVersion_1_2.ToString(),
                version: mobileAppVersion,
                deviceType: mobileDeviceType
            );

            var getNewsPortalsQueryResponse = await Mediator.Send(
                request: request,
                cancellationToken: cancellationToken
            ).ConfigureAwait(false);
            return Ok(getNewsPortalsQueryResponse);
        }

    }
}
