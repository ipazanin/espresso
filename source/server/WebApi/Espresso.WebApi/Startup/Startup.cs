// Startup.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.WebApi.Configuration;
using Microsoft.Extensions.Configuration;

namespace Espresso.WebApi.Startup;

internal sealed partial class Startup
{
    private const string CustomCorsPolicyName = nameof(CustomCorsPolicyName);
    private const string ClientAppStaticFilesDirectory = "../../../client/build";
    private const string ClientAppDirectory = "../../../client";
    private const string SwaggerDefinitionFileName = "swagger" + FileExtensionConstants.Json;
    private const string ApiDescriptionNamePrefix = "Espresso API";
    private const string SwaggerApiExplorerRoute = "docs";
    private const string SwaggerDocumentDefinitionRoutePrefix = "swagger";
    private readonly IWebApiConfiguration _webApiConfiguration;

    /// <summary>
    /// Initializes a new instance of the <see cref="Startup"/> class.
    /// </summary>
    /// <param name="configuration"></param>
    public Startup(IConfiguration configuration)
    {
        _webApiConfiguration = new WebApiConfiguration(configuration);
    }
}
