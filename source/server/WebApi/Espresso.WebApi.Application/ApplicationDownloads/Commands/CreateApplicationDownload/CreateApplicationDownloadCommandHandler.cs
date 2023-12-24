// CreateApplicationDownloadCommandHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.Models;
using Espresso.Domain.Entities;
using Espresso.Persistence.Database;
using MediatR;

namespace Espresso.WebApi.Application.ApplicationDownloads.Commands.CreateApplicationDownload;

public class CreateApplicationDownloadCommandHandler : IRequestHandler<CreateApplicationDownloadCommand>
{
    private readonly IEspressoDatabaseContext _context;
    private readonly ApplicationInformation _applicationInformation;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateApplicationDownloadCommandHandler"/> class.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="applicationInformation"></param>
    public CreateApplicationDownloadCommandHandler(
        IEspressoDatabaseContext context,
        ApplicationInformation applicationInformation)
    {
        _context = context;
        _applicationInformation = applicationInformation;
    }

    public async Task Handle(CreateApplicationDownloadCommand request, CancellationToken cancellationToken)
    {
        var applicationDownload = new ApplicationDownload(
            webApiVersion: _applicationInformation.Version,
            mobileAppVersion: request.ConsumerVersion,
            downloadedTime: DateTimeOffset.UtcNow,
            mobileDeviceType: request.DeviceType);

        _ = _context.ApplicationDownload.Add(applicationDownload);

        _ = await _context
            .SaveChangesAsync(cancellationToken);
    }
}
