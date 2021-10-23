// ApplicationDownload.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Enums;
using Espresso.Domain.Infrastructure;
using System;

namespace Espresso.Domain.Entities
{
    public class ApplicationDownload : IEntity<int, ApplicationDownload>
    {
        public const int WebApiVersionMaxLenght = 10;

        public const int MobileAppVersionMaxLenght = 20;

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
            DeviceType mobileDeviceType)
        {
            WebApiVersion = webApiVersion;
            MobileAppVersion = mobileAppVersion;
            DownloadedTime = downloadedTime;
            MobileDeviceType = mobileDeviceType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDownload"/> class.
        /// ORM Constructor.
        /// </summary>
        private ApplicationDownload()
        {
            WebApiVersion = null!;
            MobileAppVersion = null!;
        }

        public int Id { get; private set; }

        public string WebApiVersion { get; private set; }

        public string MobileAppVersion { get; private set; }

        public DateTime DownloadedTime { get; private set; }

        public DeviceType MobileDeviceType { get; private set; }
    }
}
