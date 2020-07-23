using System;
using System.Linq.Expressions;

using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.Domain.Infrastructure;

namespace Espresso.Domain.Entities
{
    public class ApplicationDownload : IEntity<int, ApplicationDownload>
    {
        #region Properties
        public int Id { get; private set; }

        public string WebApiVersion { get; private set; }

        public string MobileAppVersion { get; private set; }

        /// <summary>
        /// UTC
        /// </summary>
        public DateTime DownloadedTime { get; private set; }

        public DeviceType MobileDeviceType { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// ORM Constructor
        /// </summary>
        private ApplicationDownload()
        {
            WebApiVersion = null!;
            MobileAppVersion = null!;
        }

        public ApplicationDownload(
            string webApiVersion,
            string mobileAppVersion,
            DateTime downloadedTime,
            DeviceType mobileDeviceType
        )
        {
            WebApiVersion = webApiVersion;
            MobileAppVersion = mobileAppVersion;
            DownloadedTime = downloadedTime;
            MobileDeviceType = mobileDeviceType;
        }
        #endregion
    }
}
