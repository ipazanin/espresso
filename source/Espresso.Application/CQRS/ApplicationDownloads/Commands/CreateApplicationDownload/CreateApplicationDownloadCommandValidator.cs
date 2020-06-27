using System;
using Espresso.Common.Constants;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using FluentValidation;

namespace Espresso.Application.CQRS.ApplicationDownloads.Commands.CreateApplicationDownload
{
    public class CreateApplicationDownloadCommandValidator : AbstractValidator<CreateApplicationDownloadCommand>
    {
        public CreateApplicationDownloadCommandValidator()
        {
            RuleFor(createApplicationDownloadCommand => createApplicationDownloadCommand.Version).NotEmpty().MaximumLength(PropertyConstraintConstants.ApplicationDownloadMobileAppVersionHasMaxLenght);

            RuleFor(createApplicationDownloadCommand => createApplicationDownloadCommand.CurrentEspressoWebApiVersion).NotEmpty().MaximumLength(PropertyConstraintConstants.ApplicationDownloadWebApiVersionHasMaxLenght);

            RuleFor(createApplicationDownloadCommand => createApplicationDownloadCommand.DeviceType).Must(mobileDeviceType => Enum.IsDefined(typeof(DeviceType), mobileDeviceType) && mobileDeviceType != DeviceType.Undefined);

        }
    }
}
