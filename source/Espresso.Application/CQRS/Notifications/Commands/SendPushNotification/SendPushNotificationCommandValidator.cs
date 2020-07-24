using Espresso.Domain.Entities;
using FluentValidation;

namespace Espresso.Application.CQRS.Notifications.Commands.SendPushNotification
{
    public class SendPushNotificationCommandValidator : AbstractValidator<SendPushNotificationCommand>
    {
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
}
