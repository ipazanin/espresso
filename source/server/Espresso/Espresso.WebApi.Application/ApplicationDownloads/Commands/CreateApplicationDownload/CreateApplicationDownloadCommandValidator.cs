using System;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using FluentValidation;

namespace Espresso.WebApi.Application.ApplicationDownloads.Commands.CreateApplicationDownload
{
    public class CreateApplicationDownloadCommandValidator : AbstractValidator<CreateApplicationDownloadCommand>
    {
        public CreateApplicationDownloadCommandValidator()
        {
            RuleFor(createApplicationDownloadCommand => createApplicationDownloadCommand.ConsumerVersion).NotEmpty().MaximumLength(PropertyConstraintConstants.ApplicationDownloadMobileAppVersionHasMaxLenght);

            RuleFor(createApplicationDownloadCommand => createApplicationDownloadCommand.DeviceType).Must(mobileDeviceType => Enum.IsDefined(typeof(DeviceType), mobileDeviceType) && mobileDeviceType != DeviceType.Undefined);
        }
    }
}
