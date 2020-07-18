using FluentValidation;

namespace Espresso.Application.CQRS.Notifications.Commands.SendPushNotification
{
    public class SendPushNotificationCommandValidator : AbstractValidator<SendPushNotificationCommand>
    {
        public SendPushNotificationCommandValidator()
        {
            RuleFor(requets => requets.Message).NotEmpty();
            RuleFor(requets => requets.Topic).NotEmpty();
            RuleFor(requets => requets.ArticleUrl).NotEmpty();
        }
    }
}
