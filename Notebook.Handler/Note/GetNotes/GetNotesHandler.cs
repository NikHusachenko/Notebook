using MediatR;
using Notebook.Database.Entities;
using Notebook.EntityFramework.Repositories;
using Notebook.Handler.Models;
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
            request.Filter.AuthorLogin,
            request.Filter.Page,
            request.Filter.Take);

        return records.Select(record => new NoteModel()
        {
            CanRemove = CantRemoveNote(record.CreatedAt),
            Content = record.Content,
            Likes = record.Likes.Count,
            Id = record.Id,
            IsLikedByUser = record.Likes.Where(like => like.UserId == currentUserContext.Id) is not null,
            OwnerId = record.OwnerId,
            UpdatedAt = record.UpdatedAt,
        })
        .ToList();
    }

    private bool CantRemoveNote(DateTimeOffset creationDate) =>
        (DateTimeOffset.Now - creationDate).Days < REMOVE_PERIOD;
}