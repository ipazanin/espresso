using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.Configuration
{
    public class ApplicationDownloadConfiguration : IEntityTypeConfiguration<ApplicationDownload>
    {
        public void Configure(EntityTypeBuilder<ApplicationDownload> builder)
        {
            builder.Property(applicationDownload => applicationDownload.WebApiVersion)
                .IsRequired(PropertyConstraintConstants.ApplicationDownloadWebApiVersionIsRequired)
                .HasMaxLength(PropertyConstraintConstants.ApplicationDownloadWebApiVersionHasMaxLenght);

            builder.Property(applicationDownload => applicationDownload.MobileAppVersion)
                .IsRequired(PropertyConstraintConstants.ApplicationDownloadMobileAppVersionIsRequired)
                .HasMaxLength(PropertyConstraintConstants.ApplicationDownloadMobileAppVersionHasMaxLenght);

            builder.Property(applicationDownload => applicationDownload.MobileDeviceType)
                .IsRequired(PropertyConstraintConstants.ApplicationDownloadMobileDeviceTypeisRequired);

            builder.Property(applicationDownload => applicationDownload.DownloadedTime)
                .IsRequired(PropertyConstraintConstants.ApplicationDownloadDownloadedTimeIsRequired);
        }
    }
}
