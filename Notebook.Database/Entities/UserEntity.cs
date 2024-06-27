using Notebook.Database.Enums;

namespace Notebook.Database.Entities;

public sealed record UserEntity : EntityBase
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ImagePath { get; set; }
    public UserRole Role { get; set; }

    public Guid CreedntialsId { get; set; }
    public CredentialsEntity Credentials { get; set; }
}