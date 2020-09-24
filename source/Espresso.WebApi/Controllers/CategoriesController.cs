using System.Threading;
using System.Threading.Tasks;
using Espresso.WebApi.Application.Categories.Queries.GetCategories;
using Espresso.Common.Constants;
using Espresso.WebApi.Authentication;
using Espresso.WebApi.Configuration;
using Espresso.WebApi.Infrastructure;
using Espresso.WebApi.Parameters.HeaderParameters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Espresso.WebApi.DataTransferObjects;

namespace Espresso.WebApi.Controllers
{
    /// <summary>
    /// Categories Controller
    /// </summary>
    public class CategoriesController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="webApiConfiguration"></param>
        public CategoriesController(
            IMediator mediator,
            IWebApiConfiguration webApiConfiguration)
         : base(
             mediator,
            webApiConfiguration
        )
        {
        }


        /// <summary>
        /// Get all Espresso categories
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Get /api/categories
        /// </remarks>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Response object containing Espresso categories</returns>
        /// <response code="200">Response object containing Espresso categories</response>
        /// <response code="400">If validation fails</response>
        /// <response code="401">If API Key is invalid or missing</response>
        /// <response code="403">If API Key is forbiden from requested resource</response>
        /// <response code="500">If unhandled exception occurred</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCategoriesQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
        [ApiVersion("1.3")]
        [ApiVersion("1.2")]
        [HttpGet]
        [Authorize(Roles = ApiKey.DevMobileAppRole + "," + ApiKey.MobileAppRole)]
        [Route("api/categories")]
        public async Task<IActionResult> GetCategories(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            CancellationToken cancellationToken
        )
        {
            var categories = await Mediator.Send(
                request: new GetCategoriesQuery(
                    currentEspressoWebApiVersion: WebApiConfiguration.AppVersionConfiguration.Version,
                    targetedEspressoWebApiVersion: basicInformationsHeaderParameters.EspressoWebApiVersion,
                    consumerVersion: basicInformationsHeaderParameters.Version,
                    deviceType: basicInformationsHeaderParameters.DeviceType,
                    appEnvironment: WebApiConfiguration.AppConfiguration.AppEnvironment
                ),
                cancellationToken: cancellationToken
            );

            return Ok(categories);
        }
    }
}
