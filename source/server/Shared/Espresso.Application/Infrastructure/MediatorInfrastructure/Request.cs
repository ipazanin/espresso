// Request.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Enums;
using MediatR;

namespace Espresso.Application.Infrastructure.MediatorInfrastructure
{
    public abstract record Request<TResponse> : IRequest<TResponse>
    {
        /// <summary>
        /// Gets targeted api version.
        /// </summary>
        public string TargetedApiVersion { get; init; } = string.Empty;

        /// <summary>
        /// Gets consumer version.
        /// </summary>
        public string ConsumerVersion { get; init; } = string.Empty;

        /// <summary>
        /// Gets device type.
        /// </summary>
        public DeviceType DeviceType { get; init; }
    }
}
