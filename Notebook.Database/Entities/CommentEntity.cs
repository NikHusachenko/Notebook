namespace Notebook.Database.Entities;

public sealed record CommentEntity : EntityBase
{
    public string Content { get; set; }

    public Guid? UserId { get; set; }
    public UserEntity User { get; set; }

    public Guid NoteId { get; set; }
    public NoteEntity Note { get; set; }
}