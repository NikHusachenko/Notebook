using MediatR;
using Notebook.Database.Entities;
using Notebook.EntityFramework.GenericRepository;
using Notebook.Services.CryptingServices;
using Notebook.Services.ResultService;

namespace Notebook.Handler.Note.Update;

public sealed class UpdateNoteContentHandler(
    IGenericRepository<NoteEntity> repository,
    ICryptingManager cryptingManager)
    : IRequestHandler<UpdateNoteContentRequest, Result<NoteEntity>>
{
    private const int REMOVE_PERIOD = 2;
    private const string NotFoundError = "Note not found.";
    private const string CantUpdateError = "Can't update note content, update period was expired.";
    private const string UpdateError = "Error while update note content.";

    public async Task<Result<NoteEntity>> Handle(UpdateNoteContentRequest request, CancellationToken cancellationToken)
    {
        NoteEntity? dbRecord = await repository.GetById(request.Id);
        if (dbRecord is null)
        {
            return Result<NoteEntity>.Error(NotFoundError);
        }

        if ((DateTimeOffset.Now - dbRecord.CreatedAt).Days > REMOVE_PERIOD)
        {
            return Result<NoteEntity>.Error(CantUpdateError);
        }

        dbRecord.Content = cryptingManager.Encrypt(request.Content);
        dbRecord.Indexes = IndexContent(request.Content);

        try
        {
            await repository.Update(dbRecord);
        }
        catch
        {
            return Result<NoteEntity>.Error(UpdateError);
        }
        return Result<NoteEntity>.Success(dbRecord);
    }

    private string[] IndexContent(string content) =>
        content.Split(' ').Select(word => cryptingManager.Encrypt(word)).ToArray();
}