// GetPushNotificationsQueryValidator.cs
//
// © 2022 Espresso News. All rights reserved.

using FluentValidation;

namespace Espresso.WebApi.Application.Notifications.Queries.GetPushNotifications;

public class GetPushNotificationsQueryValidator : AbstractValidator<GetPushNotificationsQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetPushNotificationsQueryValidator"/> class.
    /// </summary>
    public GetPushNotificationsQueryValidator()
    {
        RuleFor(request => request.Take)
            .GreaterThan(0)
            .LessThan(100);

        RuleFor(request => request.Skip)
            .GreaterThanOrEqualTo(0);
    }
}
