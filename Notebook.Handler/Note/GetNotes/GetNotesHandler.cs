using MediatR;
using Microsoft.EntityFrameworkCore;
using Notebook.Database.Entities;
using Notebook.EntityFramework.GenericRepository;
using Notebook.Handler.Models;
using Notebook.Services.UserServices;
using System.Linq.Expressions;

namespace Notebook.Handler.Note.GetNotes;

public sealed class GetNotesHandler(
    IGenericRepository<NoteEntity> repository,
    ICurrentUserContext currentUserContext)
    : IRequestHandler<GetNotesRequest, List<NoteModel>>
{
    private const int REMOVE_PERIOD = 2;

    public async Task<List<NoteModel>> Handle(GetNotesRequest request, CancellationToken cancellationToken)
    {
        List<NoteEntity> records = await Filter(repository.GetAll(), request.Filter);

        return records.Select(record => new NoteModel()
        {
            Content = record.Content,
            Id = record.Id,
            OwnerId = record.OwnerId,
            CreatedAt = record.CreatedAt,
        })
        .ToList();
    }

    private async Task<List<NoteEntity>> Filter(IQueryable<NoteEntity> query,
        GetNotesFilter filter)
    {
        query = Filter(query, filter.DateFrom, note => note.CreatedAt >= filter.DateFrom);
        query = Filter(query, filter.DateTo, note => note.CreatedAt <= filter.DateTo);
        query = Filter(query, filter.Content, note => note.Content.Contains(filter.Content!));
        query = Filter(query, filter.AuthorLogin, note => note.Owner.Credentials.Login.Contains(filter.AuthorLogin!));

        int skip = filter.Take <= 1 ? 0 : (filter.Page - 1) * filter.Take;
        query = query.Skip(skip).Take(filter.Take);

        return await query.ToListAsync();
    }

    private IQueryable<NoteEntity> Filter<T>(IQueryable<NoteEntity> query, T? arg, Expression<Func<NoteEntity, bool>> expression) =>
        arg is not null ? query.Where(expression) : query;

    private bool CantRemoveNote(DateTimeOffset creationDate) =>
        (DateTimeOffset.Now - creationDate).Days < REMOVE_PERIOD;
}