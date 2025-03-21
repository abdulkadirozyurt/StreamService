﻿using System;
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

    public virtual Task<List<TEntity>> GetAllAsync()
    {
        return _entityRepository.GetAllAsync();
    }

    public virtual Task<TEntity> GetByIdAsync(string id)
    {
        return _entityRepository.GetByIdAsync(id);
    }

    public Task<TEntity> CreateAsync(TEntity entity)
    {
        return _entityRepository.CreateAsync(entity);
    }

    public Task<TEntity> UpdateAsync(TEntity entity)
    {
        return _entityRepository.UpdateAsync(entity);
    }

    public async Task<bool> DeactivateAsync(string id)
    {
        return await _entityRepository.DeactivateAsync(id);
    }

    public Task<bool> ActivateAsync(string id)
    {
        return _entityRepository.ActivateAsync(id);
    }
}
