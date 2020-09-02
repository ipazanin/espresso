using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.WebApi.Configuration;
using Espresso.WebApi.Infrastructure;
using Espresso.WebApi.Parameters.BodyParameters;
using Espresso.WebApi.Parameters.HeaderParameters;
using FluentValidation;
using GraphQL;
using GraphQL.Types;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Espresso.WebApi.Authentication;
using System.Collections.Generic;
using Espresso.Common.Enums;

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
        /// <param name="mediator"></param>
        /// <param name="webApiConfiguration"></param>
        /// <param name="schema"></param>
        /// <param name="executer"></param>
        /// <returns></returns>
        public GraphQlController(
            IMediator mediator,
            IWebApiConfiguration webApiConfiguration,
            ISchema schema,
            IDocumentExecuter executer
        ) : base(mediator, webApiConfiguration)
        {
            _schema = schema;
            _executer = executer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basicInformationsHeaderParameters"></param>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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
                executionOptions.Inputs = new Inputs(query.Variables);
                executionOptions.OperationName = query.OperationName;
                executionOptions.CancellationToken = cancellationToken;

                if (WebApiConfiguration.AppConfiguration.AppEnvironment.Equals(AppEnvironment.Local))
                {
                    executionOptions.ExposeExceptions = true;
                    executionOptions.EnableMetrics = true;
                }

                executionOptions.UserContext = new Dictionary<string, object>
                {
                    { "currentEspressoWebApiVersion", WebApiConfiguration.AppVersionConfiguration.Version },
                    { "targetedEspressoWebApiVersion", basicInformationsHeaderParameters.EspressoWebApiVersion },
                    { "consumerVersion", basicInformationsHeaderParameters.Version },
                    { "deviceType", basicInformationsHeaderParameters.DeviceType },
                };
            });

            return result.Errors?.Count > 0 ?
                throw new ValidationException(
                    message: string.Join(
                        separator: ",",
                        values: result.Errors.Select(executionError => executionError.Message)
                    )
                ) :
                Ok(result.Data);
        }
    }
}
