
using FluentValidation;

namespace Espresso.WebApi.Application.CQRS.Notifications.Queries.GetPushNotifications
{
    public class GetPushNotificationsQueryValidator : AbstractValidator<GetPushNotificationsQuery>
    {
        public GetPushNotificationsQueryValidator()
        {
            _ = RuleFor(request => request.Take)
                .GreaterThan(0)
                .LessThan(100);

            _ = RuleFor(request => request.Skip)
                .GreaterThanOrEqualTo(0);
        }
    }
}
