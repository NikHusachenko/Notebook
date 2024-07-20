using Notebook.Services.ResultService;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Options;

namespace Notebook.Services.EmailServices;

public sealed class SmtpEmailService : IEmailService
{
    private const string SendEmailError = "Error while send email.";

    private readonly SmtpOptions _smtpOptions;
    private readonly SmtpClient _client;

    public SmtpEmailService(IOptionsMonitor<SmtpOptions> optionsMonitor)
    {
        _smtpOptions = optionsMonitor.CurrentValue;
        _client = new SmtpClient();
    }

    public async Task<Result> SendEmail(string recipientName, string recipientAddress, string title, string htmlContent)
    {
        try
        {
            await _client.ConnectAsync(_smtpOptions.Host, _smtpOptions.Port, _smtpOptions.UseSsl);
            await _client.AuthenticateAsync(_smtpOptions.Address, _smtpOptions.Password);
            await _client.SendAsync(CreateMessage(recipientName, recipientAddress, title, htmlContent));
            await _client.DisconnectAsync(true);
            return Result.Success();
        }
        catch
        {
            return Result.Error(SendEmailError);
        }
    }

    private MimeMessage CreateMessage(string recipientName, string recipientAddress, string title, string htmlContent)
    {
        MimeMessage message = new MimeMessage();
        message.From.Add(new MailboxAddress(_smtpOptions.Name, _smtpOptions.Address));
        message.To.Add(new MailboxAddress(recipientName, recipientAddress));
        message.Subject = title;

        BodyBuilder builder = new BodyBuilder() { HtmlBody = htmlContent };
        message.Body = builder.ToMessageBody();

        return message;
    }
}