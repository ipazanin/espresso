using Espresso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.Configuration
{
    public class PushNotificationConfiguration : IEntityTypeConfiguration<PushNotification>
    {
        public void Configure(EntityTypeBuilder<PushNotification> builder)
        {
            builder.Property(pushNotification => pushNotification.ArticleUrl).HasMaxLength(PushNotification.ArticleUrlMaxLength);
            builder.Property(pushNotification => pushNotification.InternalName).HasMaxLength(PushNotification.InternalNameMaxLength);
            builder.Property(pushNotification => pushNotification.Message).HasMaxLength(PushNotification.MessageMaxLength);
            builder.Property(pushNotification => pushNotification.Title).HasMaxLength(PushNotification.TitleMaxLength);
            builder.Property(pushNotification => pushNotification.Topic).HasMaxLength(PushNotification.TopicMaxLength);
        }
    }
}
