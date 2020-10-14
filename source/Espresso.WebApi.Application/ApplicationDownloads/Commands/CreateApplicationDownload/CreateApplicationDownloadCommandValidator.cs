﻿using System;
using Espresso.Common.Constants;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using FluentValidation;

namespace Espresso.WebApi.Application.ApplicationDownloads.Commands.CreateApplicationDownload
{
    public class CreateApplicationDownloadCommandValidator : AbstractValidator<CreateApplicationDownloadCommand>
    {
        public CreateApplicationDownloadCommandValidator()
        {
            _ = RuleFor(createApplicationDownloadCommand => createApplicationDownloadCommand.ConsumerVersion).NotEmpty().MaximumLength(PropertyConstraintConstants.ApplicationDownloadMobileAppVersionHasMaxLenght);

            _ = RuleFor(createApplicationDownloadCommand => createApplicationDownloadCommand.CurrentApiVersion).NotEmpty().MaximumLength(PropertyConstraintConstants.ApplicationDownloadWebApiVersionHasMaxLenght);

            _ = RuleFor(createApplicationDownloadCommand => createApplicationDownloadCommand.DeviceType).Must(mobileDeviceType => Enum.IsDefined(typeof(DeviceType), mobileDeviceType) && mobileDeviceType != DeviceType.Undefined);
        }
    }
}