using MediatR;
using Notebook.Database.Entities;
using Notebook.Handler.Models;

namespace Notebook.Handler.Comment.GetComments;

public sealed record GetCommentsRequest(GetCommentFilter Filter) : IRequest<List<CommentEntity>>;