using Notebook.Database.Enums;

namespace Notebook.Database.Entities;

public sealed record UserEntity : EntityBase
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ImagePath { get; set; }
    public UserRole Role { get; set; }
    public DateTimeOffset LastSeen { get; set; }

    public Guid CredentialsId { get; set; }
    public CredentialsEntity Credentials { get; set; }

    public List<UserLikesEntity> Liked { get; set; } = new List<UserLikesEntity>();
    public List<NoteEntity> Notes { get; set; } = new List<NoteEntity>();
    public List<CommentEntity> Comments { get; set; } = new List<CommentEntity>();
    public List<FollowingEntity> Followers { get; set; } = new List<FollowingEntity>();
    public List<FollowingEntity> Followings { get; set; } = new List<FollowingEntity>();
}