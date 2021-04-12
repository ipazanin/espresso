using Espresso.Common.Enums;
using MediatR;

namespace Espresso.Application.Infrastructure.MediatorInfrastructure
{
    public abstract record Request<TResponse> : IRequest<TResponse>
    {
        public string TargetedApiVersion { get; init; } = "";

        public string ConsumerVersion { get; init; } = "";

        public DeviceType DeviceType { get; init; }
    }
}
