using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using Notebook.Database.Entities;
using Notebook.EntityFramework.GenericRepository;
using Notebook.Services.CryptingServices;
using Notebook.Services.ResultService;

namespace Notebook.Handler.Note.NewNote;

public sealed class NewNoteHandler(
    IGenericRepository<NoteEntity> repository,
    ICryptingManager cryptingManager)
    : IRequestHandler<NewNoteRequest, Result<NoteEntity>>
{
    private const string CreationErrror = "Error while create new note.";

    public async Task<Result<NoteEntity>> Handle(NewNoteRequest request, CancellationToken cancellationToken)
    {
        NoteEntity dbReecord = new()
        {
            Content = cryptingManager.Encrypt(request.Content),
            OwnerId = request.OwnerId,
        };

        using (IDbContextTransaction transaction = await repository.NewTransaction())
        {
            try
            {
                await repository.Create(dbReecord);
            }
            catch
            {
                await transaction.RollbackAsync();
                return Result<NoteEntity>.Error(CreationErrror);
            }
        }
        return Result<NoteEntity>.Success(dbReecord);
    }
}