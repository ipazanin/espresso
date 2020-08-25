using Newtonsoft.Json.Linq;

namespace Espresso.WebApi.Parameters.BodyParameters
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

        // public string? NamedQuery { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string? Query { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public JObject? Variables { get; set; }
    }
}
