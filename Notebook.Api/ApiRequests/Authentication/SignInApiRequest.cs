namespace Notebook.Api.ApiRequests.Authentication;

public sealed record SignInApiRequest
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}