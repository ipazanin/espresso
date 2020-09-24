using System.Threading;
using System.Threading.Tasks;
using Espresso.WebApi.Application.Articles.Commands.CalculateTrendingScore;
using Espresso.WebApi.Application.Articles.Commands.UpdateInMemoryArticles;
using Espresso.WebApi.Application.Notifications.Commands.SendArticlesNotifications;
using Espresso.WebApi.Application.Notifications.Commands.SendPushNotification;
using Espresso.WebApi.Application.Notifications.Queries.GetPushNotifications;
using Espresso.Common.Constants;
using Espresso.WebApi.Authentication;
using Espresso.WebApi.Configuration;
using Espresso.WebApi.Infrastructure;
using Espresso.WebApi.Parameters.HeaderParameters;
using Espresso.WebApi.RequestObject;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Espresso.Application.DataTransferObjects;
using Espresso.WebApi.DataTransferObjects;

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
        /// <param name="mediator"></param>
        /// <param name="webApiConfiguration"></param>
        public NotificationsController(
            IMediator mediator,
            IWebApiConfiguration webApiConfiguration
        ) : base(
            mediator, webApiConfiguration
        )
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
        [ApiVersion("1.4")]
        [HttpPost]
        [Authorize(Roles = ApiKey.ParserRole)]
        [Route("api/notifications/articles")]
        public async Task<IActionResult> SendLatestArticlesNotificition(
            [FromBody] ArticlesBodyDto articlesRequest,
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            CancellationToken cancellationToken
        )
        {
            await Mediator.Send(
                request: new UpdateInMemoryArticlesCommand(
                    articlesRequest.CreatedArticles,
                    articlesRequest.UpdatedArticles,
                    maxAgeOfArticle: WebApiConfiguration.DateTimeConfiguration.MaxAgeOfArticle,
                    currentEspressoWebApiVersion: WebApiConfiguration.AppVersionConfiguration.Version,
                    targetedEspressoWebApiVersion: basicInformationsHeaderParameters.EspressoWebApiVersion,
                    consumerVersion: basicInformationsHeaderParameters.Version,
                    deviceType: basicInformationsHeaderParameters.DeviceType,
                    appEnvironment: WebApiConfiguration.AppConfiguration.AppEnvironment
                ),
                cancellationToken: cancellationToken
            );

            await Mediator.Send(
                request: new CalculateTrendingScoreCommand(
                    currentEspressoWebApiVersion: WebApiConfiguration.AppVersionConfiguration.Version,
                    targetedEspressoWebApiVersion: basicInformationsHeaderParameters.EspressoWebApiVersion,
                    consumerVersion: basicInformationsHeaderParameters.Version,
                    deviceType: basicInformationsHeaderParameters.DeviceType,
                    appEnvironment: WebApiConfiguration.AppConfiguration.AppEnvironment
                ),
                cancellationToken: cancellationToken
            );

            await Mediator.Send(
                request: new SendArticlesNotificationsCommand(
                    articlesRequest.CreatedArticles,
                    articlesRequest.UpdatedArticles,
                    currentEspressoWebApiVersion: WebApiConfiguration.AppVersionConfiguration.Version,
                    targetedEspressoWebApiVersion: basicInformationsHeaderParameters.EspressoWebApiVersion,
                    consumerVersion: basicInformationsHeaderParameters.Version,
                    deviceType: basicInformationsHeaderParameters.DeviceType,
                    appEnvironment: WebApiConfiguration.AppConfiguration.AppEnvironment
                ),
                cancellationToken: cancellationToken
            );

            return Ok();
        }


        /// <summary>
        /// Sends Push notification to mobile apps
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <param name="sendPushNotificationRequestObject"></param>
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
        [ApiVersion("1.4")]
        [HttpPost]
        [Authorize(Roles = ApiKey.DevMobileAppRole)]
        [Route("api/notifications")]
        public async Task<IActionResult> SendPushNotificition(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            [FromBody] SendPushNotificationRequestObject sendPushNotificationRequestObject,
            CancellationToken cancellationToken
        )
        {
            await Mediator.Send(
                request: new SendPushNotificationCommand(
                    internalName: sendPushNotificationRequestObject.InternalName,
                    title: sendPushNotificationRequestObject.Title,
                    message: sendPushNotificationRequestObject.Message,
                    topic: sendPushNotificationRequestObject.Topic,
                    articleUrl: sendPushNotificationRequestObject.ArticleUrl,
                    isSoundEnabled: sendPushNotificationRequestObject.IsSoundEnabled,
                    currentEspressoWebApiVersion: WebApiConfiguration.AppVersionConfiguration.Version,
                    targetedEspressoWebApiVersion: basicInformationsHeaderParameters.EspressoWebApiVersion,
                    consumerVersion: basicInformationsHeaderParameters.Version,
                    deviceType: basicInformationsHeaderParameters.DeviceType,
                    appEnvironment: WebApiConfiguration.AppConfiguration.AppEnvironment
                ),
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
            var response = await Mediator.Send(
                request: new GetPushNotificationsQuery(
                    take: take,
                    skip: skip,
                    currentEspressoWebApiVersion: WebApiConfiguration.AppVersionConfiguration.Version,
                    targetedEspressoWebApiVersion: basicInformationsHeaderParameters.EspressoWebApiVersion,
                    consumerVersion: basicInformationsHeaderParameters.Version,
                    deviceType: basicInformationsHeaderParameters.DeviceType,
                    appEnvironment: WebApiConfiguration.AppConfiguration.AppEnvironment
                ),
                cancellationToken: cancellationToken
            );
            return Ok(response);
        }
    }
}
