namespace Notebook.Database.Entities;

public sealed record TokenEntity : EntityBase
{
    public string Token { get; set; } = string.Empty;
    public DateTimeOffset ExpiredAt { get; set; }

    public Guid CredentialsId { get; set; }
    public CredentialsEntity Credentials { get; set; }
}