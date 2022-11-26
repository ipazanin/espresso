// IEmailService.cs
//
// © 2022 Espresso News. All rights reserved.

namespace Espresso.Common.Services.Contracts;

/// <summary>
/// Email service.
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Sends email.
    /// </summary>
    /// <param name="to">Email receiver.</param>
    /// <param name="subject">Email subject.</param>
    /// <param name="content">Email content.</param>
    /// <param name="htmlContent">HTML content.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public Task<bool> SendMail(
        string to,
        string subject,
        string content,
        string htmlContent);
}
