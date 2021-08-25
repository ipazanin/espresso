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
        /// <value></value>
        public string? OperationName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string? NamedQuery { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string? Query { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [JsonConverter(typeof(ObjectDictionaryConverter))]
        public Dictionary<string, object>? Variables { get; set; }
    }
}
