// ApplicationInformation.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Enums;

namespace Espresso.Application.Models
{
    /// <summary>
    /// Application information.
    /// </summary>
    public class ApplicationInformation
    {
        /// <summary>
        /// Gets application environment.
        /// </summary>
        public AppEnvironment AppEnvironment { get; }

        /// <summary>
        /// Gets application (server) version.
        /// </summary>
        public string Version { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationInformation"/> class.
        /// </summary>
        /// <param name="appEnvironment">Application environment.</param>
        /// <param name="version">Application version.</param>
        public ApplicationInformation(AppEnvironment appEnvironment, string version)
        {
            AppEnvironment = appEnvironment;
            Version = version;
        }
    }
}
