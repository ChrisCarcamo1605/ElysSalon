using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using ElysSalon2._0.adapters.OutBound.DataBase;
using ElysSalon2._0.aplication.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using OpenTK.Graphics.GL;

namespace ElysSalon2._0.adapters.OutBound.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly ElyDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(ElyDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<TEntity> SaveAsync(TEntity entity)
    {
        var entityAdded = await _context.AddAsync((TEntity)entity);
        await _context.SaveChangesAsync();
        return entityAdded.Entity;
    }

    public async Task SaveRangeAsync(List<TEntity> entities)
    {
        await _context.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var entityEntry = _dbSet.Entry(entity);
        entityEntry.State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task DeleteAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<TEntity> GetByIdAsync<T>(T id)
    {
        return await _context.FindAsync<TEntity>(id) ?? throw new NullReferenceException();
    }

    public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(predicate);

        return entity == null ? null : entity ;
    }
    public async Task<ObservableCollection<T>> FindAsync<T>(Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, T>> selector)
    {
        return new ObservableCollection<T>(await _dbSet.Where(predicate).Select(selector).ToListAsync());
    }

    public async Task<ObservableCollection<TEntity>> GetAllAsync()
    {
        var entities = await _dbSet.ToListAsync();
        return new ObservableCollection<TEntity>(entities);
    }

    public async Task<ObservableCollection<TEntity>> GetAllWithIncludesAsync(
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = _dbSet;

        foreach (var includeProperty in includeProperties) query = query.Include(includeProperty);

        var entities = await query.ToListAsync();
        return new ObservableCollection<TEntity>(entities);
    }
}