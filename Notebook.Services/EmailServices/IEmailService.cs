using MimeKit;
using Notebook.Services.ResultService;

namespace Notebook.Services.EmailServices;

public interface IEmailService
{
    Task<Result> SendEmail(string recipientName, string recipientAddress, string title, string htmlContent);
}