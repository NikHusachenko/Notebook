namespace Notebook.Handler.Models;

public sealed record GetCommentFilter
{
    public Guid NoteId { get; set; }
    public int Page { get; set; } = 1;
    public int Take { get; set; } = 10;
}