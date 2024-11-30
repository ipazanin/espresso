// Startup.Configure.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.Middleware.SecurityHeaders;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Domain.IServices;
using Espresso.WebApi.Application.Hubs;
using Espresso.WebApi.Application.Initialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.DependencyInjection;

namespace Espresso.WebApi.StartupConfiguration;

public sealed partial class Startup
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
        var serverAddressesFeature = app.ServerFeatures.Get<IServerAddressesFeature>();

        loggerService.Log(
            eventName: "WebApi Startup",
            logLevel: Microsoft.Extensions.Logging.LogLevel.Information,
            namedArguments:
            [
                   ("version", _webApiConfiguration.AppConfiguration.Version),
                   ("address", string.Join(',', serverAddressesFeature!.Addresses)),
            ]);

#pragma warning disable VSTHRD002 // Avoid problematic synchronous waits
        memoryCacheInit.InitWebApi().GetAwaiter().GetResult();
#pragma warning restore VSTHRD002 // Avoid problematic synchronous waits

        _ = app.UseSecurityHeadersMiddleware(securityHeadersBuilder => securityHeadersBuilder.AddDefaultSecurePolicy());

        if (_webApiConfiguration.SpaConfiguration.EnableCors)
        {
            _ = app.UseCors(CustomCorsPolicyName);
        }

        _ = app.UseHsts();

        _ = app.UseSwagger();
        _ = app.UseSwaggerUI(options =>
        {
            foreach (var apiVersion in _webApiConfiguration.AppConfiguration.ApiVersions)
            {
                options.SwaggerEndpoint(
                    url: $"/{SwaggerDocumentDefinitionRoutePrefix}/{apiVersion}/{SwaggerDefinitionFileName}",
                    name: $"{ApiDescriptionNamePrefix} {apiVersion}");
            }

            options.RoutePrefix = SwaggerApiExplorerRoute;
        });

        _ = app.UseRouting();
        _ = app.UseAuthentication();
        _ = app.UseAuthorization();

        _ = app.UseStaticFiles();
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

        _ = app.UseResponseCaching();
        _ = app.UseEndpoints(endpoints =>
        {
            _ = endpoints.MapControllers();
            _ = endpoints.MapHealthChecks("/health/startup", new HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains(HealthCheckConstants.StartupTag),
            });
            _ = endpoints.MapHealthChecks("/health/readiness", new HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains(HealthCheckConstants.ReadinessTag),
            });
            _ = endpoints.MapHealthChecks("/health/liveness", new HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains(HealthCheckConstants.LivenessTag),
            });
            _ = endpoints.MapHub<ArticlesNotificationHub>("/notifications/articles");
        });
    }
}
