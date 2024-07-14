namespace Notebook.Handler.Models;

public sealed record CommentModel
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public DateTimeOffset PublishedOn { get; set; }
    public Guid AuthorId { get; set; }
}