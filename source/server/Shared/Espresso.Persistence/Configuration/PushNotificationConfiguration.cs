// PushNotificationConfiguration.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.Configuration;

/// <summary>
/// <see cref="PushNotification"/> entity configuration.
/// </summary>
public class PushNotificationConfiguration : IEntityTypeConfiguration<PushNotification>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<PushNotification> builder)
    {
        _ = builder.Property(pushNotification => pushNotification.ArticleUrl)
            .HasMaxLength(PushNotification.ArticleUrlMaxLength);

        _ = builder.Property(pushNotification => pushNotification.InternalName)
            .HasMaxLength(PushNotification.InternalNameMaxLength);

        _ = builder.Property(pushNotification => pushNotification.Message)
            .HasMaxLength(PushNotification.MessageMaxLength);

        _ = builder.Property(pushNotification => pushNotification.Title)
            .HasMaxLength(PushNotification.TitleMaxLength);

        _ = builder.Property(pushNotification => pushNotification.Topic)
            .HasMaxLength(PushNotification.TopicMaxLength);
    }
}
