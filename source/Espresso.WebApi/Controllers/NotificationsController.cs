using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.CQRS.Articles.Commands.CalculateTrendingScore;
using Espresso.Application.CQRS.Articles.Commands.UpdateInMemoryArticles;
using Espresso.Application.CQRS.Notifications.Commands.SendArticlesNotifications;
using Espresso.Application.CQRS.Notifications.Commands.SendPushNotification;
using Espresso.Application.CQRS.Notifications.Queries.GetPushNotifications;
using Espresso.Application.DataTransferObjects;
using Espresso.Common.Constants;
using Espresso.WebApi.Configuration;
using Espresso.WebApi.Infrastructure;
using Espresso.WebApi.Parameters.HeaderParameters;
using Espresso.WebApi.RequestObject;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public NotificationsController(IMediator mediator, IWebApiConfiguration webApiConfiguration) : base(mediator, webApiConfiguration)
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
        /// <response code="401">If API Key is invalid or missing</response>
        /// <response code="500">If unhandled exception occurred</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        [Route("api/notifications")]
        public async Task<IActionResult> SendLatestArticlesNotificition(
            [FromBody] ArticlesRequestObjectDto articlesRequest,
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            CancellationToken cancellationToken
        )
        {
            await Mediator.Send(
                request: new UpdateInMemoryArticlesCommand(
                    articlesRequest.CreatedArticles,
                    articlesRequest.UpdatedArticles,
                    currentEspressoWebApiVersion: WebApiConfiguration.Version,
                    targetedEspressoWebApiVersion: basicInformationsHeaderParameters.EspressoWebApiVersion,
                    consumerVersion: basicInformationsHeaderParameters.Version,
                    deviceType: basicInformationsHeaderParameters.DeviceType
                ),
                cancellationToken: cancellationToken
            ).ConfigureAwait(false);

            await Mediator.Send(
                request: new CalculateTrendingScoreCommand(
                    currentEspressoWebApiVersion: WebApiConfiguration.Version,
                    targetedEspressoWebApiVersion: basicInformationsHeaderParameters.EspressoWebApiVersion,
                    consumerVersion: basicInformationsHeaderParameters.Version,
                    deviceType: basicInformationsHeaderParameters.DeviceType
                ),
                cancellationToken: cancellationToken
            ).ConfigureAwait(false);

            await Mediator.Send(
                request: new SendArticlesNotificationsCommand(
                    articlesRequest.CreatedArticles,
                    articlesRequest.UpdatedArticles,
                    currentEspressoWebApiVersion: WebApiConfiguration.Version,
                    targetedEspressoWebApiVersion: basicInformationsHeaderParameters.EspressoWebApiVersion,
                    consumerVersion: basicInformationsHeaderParameters.Version,
                    deviceType: basicInformationsHeaderParameters.DeviceType
                ),
                cancellationToken: cancellationToken
            ).ConfigureAwait(false);

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
        /// <response code="500">If unhandled exception occurred</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("api/notifications/push")]
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
                    currentEspressoWebApiVersion: WebApiConfiguration.Version,
                    targetedEspressoWebApiVersion: basicInformationsHeaderParameters.EspressoWebApiVersion,
                    consumerVersion: basicInformationsHeaderParameters.Version,
                    deviceType: basicInformationsHeaderParameters.DeviceType
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
        /// <response code="500">If unhandled exception occurred</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("api/notifications/push")]
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
                    currentEspressoWebApiVersion: WebApiConfiguration.Version,
                    targetedEspressoWebApiVersion: basicInformationsHeaderParameters.EspressoWebApiVersion,
                    consumerVersion: basicInformationsHeaderParameters.Version,
                    deviceType: basicInformationsHeaderParameters.DeviceType
                ),
                cancellationToken: cancellationToken
            );
            return Ok(response);
        }
    }
}
