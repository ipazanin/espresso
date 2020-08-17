using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.WebApi.GraphQl.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class GraphQlApplicationContext
    {
        #region Properties
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string CurrentEspressoWebApiVersion { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string TargetedEspressoWebApiVersion { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string ConsumerVersion { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public DeviceType DeviceType { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentEspressoWebApiVersion"></param>
        /// <param name="targetedEspressoWebApiVersion"></param>
        /// <param name="consumerVersion"></param>
        /// <param name="deviceType"></param>
        public GraphQlApplicationContext(
            string currentEspressoWebApiVersion,
            string targetedEspressoWebApiVersion,
            string consumerVersion,
            DeviceType deviceType
        )
        {
            CurrentEspressoWebApiVersion = currentEspressoWebApiVersion;
            TargetedEspressoWebApiVersion = targetedEspressoWebApiVersion;
            ConsumerVersion = consumerVersion;
            DeviceType = deviceType;
        }
        #endregion
    }
}
