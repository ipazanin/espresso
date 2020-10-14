using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.WebApi.Authentication;
using Espresso.WebApi.Configuration;
using Espresso.WebApi.DataTransferObjects;
using Espresso.WebApi.GraphQl.Infrastructure;
using Espresso.WebApi.Infrastructure;
using Espresso.WebApi.Parameters.BodyParameters;
using Espresso.WebApi.Parameters.HeaderParameters;
using FluentValidation;
using GraphQL;
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
        /// <returns></returns>
        public GraphQlController(
            ISender sender,
            IWebApiConfiguration webApiConfiguration,
            ISchema schema,
            IDocumentExecuter executer
        ) : base(sender, webApiConfiguration)
        {
            _schema = schema;
            _executer = executer;
        }

        /// <summary>
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="basicInformationsHeaderParameters">Basic App Informations</param>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <response code="200">When operation is sucessfull</response>
        /// <response code="400">If validation fails</response>
        /// <response code="401">If API Key is invalid or missing</response>
        /// <response code="403">If API Key is forbiden from requested resource</response>
        /// <response code="404">If resource is nout found</response>
        /// <response code="500">If unhandled exception occurred</response>
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

                if (
                    WebApiConfiguration
                        .AppConfiguration
                        .AppEnvironment
                        .Equals(AppEnvironment.Local)
                )
                {
                    executionOptions.ExposeExceptions = true;
                    executionOptions.EnableMetrics = true;
                }

                executionOptions.UserContext = new GraphQlUserContext
                {
                    TargetedApiVersion = basicInformationsHeaderParameters.EspressoWebApiVersion,
                    ConsumerVersion = basicInformationsHeaderParameters.Version,
                    DeviceType = basicInformationsHeaderParameters.DeviceType,
                };
            });

            if (result.Errors?.Count > 0)
            {
                var values = new List<string>(result.Errors.Select(executionError => executionError.Message));
                values.AddRange(result.Errors.Select(executionError => executionError.InnerException?.Message ?? ""));
                throw new ValidationException(
                    message: string.Join(
                        separator: ",",
                        values: values
                    )
                );
            }

            return Ok(new { data = result.Data });
        }
    }
}
