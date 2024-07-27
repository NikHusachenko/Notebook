using Microsoft.EntityFrameworkCore;
using Notebook.Database.Entities;
using Notebook.EntityFramework.GenericRepository;

namespace Notebook.EntityFramework.Repositories;

public sealed class UserRepository : GenericRepository<UserEntity>
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public async Task<UserEntity> GetByToken(string token) =>
        await _table.FirstOrDefaultAsync(user => user.)
}