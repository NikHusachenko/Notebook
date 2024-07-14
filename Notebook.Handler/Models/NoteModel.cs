namespace Notebook.Handler.Models;

public sealed record NoteModel
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public int Likes { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public Guid? OwnerId { get; set; }

    public bool CanRemove { get; set; }
    public bool IsLikedByUser { get; set; }
}