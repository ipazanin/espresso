using System;
using Espresso.Common.Enums;
using Espresso.Domain.Entities;
using FluentValidation;

namespace Espresso.WebApi.Application.ApplicationDownloads.Commands.CreateApplicationDownload
{
    public class CreateApplicationDownloadCommandValidator : AbstractValidator<CreateApplicationDownloadCommand>
    {
        public CreateApplicationDownloadCommandValidator()
        {
            RuleFor(createApplicationDownloadCommand => createApplicationDownloadCommand.ConsumerVersion).NotEmpty().MaximumLength(ApplicationDownload.MobileAppVersionMaxLenght);

            RuleFor(createApplicationDownloadCommand => createApplicationDownloadCommand.DeviceType).Must(mobileDeviceType => Enum.IsDefined(typeof(DeviceType), mobileDeviceType) && mobileDeviceType != DeviceType.Undefined);
        }
    }
}
