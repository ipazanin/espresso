// ConfigurationController.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.WebApi.Application.Configuration.Queries.GetConfiguration;
using Espresso.WebApi.Application.Configuration.Queries.GetConfiguration_1_3;
using Espresso.WebApi.Application.Configuration.Queries.GetWebConfiguration;
using Espresso.WebApi.Authentication;
using Espresso.WebApi.Configuration;
using Espresso.WebApi.DataTransferObjects;
using Espresso.WebApi.Infrastructure;
using Espresso.WebApi.RequestData.Header;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Espresso.WebApi.Controllers
{
    /// <summary>
    ///
    /// </summary>
    public class ConfigurationController : ApiController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationController"/> class.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="webApiConfiguration"></param>
        public ConfigurationController(
            ISender sender,
            IWebApiConfiguration webApiConfiguration
        )
            : base(sender, webApiConfiguration)
        {
        }

        /// <summary>
        /// Get Web Configuration.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Get /api/web-configuration.
        /// </remarks>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Response object containing app configuration.</returns>
        /// <response code="200">Response object containing app configuration.</response>
        /// <response code="400">If validation fails.</response>
        /// <response code="401">If API Key is invalid or missing.</response>
        /// <response code="403">If API Key is forbiden from requested resource.</response>
        /// <response code="500">If unhandled exception occurred.</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetConfigurationQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
        [ApiVersion("2.2")]
        [ApiVersion("2.1")]
        [ApiVersion("2.0")]
        [ApiVersion("1.4")]
        [ResponseCache(
            Duration = 12 * 60 * 60,
            Location = ResponseCacheLocation.Any
        )]
        [HttpGet]
        [Authorize(Roles = ApiKey.WebAppRole)]
        [Route("api/web-configuration")]
        public async Task<IActionResult> GetWebConfiguration(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            CancellationToken cancellationToken
        )
        {
            var request = new GetWebConfigurationQuery
            {
                MaxAgeOfNewNewsPortal = WebApiConfiguration.DateTimeConfiguration.MaxAgeOfNewNewsPortal,
                TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                ConsumerVersion = basicInformationsHeaderParameters.Version,
                DeviceType = basicInformationsHeaderParameters.DeviceType,
            };

            var getNewsPortalsQueryResponse = await Sender.Send(
                    request: request,
                    cancellationToken: cancellationToken
                );

            return Ok(getNewsPortalsQueryResponse);
        }

        /// <summary>
        /// Get App Configuration.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Get /api/configuration.
        /// </remarks>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Response object containing app configuration.</returns>
        /// <response code="200">Response object containing app configuration.</response>
        /// <response code="40">If validation fails.</response>
        /// <response code="401">If API Key is invalid or missing.</response>
        /// <response code="403">If API Key is forbiden from requested resource.</response>
        /// <response code="500">If unhandled exception occurred.</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetConfigurationQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
        [ApiVersion("2.2")]
        [ApiVersion("2.1")]
        [ApiVersion("2.0")]
        [ApiVersion("1.4")]
        [ResponseCache(
            Duration = 12 * 60 * 60,
            Location = ResponseCacheLocation.Any
        )]
        [HttpGet]
        [Authorize(Roles = ApiKey.DevMobileAppRole + "," + ApiKey.MobileAppRole)]
        [Route("api/configuration")]
        public async Task<IActionResult> GetConfiguration(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            CancellationToken cancellationToken
        )
        {
            var request = new GetConfigurationQuery
            {
                MaxAgeOfNewNewsPortal = WebApiConfiguration.DateTimeConfiguration.MaxAgeOfNewNewsPortal,
                TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                ConsumerVersion = basicInformationsHeaderParameters.Version,
                DeviceType = basicInformationsHeaderParameters.DeviceType,
            };
            var getNewsPortalsQueryResponse = await Sender.Send(
                request: request,
                cancellationToken: cancellationToken
            );

            return Ok(getNewsPortalsQueryResponse);
        }

        /// <summary>
        /// Get App Configuration.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Get /api/configuration.
        /// </remarks>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Response object containing app configuration.</returns>
        /// <response code="200">Response object containing app configuration.</response>
        /// <response code="400">If validation fails.</response>
        /// <response code="401">If API Key is invalid or missing.</response>
        /// <response code="403">If API Key is forbiden from requested resource.</response>
        /// <response code="500">If unhandled exception occurred.</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetConfigurationQueryResponse_1_3))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
        [ApiVersion("1.3")]
        [HttpGet]
        [Authorize(Roles = ApiKey.DevMobileAppRole + "," + ApiKey.MobileAppRole)]
        [Route("api/configuration")]
        public async Task<IActionResult> GetConfiguration_1_3(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            CancellationToken cancellationToken
        )
        {
            var request = new GetConfigurationQuery_1_3
            {
                TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                ConsumerVersion = basicInformationsHeaderParameters.Version,
                DeviceType = basicInformationsHeaderParameters.DeviceType,
            };
            var getNewsPortalsQueryResponse = await Sender.Send(
                request: request,
                cancellationToken: cancellationToken
            );

            return Ok(getNewsPortalsQueryResponse);
        }

        /// <summary>
        /// Get App Configuration.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Get /api/configuration.
        /// </remarks>
        /// <param name="mobileAppVersion">Mobile app version.</param>
        /// <param name="mobileDeviceType">Mobile device type.</param>
        /// <param name="cancellationToken">Mobile Device Type (1 = Android, 2 = Ios).</param>
        /// <returns>Response object containing app configuration.</returns>
        /// <response code="200">Response object containing app configuration.</response>
        /// <response code="400">If <paramref name="mobileAppVersion"/> is empty or longer than 20 characters or deviceType is not 1 or 2.</response>
        /// <response code="401">If API Key is invalid or missing.</response>
        /// <response code="403">If API Key is forbiden from requested resource.</response>
        /// <response code="500">If unhandled exception occurred.</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetConfigurationQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
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
            var request = new GetConfigurationQuery_1_3
            {
                TargetedApiVersion = "1.2",
                ConsumerVersion = mobileAppVersion,
                DeviceType = mobileDeviceType,
            };

            var getNewsPortalsQueryResponse = await Sender.Send(
                    request: request,
                    cancellationToken: cancellationToken
                );
            return Ok(getNewsPortalsQueryResponse);
        }
    }
}
