﻿// SendGridEmailService.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Common.Services.Contacts;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Espresso.Common.Services.Implementations;

/// <summary>
/// Send grid email sender service.
/// </summary>
public class SendGridEmailService : IEmailService
{
    private const string SenderEmail = "dashboard-noreply@espressonews.co";
    private const string SenderName = "Dashboard NoReply";

    private readonly string _sendGridKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="SendGridEmailService"/> class.
    /// </summary>
    /// <param name="sendGridKey">Send grid API key.</param>
    public SendGridEmailService(
        string sendGridKey)
    {
        _sendGridKey = sendGridKey;
    }

    /// <inheritdoc/>
    public async Task<bool> SendMail(
        string recipient,
        string subject,
        string content,
        string htmlContent)
    {
        var client = new SendGridClient(_sendGridKey);
        var fromEmail = new EmailAddress(SenderEmail, SenderName);
        var toEmail = new EmailAddress(recipient);

        var message = MailHelper.CreateSingleEmail(
            from: fromEmail,
            to: toEmail,
            subject: subject,
            plainTextContent: content,
            htmlContent: htmlContent);

        var response = await client.SendEmailAsync(message);

        return response.IsSuccessStatusCode;
    }
}
