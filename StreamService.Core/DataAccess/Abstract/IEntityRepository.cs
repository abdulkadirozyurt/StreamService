using System;
using StreamService.Core.Entities;

namespace StreamService.Core.DataAccess.Abstract;

public interface IEntityRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(string id);
    Task<TEntity> CreateAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<bool> DeleteAsync(string id);
}
