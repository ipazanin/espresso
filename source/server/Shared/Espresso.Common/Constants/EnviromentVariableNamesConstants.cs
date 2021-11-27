// EnviromentVariableNamesConstants.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.Common.Constants;

/// <summary>
/// Environment variable names.
/// </summary>
#pragma warning disable SA1649 // File name should match first type name
public static class EnvironmentVariableNamesConstants
#pragma warning restore SA1649 // File name should match first type name
{
    /// <summary>
    /// Environment variable used by ASP.NET Core framework for environment.
    /// </summary>
    public const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";
}
