using MediatR;
using Notebook.Database.Entities;
using Notebook.Services.ResultService;

namespace Notebook.Handler.Note.NewNote;

public sealed record NewNoteRequest(string Content) : IRequest<Result<NoteEntity>>;