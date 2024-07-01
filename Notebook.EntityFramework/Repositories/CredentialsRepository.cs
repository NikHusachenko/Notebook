using Microsoft.EntityFrameworkCore;
using Notebook.Database.Entities;
using Notebook.EntityFramework.GenericRepository;
using System.Linq.Expressions;

namespace Notebook.EntityFramework.Repositories;

public sealed class CredentialsRepository : GenericRepository<CredentialsEntity>
{
    public CredentialsRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public override Task<CredentialsEntity?> GetBy(Expression<Func<CredentialsEntity, bool>> predicate) =>
        _table.Include(credentials => credentials.User)
            .FirstOrDefaultAsync(predicate);
}