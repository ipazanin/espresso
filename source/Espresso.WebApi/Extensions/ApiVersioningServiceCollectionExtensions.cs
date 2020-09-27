using Espresso.Common.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace Espresso.WebApi.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ApiVersioningServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApiVersioningServices(this IServiceCollection services)
        {
            services.AddApiVersioning(options => ConfigureApiVersioning(options));

            return services;
        }

        /// <summary>
        /// https://dev.to/htissink/versioning-asp-net-core-apis-with-swashbuckle-making-space-potatoes-v-x-x-x-3po7
        /// </summary>
        private static void ConfigureApiVersioning(ApiVersioningOptions apiVersioningOptions)
        {
            apiVersioningOptions.DefaultApiVersion = new ApiVersion(
                majorVersion: 1,
                minorVersion: 2
            );
            apiVersioningOptions.AssumeDefaultVersionWhenUnspecified = true;
            apiVersioningOptions.ReportApiVersions = true;
            apiVersioningOptions.ApiVersionReader = new HeaderApiVersionReader(HttpHeaderConstants.EspressoApiHeaderName);
        }
    }
}
