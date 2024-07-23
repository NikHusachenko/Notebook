using Notebook.Services.ResultService;

namespace Notebook.Services.EmailServices;

public interface IEmailService
{
    Task<Result> SendEmail(string recipientAddress, string title, string htmlContent);
}