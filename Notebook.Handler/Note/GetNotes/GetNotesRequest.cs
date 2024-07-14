using MediatR;
using Notebook.Handler.Models;

namespace Notebook.Handler.Note.GetNotes;

public sealed record GetNotesRequest(GetNotesFilter Filter) : IRequest<List<NoteModel>>;