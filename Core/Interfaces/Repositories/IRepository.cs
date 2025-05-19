using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace Core.Interfaces.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> SaveAsync(TEntity entity);
    Task SaveRangeAsync(List<TEntity> entities);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task<TEntity> GetByIdAsync<T>(T id);
    Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task<ObservableCollection<TEntity>> GetAllAsync();

    public Task<ObservableCollection<TEntity>> GetAllWithIncludesAsync(
        params Expression<Func<TEntity, object>>[] includeProperties);

    Task<ObservableCollection<T>> FindAsync<T>(Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, T>> selector);
}