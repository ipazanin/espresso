// DatabaseController.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.Common.Services.Contacts;
using Espresso.Dashboard.Application.Settings.Queries.ExportDatabase;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Espresso.Dashboard.Controllers;

[ApiController]
[Route("api")]
public class DatabaseController : ControllerBase
{
    private readonly ISender _sender;

    private readonly IJsonService _jsonService;

    public DatabaseController(ISender sender, IJsonService jsonService)
    {
        _sender = sender;
        _jsonService = jsonService;
    }

    [HttpGet]
    [Route("database")]
    public async Task<IActionResult> ExportDatabase(CancellationToken cancellationToken)
    {
        var database = await _sender.Send(new ExportDatabaseQuery(), cancellationToken);

        var serializedDatabase = await _jsonService.SerializeToStream(database, cancellationToken);

        return File(serializedDatabase, MimeTypeConstants.Json, $"database-{DateTimeOffset.UtcNow.DateTime:yyyy-MM-dd}{FileExtensionConstants.Json}");
    }
}
