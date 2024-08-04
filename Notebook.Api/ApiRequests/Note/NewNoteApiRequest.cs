namespace Notebook.Api.ApiRequests.Note;

public sealed record NewNoteApiRequest
{
    public string Content { get; set; } = string.Empty;
}