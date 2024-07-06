namespace Notebook.Handler.Note.Models;

public sealed record GetNotesFilter
{
    public DateTimeOffset? DateFrom { get; set; }
    public DateTimeOffset? DateTo { get; set; }
    public string? Content { get; set; }
    public string? AuthorLogin { get; set; }
}