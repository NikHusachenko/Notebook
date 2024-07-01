namespace Notebook.Services.Jwt;

public sealed record JwtOptions
{
    public string Key { get; set; } = string.Empty;
    public int Expiration { get; set; }
}