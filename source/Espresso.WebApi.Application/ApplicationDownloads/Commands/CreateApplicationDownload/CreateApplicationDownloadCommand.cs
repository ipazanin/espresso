﻿using Espresso.Application.Infrastructure.MediatorInfrastructure;
using MediatR;

namespace Espresso.WebApi.Application.ApplicationDownloads.Commands.CreateApplicationDownload
{
    public record CreateApplicationDownloadCommand : Request<Unit>
    {
    }
}