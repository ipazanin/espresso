using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.CQRS.Articles.Commands.CalculateTrendingScore;
using Espresso.Application.CQRS.Articles.Commands.UpdateInMemoryArticles;
using Espresso.Application.CQRS.Notifications.Queries.SendArticlesNotifications;
using Espresso.Application.DataTransferObjects;
using Espresso.Common.Constants;
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
    public class ArticlesNotificationTrigerController : ApiController
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="webApiConfiguration"></param>
        public ArticlesNotificationTrigerController(IMediator mediator, IWebApiConfiguration webApiConfiguration) : base(mediator, webApiConfiguration)
        {
        }


        /// <summary>
        /// Sends notification to mobile app about new articles
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Get /api/configuration
        /// </remarks>
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
                    currentEspressoWebApiVersion: WebApiConfiguration.EspressoWebApiVersion,
                    espressoWebApiVersion: basicInformationsHeaderParameters.EspressoWebApiVersion,
                    version: basicInformationsHeaderParameters.Version,
                    deviceType: basicInformationsHeaderParameters.DeviceType
                ),
                cancellationToken: cancellationToken
            ).ConfigureAwait(false);

            await Mediator.Send(
                request: new CalculateTrendingScoreCommand(
                    currentEspressoWebApiVersion: WebApiConfiguration.EspressoWebApiVersion,
                    espressoWebApiVersion: basicInformationsHeaderParameters.EspressoWebApiVersion,
                    version: basicInformationsHeaderParameters.Version,
                    deviceType: basicInformationsHeaderParameters.DeviceType
                ),
                cancellationToken: cancellationToken
            ).ConfigureAwait(false);

            await Mediator.Send(
                request: new SendArticlesNotificationsQuery(
                    articlesRequest.CreatedArticles,
                    articlesRequest.UpdatedArticles,
                    currentEspressoWebApiVersion: WebApiConfiguration.EspressoWebApiVersion,
                    espressoWebApiVersion: basicInformationsHeaderParameters.EspressoWebApiVersion,
                    version: basicInformationsHeaderParameters.Version,
                    deviceType: basicInformationsHeaderParameters.DeviceType
                ),
                cancellationToken: cancellationToken
            ).ConfigureAwait(false);

            return Ok();
        }
    }
}
