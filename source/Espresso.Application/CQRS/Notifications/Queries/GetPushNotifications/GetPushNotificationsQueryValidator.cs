
using FluentValidation;

namespace Espresso.Application.CQRS.Notifications.Queries.GetPushNotifications
{
    public class GetPushNotificationsQueryValidator : AbstractValidator<GetPushNotificationsQuery>
    {
        public GetPushNotificationsQueryValidator()
        {
            RuleFor(request => request.Take)
                .GreaterThan(0)
                .LessThan(100);

            RuleFor(request => request.Skip)
                .GreaterThanOrEqualTo(0);
        }
    }
}
