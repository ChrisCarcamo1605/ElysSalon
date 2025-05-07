using System.Collections.ObjectModel;

namespace ElysSalon2._0.aplication.Interfaces.Repositories;

public interface IRepository<T> where T : class
{
    Task SaveAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
    Task<T> GetByIdAsync(int id);
    Task<ObservableCollection<T>> FindAsync(Func<T,bool> func);
    Task<ObservableCollection<T>> GetAllAsync();
}