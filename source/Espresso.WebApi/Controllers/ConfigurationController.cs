using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Espresso.WebApi.Application.Configuration.Queries.GetConfiguration;
using Espresso.WebApi.Application.Configuration.Queries.GetConfiguration_1_3;
using Espresso.WebApi.Application.Configuration.Queries.GetWebConfiguration;
using Espresso.Common.Constants;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.WebApi.Authentication;
using Espresso.WebApi.Configuration;
using Espresso.WebApi.Infrastructure;
using Espresso.WebApi.Parameters.HeaderParameters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        /// Get Web Configuration
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Get /api/web-configuration
        /// </remarks>
        /// <param name="cancellationToken"></param>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <returns>Response object containing app configuration</returns>
        /// <response code="200">Response object containing app configuration</response>
        /// <response code="401">If API Key is invalid or missing</response>
        /// <response code="500">If unhandled exception occurred</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetConfigurationQueryResponse))]
        [ApiVersion("1.4")]
        [HttpGet]
        [Authorize(Roles = ApiKey.WebAppRole)]
        [Route("api/web-configuration")]
        public async Task<IActionResult> GetWebConfiguration(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            CancellationToken cancellationToken
        )
        {
            var request = new GetWebConfigurationQuery(
                maxAgeOfNewNewsPortal: WebApiConfiguration.DateTimeConfiguration.MaxAgeOfNewNewsPortal,
                currentEspressoWebApiVersion: WebApiConfiguration.AppVersionConfiguration.Version,
                targetedEspressoWebApiVersion: basicInformationsHeaderParameters.EspressoWebApiVersion,
                consumerVersion: basicInformationsHeaderParameters.Version,
                deviceType: basicInformationsHeaderParameters.DeviceType,
                appEnvironment: WebApiConfiguration.AppConfiguration.AppEnvironment
            );
            var getNewsPortalsQueryResponse = await Mediator.Send(
                request: request,
                cancellationToken: cancellationToken
            );

            return Ok(getNewsPortalsQueryResponse);
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
        [ApiVersion("1.4")]
        [HttpGet]
        [Authorize(Roles = ApiKey.DevMobileAppRole + "," + ApiKey.MobileAppRole)]
        [Route("api/configuration")]
        public async Task<IActionResult> GetConfiguration(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            CancellationToken cancellationToken
        )
        {
            var request = new GetConfigurationQuery(
                maxAgeOfNewNewsPortal: WebApiConfiguration.DateTimeConfiguration.MaxAgeOfNewNewsPortal,
                currentEspressoWebApiVersion: WebApiConfiguration.AppVersionConfiguration.Version,
                targetedEspressoWebApiVersion: basicInformationsHeaderParameters.EspressoWebApiVersion,
                consumerVersion: basicInformationsHeaderParameters.Version,
                deviceType: basicInformationsHeaderParameters.DeviceType,
                appEnvironment: WebApiConfiguration.AppConfiguration.AppEnvironment
            );
            var getNewsPortalsQueryResponse = await Mediator.Send(
                request: request,
                cancellationToken: cancellationToken
            );

            return Ok(getNewsPortalsQueryResponse);
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetConfigurationQueryResponse_1_3))]
        [ApiVersion("1.3")]
        [HttpGet]
        [Authorize(Roles = ApiKey.DevMobileAppRole + "," + ApiKey.MobileAppRole)]
        [Route("api/configuration")]
        public async Task<IActionResult> GetConfiguration_1_3(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            CancellationToken cancellationToken
        )
        {
            var request = new GetConfigurationQuery_1_3(
                currentEspressoWebApiVersion: WebApiConfiguration.AppVersionConfiguration.Version,
                targetedEspressoWebApiVersion: basicInformationsHeaderParameters.EspressoWebApiVersion,
                consumerVersion: basicInformationsHeaderParameters.Version,
                deviceType: basicInformationsHeaderParameters.DeviceType,
                appEnvironment: WebApiConfiguration.AppConfiguration.AppEnvironment
            );
            var getNewsPortalsQueryResponse = await Mediator.Send(
                request: request,
                cancellationToken: cancellationToken
            );

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
        [ApiVersion("1.2")]
        [HttpGet]
        [Authorize(Roles = ApiKey.DevMobileAppRole + "," + ApiKey.MobileAppRole)]
        [Route("api/configuration")]
        public async Task<IActionResult> GetConfiguration_1_2(
            [Required] string mobileAppVersion,
            [Required] DeviceType mobileDeviceType,
            CancellationToken cancellationToken
        )
        {
            var request = new GetConfigurationQuery_1_3(
                currentEspressoWebApiVersion: WebApiConfiguration.AppVersionConfiguration.Version,
                targetedEspressoWebApiVersion: WebApiConfiguration.AppVersionConfiguration.EspressoWebApiVersion_1_2.ToString(),
                consumerVersion: mobileAppVersion,
                deviceType: mobileDeviceType,
                appEnvironment: WebApiConfiguration.AppConfiguration.AppEnvironment
            );

            var getNewsPortalsQueryResponse = await Mediator.Send(
                request: request,
                cancellationToken: cancellationToken
            );
            return Ok(getNewsPortalsQueryResponse);
        }

    }
}
