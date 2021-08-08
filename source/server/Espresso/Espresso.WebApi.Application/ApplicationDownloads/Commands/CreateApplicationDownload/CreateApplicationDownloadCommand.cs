// CreateApplicationDownloadCommand.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Application.Infrastructure.MediatorInfrastructure;
using MediatR;

namespace Espresso.WebApi.Application.ApplicationDownloads.Commands.CreateApplicationDownload
{
    public record CreateApplicationDownloadCommand : Request<Unit>
    {
    }
}
