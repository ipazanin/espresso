// CreateApplicationDownloadCommanHandler.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Application.Models;
using Espresso.Domain.Entities;
using Espresso.Persistence.Database;
using MediatR;

namespace Espresso.WebApi.Application.ApplicationDownloads.Commands.CreateApplicationDownload
{
    public class CreateApplicationDownloadCommanHandler : IRequestHandler<CreateApplicationDownloadCommand>
    {
        private readonly IEspressoDatabaseContext _context;
        private readonly ApplicationInformation _applicationInformation;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateApplicationDownloadCommanHandler"/> class.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="applicationInformation"></param>
        public CreateApplicationDownloadCommanHandler(
            IEspressoDatabaseContext context,
            ApplicationInformation applicationInformation)
        {
            _context = context;
            _applicationInformation = applicationInformation;
        }

        public async Task<Unit> Handle(CreateApplicationDownloadCommand request, CancellationToken cancellationToken)
        {
            var applicationDownload = new ApplicationDownload(
                webApiVersion: _applicationInformation.Version,
                mobileAppVersion: request.ConsumerVersion,
                downloadedTime: DateTime.UtcNow,
                mobileDeviceType: request.DeviceType);

            _context.ApplicationDownload.Add(applicationDownload);

            await _context
                .SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
