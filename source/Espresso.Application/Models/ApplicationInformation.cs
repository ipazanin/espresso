using Espresso.Common.Enums;

namespace Espresso.Application.Models
{
    public class ApplicationInformation
    {
        #region Properties
        public AppEnvironment AppEnvironment { get; }

        public string Version { get; }
        #endregion

        #region Constructors
        public ApplicationInformation(AppEnvironment appEnvironment, string version)
        {
            AppEnvironment = appEnvironment;
            Version = version;
        }
        #endregion
    }
}