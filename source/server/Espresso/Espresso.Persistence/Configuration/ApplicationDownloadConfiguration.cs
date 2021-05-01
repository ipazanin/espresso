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
                .HasMaxLength(ApplicationDownload.WebApiVersionMaxLenght);

            builder.Property(applicationDownload => applicationDownload.MobileAppVersion)
                .HasMaxLength(ApplicationDownload.MobileAppVersionMaxLenght);

            builder.Property(applicationDownload => applicationDownload.MobileDeviceType);

            builder.Property(applicationDownload => applicationDownload.DownloadedTime);
        }
    }
}
