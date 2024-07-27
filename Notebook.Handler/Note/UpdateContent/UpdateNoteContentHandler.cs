using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using Notebook.Database.Entities;
using Notebook.EntityFramework.GenericRepository;
using Notebook.Services.ResultService;

namespace Notebook.Handler.Note.UpdateContent;

public sealed class UpdateNoteContentHandler(IGenericRepository<NoteEntity> repository)
    : IRequestHandler<UpdateNoteContentRequest, Result>
{
    private const string NoteNotFoundError = "Note not found.";
    private const string UpdatingError = "Error while update note content.";

    public async Task<Result> Handle(UpdateNoteContentRequest request, CancellationToken cancellationToken)
    {
        NoteEntity? dbRecord = await repository.GetById(request.Id);
        if (dbRecord is null)
        {
            return Result.Error(NoteNotFoundError);
        }

        dbRecord!.Content = request.Content;

        using (IDbContextTransaction transaction = await repository.NewTransaction())
        {
            try
            {
                await repository.Update(dbRecord!);
            }
            catch
            {
                await transaction.RollbackAsync();
                return Result.Error(UpdatingError);
            }
        }
        return Result.Success();
    }
}