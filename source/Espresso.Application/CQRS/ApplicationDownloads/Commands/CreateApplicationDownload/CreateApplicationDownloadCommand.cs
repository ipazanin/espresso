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
            string espressoWebApiVersion,
            string version,
            DeviceType deviceType
        ) : base(
            currentEspressoWebApiVersion,
            espressoWebApiVersion,
            version,
            deviceType,
            Event.CreateApplicationDownloadCommand
        )
        {
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{nameof(CurrentEspressoWebApiVersion)}:{CurrentEspressoWebApiVersion}, " +
                $"{nameof(Version)}:{Version}, " +
                $"{nameof(DeviceType)}:{DeviceType} ";
        }
        #endregion
    }
}
