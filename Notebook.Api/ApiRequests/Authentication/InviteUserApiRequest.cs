namespace Notebook.Api.ApiRequests.Authentication;

public sealed record InviteUserApiRequest
{
    public string Email { get; set; } = string.Empty;
}