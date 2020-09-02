using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.Domain.Extensions;
using Espresso.Domain.IServices;
using Espresso.Persistence.Database;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.Application.CQRS.ApplicationDownloads.Commands.CreateApplicationDownload
{
    public class CreateApplicationDownloadCommanHandler : IRequestHandler<CreateApplicationDownloadCommand>
    {
        #region Fields
        private readonly IApplicationDatabaseContext _context;
        private readonly IMemoryCache _memoryCache;
        private readonly ISlackService _slackService;
        #endregion

        #region Constructors
        public CreateApplicationDownloadCommanHandler(
            IApplicationDatabaseContext context,
            IMemoryCache memoryCache,
            ISlackService slackService
        )
        {
            _context = context;
            _memoryCache = memoryCache;
            _slackService = slackService;
        }
        #endregion

        #region Methods
        public async Task<Unit> Handle(CreateApplicationDownloadCommand request, CancellationToken cancellationToken)
        {
            var applicationDownload = new ApplicationDownload(
                webApiVersion: request.CurrentEspressoWebApiVersion,
                mobileAppVersion: request.ConsumerVersion,
                downloadedTime: DateTime.UtcNow,
                mobileDeviceType: request.DeviceType
            );

            _ = _context.ApplicationDownload.Add(applicationDownload);

            _ = await _context
                .SaveChangesAsync(cancellationToken)
                ;

            var applicationDownloads = _memoryCache.Get<IEnumerable<ApplicationDownload>>(
                key: MemoryCacheConstants.ApplicationDownloadKey
            );

            applicationDownloads = applicationDownloads.Append(applicationDownload);

            _ = _memoryCache.Set(
                key: MemoryCacheConstants.ApplicationDownloadKey,
                value: applicationDownloads.ToList()
            );

            var todayAndroidCount = applicationDownloads.Count(applicationDownloads =>
                applicationDownloads.MobileDeviceType == DeviceType.Android &&
                applicationDownloads.DownloadedTime.Date == DateTime.UtcNow.Date
            );

            var todayIosCount = applicationDownloads.Count(applicationDownloads =>
                applicationDownloads.MobileDeviceType == DeviceType.Ios &&
                applicationDownloads.DownloadedTime.Date == DateTime.UtcNow.Date
            );

            var totalIosCount = applicationDownloads.Count(applicationDownloads =>
                applicationDownloads.MobileDeviceType == DeviceType.Ios
            );
            var totalAndroidCount = applicationDownloads.Count(applicationDownloads =>
                applicationDownloads.MobileDeviceType == DeviceType.Android
            );

            await _slackService.LogAppDownload(
                    mobileDeviceType: request.DeviceType.GetDisplayName(),
                    todayAndroidCount: todayAndroidCount,
                    todayIosCount: todayIosCount,
                    totalAndroidCount: totalAndroidCount,
                    totalIosCount: totalIosCount,
                    appEnvironment: request.AppEnvironment,
                    cancellationToken: cancellationToken
            );

            return Unit.Value;
        }

        #endregion
    }
}
