using MediatR;
using Notebook.Services.ResultService;

namespace Notebook.Handler.Note.UpdateContent;

public sealed record UpdateNoteContentRequest(Guid Id, string Content) : IRequest<Result>;