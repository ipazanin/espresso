using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.Application.GraphQl.Infrastructure
{
    public class GraphQlApplicationContext
    {
        #region Properties
        public string CurrentEspressoWebApiVersion { get; }

        public string TargetedEspressoWebApiVersion { get; }

        public string ConsumerVersion { get; }

        public DeviceType DeviceType { get; }
        #endregion

        #region Constructors
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
