using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using MediatR;

namespace Espresso.Application.Infrastructure.MediatorInfrastructure
{
    public abstract record Request<TResponse> : IRequest<TResponse>
    {
        #region Properties
        public string TargetedApiVersion { get; init; } = "";

        public string ConsumerVersion { get; init; } = "";

        public DeviceType DeviceType { get; init; }
        #endregion
    }
}
