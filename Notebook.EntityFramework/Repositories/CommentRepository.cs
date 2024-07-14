using Microsoft.EntityFrameworkCore;
using Notebook.Database.Entities;
using Notebook.EntityFramework.GenericRepository;

namespace Notebook.EntityFramework.Repositories;

public sealed class CommentRepository(ApplicationDbContext dbContext) : GenericRepository<CommentEntity>(dbContext)
{
    public async Task<List<CommentEntity>> GetComments(Guid noteId, int page = 1, int take = 10)
    {
        IQueryable<CommentEntity> query = _table
            .Where(comment => !comment.DeletedAt.HasValue &&
                comment.NoteId == noteId)
            .AsNoTracking();

        int skip = page <= 1 ? 0 : (page - 1) * take;
        query = query.Skip(skip).Take(take);

        return await query.ToListAsync();
    }
}