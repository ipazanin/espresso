// WebApiInit.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Persistence.Database;
using Espresso.WebApi.Application.HealthChecks;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;

namespace Espresso.WebApi.Application.Initialization;

/// <summary>
///
/// </summary>
public class WebApiInit : IWebApiInit
{
    private const string ConfigurationFileName = "firebase-key.json";

    private readonly IEspressoDatabaseContext _context;
    private readonly ReadinessHealthCheck _readinessHealthCheck;
    private readonly IRefreshWebApiCache _refreshWebApiCache;

    /// <summary>
    /// Initializes a new instance of the <see cref="WebApiInit"/> class.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="readinessHealthCheck"></param>
    /// <param name="refreshWebApiCache"></param>
    /// <summary>
    /// Initializes a new instance of the <see cref="WebApiInit"/> class.
    /// </summary>
    public WebApiInit(
        IEspressoDatabaseContext context,
        ReadinessHealthCheck readinessHealthCheck,
        IRefreshWebApiCache refreshWebApiCache)
    {
        _context = context;
        _readinessHealthCheck = readinessHealthCheck;
        _refreshWebApiCache = refreshWebApiCache;
    }

    public async Task InitWebApi()
    {
        InitializeGoogleServices();

        await _context.Database.MigrateAsync();

        await _refreshWebApiCache.RefreshCacheValues();

        _readinessHealthCheck.ReadinessTaskCompleted = true;
    }

    private static void InitializeGoogleServices()
    {
        var firebaseKeyPath = Path.Combine(
            path1: AppDomain.CurrentDomain.BaseDirectory ?? string.Empty,
            path2: ConfigurationFileName);

        FirebaseApp.Create(new AppOptions()
        {
            Credential = GoogleCredential.FromFile(firebaseKeyPath),
        });
    }
}
