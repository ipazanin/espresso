﻿// ApplicationDownloadConfiguration.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.Configuration;

/// <summary>
/// <see cref="ApplicationDownload"/> entity configuration.
/// </summary>
public class ApplicationDownloadConfiguration : IEntityTypeConfiguration<ApplicationDownload>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<ApplicationDownload> builder)
    {
        builder.Property(applicationDownload => applicationDownload.WebApiVersion)
            .HasMaxLength(ApplicationDownload.WebApiVersionMaxLenght);

        builder.Property(applicationDownload => applicationDownload.MobileAppVersion)
            .HasMaxLength(ApplicationDownload.MobileAppVersionMaxLenght);

        builder.Property(applicationDownload => applicationDownload.MobileDeviceType);

        builder.Property(applicationDownload => applicationDownload.DownloadedTime);
    }
}
