namespace Notebook.Database.Entities;

public sealed record CredentialsEntity : EntityBase
{
    public string Email { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string HashedPassword { get; set; } = string.Empty;
    public byte[] Salt { get; set; } = [];
    public DateTimeOffset? VerifiedAt { get; set; }

    public UserEntity User { get; set; }

    public List<TokenEntity> Tokens { get; set; } = new List<TokenEntity>();
}