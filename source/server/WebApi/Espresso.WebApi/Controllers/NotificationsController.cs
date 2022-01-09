// NotificationsController.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.ArticleDataTransferObjects;
using Espresso.Common.Constants;
using Espresso.WebApi.Application.Articles.Commands.UpdateInMemoryArticles;
using Espresso.WebApi.Application.Notifications.Commands.SendArticlesNotifications;
using Espresso.WebApi.Application.Notifications.Commands.SendPushNotification;
using Espresso.WebApi.Application.Notifications.Queries.GetPushNotifications;
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

namespace Espresso.WebApi.Controllers;

/// <summary>
///
/// </summary>
public class NotificationsController : ApiController
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationsController"/> class.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="webApiConfiguration"></param>
    public NotificationsController(
        ISender sender,
        IWebApiConfiguration webApiConfiguration)
        : base(sender, webApiConfiguration)
    {
    }

    /// <summary>
    /// Sends notification to mobile app about new articles.
    /// </summary>
    /// <param name="articlesRequest"></param>
    /// <param name="basicInformationsHeaderParameters"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Response object containing app configuration.</returns>
    /// <response code="200">Response object containing app configuration.</response>
    /// <response code="400">If validation fails.</response>
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
    [HttpPost]
    [Authorize(Roles = ApiKey.ParserRole)]
    [Route("api/notifications/articles")]
    public async Task<IActionResult> SendLatestArticlesNotification(
        [FromBody] ArticlesBodyDto articlesRequest,
        [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
        CancellationToken cancellationToken)
    {
        await Sender.Send(
            request: new UpdateInMemoryArticlesCommand
            {
                CreatedArticleIds = articlesRequest.CreatedArticleIds,
                UpdatedArticleIds = articlesRequest.UpdatedArticleIds,
                TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                ConsumerVersion = basicInformationsHeaderParameters.Version,
                DeviceType = basicInformationsHeaderParameters.DeviceType,
            },
            cancellationToken: cancellationToken);

        await Sender.Send(
            request: new SendArticlesNotificationsCommand
            {
                CreatedArticleIds = articlesRequest.CreatedArticleIds,
                UpdatedArticleIds = articlesRequest.UpdatedArticleIds,
                TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                ConsumerVersion = basicInformationsHeaderParameters.Version,
                DeviceType = basicInformationsHeaderParameters.DeviceType,
            },
            cancellationToken: cancellationToken);

        return Ok();
    }

    /// <summary>
    /// Sends Push notification to mobile apps.
    /// </summary>
    /// <param name="basicInformationsHeaderParameters"></param>
    /// <param name="sendPushNotificationRequestBody"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>OK.</returns>
    /// <response code="200">Response object containing app configuration.</response>
    /// <response code="400">If Request body is invalid.</response>
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
    [HttpPost]
    [Authorize(Roles = ApiKey.DevMobileAppRole)]
    [Route("api/notifications")]
    public async Task<IActionResult> SendPushNotificition(
        [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
        [FromBody] SendPushNotificationRequestBody sendPushNotificationRequestBody,
        CancellationToken cancellationToken)
    {
        await Sender.Send(
            request: new SendPushNotificationCommand
            {
                ArticleId = sendPushNotificationRequestBody.ArticleId,
                InternalName = sendPushNotificationRequestBody.InternalName ?? string.Empty,
                Title = sendPushNotificationRequestBody.Title ?? string.Empty,
                Message = sendPushNotificationRequestBody.Message ?? string.Empty,
                Topic = sendPushNotificationRequestBody.Topic ?? string.Empty,
                ArticleUrl = sendPushNotificationRequestBody.ArticleUrl ?? string.Empty,
                IsSoundEnabled = sendPushNotificationRequestBody.IsSoundEnabled,
                TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                ConsumerVersion = basicInformationsHeaderParameters.Version,
                DeviceType = basicInformationsHeaderParameters.DeviceType,
            },
            cancellationToken: cancellationToken);

        return Ok();
    }

    /// <summary>
    /// Get Push notifications.
    /// </summary>
    /// <param name="basicInformationsHeaderParameters"></param>
    /// <param name="take"></param>
    /// <param name="skip"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Response object containing push notifications.</returns>
    /// <response code="200">Response object containing app configuration.</response>
    /// <response code="400">If Request body is invalid.</response>
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
    [HttpGet]
    [Authorize(Roles = ApiKey.DevMobileAppRole)]
    [Route("api/notifications")]
    public async Task<IActionResult> GetPushNotificition(
        [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
        [FromQuery] int take,
        [FromQuery] int skip,
        CancellationToken cancellationToken)
    {
        var response = await Sender.Send(
            request: new GetPushNotificationsQuery
            {
                Take = take,
                Skip = skip,
                TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                ConsumerVersion = basicInformationsHeaderParameters.Version,
                DeviceType = basicInformationsHeaderParameters.DeviceType,
            },
            cancellationToken: cancellationToken);
        return Ok(response);
    }
}
