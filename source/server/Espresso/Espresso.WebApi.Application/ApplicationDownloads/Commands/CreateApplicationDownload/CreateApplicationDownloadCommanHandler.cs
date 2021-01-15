using System;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.Models;
using Espresso.Domain.Entities;
using Espresso.Persistence.Database;
using MediatR;

namespace Espresso.WebApi.Application.ApplicationDownloads.Commands.CreateApplicationDownload
{
    public class CreateApplicationDownloadCommanHandler : IRequestHandler<CreateApplicationDownloadCommand>
    {
        #region Fields
        private readonly IApplicationDatabaseContext _context;
        private readonly ApplicationInformation _applicationInformation;
        #endregion

        #region Constructors
        public CreateApplicationDownloadCommanHandler(
            IApplicationDatabaseContext context,
            ApplicationInformation applicationInformation
        )
        {
            _context = context;
            _applicationInformation = applicationInformation;
        }
        #endregion

        #region Methods
        public async Task<Unit> Handle(CreateApplicationDownloadCommand request, CancellationToken cancellationToken)
        {
            var applicationDownload = new ApplicationDownload(
                webApiVersion: _applicationInformation.Version,
                mobileAppVersion: request.ConsumerVersion,
                downloadedTime: DateTime.UtcNow,
                mobileDeviceType: request.DeviceType
            );

            _context.ApplicationDownload.Add(applicationDownload);

            await _context
                .SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        #endregion
    }
}
