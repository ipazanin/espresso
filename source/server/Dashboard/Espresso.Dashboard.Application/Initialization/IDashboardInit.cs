// IDashboardInit.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.Dashboard.Application.Initialization;

public interface IDashboardInit
{
    /// <summary>
    ///
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    public Task InitParserDeleter();
}
