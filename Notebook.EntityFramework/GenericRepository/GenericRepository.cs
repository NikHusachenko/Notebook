using Microsoft.EntityFrameworkCore;
using Notebook.Database.Entities;
using System.Linq.Expressions;

namespace Notebook.EntityFramework.GenericRepository;

public abstract class GenericRepository<T> : IGenericRepository<T> where T : EntityBase
{
    protected readonly ApplicationDbContext _dbContext;
    protected readonly DbSet<T> _table;

    protected GenericRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _table = _dbContext.Set<T>();
    }

    public virtual async Task Create(T entity)
    {
        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateTimeOffset.Now.ToUniversalTime();
        entity.UpdatedAt = DateTimeOffset.Now.ToUniversalTime();

        await _table.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public virtual Task Delete(T entity)
    {
        entity.DeletedAt = DateTimeOffset.Now.ToUniversalTime();
        return Update(entity);
    }

    public virtual Task<List<T>> GetAll() => 
        _table.Where(record => !record.DeletedAt.HasValue)
        .AsNoTracking()
        .ToListAsync();

    public virtual Task<List<T>> GetAllBy(Expression<Func<T, bool>> predicate) =>
        _table.Where(record => !record.DeletedAt.HasValue)
            .Where(predicate)
            .AsNoTracking()
            .ToListAsync();

    public virtual Task<T?> GetBy(Expression<Func<T, bool>> predicate) =>
        _table.Where(record => !record.DeletedAt.HasValue)
        .FirstOrDefaultAsync(predicate);

    public virtual async Task<T?> GetById(Guid id) =>
        await _table.Where(record => !record.DeletedAt.HasValue)
        .FirstOrDefaultAsync(record => record.Id == id);

    public virtual async Task Update(T entity)
    {
        _table.Update(entity);
        await _dbContext.SaveChangesAsync();
    }
}