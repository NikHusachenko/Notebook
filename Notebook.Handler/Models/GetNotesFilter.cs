namespace Notebook.Handler.Models;

public sealed record GetNotesFilter
{
    public DateTimeOffset? DateFrom { get; set; }
    public DateTimeOffset? DateTo { get; set; }
    public string? Content { get; set; }
    public string? AuthorLogin { get; set; }
    public int Page { get; set; } = 1;
    public int Take { get; set; } = 5;
}