using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using MediatR;

namespace Espresso.Application.Infrastructure
{
    public abstract class Request<TResponse> : IRequest<TResponse>
    {
        #region Properties
        public string CurrentEspressoWebApiVersion { get; }

        public string TargetedEspressoWebApiVersion { get; }

        public string ConsumerVersion { get; }

        public DeviceType DeviceType { get; }

        public Event EventIdEnum { get; }
        #endregion

        #region Constructors
        protected Request(
            string currentEspressoWebApiVersion,
            string targetedEspressoWebApiVersion,
            string consumerVersion,
            DeviceType deviceType,
            Event eventIdEnum
        )
        {
            CurrentEspressoWebApiVersion = currentEspressoWebApiVersion;
            TargetedEspressoWebApiVersion = targetedEspressoWebApiVersion;
            ConsumerVersion = consumerVersion;
            DeviceType = deviceType;
            EventIdEnum = eventIdEnum;
        }
        #endregion
    }
}
