using MediatR;
using Microsoft.EntityFrameworkCore;
using Notebook.Database.Entities;
using Notebook.EntityFramework.Repositories;
using Notebook.Handler.Note.Models;
using Notebook.Services.UserServices;

namespace Notebook.Handler.Note.GetNotes;

public sealed class GetNotesHandler(
    IRepositoryFactory repositoryFactory,
    ICurrentUserContext currentUserContext)
    : IRequestHandler<GetNotesRequest, List<NoteModel>>
{
    private const int REMOVE_PERIOD = 2;

    public async Task<List<NoteModel>> Handle(GetNotesRequest request, CancellationToken cancellationToken)
    {
        NoteRepository repository = repositoryFactory.NewNoteRepository();
        List<NoteEntity> records = await repository.GetNotes(request.Filter.DateFrom,
            request.Filter.DateTo,
            request.Filter.Content,
            request.Filter.AuthorLogin);

        List<UserLikesEntity> likes = await UserLikes(currentUserContext.Id);
        return records.Select(record => new NoteModel()
        {
            CanRemove = CantRemoveNote(record.CreatedAt),
            Content = record.Content,
            Id = record.Id,
            IsLikedByUser = likes.Where(ul => ul.NoteId == record.Id) is not null,
            OwnerId = record.OwnerId,
            UpdatedAt = record.UpdatedAt,
        })
        .ToList();
    }

    private bool CantRemoveNote(DateTimeOffset creationDate) =>
        (DateTimeOffset.Now - creationDate).Days < REMOVE_PERIOD;

    private async Task<List<UserLikesEntity>> UserLikes(Guid userId)
    {
        UserLikesRepository repository = repositoryFactory.NewUserLikesRepository();
        return await repository.GetAllBy(record => 
            record.UserId == userId)
            .ToListAsync();
    }
}