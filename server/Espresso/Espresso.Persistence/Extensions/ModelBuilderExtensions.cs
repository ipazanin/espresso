using System;
using System.Linq;
using System.Reflection;
using Espresso.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Persistence.Extensions
{
    public static class ModelBuilderExtensions
    {
        private const string ApplyConfigurationMethodName = "ApplyConfiguration";

        public static void ApplyAllConfigurations(this ModelBuilder modelBuilder)
        {
            var applyConfigurationMethodInfo = modelBuilder
                .GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .First(m => m.Name.Equals(ApplyConfigurationMethodName, StringComparison.OrdinalIgnoreCase));

            var ret = typeof(ApplicationDatabaseContext).Assembly
                .GetTypes()
                .Select(t => (t, i: t.GetInterfaces().FirstOrDefault(i => i.Name.Equals(typeof(IEntityTypeConfiguration<>).Name, StringComparison.Ordinal))))
                .Where(it => it.i != null)
                .Select(it => (et: it.i!.GetGenericArguments()[0], cfgObj: Activator.CreateInstance(it.t)))
                .Select(it => applyConfigurationMethodInfo.MakeGenericMethod(it.et).Invoke(modelBuilder, new[] { it.cfgObj }))
                .ToList();
        }
    }
}
