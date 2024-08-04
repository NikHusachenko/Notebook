using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using Notebook.Database.Entities;
using Notebook.EntityFramework.GenericRepository;
using Notebook.Services.CryptingServices;
using Notebook.Services.ResultService;
using Notebook.Services.UserServices;

namespace Notebook.Handler.Note.NewNote;

public sealed class NewNoteHandler(
    IGenericRepository<NoteEntity> noteRepository,
    IGenericRepository<UserEntity> userRepository,
    ICryptingManager cryptingManager,
    ICurrentUserContext userContext)
    : IRequestHandler<NewNoteRequest, Result<NoteEntity>>
{
    private const string CreationError = "Error while create new note.";
    private const string UnauthorizedError = "Unauthorized.";

    public async Task<Result<NoteEntity>> Handle(NewNoteRequest request, CancellationToken cancellationToken)
    {
        Result<Guid> currentUserResult = await GetUserId();
        if (currentUserResult.ErrorMessages.Any())
        {
            return Result<NoteEntity>.Error(currentUserResult.ErrorMessages);
        }

        NoteEntity dbRecord = new()
        {
            Content = cryptingManager.Encrypt(request.Content),
            OwnerId = currentUserResult.Value,
            Indexes = IndexContent(request.Content)
        };

        using (IDbContextTransaction transaction = await noteRepository.NewTransaction())
        {
            try
            {
                await noteRepository.Create(dbRecord);
            }
            catch
            {
                await transaction.RollbackAsync();
                return Result<NoteEntity>.Error(CreationError);
            }

            await transaction.CommitAsync();
        }
        return Result<NoteEntity>.Success(dbRecord);
    }

    private string[] IndexContent(string content) =>
        content.Split(' ').Select(word => cryptingManager.Encrypt(word)).ToArray();

    private async Task<Result<Guid>> GetUserId()
    {
        UserEntity? dbRecord = await userRepository.GetBy(user => user.CredentialsId == userContext.Id);
        return dbRecord is null ?
            Result<Guid>.Error(UnauthorizedError) :
            Result<Guid>.Success(dbRecord.Id);
    }
}