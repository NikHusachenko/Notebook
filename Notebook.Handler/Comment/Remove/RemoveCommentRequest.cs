using MediatR;
using Notebook.Services.ResultService;

namespace Notebook.Handler.Comment.Remove;

public sealed record RemoveCommentRequest(Guid Id) : IRequest<Result>;