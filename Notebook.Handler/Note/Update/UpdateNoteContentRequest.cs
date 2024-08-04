using MediatR;
using Notebook.Database.Entities;
using Notebook.Services.ResultService;

namespace Notebook.Handler.Note.Update;

public sealed record UpdateNoteContentRequest(Guid Id, string Content) : IRequest<Result<NoteEntity>>;