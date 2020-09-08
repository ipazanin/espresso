﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Enums;
using Espresso.WebApi.Authentication;
using Espresso.WebApi.Configuration;
using Espresso.WebApi.GraphQl.Infrastructure;
using Espresso.WebApi.Infrastructure;
using Espresso.WebApi.Parameters.BodyParameters;
using Espresso.WebApi.Parameters.HeaderParameters;
using FluentValidation;
using GraphQL;
using GraphQL.Types;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
            // var graphqlPrinter = new SchemaPrinter(_schema);
            // var r = graphqlPrinter.Print();
            // Console.WriteLine(r);

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
