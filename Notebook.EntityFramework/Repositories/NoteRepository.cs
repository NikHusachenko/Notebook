using Microsoft.EntityFrameworkCore;
using Notebook.Database.Entities;
using Notebook.EntityFramework.GenericRepository;
using System.Linq.Expressions;

namespace Notebook.EntityFramework.Repositories;

public sealed class NoteRepository(ApplicationDbContext dbContext) : GenericRepository<NoteEntity>(dbContext)
{
    public async Task<List<NoteEntity>> GetNotes(DateTimeOffset? from, DateTimeOffset? to, string? content, string? authorLogin, int page = 1, int take = 5)
    {
        IQueryable<NoteEntity> query = _table
            .Where(note => !note.DeletedAt.HasValue)
            .Include(note => note.Likes)
            .AsNoTracking();

        query = Filter(query, from, note => note.CreatedAt >= from);
        query = Filter(query, to, note => note.CreatedAt <= to);
        query = Filter(query, content, note => note.Content.Contains(content!));
        query = Filter(query, authorLogin, note => note.Owner.Credentials.Login.Contains(authorLogin!));

        int skip = page <= 1 ? 0 : (page - 1) * take;
        query = query.Skip(skip).Take(take);

        return await query.ToListAsync();
    }

    private IQueryable<NoteEntity> Filter<T>(IQueryable<NoteEntity> query, T? arg, Expression<Func<NoteEntity, bool>> expression) =>
        arg is not null ? query.Where(expression) : query;
}