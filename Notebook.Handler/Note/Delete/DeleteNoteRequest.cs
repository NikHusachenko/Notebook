using MediatR;
using Notebook.Services.ResultService;

namespace Notebook.Handler.Note.Delete;

public sealed record DeleteNoteRequest(Guid Id) : IRequest<Result>;