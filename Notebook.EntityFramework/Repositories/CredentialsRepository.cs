using Notebook.Database.Entities;
using Notebook.EntityFramework.GenericRepository;

namespace Notebook.EntityFramework.Repositories;

public sealed class CredentialsRepository : GenericRepository<CredentialsEntity>
{
    public CredentialsRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}