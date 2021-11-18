// SettingProvider.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Espresso.Domain.Infrastructure;
using Espresso.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Application.Infrastructure.SettingsInfrastructure
{
    /// <summary>
    /// <see cref="Setting"/> provider.
    /// </summary>
    public sealed class SettingProvider : ISettingProvider
    {
        private readonly IEspressoDatabaseContext _espressoDatabaseContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingProvider"/> class.
        /// </summary>
        /// <param name="espressoDatabaseContext">Espresso database context.</param>
        public SettingProvider(IEspressoDatabaseContext espressoDatabaseContext)
        {
            _espressoDatabaseContext = espressoDatabaseContext;

            // This is necessary evil to apply migrations before settings are used in configuring application
            espressoDatabaseContext.Database.Migrate();

            LatestSetting = GetLatestSettingFromStore();
        }

        /// <inheritdoc/>
        public Setting LatestSetting { get; private set; }

        /// <inheritdoc/>
        public void UpdateLatestSetting()
        {
            LatestSetting = GetLatestSettingFromStore();
        }

        private Setting GetLatestSettingFromStore()
        {
            return _espressoDatabaseContext
                .Settings
                .OrderByDescending(setting => setting.Created)
                .First();
        }
    }
}
