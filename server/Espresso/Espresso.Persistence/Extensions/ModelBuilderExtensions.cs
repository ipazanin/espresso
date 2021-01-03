using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Persistence.Extensions
{
    public static class ModelBuilderExtensions
    {
        private const string ApplyConfigurationMethodName = "ApplyConfiguration";

        public static void ApplyAllConfigurations(
            this ModelBuilder modelBuilder,
            Assembly configurationsAssembly
        )
        {
            var applyConfigurationMethodInfo = modelBuilder
                .GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .First(method => method.Name.Equals(ApplyConfigurationMethodName, StringComparison.OrdinalIgnoreCase));

            var ret = configurationsAssembly
                .GetTypes()
                .Select(
                    assemblyType =>
                    (
                        assemblyType,
                        iEntityTypeConfigurationInterface: assemblyType
                            .GetInterfaces()
                            .FirstOrDefault(i => i.Name.Equals(typeof(IEntityTypeConfiguration<>).Name, StringComparison.Ordinal))
                    )
                )
                .Where(assemblyTypeAndInterface => assemblyTypeAndInterface.iEntityTypeConfigurationInterface != null)
                .Select(
                    assemblyTypeAndInterface =>
                    (
                        genericArgument: assemblyTypeAndInterface.iEntityTypeConfigurationInterface!.GetGenericArguments()[0],
                        configurationObject: Activator.CreateInstance(assemblyTypeAndInterface.assemblyType)
                    )
                )
                .Select(
                    genericArgumentAndConfigurationObject =>
                        applyConfigurationMethodInfo
                            .MakeGenericMethod(genericArgumentAndConfigurationObject.genericArgument)
                            .Invoke(modelBuilder, new[] { genericArgumentAndConfigurationObject.configurationObject }
                        )
                )
                .ToList();
        }
    }
}
