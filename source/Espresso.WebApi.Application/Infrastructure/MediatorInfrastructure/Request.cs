using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.Domain.Extensions;
using MediatR;

namespace Espresso.WebApi.Application.Infrastructure.MediatorInfrastructure
{
    public abstract class Request<TResponse> : IRequest<TResponse>
    {
        #region Properties
        public string CurrentApiVersion { get; }

        public string TargetedApiVersion { get; }

        public string ConsumerVersion { get; }

        public DeviceType DeviceType { get; }

        public Event EventIdEnum { get; }

        public AppEnvironment AppEnvironment { get; }
        #endregion

        #region Constructors
        protected Request(
            string currentEspressoWebApiVersion,
            string targetedEspressoWebApiVersion,
            string consumerVersion,
            DeviceType deviceType,
            AppEnvironment appEnvironment,
            Event eventIdEnum
        )
        {
            CurrentApiVersion = currentEspressoWebApiVersion;
            TargetedApiVersion = targetedEspressoWebApiVersion;
            ConsumerVersion = consumerVersion;
            DeviceType = deviceType;
            EventIdEnum = eventIdEnum;
            AppEnvironment = appEnvironment;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{nameof(CurrentApiVersion)}:{CurrentApiVersion}, " +
            $"{nameof(TargetedApiVersion)}:{TargetedApiVersion}, " +
            $"{nameof(ConsumerVersion)}:{ConsumerVersion}, " +
            $"{nameof(DeviceType)}:{DeviceType.GetDisplayName()}, " +
            $"{nameof(EventIdEnum)}:{EventIdEnum.GetDisplayName()}, " +
            $"{nameof(AppEnvironment)}:{AppEnvironment.GetDisplayName()}";
        }
        #endregion
    }
}
