namespace Notebook.Services.EmailServices;

public sealed class EmailMessageManager
{
    private const string TEMPLATE_FOLDER_PATH = "wwwroot/Templates/";
    private const string INVITE_TEMPLATE_NAME = "InviteTemplate.html";

    public async Task<string> InviteTemplate(string url, string message)
    {
        using StreamReader reader = new StreamReader($"{TEMPLATE_FOLDER_PATH}{INVITE_TEMPLATE_NAME}");
        string html = await reader.ReadToEndAsync();
        return string.Format(html, message, url);
    }
}