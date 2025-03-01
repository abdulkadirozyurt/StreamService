using System;
using StreamService.Core.Business.Abstract;
using StreamService.Entities.Concrete;

namespace StreamService.Business.Abstract;

public interface IUserService : IEntityService<User> { }
