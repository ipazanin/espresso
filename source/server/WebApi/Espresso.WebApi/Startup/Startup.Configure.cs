// Startup.Configure.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Application.Middleware.SecurityHeaders;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Domain.IServices;
using Espresso.WebApi.Application.Hubs;
using Espresso.WebApi.Application.Initialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Espresso.WebApi.Startup;

internal sealed partial class Startup
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="app"></param>
    /// <param name="memoryCacheInit"></param>
    /// <param name="loggerService"></param>
    /// <param name="env"></param>
    public void Configure(
        IApplicationBuilder app,
        IWebApiInit memoryCacheInit,
        ILoggerService<Startup> loggerService,
        IWebHostEnvironment env)
    {
        loggerService.Log(
            eventName: "WebApi Startup",
            logLevel: Microsoft.Extensions.Logging.LogLevel.Information,
            namedArguments: new (string, object)[] { ("version", _webApiConfiguration.AppConfiguration.Version) });

#pragma warning disable VSTHRD002 // Avoid problematic synchronous waits
        memoryCacheInit.InitWebApi().GetAwaiter().GetResult();
#pragma warning restore VSTHRD002 // Avoid problematic synchronous waits

        app.UseSecurityHeadersMiddleware(securityHeadersBuilder => securityHeadersBuilder.AddDefaultSecurePolicy());

        if (_webApiConfiguration.SpaConfiguration.EnableCors)
        {
            app.UseCors(CustomCorsPolicyName);
        }

        app.UseHsts();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            foreach (var apiVersion in _webApiConfiguration.AppConfiguration.ApiVersions)
            {
                options.SwaggerEndpoint(
                    url: $"/{SwaggerDocumentDefinitionRoutePrefix}/{apiVersion}/{SwaggerDefinitionFileName}",
                    name: $"{ApiDescriptionNamePrefix} {apiVersion}");
            }

            options.RoutePrefix = SwaggerApiExplorerRoute;
        });

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseStaticFiles();
        app.UseSpaStaticFiles();

        app.UseSpa(spa =>
        {
            spa.Options.SourcePath = Path.Join(env.ContentRootPath, ClientAppDirectory);

            if (
                _webApiConfiguration.AppConfiguration.AppEnvironment.Equals(AppEnvironment.Local) &&
                _webApiConfiguration.SpaConfiguration.UseSpaProxyServer)
            {
                spa.UseProxyToSpaDevelopmentServer(_webApiConfiguration.SpaConfiguration.SpaProxyServerUrl);
            }
        });

        app.UseResponseCaching();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/health/startup", new HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains(HealthCheckConstants.StartupTag),
            });
            endpoints.MapHealthChecks("/health/readiness", new HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains(HealthCheckConstants.ReadinessTag),
            });
            endpoints.MapHealthChecks("/health/liveness", new HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains(HealthCheckConstants.LivenessTag),
            });
            endpoints.MapHub<ArticlesNotificationHub>("/notifications/articles");
        });
    }
}
