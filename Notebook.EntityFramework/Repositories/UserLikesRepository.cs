using Microsoft.EntityFrameworkCore;
using Notebook.Database.Entities;
using Notebook.EntityFramework.GenericRepository;

namespace Notebook.EntityFramework.Repositories;

public sealed class UserLikesRepository : GenericRepository<UserLikesEntity>
{
    public UserLikesRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public async Task ToggleLike(Guid noteId, Guid userId)
    {
        UserLikesEntity? dbRecord = await _table.FirstOrDefaultAsync(
            record => record.NoteId == noteId &&
            record.UserId == userId);

        if (dbRecord is null)
        {
            await Create(new UserLikesEntity()
            {
                CreatedAt = DateTimeOffset.Now.ToUniversalTime(),
                NoteId = noteId,
                Id = Guid.NewGuid(),
                UserId = userId,
                UpdatedAt = DateTimeOffset.Now.ToUniversalTime(),
            });
        }
        else
        {
            await Delete(dbRecord);
        }
    }

    public override async Task Delete(UserLikesEntity entity)
    {
        _table.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}