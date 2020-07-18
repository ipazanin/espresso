using Espresso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.Configuration
{
    public class PushNotificationConfiguration : IEntityTypeConfiguration<PushNotification>
    {
        public void Configure(EntityTypeBuilder<PushNotification> builder)
        {
            builder.Property(pushNotification => pushNotification.ArticleUrl).HasMaxLength(1000);
            builder.Property(pushNotification => pushNotification.InternalName).HasMaxLength(100);
            builder.Property(pushNotification => pushNotification.Message).HasMaxLength(100);
            builder.Property(pushNotification => pushNotification.Title).HasMaxLength(100);
            builder.Property(pushNotification => pushNotification.Topic).HasMaxLength(100);
        }
    }
}
