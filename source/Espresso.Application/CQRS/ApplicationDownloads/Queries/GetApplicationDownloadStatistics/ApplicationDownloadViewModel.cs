using System;
using System.Linq.Expressions;

using Espresso.Domain.Entities;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.Application.CQRS.ApplicationDownloads.Queries.GetApplicationDownloadStatistics
{
    public class ApplicationDownloadViewModel
    {
        #region Properties
        public int Id { get; private set; }

        public string WebApiVersion { get; private set; }

        public string MobileAppVersion { get; private set; }

        public DateTime DownloadedTime { get; private set; }

        public DeviceType MobileDeviceType { get; private set; }

        public static Expression<Func<ApplicationDownload, ApplicationDownloadViewModel>> Projection => applicationDownload => new ApplicationDownloadViewModel
        {
            Id = applicationDownload.Id,
            WebApiVersion = applicationDownload.WebApiVersion,
            MobileAppVersion = applicationDownload.MobileAppVersion,
            MobileDeviceType = applicationDownload.MobileDeviceType,
            DownloadedTime = applicationDownload.DownloadedTime
        };
        #endregion

        #region Constructors
        private ApplicationDownloadViewModel()
        {
            WebApiVersion = null!;
            MobileAppVersion = null!;
        }
        #endregion
    }
}
