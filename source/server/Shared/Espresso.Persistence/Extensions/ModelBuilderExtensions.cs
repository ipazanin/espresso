// ModelBuilderExtensions.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Espresso.Persistence.Extensions
{
    /// <summary>
    /// <see cref="ModelBuilder"/> extensions.
    /// </summary>
    public static class ModelBuilderExtensions
    {
        private const string ApplyConfigurationMethodName = "ApplyConfiguration";

        /// <summary>
        /// Applies all <see cref="IEntityTypeConfiguration{TEntity}"/> in <paramref name="configurationsAssembly"/>.
        /// </summary>
        /// <param name="modelBuilder">Model builder.</param>
        /// <param name="configurationsAssembly">Assembly.</param>
        public static void ApplyAllConfigurations(
            this ModelBuilder modelBuilder,
            Assembly configurationsAssembly)
        {
            var applyConfigurationMethodInfo = modelBuilder
                .GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .First(method => method.Name.Equals(ApplyConfigurationMethodName, StringComparison.OrdinalIgnoreCase));

            _ = configurationsAssembly
                .GetTypes()
                .Select(
                    assemblyType =>
                    (
                        assemblyType,
                        iEntityTypeConfigurationInterface: Array.Find(
                            array: assemblyType.GetInterfaces(),
                            match: i => i.Name.Equals(typeof(IEntityTypeConfiguration<>).Name, StringComparison.Ordinal))))
                .Where(assemblyTypeAndInterface => assemblyTypeAndInterface.iEntityTypeConfigurationInterface != null)
                .Select(
                    assemblyTypeAndInterface =>
                    (
                        genericArgument: assemblyTypeAndInterface.iEntityTypeConfigurationInterface!.GetGenericArguments()[0],
                        configurationObject: Activator.CreateInstance(assemblyTypeAndInterface.assemblyType)))
                .Select(
                    genericArgumentAndConfigurationObject =>
                        applyConfigurationMethodInfo
                            .MakeGenericMethod(genericArgumentAndConfigurationObject.genericArgument)
                            .Invoke(modelBuilder, new[] { genericArgumentAndConfigurationObject.configurationObject }))
                .ToList();
        }
    }
}
