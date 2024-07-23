using Notebook.Database.Entities;
using Notebook.EntityFramework.GenericRepository;

namespace Notebook.EntityFramework.Repositories;

public sealed class TokenRepository(ApplicationDbContext dbContext) : GenericRepository<TokenEntity>(dbContext)
{
}