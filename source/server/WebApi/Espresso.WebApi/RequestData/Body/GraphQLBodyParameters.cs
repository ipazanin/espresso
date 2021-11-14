// GraphQLBodyParameters.cs
//
// © 2021 Espresso News. All rights reserved.

using GraphQL.SystemTextJson;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Espresso.WebApi.RequestData.Body
{
    /// <summary>
    ///
    /// </summary>
    public class GraphQLBodyParameters
    {
        /// <summary>
        ///
        /// </summary>
        public string? OperationName { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string? NamedQuery { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string? Query { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonConverter(typeof(InputsConverter))]
        public Dictionary<string, object>? Variables { get; set; }
    }
}
