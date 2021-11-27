// ApplicationDownloadsController.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.WebApi.Application.ApplicationDownloads.Commands.CreateApplicationDownload;
using Espresso.WebApi.Application.ApplicationDownloads.Queries.GetApplicationDownloadStatistics;
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

namespace Espresso.WebApi.Controllers;

/// <summary>
///
/// </summary>
public class ApplicationDownloadsController : ApiController
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDownloadsController"/> class.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="webApiConfiguration"></param>
    public ApplicationDownloadsController(
        ISender sender,
        IWebApiConfiguration webApiConfiguration)
        : base(sender, webApiConfiguration)
    {
    }

    /// <summary>
    /// Creates New Application Download.
    /// </summary>
    /// <param name="basicInformationsHeaderParameters"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Response object containing articles from provided category.</response>
    /// <response code="400">If validation failed.</response>
    /// <response code="401">If API Key is invalid or missing.</response>
    /// <response code="403">If API Key is forbiden from requested resource.</response>
    /// <response code="500">If unhandled exception occurred.</response>
    [Produces(MimeTypeConstants.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionDto))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ExceptionDto))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
    [ApiVersion("2.2")]
    [ApiVersion("2.1")]
    [ApiVersion("2.0")]
    [ApiVersion("1.4")]
    [HttpPost]
    [Authorize(Roles = ApiKey.DevMobileAppRole + "," + ApiKey.MobileAppRole + "," + ApiKey.WebAppRole)]
    [Route("api/application-downloads")]
    public async Task<IActionResult> CreateApplicationDownload(
        [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
        CancellationToken cancellationToken)
    {
        var command = new CreateApplicationDownloadCommand
        {
            TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
            ConsumerVersion = basicInformationsHeaderParameters.Version,
            DeviceType = basicInformationsHeaderParameters.DeviceType,
        };

        await Sender.Send(
                request: command,
                cancellationToken: cancellationToken);

        return Ok();
    }

    /// <summary>
    /// Creates New Application Download.
    /// </summary>
    /// <param name="basicInformationsHeaderParameters"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Response object containing articles from provided category.</response>
    /// <response code="400">If validation failed.</response>
    /// <response code="401">If API Key is invalid or missing.</response>
    /// <response code="403">If API Key is forbiden from requested resource.</response>
    /// <response code="500">If unhandled exception occurred.</response>
    [Produces(MimeTypeConstants.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionDto))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ExceptionDto))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
    [ApiVersion("1.3")]
    [HttpPost]
    [Authorize(Roles = ApiKey.DevMobileAppRole + "," + ApiKey.MobileAppRole + "," + ApiKey.WebAppRole)]
    [Route("api/applicationDownloads/create")]
#pragma warning disable S4144 // Methods should not have identical implementations
    public async Task<IActionResult> CreateApplicationDownload_1_3(
        [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
        CancellationToken cancellationToken)
#pragma warning restore S4144 // Methods should not have identical implementations
    {
        var command = new CreateApplicationDownloadCommand
        {
            TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
            ConsumerVersion = basicInformationsHeaderParameters.Version,
            DeviceType = basicInformationsHeaderParameters.DeviceType,
        };

        await Sender.Send(
            request: command,
            cancellationToken: cancellationToken);

        return Ok();
    }

    /// <summary>
    /// Creates New Application Download.
    /// </summary>
    /// <param name="mobileAppVersion">Mobile App Version.</param>
    /// <param name="mobileDeviceType">Mobile Device Type (1 = Android, 2 = Ios).</param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Response object containing articles from provided category.</response>
    /// <response code="400">If <paramref name="mobileAppVersion"/> is empty or longer than 20 characters or deviceType is not 1 or 2.</response>
    /// <response code="401">If API Key is invalid or missing.</response>
    /// <response code="403">If API Key is forbiden from requested resource.</response>
    /// <response code="500">If unhandled exception occurred.</response>
    [Produces(MimeTypeConstants.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionDto))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ExceptionDto))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
    [ApiVersion("1.2")]
    [HttpPost]
    [Authorize(Roles = ApiKey.DevMobileAppRole + "," + ApiKey.MobileAppRole + "," + ApiKey.WebAppRole)]
    [Route("api/applicationDownloads/create")]
    public async Task<IActionResult> CreateApplicationDownload_1_2(
        [Required] string mobileAppVersion,
        [Required] DeviceType mobileDeviceType,
        CancellationToken cancellationToken)
    {
        var command = new CreateApplicationDownloadCommand
        {
            TargetedApiVersion = "1.2",
            ConsumerVersion = mobileAppVersion,
            DeviceType = mobileDeviceType,
        };
        await Sender.Send(
            request: command,
            cancellationToken: cancellationToken);

        return Ok();
    }

    /// <summary>
    /// Gets App Downloads Statistics.
    /// </summary>
    /// <param name="basicInformationsHeaderParameters"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Response object containing articles from provided category.</returns>
    /// <response code="200">Response object containing articles from popular news portals.</response>
    /// <response code="400">If validation failed.</response>
    /// <response code="401">If API Key is invalid or missing.</response>
    /// <response code="403">If API Key is forbiden from requested resource.</response>
    /// <response code="500">If unhandled exception occurred.</response>
    [Produces(MimeTypeConstants.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetApplicationDownloadStatisticsQueryResponse))]
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
    [Route("api/application-downloads")]
    public async Task<IActionResult> GetApplicationDownloadsStatistics(
        [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
        CancellationToken cancellationToken)
    {
        var response = await Sender.Send(
            request: new GetApplicationDownloadStatisticsQuery
            {
                TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                ConsumerVersion = basicInformationsHeaderParameters.Version,
                DeviceType = basicInformationsHeaderParameters.DeviceType,
            },
            cancellationToken: cancellationToken);

        return Ok(response);
    }
}
