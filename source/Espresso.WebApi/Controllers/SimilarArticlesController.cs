using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.DataTransferObjects;
using Espresso.Common.Constants;
using Espresso.WebApi.Application;
using Espresso.WebApi.Authentication;
using Espresso.WebApi.Configuration;
using Espresso.WebApi.DataTransferObjects;
using Espresso.WebApi.Infrastructure;
using Espresso.WebApi.RequestData.Header;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Espresso.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class SimilarArticlesController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="webApiConfiguration"></param>
        /// <returns></returns>
        public SimilarArticlesController(
            ISender sender,
            IWebApiConfiguration webApiConfiguration
        ) : base(
            sender,
            webApiConfiguration
        )
        {
        }

        /// <summary>
        /// Update Similar Articles
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <param name="similarArticlesBody"></param>
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
        [HttpPut]
        [Authorize(Roles = ApiKey.ParserRole)]
        [Route("api/similar-articles")]
        public async Task<IActionResult> UpdateSimilarArticles(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            [FromBody] SimilarArticlesBodyDto similarArticlesBody,
            CancellationToken cancellationToken
        )
        {
            await Sender.Send(
                request: new UpdateInMemorySimilarArticlesCommand
                {
                    SimilarArticles = similarArticlesBody.SimilarArticles,
                    CurrentApiVersion = WebApiConfiguration.AppConfiguration.Version,
                    TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                    ConsumerVersion = basicInformationsHeaderParameters.Version,
                    DeviceType = basicInformationsHeaderParameters.DeviceType,
                    AppEnvironment = WebApiConfiguration.AppConfiguration.AppEnvironment
                },
                cancellationToken: cancellationToken
            );

            return Ok();
        }
    }
}