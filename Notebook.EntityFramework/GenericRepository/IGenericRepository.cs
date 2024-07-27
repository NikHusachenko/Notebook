using Microsoft.EntityFrameworkCore.Storage;
using Notebook.Database.Entities;
using System.Linq.Expressions;

namespace Notebook.EntityFramework.GenericRepository;

public interface IGenericRepository<T> where T : EntityBase
{
    Task Create(T entity);
    Task Update(T entity);
    Task Delete(T entity);

    Task<IDbContextTransaction> NewTransaction();

    Task<T?> GetById(Guid id);
    Task<T?> GetBy(Expression<Func<T, bool>> predicate);

    IQueryable<T> GetAll();
    IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
}