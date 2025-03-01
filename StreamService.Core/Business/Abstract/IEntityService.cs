using System;

namespace StreamService.Core.Business.Abstract;

public interface IEntityService<TEntity>
{
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(string id);
    Task<TEntity> CreateAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<bool> DeleteAsync(string id);
}
