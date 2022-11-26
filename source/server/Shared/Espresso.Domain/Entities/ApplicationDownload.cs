// ApplicationDownload.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Common.Enums;

#pragma warning disable RCS1170

namespace Espresso.Domain.Entities;

public class ApplicationDownload
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
        DateTimeOffset downloadedTime,
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

    public DateTimeOffset DownloadedTime { get; private set; }

    public DeviceType MobileDeviceType { get; private set; }
}
