using System.Threading;
using System.Threading.Tasks;
using Espresso.WebApi.Application.Articles.Commands.UpdateInMemoryArticles;
using Espresso.WebApi.Application.Notifications.Commands.SendArticlesNotifications;
using Espresso.WebApi.Application.Notifications.Commands.SendPushNotification;
using Espresso.WebApi.Application.Notifications.Queries.GetPushNotifications;
using Espresso.Common.Constants;
using Espresso.WebApi.Authentication;
using Espresso.WebApi.Configuration;
using Espresso.WebApi.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Espresso.Application.DataTransferObjects;
using Espresso.WebApi.DataTransferObjects;
using Espresso.WebApi.RequestData.Header;
using Espresso.WebApi.RequestData.Body;
using Espresso.Application.DataTransferObjects.ArticleDataTransferObjects;
using System;
using Espresso.Application.Services;

namespace Espresso.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class NotificationsController : ApiController
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="webApiConfiguration"></param>
        public NotificationsController(
            ISender sender,
            IWebApiConfiguration webApiConfiguration
        ) : base(sender, webApiConfiguration)
        {
        }

        /// <summary>
        /// Sends notification to mobile app about new articles
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <param name="articlesRequest"></param>
        /// <returns>Response object containing app configuration</returns>
        /// <response code="200">Response object containing app configuration</response>
        /// <response code="400">If validation fails</response>
        /// <response code="401">If API Key is invalid or missing</response>
        /// <response code="403">If API Key is forbiden from requested resource</response>
        /// <response code="500">If unhandled exception occurred</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
        [ApiVersion("2.1")]
        [HttpPost]
        [Authorize(Roles = ApiKey.ParserRole)]
        [Route("api/notifications/articles")]
        public async Task<IActionResult> SendLatestArticlesNotificition(
            [FromBody] ArticlesBodyDto articlesRequest,
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            CancellationToken cancellationToken
        )
        {
            await Sender.Send(
                request: new UpdateInMemoryArticlesCommand
                {
                    CreatedArticles = articlesRequest.CreatedArticles,
                    UpdatedArticles = articlesRequest.UpdatedArticles,
                    MaxAgeOfArticle = WebApiConfiguration.DateTimeConfiguration.MaxAgeOfArticle,
                    TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                    ConsumerVersion = basicInformationsHeaderParameters.Version,
                    DeviceType = basicInformationsHeaderParameters.DeviceType,
                },
                cancellationToken: cancellationToken
            );

            await Sender.Send(
                request: new SendArticlesNotificationsCommand
                {
                    CreatedArticles = articlesRequest.CreatedArticles,
                    UpdatedArticles = articlesRequest.UpdatedArticles,
                    TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                    ConsumerVersion = basicInformationsHeaderParameters.Version,
                    DeviceType = basicInformationsHeaderParameters.DeviceType,
                },
                cancellationToken: cancellationToken
            );

            return Ok();
        }


        /// <summary>
        /// Sends Push notification to mobile apps
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <param name="sendPushNotificationRequestBody"></param>
        /// <returns>OK</returns>
        /// <response code="200">Response object containing app configuration</response>
        /// <response code="400">If Request body is invalid</response>
        /// <response code="401">If API Key is invalid or missing</response>
        /// <response code="403">If API Key is forbiden from requested resource</response>
        /// <response code="500">If unhandled exception occurred</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
        [ApiVersion("2.1")]
        [ApiVersion("2.0")]
        [HttpPost]
        [Authorize(Roles = ApiKey.DevMobileAppRole)]
        [Route("api/notifications")]
        public async Task<IActionResult> SendPushNotificition(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            [FromBody] SendPushNotificationRequestBody sendPushNotificationRequestBody,
            CancellationToken cancellationToken
        )
        {
            await Sender.Send(
                request: new SendPushNotificationCommand
                {
                    ArticleId = sendPushNotificationRequestBody.ArticleId,
                    InternalName = sendPushNotificationRequestBody.InternalName ?? "",
                    Title = sendPushNotificationRequestBody.Title ?? "",
                    Message = sendPushNotificationRequestBody.Message ?? "",
                    Topic = sendPushNotificationRequestBody.Topic ?? "",
                    ArticleUrl = sendPushNotificationRequestBody.ArticleUrl ?? "",
                    IsSoundEnabled = sendPushNotificationRequestBody.IsSoundEnabled,
                    TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                    ConsumerVersion = basicInformationsHeaderParameters.Version,
                    DeviceType = basicInformationsHeaderParameters.DeviceType,
                },
                cancellationToken: cancellationToken
            );

            return Ok();
        }

        /// <summary>
        /// Get Push notifications
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns>Response object containing push notifications</returns>
        /// <response code="200">Response object containing app configuration</response>
        /// <response code="400">If Request body is invalid</response>
        /// <response code="401">If API Key is invalid or missing</response>
        /// <response code="403">If API Key is forbiden from requested resource</response>
        /// <response code="500">If unhandled exception occurred</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
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
            CancellationToken cancellationToken
        )
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
                cancellationToken: cancellationToken
            );
            return Ok(response);
        }
    }
}
