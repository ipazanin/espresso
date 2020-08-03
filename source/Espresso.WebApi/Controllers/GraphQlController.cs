using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.GraphQl.Infrastructure;
using Espresso.WebApi.Configuration;
using Espresso.WebApi.Infrastructure;
using Espresso.WebApi.Parameters.BodyParameters;
using Espresso.WebApi.Parameters.HeaderParameters;
using FluentValidation;
using GraphQL;
using GraphQL.Types;
using MediatR;
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
        [HttpPost]
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
                executionOptions.Inputs = query.Variables?.ToInputs();
                executionOptions.CancellationToken = cancellationToken;
                executionOptions.UserContext = new GraphQlApplicationContext(
                    currentEspressoWebApiVersion: WebApiConfiguration.Version,
                    targetedEspressoWebApiVersion: basicInformationsHeaderParameters.EspressoWebApiVersion,
                    consumerVersion: basicInformationsHeaderParameters.Version,
                    deviceType: basicInformationsHeaderParameters.DeviceType
                );
            });

            if (result.Errors?.Count > 0)
            {
                throw new ValidationException(
                    message: string.Join(
                        separator: ",",
                        values: result
                            .Errors
                            .Select(executionError => executionError.Message)
                        )
                    );
            }
            return Ok(result.Data);
        }
    }
}
