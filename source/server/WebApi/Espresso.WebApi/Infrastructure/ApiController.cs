// ApiController.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.WebApi.Configuration;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Espresso.WebApi.Infrastructure;

/// <summary>
/// Api controlles base.
/// </summary>
[ApiController]
public abstract class ApiController : ControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApiController"/> class.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="webApiConfiguration"></param>
    protected ApiController(ISender sender, IWebApiConfiguration webApiConfiguration)
    {
        Sender = sender;
        WebApiConfiguration = webApiConfiguration;
    }

    /// <summary>
    ///
    /// </summary>
    protected ISender Sender { get; }

    /// <summary>
    ///
    /// </summary>
    protected IWebApiConfiguration WebApiConfiguration { get; }
}
