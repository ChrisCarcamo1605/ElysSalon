using System;
using System.Collections.ObjectModel;
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

    public async Task SaveAsync(TEntity entity)
    {
        await _context.AddAsync((TEntity)entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _dbSet.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        _dbSet.Remove(_dbSet.Find(id) ?? throw new NullReferenceException());
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await _context.FindAsync<TEntity>(id) ?? throw new NullReferenceException();
    }

    public async Task<ObservableCollection<TEntity>> FindAsync(Func<TEntity, bool> func)
    {
        return new ObservableCollection<TEntity>(_dbSet.Where(func).ToList());
    }

    public async Task<ObservableCollection<TEntity>> GetAllAsync()
    {
        var entities = await _dbSet.ToListAsync();
        return new ObservableCollection<TEntity>(entities);
    }
}