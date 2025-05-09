using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Windows.Data.Xml.Dom;

namespace ElysSalon2._0.aplication.Interfaces.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task SaveAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task<TEntity> GetByIdAsync(int id);
    Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task<ObservableCollection<TEntity>> GetAllAsync();

    Task<ObservableCollection<T>> FindAsync<T>(Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, T>> selector);


}