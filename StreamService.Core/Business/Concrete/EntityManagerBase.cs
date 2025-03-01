using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StreamService.Core.Business.Abstract;
using StreamService.Core.DataAccess.Abstract;
using StreamService.Core.Entities;

namespace StreamService.Core.Business.Concrete;

public abstract class EntityManagerBase<TEntity>(IEntityRepository<TEntity> entityRepository) : IEntityService<TEntity>
    where TEntity : BaseEntity
{
    private readonly IEntityRepository<TEntity> _entityRepository = entityRepository;

    public Task<TEntity> CreateAsync(TEntity entity)
    {
        return _entityRepository.CreateAsync(entity);
    }

    public Task<bool> DeleteAsync(string id)
    {
        return _entityRepository.DeleteAsync(id);
    }

    public Task<List<TEntity>> GetAllAsync()
    {
        return _entityRepository.GetAllAsync();
    }

    public Task<TEntity> GetByIdAsync(string id)
    {
        return _entityRepository.GetByIdAsync(id);
    }

    public Task<TEntity> UpdateAsync(TEntity entity)
    {
        return _entityRepository.UpdateAsync(entity);
    }
}
