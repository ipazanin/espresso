using System;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Domain.Entities;
using Espresso.Persistence.Database;
using MediatR;

namespace Espresso.Application.CQRS.ApplicationDownloads.Commands.CreateApplicationDownload
{
    public class CreateApplicationDownloadCommanHandler : IRequestHandler<CreateApplicationDownloadCommand>
    {
        #region Fields
        private readonly IApplicationDatabaseContext _context;
        #endregion

        #region Constructors
        public CreateApplicationDownloadCommanHandler(
            IApplicationDatabaseContext context
        )
        {
            _context = context;
        }
        #endregion

        #region Methods
        public async Task<Unit> Handle(CreateApplicationDownloadCommand request, CancellationToken cancellationToken)
        {
            var applicationDownload = new ApplicationDownload(
                webApiVersion: request.CurrentApiVersion,
                mobileAppVersion: request.ConsumerVersion,
                downloadedTime: DateTime.UtcNow,
                mobileDeviceType: request.DeviceType
            );

            _ = _context.ApplicationDownload.Add(applicationDownload);

            _ = await _context
                .SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        #endregion
    }
}
