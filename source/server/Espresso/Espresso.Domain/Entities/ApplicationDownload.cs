// ApplicationDownload.cs
//
// © 2021 Espresso News. All rights reserved.

using System;
using Espresso.Common.Enums;
using Espresso.Domain.Infrastructure;

namespace Espresso.Domain.Entities
{
    public class ApplicationDownload : IEntity<int, ApplicationDownload>
    {
        public const int WebApiVersionMaxLenght = 10;

        public const int MobileAppVersionMaxLenght = 20;

        public int Id { get; private set; }

        public string WebApiVersion { get; private set; }

        public string MobileAppVersion { get; private set; }

        /// <summary>
        /// Gets uTC.
        /// </summary>
        public DateTime DownloadedTime { get; private set; }

        public DeviceType MobileDeviceType { get; private set; }

        /// <summary>
        /// ORM Constructor.
        /// </summary>
        private ApplicationDownload()
        {
            WebApiVersion = null!;
            MobileAppVersion = null!;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDownload"/> class.
        /// </summary>
        /// <param name="webApiVersion"></param>
        /// <param name="mobileAppVersion"></param>
        /// <param name="downloadedTime"></param>
        /// <param name="mobileDeviceType"></param>
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
    }
}
