namespace Notebook.Api.ApiRequests.Note;

public sealed record UpdateNoteContentApiRequest
{
    public string Content { get; set; } = string.Empty;
}