using Espresso.Domain.Entities;
using FluentValidation;

namespace Espresso.Application.CQRS.Notifications.Commands.SendPushNotification
{
    public class SendPushNotificationCommandValidator : AbstractValidator<SendPushNotificationCommand>
    {
        public SendPushNotificationCommandValidator()
        {
            _ = RuleFor(requets => requets.InternalName)
                .MaximumLength(PushNotification.InternalNameMaxLength);

            _ = RuleFor(requets => requets.Title)
                .MaximumLength(PushNotification.InternalNameMaxLength);

            _ = RuleFor(requets => requets.Message)
                .MaximumLength(PushNotification.InternalNameMaxLength)
                .NotEmpty();

            _ = RuleFor(requets => requets.Topic)
                .MaximumLength(PushNotification.InternalNameMaxLength)
                .NotEmpty();

            _ = RuleFor(requets => requets.ArticleUrl)
                .MaximumLength(PushNotification.InternalNameMaxLength)
                .NotEmpty();
        }
    }
}
