using Espresso.Common.Enums;
using MediatR;

namespace Espresso.Application.Infrastructure.MediatorInfrastructure
{
    public abstract record Request<TResponse> : IRequest<TResponse>
    {
        public string TargetedApiVersion { get; init; } = string.Empty;

        public string ConsumerVersion { get; init; } = string.Empty;

        public DeviceType DeviceType { get; init; }
    }
}
