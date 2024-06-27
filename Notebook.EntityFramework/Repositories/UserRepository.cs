using Notebook.Database.Entities;
using Notebook.EntityFramework.GenericRepository;

namespace Notebook.EntityFramework.Repositories;

public sealed class UserRepository : GenericRepository<UserEntity>
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}