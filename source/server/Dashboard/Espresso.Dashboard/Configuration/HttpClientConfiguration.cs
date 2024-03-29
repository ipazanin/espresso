﻿// HttpClientConfiguration.cs
//
// © 2022 Espresso News. All rights reserved.

using Microsoft.Extensions.Configuration;

namespace Espresso.Dashboard.Configuration;

/// <summary>
/// HttpClientConfiguration.
/// </summary>
public class HttpClientConfiguration
{
    private readonly IConfigurationSection _configurationSection;

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpClientConfiguration"/> class.
    /// </summary>
    /// <param name="configurationSection">Part of app configuration.</param>
    public HttpClientConfiguration(
        IConfigurationSection configurationSection)
    {
        _configurationSection = configurationSection;
    }

    /// <summary>
    /// Gets maximum Number Of Retries.
    /// </summary>
    public int MaxRetries => _configurationSection.GetValue<int>("MaxRetries");

    /// <summary>
    /// Gets hTTP Client Timeout.
    /// </summary>
    public TimeSpan Timeout => TimeSpan.FromSeconds(
        _configurationSection.GetValue<int>("TimeoutInSeconds"));
}
