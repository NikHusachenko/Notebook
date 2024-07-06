using MediatR;
using Notebook.Database.Entities;
using Notebook.EntityFramework.Repositories;
using Notebook.Services.ResultService;

namespace Notebook.Handler.Note.NewNote;

public sealed class NewNoteHandler(IRepositoryFactory repositoryFactory)
    : IRequestHandler<NewNoteRequest, Result<NoteEntity>>
{
    private const string CreationErrror = "Error while create new note.";

    public async Task<Result<NoteEntity>> Handle(NewNoteRequest request, CancellationToken cancellationToken)
    {
        NoteRepository repository = repositoryFactory.NewNoteRepository();

        NoteEntity dbReecord = new()
        {
            Content = request.Content,
            OwnerId = request.OwnerId,
        };

        try
        {
            await repository.Create(dbReecord);
        }
        catch
        {
            return Result<NoteEntity>.Error(CreationErrror);
        }
        return Result<NoteEntity>.Success(dbReecord);
    }
}