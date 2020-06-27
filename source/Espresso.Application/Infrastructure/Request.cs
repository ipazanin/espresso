using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using MediatR;

namespace Espresso.Application.Infrastructure
{
    public abstract class Request<TResponse> : IRequest<TResponse>
    {
        #region Properties
        public string CurrentEspressoWebApiVersion { get; }

        public string EspressoWebApiVersion { get; }

        public string Version { get; }

        public DeviceType DeviceType { get; }

        public Event EventIdEnum { get; }
        #endregion

        #region Constructors
        protected Request(
            string currentEspressoWebApiVersion,
            string espressoWebApiVersion,
            string version,
            DeviceType deviceType,
            Event eventIdEnum
        )
        {
            CurrentEspressoWebApiVersion = currentEspressoWebApiVersion;
            EspressoWebApiVersion = espressoWebApiVersion;
            Version = version;
            DeviceType = deviceType;
            EventIdEnum = eventIdEnum;
        }
        #endregion
    }
}
