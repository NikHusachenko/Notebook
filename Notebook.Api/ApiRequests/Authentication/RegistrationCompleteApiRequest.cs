namespace Notebook.Api.ApiRequests.Authentication;

public sealed record RegistrationCompleteApiRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}