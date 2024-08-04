using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using Notebook.Database.Entities;
using Notebook.EntityFramework.GenericRepository;
using Notebook.Services.ResultService;

namespace Notebook.Handler.Note.Delete;

public sealed class DeleteNoteHandler(IGenericRepository<NoteEntity> repository)
    : IRequestHandler<DeleteNoteRequest, Result>
{
    private const int REMOVE_PERIOD = 2;
    private const string NoteNotFoundError = "Note not found.";
    private const string CantRemoveNoteError = "Can't remove notes older 2 days";
    private const string RemovingError = "Error while remove note.";

    public async Task<Result> Handle(DeleteNoteRequest request, CancellationToken cancellationToken)
    {
        NoteEntity? dbRecord = await repository.GetById(request.Id);
        if (dbRecord is null)
        {
            return Result.Error(NoteNotFoundError);
        }

        if ((DateTimeOffset.Now - dbRecord.CreatedAt).Days > REMOVE_PERIOD)
        {
            return Result.Error(CantRemoveNoteError);
        }

        try
        {
            await repository.Delete(dbRecord);
        }
        catch
        {
            return Result.Error(RemovingError);
        }

        return Result.Success();
    }
}