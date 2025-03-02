using System;
using Microsoft.EntityFrameworkCore;
using StreamService.Core.DataAccess.Abstract;
using StreamService.Core.Entities;

namespace StreamService.Core.DataAccess.Concrete;

public abstract class EntityRepositoryBase<TEntity, TContext>(TContext context) : IEntityRepository<TEntity>
    where TEntity : BaseEntity
    where TContext : DbContext
{
    private readonly TContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        var entry = _context.Set<TEntity>().Add(entity);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var entity = _context.Set<TEntity>().Find(id);
        if (entity == null)
            return false;
        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public virtual async Task<List<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public virtual async Task<TEntity> GetByIdAsync(string id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);
        if (entity == null)
        {
            throw new KeyNotFoundException($"Entity with id {id} not found.");
        }
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var entry = _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }
}
