using MediatR;
using Notebook.Database.Entities;
using Notebook.Services.ResultService;

namespace Notebook.Handler.Comment.Write;

public sealed record WriteCommentRequest(string Content, Guid AuthorId, Guid NoteId) : IRequest<Result<CommentEntity>>;