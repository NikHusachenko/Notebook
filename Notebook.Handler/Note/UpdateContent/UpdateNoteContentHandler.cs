using MediatR;
using Notebook.Database.Entities;
using Notebook.EntityFramework.Repositories;
using Notebook.Services.ResultService;

namespace Notebook.Handler.Note.UpdateContent;

public sealed class UpdateNoteContentHandler(IRepositoryFactory repositoryFactory)
    : IRequestHandler<UpdateNoteContentRequest, Result>
{
    private const string NoteNotFoundError = "Note not found.";
    private const string UpdatingError = "Error while update note content.";

    public async Task<Result> Handle(UpdateNoteContentRequest request, CancellationToken cancellationToken)
    {
        NoteRepository repository = repositoryFactory.NewNoteRepository();
        NoteEntity? dbRecord = await repository.GetById(request.Id);

        if (dbRecord is null)
        {
            return Result.Error(NoteNotFoundError);
        }

        dbRecord!.Content = request.Content;

        try
        {
            await repository.Update(dbRecord!);
        }
        catch
        {
            return Result.Error(UpdatingError);
        }
        return Result.Success();
    }
}