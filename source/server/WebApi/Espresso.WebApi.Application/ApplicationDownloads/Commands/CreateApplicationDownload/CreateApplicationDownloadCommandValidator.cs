// CreateApplicationDownloadCommandValidator.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Common.Enums;
using Espresso.Domain.Entities;
using FluentValidation;

namespace Espresso.WebApi.Application.ApplicationDownloads.Commands.CreateApplicationDownload;

public class CreateApplicationDownloadCommandValidator : AbstractValidator<CreateApplicationDownloadCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateApplicationDownloadCommandValidator"/> class.
    /// </summary>
    public CreateApplicationDownloadCommandValidator()
    {
        _ = RuleFor(createApplicationDownloadCommand => createApplicationDownloadCommand.ConsumerVersion).NotEmpty().MaximumLength(ApplicationDownload.MobileAppVersionMaxLenght);

        _ = RuleFor(createApplicationDownloadCommand => createApplicationDownloadCommand.DeviceType).Must(mobileDeviceType => Enum.IsDefined(typeof(DeviceType), mobileDeviceType) && mobileDeviceType != DeviceType.Undefined);
    }
}
