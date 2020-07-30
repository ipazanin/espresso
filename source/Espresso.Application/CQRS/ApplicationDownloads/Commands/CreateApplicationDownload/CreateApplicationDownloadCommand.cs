using Espresso.Application.Infrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using MediatR;

namespace Espresso.Application.CQRS.ApplicationDownloads.Commands.CreateApplicationDownload
{
    public class CreateApplicationDownloadCommand : Request<Unit>
    {
        #region Constructors
        public CreateApplicationDownloadCommand(
            string currentEspressoWebApiVersion,
            string targetedEspressoWebApiVersion,
            string consumerVersion,
            DeviceType deviceType
        ) : base(
            currentEspressoWebApiVersion: currentEspressoWebApiVersion,
            targetedEspressoWebApiVersion: targetedEspressoWebApiVersion,
            consumerVersion: consumerVersion,
            deviceType: deviceType,
            Event.CreateApplicationDownloadCommand
        )
        {
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{nameof(CurrentEspressoWebApiVersion)}:{CurrentEspressoWebApiVersion}, " +
                $"{nameof(ConsumerVersion)}:{ConsumerVersion}, " +
                $"{nameof(DeviceType)}:{DeviceType} ";
        }
        #endregion
    }
}
