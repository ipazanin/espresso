using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.WebApi.Authentication;
using Espresso.WebApi.Configuration;
using Espresso.WebApi.DataTransferObjects;
using Espresso.WebApi.GraphQl.Infrastructure;
using Espresso.WebApi.Infrastructure;
using Espresso.WebApi.RequestData.Body;
using Espresso.WebApi.RequestData.Header;
using GraphQL;
using GraphQL.Execution;
using GraphQL.Types;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Espresso.WebApi.Controllers
{
    /// <summary>
    ///
    /// </summary>
    public class GraphQlController : ApiController
    {
        private readonly ISchema _schema;
        private readonly IDocumentExecuter _executer;

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="webApiConfiguration"></param>
        /// <param name="schema"></param>
        /// <param name="executer"></param>
        public GraphQlController(
            ISender sender,
            IWebApiConfiguration webApiConfiguration,
            ISchema schema,
            IDocumentExecuter executer
        )
            : base(sender, webApiConfiguration)
        {
            _schema = schema;
            _executer = executer;
        }

        /// <summary>
        /// </summary>
        /// <param name="basicInformationsHeaderParameters">Basic App Information.</param>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="200">When operation is successfull.</response>
        /// <response code="400">If validation fails.</response>
        /// <response code="401">If API Key is invalid or missing.</response>
        /// <response code="403">If API Key is forbiden from requested resource.</response>
        /// <response code="404">If resource is not found.</response>
        /// <response code="500">If unhandled exception occurred.</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
        [ApiVersion("1.4")]
        [HttpPost]
        [Authorize(Roles = ApiKey.DevMobileAppRole + "," + ApiKey.WebAppRole)]
        [Route("graphql")]
        public async Task<IActionResult> Post(
            [FromHeader] BasicInformationsHeaderParameters basicInformationsHeaderParameters,
            [FromBody] GraphQLBodyParameters query,
            CancellationToken cancellationToken
        )
        {
            var result = await _executer.ExecuteAsync(executionOptions =>
            {
                executionOptions.Schema = _schema;
                executionOptions.Query = query.Query;
                executionOptions.Inputs = query.Variables is null ? null : new Inputs(query.Variables);
                executionOptions.OperationName = query.OperationName;
                executionOptions.CancellationToken = cancellationToken;
                executionOptions.ThrowOnUnhandledException = true;

                if (
                    WebApiConfiguration
                        .AppConfiguration
                        .AppEnvironment
                        .Equals(AppEnvironment.Local)
                )
                {
                    executionOptions.EnableMetrics = true;
                }

                executionOptions.UserContext = new GraphQlUserContext
                {
                    TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                    ConsumerVersion = basicInformationsHeaderParameters.Version,
                    DeviceType = basicInformationsHeaderParameters.DeviceType,
                };
            });

            var data = ((ExecutionNode)result.Data).ToValue();

            return Ok(new { data });
        }
    }
}
