namespace Notebook.Handler.Models;

public sealed record NoteModel
{
    public Guid Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public Guid? OwnerId { get; set; }
    public string OwnerFullName { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
}