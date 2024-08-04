namespace Notebook.Database.Entities;

public sealed record NoteEntity : EntityBase
{
    public string Content { get; set; } = string.Empty;
    public string[] Indexes { get; set; } = [];

    public Guid? OwnerId { get; set; }
    public UserEntity Owner { get; set; }

    public List<UserLikesEntity> Likes { get; set; } = new List<UserLikesEntity>();
    public List<CommentEntity> Comments { get; set; } = new List<CommentEntity>();
}