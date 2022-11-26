// SendPushNotificationCommandValidator.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using FluentValidation;

namespace Espresso.WebApi.Application.Notifications.Commands.SendPushNotification;

public class SendPushNotificationCommandValidator : AbstractValidator<SendPushNotificationCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SendPushNotificationCommandValidator"/> class.
    /// </summary>
    public SendPushNotificationCommandValidator()
    {
        RuleFor(requets => requets.InternalName)
            .MaximumLength(PushNotification.InternalNameMaxLength);

        RuleFor(requets => requets.Title)
            .MaximumLength(PushNotification.InternalNameMaxLength);

        RuleFor(requets => requets.Message)
            .MaximumLength(PushNotification.InternalNameMaxLength)
            .NotEmpty();

        RuleFor(requets => requets.Topic)
            .MaximumLength(PushNotification.InternalNameMaxLength)
            .NotEmpty();

        RuleFor(requets => requets.ArticleUrl)
            .MaximumLength(PushNotification.InternalNameMaxLength)
            .NotEmpty();
    }
}
