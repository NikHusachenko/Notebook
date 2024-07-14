using MediatR;
using Notebook.Database.Entities;
using Notebook.Services.ResultService;

namespace Notebook.Handler.Comment.Update;

public sealed record UpdateCommentRequest(Guid Id, string Content) : IRequest<Result<CommentEntity>>;