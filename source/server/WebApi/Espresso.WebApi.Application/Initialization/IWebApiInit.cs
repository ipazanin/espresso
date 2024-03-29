﻿// IWebApiInit.cs
//
// © 2022 Espresso News. All rights reserved.

namespace Espresso.WebApi.Application.Initialization;

public interface IWebApiInit
{
    /// <summary>
    ///
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    public Task InitWebApi();
}
