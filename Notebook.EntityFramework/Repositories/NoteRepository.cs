using Microsoft.EntityFrameworkCore;
using Notebook.Database.Entities;
using Notebook.EntityFramework.GenericRepository;
using System.Linq.Expressions;

namespace Notebook.EntityFramework.Repositories;

public sealed class NoteRepository : GenericRepository<NoteEntity>
{
    public NoteRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public async Task<List<NoteEntity>> GetNotes(DateTimeOffset? from, DateTimeOffset? to, string? content, string? authorLogin)
    {
        IQueryable<NoteEntity> query = _table.AsNoTracking();

        query = Filter(query, from, note => note.CreatedAt >= from);
        query = Filter(query, to, note => note.CreatedAt <= to);
        query = Filter(query, content, note => note.Content.Contains(content!));
        query = Filter(query, authorLogin, note => note.Owner.Credentials.Login.Contains(authorLogin!));

        return await query.ToListAsync();
    }

    private IQueryable<NoteEntity> Filter<T>(IQueryable<NoteEntity> query, T? arg, Expression<Func<NoteEntity, bool>> expression) =>
        arg is not null ? query.Where(expression) : query;
}